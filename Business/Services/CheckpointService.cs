using Business.Models;
using Data.UnitOfWork;

namespace Business.Services
{
    public class CheckpointService : GenericService, ICheckpointService
    {
        public CheckpointService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public Checkpoint Create(Checkpoint entity)
        {
            var dbUser = Uow.Checkpoints.Create(entity);

            Uow.SaveChanges();

            return dbUser;
        }

        public Checkpoint Get(int id)
        {
            return Uow.Checkpoints.Read(id);
        }

        public List<Checkpoint> GetAll()
        {
            return Uow.Checkpoints.ReadAll()
                    .Select(c => (Checkpoint)c).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Checkpoint entity)
        {
            Uow.Checkpoints.Update(entity);

            Uow.SaveChanges();
        }
    }
}
