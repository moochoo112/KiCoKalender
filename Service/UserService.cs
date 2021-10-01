using Domain;
using Repository;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private UserRepository UserRepository { get; }

        public UserService(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<User> FindUserById(long? userId)
        {
            return await UserRepository.FindUserById(userId);
        }

        public void AddUser(User user)
        {
            UserRepository.AddUser(user);
        }

        public async Task<User> FindUserByName(string name)
        {
            return await UserRepository.FindUserByName(name);
        }

        public void UpdateUser(User user)
        {
            UserRepository.UpdateUser(user);
        }

        public void DeleteUser(long? userId)
        {
            UserRepository.DeleteUser(userId);
        }
    }
}
