using Business.Models;

namespace Business.Services
{
    public interface IAccessLevelService : IGenericService<AccessLevel>
    {
        AccessLevel ReadByName(string name);
    }
}
