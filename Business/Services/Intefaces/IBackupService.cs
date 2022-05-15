using Business.Models;

namespace Business.Services
{
    public interface IBackupService
    {
        List<Backup> GetBackUps();
        void MakeBackUp(string fileName);
        void MakeRestore(string backupName);
        void DeleteBackup(Backup backup);
    }
}
