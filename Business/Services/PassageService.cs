using Business.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class PassageService : GenericService, IPassageService
    {
        public PassageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }


        public List<Checkpoint> GetUserCurrentCheckpoints(int userId)
        {
            var currentRoomId = Uow.Users.Read(userId).CurrentRoomId;
            return Uow.Rooms.ReadAll()
                    .Include(r => r.Checkpoints)
                    .Single(r => r.Id == currentRoomId)
                    .Checkpoints.Select(c => (Checkpoint)c)
                    .ToList();
        }

        public bool IsAccessible(int userId, int checkpointId)
        {
            var userAccessLevelses
                = Uow.Users.ReadAll()
                    .Include(u => u.AccessLevels)
                    .Single(u => u.Id == userId)
                    .AccessLevels.Select(a => a.Id);
            var checkpointAccessLevels
                = Uow.Checkpoints.ReadAll()
                    .Include(c => c.AccessLevels)
                    .Single(c => c.Id == checkpointId)
                    .AccessLevels.Select(a => a.Id);
            return checkpointAccessLevels.Intersect(userAccessLevelses).Count() > 0;
        }

        public void PassCheckpoint(int userId, int checkpointId)
        {
            var checkpointRoomsId =
                Uow.Checkpoints.ReadAll()
                    .Include(c => c.Rooms)
                    .Single(c => c.Id == checkpointId)
                    .Rooms.Select(r => r.Id)
                    .ToList();
            var user = Uow.Users.Read(userId);
            user.CurrentRoomId
                = user.CurrentRoomId == checkpointRoomsId[0]
                    ? checkpointRoomsId[1]
                    : checkpointRoomsId[0];

            Uow.SaveChanges();
        }

        public Room GetUserCurrentRoom(int userId)
        {
            return Uow.Users.ReadAll()
                    .Include(u => u.CurrentRoom)
                    .Single(u => u.Id == userId)
                    .CurrentRoom;
        }
    }
}
