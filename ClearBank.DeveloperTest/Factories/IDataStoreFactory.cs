using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IDataStoreFactory
    {
        IAccountDataStore Create(string dataStoreType);
    }
}
