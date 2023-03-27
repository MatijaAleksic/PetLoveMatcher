using Microsoft.AspNetCore.Identity;

namespace PetLoveMatcher_Backend.DTO
{
    public class UserInfoDTO
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public IList<string> Roles { get; set; }


        public UserInfoDTO(string? userName) { }

        public UserInfoDTO(string? userName, string? firstName, string? lastName, string? email, string? phoneNumber, IList<string> roles)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Roles = roles;
        }
    }
}
