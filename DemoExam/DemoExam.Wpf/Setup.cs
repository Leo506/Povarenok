using DemoExam.Core.Repositories;
using DemoExam.Core.Services.Alert;
using DemoExam.Database;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Core;

namespace DemoExam.Wpf;

public class Setup : MvxWpfSetup<Core.App>
{
    protected override ILoggerProvider? CreateLogProvider()
    {
        return default;
    }

    protected override ILoggerFactory? CreateLogFactory()
    {
        return default!;
    }

    protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
    {
        Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => new TradeContext());
        Mvx.IoCProvider.RegisterType<IUserRepository>(() => Mvx.IoCProvider.Resolve<TradeContext>());
        Mvx.IoCProvider.RegisterType<IProductRepository>(() => Mvx.IoCProvider.Resolve<TradeContext>());
        Mvx.IoCProvider.RegisterType<IOrderRepository>(() => Mvx.IoCProvider.Resolve<TradeContext>());
        Mvx.IoCProvider.RegisterType<IPickupPointRepository>(() => Mvx.IoCProvider.Resolve<TradeContext>());
        Mvx.IoCProvider.RegisterType<IAlert, MessageBoxAlert>();
        base.InitializeFirstChance(iocProvider);
    }
}