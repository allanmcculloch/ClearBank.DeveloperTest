using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Factories
{
    public class DataStoreFactory : IDataStoreFactory
    {
        private const string BackupType = "Backup";

        public IAccountDataStore Create(string dataStoreType)
        {
            if (dataStoreType == BackupType)
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }
    }
}
