using Business.Model;
using ShemTeh.Data.UnitOfWork;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _uow;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public int Create(Role entity)
        {
            var dbUser = _uow.Roles.Create(entity);

            _uow.SaveChanges();

            return dbUser.Id;
        }

        public Role Get(int id)
        {
            return _uow.Roles.Read(id);
        }

        public List<Role> GetAll()
        {
            return _uow.Roles.ReadAll()
                    .Select(c => (Role)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            _uow.Roles.Update(entity);

            _uow.SaveChanges();
        }
    }
}
