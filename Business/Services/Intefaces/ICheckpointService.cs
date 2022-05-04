using Business.Models;

namespace Business.Services
{
    public interface ICheckpointService : IGenericService<Checkpoint>
    {
        Checkpoint ReadByName(string name);
    }
}
