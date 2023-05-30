using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Config
{
    public interface IAppSettings
    {
        DataStoreType DataStoreType { get; }
    }
}