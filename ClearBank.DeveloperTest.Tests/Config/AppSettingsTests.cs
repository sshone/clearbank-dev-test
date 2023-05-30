using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace ClearBank.DeveloperTest.Tests.Config
{
    [TestClass]
    public class AppSettingsTests
    {
        private const string DataStoreTypeKey = "DataStoreType";

        private static AppSettings CreateAppSettings(string dataStoreTypeKeyValue)
        {
            ConfigurationManager.AppSettings[DataStoreTypeKey] = dataStoreTypeKeyValue;
            return new AppSettings();
        }

        [DataTestMethod]
        [DataRow("Backup", DataStoreType.Backup)]
        [DataRow(null, DataStoreType.Default)]
        [DataRow("InvalidValue", DataStoreType.Default)]
        public void DataStoreType_ReturnsExpectedValue(string configurationValue, DataStoreType expectedDataStoreType)
        {
            // Arrange
            var appSettings = CreateAppSettings(configurationValue);

            // Act
            var dataStoreType = appSettings.DataStoreType;

            // Assert
            Assert.AreEqual(expectedDataStoreType, dataStoreType);
        }
    }

}
