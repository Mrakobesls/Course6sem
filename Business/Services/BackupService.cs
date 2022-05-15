using Business.Models;
using Common.CsvTranslator;
using Common.Extensions;
using Data.UnitOfWork;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Serialization;

namespace Business.Services
{
    public class BackupService : GenericService, IBackupService
    {
        private const string FilePath = @"D:\Visual Studio Community\Course6sem\Backups";

        public BackupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public List<Backup> GetBackUps()
        {
            var di = new DirectoryInfo(FilePath);
            var files = di.GetFiles()
                            .Select(f=> new Backup()
                            {
                                Name = f.Name
                            }).ToList();
            return files;
        }

        public void MakeBackUp(string fileName)
        {
            var dictionary = new Dictionary<string, string>();

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            foreach (var property in typeof(UnitOfWork).GetProperties())
            {
                //var a = typeof(UnitOfWork).GetProperty(property.Name).GetValue(Uow, null);
                //var objType = property.PropertyType;
                //var genericType = typeof(GenericRepository<>);
                //var type = genericType.MakeGenericType(objType);
                //var entityRepository = Activator.CreateInstance(objType);
                dynamic entityRepository = Uow.GetPropertyValue(property.Name);
                var entities = entityRepository.DbRead();
                //var csvTranslator = new CsvTranslator();
                //var csvEntities = csvTranslator.ListObjToCsv(entities);
                dictionary.Add(property.Name, JsonSerializer.Serialize(entities, jsonSerializerOptions));
            }

            using (FileStream fileStream
                = new FileStream(Path.Combine(FilePath, fileName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            using(StreamWriter outputStream = new StreamWriter(fileStream))
            {
                var serialized = JsonSerializer.Serialize(dictionary, jsonSerializerOptions);
                //serialized = serialized.Replace("@%newLine@", Environment.NewLine);
                outputStream.Write(serialized);
            }
        }

        public void MakeRestore(string backupName)
        {
            var serialized = File.ReadAllText(Path.Combine(FilePath, backupName + ".txt"));
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(serialized);
            foreach (var property in typeof(UnitOfWork).GetProperties())
            {
                dynamic entityRepository = Uow.GetPropertyValue(property.Name);
                var serializedEntities = dictionary[property.Name];
                entityRepository.DbCrouch(serializedEntities);
            }
        }

        public void DeleteBackup(Backup backup)
        {
            File.Delete(Path.Combine(FilePath, backup.Name + ".txt"));
        }
    }
}
