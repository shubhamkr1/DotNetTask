using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTaskApp.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }


        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber {get; set;}
        
        [NotMapped]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile ProfileImageFile { get; set; }
        [NotMapped]
        [AllowedExtensions(new string[] { ".doc", ".pdf" })]
        public IFormFile ResumeFile { get; set; }
    }
}
