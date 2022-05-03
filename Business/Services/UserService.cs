using Business.Models;
using Common.Hashing;
using Data.UnitOfWork;

namespace Business.Services
{
    public class UserService : GenericService, IUserService
    {
        private readonly IPasswordCrypt _crypt;

        public UserService(IUnitOfWork uow, IPasswordCrypt crypt) 
            : base(uow)
        {
            _crypt = crypt;
        }

        public User Create(User entity)
        {
            var existingUser = Uow.Users
                .ReadAll()
                .FirstOrDefault(u => u.Login == entity.Login);

            if (existingUser is not null)
            {
                if (existingUser.Login == entity.Login)
                {
                    throw new ArgumentException("There's already exists a user with this login");
                }
            }

            entity.Password = _crypt.HashPassword(entity.Password);
            User entityDb = entity;
            Uow.Users.Create(entityDb);

            Uow.SaveChanges();

            return entityDb;
        }

        public User Authenticate(string login, string password)
        {
            var user = Uow.Users.ReadAll()
                .FirstOrDefault(u => u.Login == login);
            if (user is null)
                return null;

            return _crypt.Verify(password, user.Password)
                ? user
                : null;
        }

        public bool PersonExists(string login)
        {
            return Uow.Users.ReadAll().FirstOrDefault(p => p.Login == login) is { };
        }

        public User GetByLogin(string login)
        {
            return Uow.Users.ReadAll().FirstOrDefault(p => p.Login == login);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return Uow.Users.Read(id);
        }

        public List<User> GetAll()
        {
            return Uow.Users.ReadAll().Select(u => (User)u).ToList();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
