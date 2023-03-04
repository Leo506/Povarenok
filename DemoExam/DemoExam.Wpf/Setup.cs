using DemoExam.Core.Services.Alert;
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
        Mvx.IoCProvider.RegisterType<IAlert, MessageBoxAlert>();
        base.InitializeFirstChance(iocProvider);
    }
}