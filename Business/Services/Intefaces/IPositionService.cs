using Business.Models;

namespace Business.Services
{
    public interface IPositionService : IGenericService<Position>
    {
        Position ReadByName(string name);
    }
}
