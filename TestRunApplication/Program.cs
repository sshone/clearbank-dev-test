using ClearBank.DeveloperTest.Config;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;

/*
 * I have provided this Project and snippet to showcase how I would use DependencyInjection with my refactored solution
 * This will currently return a MakePaymentResult with a Success value of false as the AccountDataStore classes are empty
 */

var serviceProvider = new ServiceCollection()
                .AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IPaymentSchemeValidator, BacsPaymentSchemeValidator>()
                .AddSingleton<IPaymentSchemeValidator, ChapsPaymentSchemeValidator>()
                .AddSingleton<IPaymentSchemeValidator, FasterPaymentsSchemeValidator>()
                .AddSingleton<IPaymentSchemeValidatorFactory, PaymentSchemeValidatorFactory>()
                .AddSingleton<IAppSettings, AppSettings>()
                .AddSingleton<IAccountDataStore>(serviceProvider =>
                {
                    var appSettings = serviceProvider.GetRequiredService<IAppSettings>();
                    var dataStoreType = appSettings.DataStoreType;

                    return dataStoreType switch
                    {
                        DataStoreType.Default => new AccountDataStore(),
                        DataStoreType.Backup => new BackupAccountDataStore(),
                        _ => throw new ArgumentOutOfRangeException(nameof(dataStoreType), dataStoreType, "Invalid data store type."),
                    };
                })
                .AddSingleton<IAccountService,  AccountService>()
                .BuildServiceProvider();

var paymentService = serviceProvider.GetService<IPaymentService>();
var paymentRequest = new MakePaymentRequest
{
    Amount = 100,
    DebtorAccountNumber = "12345678",
    PaymentScheme = PaymentScheme.Bacs
};

var result = paymentService.MakePayment(paymentRequest);

Console.WriteLine($"Result of Payment: {result.Success}");