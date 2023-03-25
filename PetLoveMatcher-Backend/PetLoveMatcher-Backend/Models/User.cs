using DtoNetProject.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLoveMatcher_Backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }

        //OneToMany
        public int? SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School? School { get; set; }

        public User() { }

        public User(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public User(string firstName, string lastName, string userName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;

            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
