using DotNetTaskApp.Areas.Identity.Data;
using DotNetTaskApp.Data;
using DotNetTaskApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DotNetTaskApp.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IHostEnvironment _environment;
        private readonly DotNetTaskAppAuthContext _dbcontext;

        public UserProfileController(IHostEnvironment environment, DotNetTaskAppAuthContext dbcontext)
        {
            _environment = environment;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfile model)
        {
            string profilepicFileName = "";
            string resumeFileName = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                if (model.ProfileImageFile != null) {
                    profilepicFileName = UploadImage(model).ToString();
                }

                if (model.ResumeFile != null)
                {
                    resumeFileName = UploadResume(model).ToString();
                }

                UserProfile userObj = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber, 
                    Email = model.Email,    
                    ProfileImageFile  = model.ProfileImageFile,
                    ResumeFile = model.ResumeFile
                };
                _dbcontext.Add(userObj);
                await _dbcontext.SaveChangesAsync();
                return View("UploadSuccess");
            }
            return View("Index");
           
        }


        [HttpGet]       
        [Route("/UserProfile/ViewProfile")]
        public IActionResult ViewProfile()
        {
            //get logged in user email
            var useremail = User.Identity.Name;
            
            //find user by email
            var userProfObj = _dbcontext.userProfiles.Where(x => x.Email == useremail).FirstOrDefault();
            var viewModel = new UserDetailsViewModel
            {
                FirstName = userProfObj.FirstName,
                LastName = userProfObj.LastName,
                Address = userProfObj.Address,
                PhoneNumber = userProfObj.PhoneNumber,
                Email = userProfObj.Email,
                AllFiles = GetFiles(),
            };

            return View(viewModel);
           
        }

        [HttpGet]
        [Route("/UserProfile/AccessFiles")]
        public IActionResult EditProfile()
        {
            var viewModel = new UserProfileViewModel
            {
                AllFiles = GetFiles(),               
            };

            return View(viewModel);
        }

        private List<string> GetFiles()
        {
            var filesPath = Path.Combine(_environment.ContentRootPath, "wwwroot/UserFiles");
            var fileNames = new List<string>();
            if (Directory.Exists(filesPath))
            {
                var files = Directory.GetFiles(filesPath);
                foreach (var file in files)
                {
                    fileNames.Add(Path.GetFileName(file));
                }
            }
            return fileNames;
        }

        // Download file from the server
        [HttpGet]
        [Route("/UserProfile/DownloadFile")]
        public async Task<IActionResult> DownloadUserFiles(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserFiles", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // Get content type
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        // Get mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
             {
               
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},      
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".png", "image/png"},
       
            };
        }

        [HttpPost]
        [Route("/UserProfile/DeleteFile")]
        public IActionResult DeleteUserFiles(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserFiles", filename);

            FileInfo fi = new FileInfo(path);

            if(fi != null)
            {
                System.IO.File.Delete(filename);
                fi.Delete();
            }
            return RedirectToAction("EditProfile");
        }


        public async Task<string> UploadImage([FromForm] UserProfile userObj)
        {
           return await UploadSingle(userObj.ProfileImageFile);
            
        }
        
        public async Task<string> UploadResume([FromForm] UserProfile userObj)
        {
            return await UploadSingle(userObj.ResumeFile);
            
        }
        public async Task<string> UploadSingle(IFormFile file)
        {
            // Handle the file here
            var filePath = "";
            string uniqueName = "";
            if (file != null && file.Length > 0)
            {
                filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/UserFiles", file.FileName);
                uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return uniqueName;
        }
    }
}
