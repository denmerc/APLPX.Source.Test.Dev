using APLPX.Client;
using APLPX.Client.Contracts;
using APLPX.Client.Mock.Proxies;
using APLPX.Entity;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.AppllicationServices
{
    public static class Cache
    {
        public static readonly StandardKernel Kernel = null;
        static Cache()
        {
            if(ConfigurationManager.AppSettings["Environment"] == "DEV")
            {
                Kernel = new StandardKernel(new ApplicationProvider());
            }
            else
            {
                Kernel = new StandardKernel(new MockApplicationProvider());

            }
        }
        public static User User { get; set; }

        public static List<Module> Modules { get; set; }
        //public MainViewModel MainViewModel{ get { return kernel.Get<MainViewModel>(
        //    new ConstructorArgument("session", _session),
        //    new ConstructorArgument("analyticService", AnalyticService),
        //    new ConstructorArgument("pricingService", PricingEverydayService),
        //    new ConstructorArgument("userService", UserService)
            
        //    ); }}
        public static LoginViewModel LoginViewModel { get { return Kernel.Get<LoginViewModel>(); } }
        public static IAnalyticService AnalyticService { get { return Kernel.Get<IAnalyticService>(); } }
        public static IPricingEverydayService PricingEverydayService { get { return Kernel.Get<IPricingEverydayService>(); } }
        public static IUserService UserService { get { return Kernel.Get<IUserService>(); } }
        public static EventAggregator EventManager { get { return Kernel.Get<EventAggregator>(); } }

    }

    public class ApplicationProvider : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnalyticService>().To<AnalyticClient>();
            Bind<IPricingEverydayService>().To<PricingEverydayClient>();
            Bind<IUserService>().To<UserClient>();
            Bind<EventAggregator>().ToSelf().InSingletonScope();

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
        }
    }
}
