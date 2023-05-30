using ClearBank.DeveloperTest.Types;
using System;
using System.Configuration;

namespace ClearBank.DeveloperTest.Config
{
    public class AppSettings : IAppSettings
    {
        private const string DataStoreTypeKey = "DataStoreType";
        private const DataStoreType DefaultDataStoreType = DataStoreType.Default;

        public DataStoreType DataStoreType
        {
            get
            {
                if (Enum.TryParse(ConfigurationManager.AppSettings[DataStoreTypeKey], out DataStoreType dataStoreType))
                {
                    return dataStoreType;
                }

                return DefaultDataStoreType;
            }
        }
    }
}
