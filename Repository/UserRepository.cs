using DAL;
using Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
    {
        ILogger Logger { get; }
        private readonly KiCoKalenderContext _kiCoContext;
        public UserRepository(ILogger<UserRepository> Logger, KiCoKalenderContext kiCoContext)
        {
            this.Logger = Logger;
            _kiCoContext = kiCoContext;
        }

        public async Task<User> FindUserById(long? userId)
        {

            User foundUser = _kiCoContext.User.Find(userId);
            Logger.LogInformation("Found user by id: "+ userId);
            return foundUser;
        }

        public async void AddUser(User user)
        {
            _kiCoContext.User.Add(user);
            _kiCoContext.SaveChanges();
            Logger.LogInformation("Inserted user");
        }

        public async Task<User> FindUserByName(string name)
        {
            User user = _kiCoContext.User.SingleOrDefault(m => m.userFirstName == name);
            Logger.LogInformation("Found user by name: "+ name);

            return user;
        }
        public async void UpdateUser(User user)
        {
            Logger.LogInformation("Updated user");
        }
        public async void DeleteUser(long? userId)
        {
            User foundUser = _kiCoContext.User.Find(userId);
            _kiCoContext.User.Remove(foundUser);
            _kiCoContext.SaveChanges();
            Logger.LogInformation("Deleted user with id: " + userId);
        }
    }
}
