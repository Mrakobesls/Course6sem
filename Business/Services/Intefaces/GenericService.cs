using Data.UnitOfWork;

namespace Business.Services
{
    public abstract class GenericService
    {
        protected readonly IUnitOfWork Uow;
        public GenericService(IUnitOfWork uow)
        {
            Uow = uow;
        }
    }
}
