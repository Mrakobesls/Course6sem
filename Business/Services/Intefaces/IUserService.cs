using Business.Models;

namespace Business.Services
{
    public interface IUserService : IGenericService<User>
    {
        User Authenticate(string login, string password);
        bool PasswordsMatch(int userId, string password);
        bool PersonExists(string login);
        void UpdatePassword(int id, string password);
        User GetByLogin(string login);
        void UpdateUsers(List<User> users);
    }
}
