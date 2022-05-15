using Business.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class RoomService : GenericService, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }


        public Room Create(Room entity)
        {
            var dbUser = Uow.Rooms.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public Room Get(int id)
        {
            return Uow.Rooms.Read(id);
        }

        public List<Room> GetAll()
        {
            return Uow.Rooms.ReadAll()
                    .Select(r => (Room)r).ToList();
        }

        public void Delete(int id)
        {
            Uow.Rooms.Delete(id);
            Uow.SaveChanges();
        }

        public void Update(Room entity)
        {
            Uow.Rooms.Update(entity);

            Uow.SaveChanges();
        }

        public Room ReadByName(string name)
        {
            return Uow.Rooms.ReadAll()
                        .FirstOrDefault(r => r.Name == name);
        }

        public List<Checkpoint> GetRoomCheckpoints(int id)
        {
            return Uow.Rooms.ReadAll()
                    .Include(r=>r.Checkpoints)
                    .Where(r=>r.Id == id)
                    .SelectMany(r=>r.Checkpoints)
                    .Select(c=>(Checkpoint)c)
                    .ToList();
        }

        public bool IsRoomEmpty(int roomId)
        {
            return Uow.Rooms.ReadAll()
                    .Include(r=>r.Users)
                    .Single(r=>r.Id == roomId)
                    .Users.Any();
        }
    }
}
