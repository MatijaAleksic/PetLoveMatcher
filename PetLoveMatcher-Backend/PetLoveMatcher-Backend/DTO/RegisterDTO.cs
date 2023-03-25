namespace DtoNetProject.DTO
{
    public class RegisterDTO
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? UserName { get; set; }
        public String? Password { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public RegisterDTO() { }

        public RegisterDTO(string? firstName, string? lastName, string? userName, string? password, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }   
    }
}
