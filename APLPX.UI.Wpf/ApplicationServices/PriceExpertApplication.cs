using APLPX.Client;
using APLPX.Client.Contracts;
using APLPX.Client.Mock.Proxies;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.ApplicationServices
{
    public class PriceExpertApplication
    {
        private static PriceExpertApplication current;
        private bool IsProperlyBoostrapped;

        private PriceExpertApplication() { }

        public static PriceExpertApplication Current
        {
            get
            {
                if( current == null)
                {
                    current = new PriceExpertApplication();
                }
                return current;
            }
        }

        public IKernel Container { get; private set; }

        public void Bootstrap()
        {

            if (ConfigurationManager.AppSettings["Environment"] == "DEV")
            {
                Container = new StandardKernel(new ApplicationProvider());
            }
            else
            {
                Container = new StandardKernel(new MockApplicationProvider());

            }
        }
    }

    public class ApplicationProvider : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnalyticService>().To<AnalyticClient>().InSingletonScope();
            Bind<IPricingEverydayService>().To<PricingEverydayClient>().InSingletonScope();
            Bind<IUserService>().To<UserClient>().InSingletonScope();
            Bind<EventAggregator>().ToSelf().InSingletonScope();

            Bind<AnalyticDisplayServices>().ToSelf().InSingletonScope()
                .WithConstructorArgument("analyticService", Kernel.Get<IAnalyticService>());


            Bind<MainViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf();
        }
    }

    public class MockApplicationProvider : NinjectModule
    {

        public override void Load()
        {
            Bind<IAnalyticService>().To<MockAnalyticClient>();
            Bind<IPricingEverydayService>().To<MockPricingEverydayClient>();
            Bind<IUserService>().To<MockUserClient>();
            Bind<EventAggregator>().ToSelf().InSingletonScope();

            Bind<MainViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf().InSingletonScope();
            Bind<SearchViewModel>().ToSelf().InTransientScope();
        }
    }

    public class ViewModelFactory
    {
        public static ViewModelBase Get(ModuleFeatureStep step)
        {
            return null;
        }
    }
}
