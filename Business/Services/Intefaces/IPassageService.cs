using Business.Models;

namespace Business.Services
{
    public interface IPassageService
    {
        List<Checkpoint> GetUserCurrentCheckpoints(int userId);
        bool IsAccessible(int userId, int checkpointId);
        void PassCheckpoint(int userId, int checkpointId);
        Room GetUserCurrentRoom(int userId);
    }
}
