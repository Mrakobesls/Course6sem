using Business.Models;

namespace Business.Services
{
    public interface IUserService : IGenericService<User>
    {
        User Authenticate(string login, string password);
        bool PersonExists(string login);
        User GetByLogin(string login);
    }
}
