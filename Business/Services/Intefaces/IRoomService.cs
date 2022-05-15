using Business.Models;

namespace Business.Services
{
    public interface IRoomService : IGenericService<Room>
    {
        Room ReadByName(string name);
        List<Checkpoint> GetRoomCheckpoints(int id);
        bool IsRoomEmpty(int roomId);
    }
}
