using Domain;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository
    {
        ILogger Logger { get; }

        public UserRepository(ILogger<UserRepository> Logger)
        {
            this.Logger = Logger;
        }

        public async Task<User> FindUserById(long? userId)
        {
            User user = new User();
            Logger.LogInformation("Found user by id: "+ userId);

            return user;
        }

        public async void AddUser(User user)
        {
            Logger.LogInformation("Inserted user");
        }

        public async Task<User> FindUserByName(string name)
        {
            User user = new User() { userId = 33, userName = "Dirk Dirksma", userRole = Role.Parent };
            Logger.LogInformation("Found user by name: "+ name);

            return user;
        }
        public async void UpdateUser(User user)
        {
            Logger.LogInformation("Updated user");
        }
        public async void DeleteUser(long? userId)
        {
            Logger.LogInformation("Deleted user with id: " + userId);
        }
    }
}
