using System;
using System.Collections.Generic;
using DemoExam.Core.Repositories;
using DemoExam.Core.Services.Alert;
using DemoExam.Core.ViewModels.ProductsViewModel;
using DemoExam.Database;
using DemoExam.Wpf.Pages;
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

    protected override IDictionary<Type, Type> InitializeLookupDictionary(IMvxIoCProvider iocProvider)
    {
        
        var dict = base.InitializeLookupDictionary(iocProvider);
        dict.Add(new KeyValuePair<Type, Type>(typeof(ClientProductsViewModel), typeof(ClientProductsPage)));
        dict.Add(new KeyValuePair<Type, Type>(typeof(ManagerProductsViewModel), typeof(ManagerProductsPage)));
        dict.Add(new KeyValuePair<Type, Type>(typeof(AdminProductsViewModel), typeof(AdminProductPage)));

        return dict;
    }
}