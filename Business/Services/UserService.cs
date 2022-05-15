using Business.Models;
using Common.Hashing;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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


            var dbEntity = (Data.Models.User)entity;
            dbEntity.CurrentRoomId = 1;

            dbEntity.Password = _crypt.HashPassword(entity.Password);
            foreach (var accessLevelId in entity.AccessLevelsId)
            {
                dbEntity.AccessLevels.Add(Uow.AccessLevels.Read(accessLevelId));
            }
            Uow.Users.Create(dbEntity);

            Uow.SaveChanges();

            return dbEntity;
        }

        public User Authenticate(string login, string password)
        {
            var user = Uow.Users.ReadAll()
                .Include(u => u.AccessLevels)
                .FirstOrDefault(u => u.Login == login);
            if (user is null)
                return null;

            return _crypt.Verify(password, user.Password)
                ? user
                : null;
        }

        public bool PasswordsMatch(int userId, string password)
        {
            var user = Uow.Users.Read(userId);

            return _crypt.Verify(password, user.Password);
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
            return Uow.Users.ReadAll()
                    .Select(u => (User)u)
                    .ToList();
        }

        public void UpdatePassword(int id, string password)
        {
            var userDb = Uow.Users.Read(id);
            userDb.Password = _crypt.HashPassword(password);
            Uow.Users.Update(userDb);

            Uow.SaveChanges();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsers(List<User> users)
        {
            foreach(var user in users)
            {
                var dbEntity = Uow.Users.Read(user.Id);
                dbEntity.IsDisabled = user.IsDisabled;
            }

            Uow.SaveChanges();
        }
    }
}
