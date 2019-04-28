using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    [TestFixture]
    public class DataStoreFactoryTests
    {
        private DataStoreFactory _dataStoreFactory;

        [SetUp]
        public void Setup()
        {
            _dataStoreFactory = new DataStoreFactory();
        }

        [TestCase("Backup", typeof(BackupAccountDataStore))]
        [TestCase("Live", typeof(AccountDataStore))]
        [TestCase("AnythingElse", typeof(AccountDataStore))]
        [TestCase("", typeof(AccountDataStore))]
        [TestCase(null, typeof(AccountDataStore))]
        public void Create_GetsCorrectType(string dataStoreType, Type type)
        {
            var dataStore = _dataStoreFactory.Create(dataStoreType);

            Assert.That(dataStore, Is.InstanceOf(type));
        }
    }
}
