using Domain;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<User> FindUserById(long? userId);
        void AddUser(User user);
        Task<User> FindUserByName(string name);
        void UpdateUser(User user);
        void DeleteUser(long? userId);
    }
}
