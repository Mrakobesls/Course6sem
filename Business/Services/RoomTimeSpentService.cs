using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class RoomTimeSpentService : GenericService, IRoomTimeSpentService
    {
        public RoomTimeSpentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public RoomTimeSpent Create(RoomTimeSpent entity)
        {
            var dbUser = Uow.RoomTimeSpents.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public RoomTimeSpent Get(int id)
        {
            return Uow.RoomTimeSpents.Read(id);
        }

        public List<RoomTimeSpent> GetAll()
        {
            return Uow.RoomTimeSpents.ReadAll()
                    .Select(c => (RoomTimeSpent)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RoomTimeSpent entity)
        {
            Uow.RoomTimeSpents.Update(entity);

            Uow.SaveChanges();
        }
    }
}
