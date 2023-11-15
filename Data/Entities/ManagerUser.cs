using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class ManagerUser : IdentityUser
    {
        public string? FistName { get; set; }

        public string? LastName { get; set; }

        public string? Avatar { get; set; }
        public string? Gender { get;set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Posts>  Posts { get; set; }
        public ICollection<Messages> Messages { get; set; }
        public ICollection<Comments>  Comments { get; set; }
        public ICollection<Friendships>  Friendships { get; set; }

    }
}
