using Business.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class CheckpointService : GenericService, ICheckpointService
    {
        public CheckpointService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Checkpoint Create(Checkpoint entity)
        {
            var dbEntity = (Data.Models.Checkpoint)entity;
            dbEntity.Rooms.Add(Uow.Rooms.Read(entity.FirstRoomId));
            dbEntity.Rooms.Add(Uow.Rooms.Read(entity.SecondRoomId));
            foreach (var accessLevelId in entity.AccessLevelsId)
            {
                dbEntity.AccessLevels.Add(Uow.AccessLevels.Read(accessLevelId));
            }
            var dbUser = Uow.Checkpoints.Create(dbEntity);

            Uow.SaveChanges();

            return dbUser;
        }

        public Checkpoint Get(int id)
        {
            return Uow.Checkpoints
                    .ReadAll()
                    .Include(c => c.Rooms)
                    .Include(c => c.AccessLevels)
                    .Single(x => x.Id == id);
        }

        public List<Checkpoint> GetAll()
        {
            return Uow.Checkpoints.ReadAll()
                    .Include(c => c.Rooms)
                    .Select(c => (Checkpoint)c).ToList();
        }

        public void Delete(int id)
        {
            Uow.Checkpoints.Delete(id);
            Uow.SaveChanges();
        }

        public void Update(Checkpoint entity)
        {
            var dbEntity = Uow.Checkpoints
                    .ReadAll()
                    .Include(c => c.Rooms)
                    .Include(c => c.AccessLevels)
                    .FirstOrDefault(c => c.Id == entity.Id);
            dbEntity.Name = entity.Name;
            dbEntity.Description = entity.Description;

            dbEntity.Rooms.Clear();
            dbEntity.AccessLevels.Clear();
            Uow.SaveChanges();

            dbEntity.Rooms.Add(Uow.Rooms.Read(entity.FirstRoomId));
            dbEntity.Rooms.Add(Uow.Rooms.Read(entity.SecondRoomId));
            foreach (var accessLevelId in entity.AccessLevelsId)
            {
                dbEntity.AccessLevels.Add(Uow.AccessLevels.Read(accessLevelId));
            }
            Uow.Checkpoints.Update(dbEntity);

            Uow.SaveChanges();
        }

        public Checkpoint ReadByName(string name)
        {
            return Uow.Checkpoints.ReadAll()
                        .Single(r => r.Name == name);
        }
    }
}
