using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class PositionService : GenericService, IPositionService
    {
        public PositionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Position Create(Position entity)
        {
            var dbUser = Uow.Positions.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public Position Get(int id)
        {
            return Uow.Positions.Read(id);
        }

        public List<Position> GetAll()
        {
            return Uow.Positions.ReadAll()
                    .Select(c => (Position)c).ToList();
        }

        public void Delete(int id)
        {
            Uow.Positions.Delete(id);
            Uow.SaveChanges();
        }

        public void Update(Position entity)
        {
            Uow.Positions.Update(entity);
            Uow.SaveChanges();
        }

        public Position ReadByName(string name)
        {
            return Uow.Positions.ReadAll()
                        .FirstOrDefault(r => r.Name == name);
        }
    }
}
