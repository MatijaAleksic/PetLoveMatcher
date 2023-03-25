using DtoNetProject.Repositories;
using Microsoft.EntityFrameworkCore;
using PetLoveMatcher_Backend.Models;

namespace DtoNetProject.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUserByID(string id)
        {
            return _userRepository.GetUserByID(id);
        }

        public void InsertUser(User user)
        {
            User? temp = _userRepository.GetUserByID(user.Id);
            if (temp != null) 
            {
                throw new Exception("User with given id alredy exists!");
            }
            _userRepository.InsertUser(user);
        }

        public void DeleteUser(string userId)
        {
            User? temp = _userRepository.GetUserByID(userId);
            if (temp == null)
            {
                throw new Exception("User with given id not found!");
            }
            else
            {
                _userRepository.DeleteUser(temp);
            }
        }

        public void DeleteUser(User user)
        {
            User? temp = _userRepository.GetUserByID(user.Id);
            if (temp == null)
            {
                throw new Exception("User with given id not found!");
            }
            else
            {
                _userRepository.DeleteUser(temp);
            }
        }

        public void UpdateUser(User user)
        {
            User? temp = _userRepository.GetUserByID(user.Id);
            if (temp == null)
            {
                throw new Exception("User with given id not found!");
            }
            else
            {
                temp.FirstName = user.FirstName;
                temp.LastName = user.LastName;
                temp.UserName = user.UserName;
                temp.Email = user.Email;

                _userRepository.UpdateUser(temp);
            }


        }

    }
}
