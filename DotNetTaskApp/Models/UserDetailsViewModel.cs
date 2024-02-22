using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetTaskApp.Models
{
    public class UserDetailsViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> AllFiles { get; set; }
    }
}
