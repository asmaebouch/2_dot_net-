using Microsoft.AspNetCore.Identity;

namespace EbookTest.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Fisrtname { get; set; }
        public string LastName { get; set; }
    }
}
