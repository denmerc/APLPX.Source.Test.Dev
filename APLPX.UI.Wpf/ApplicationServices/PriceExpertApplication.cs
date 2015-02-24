using APLPX.Client;
using APLPX.Client.Contracts;
using MOCKProxies = APLPX.Client.Mock.Proxies;
using MOCKEntity = APLPX.Common.Mock.Entity;
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
using DTO = APLPX.Entity;
using APLPX.UI.WPF.ViewModels.Analytic;
using APLPX.UI.WPF.ViewModels.Pricing;

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

            if (ConfigurationManager.AppSettings["Environment"] == "DE")
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

            Bind<PricingEverydayDisplayService>().ToSelf().InSingletonScope()
                .WithConstructorArgument("pricingService", Kernel.Get<IPricingEverydayService>());


            Bind<MainViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf();

            //Load ViewModel Resolver
            //ViewModelFactory<ViewModelBase>.Register(ModuleFeatureStepType.AdministrationHomeDashboard, () => new HomeViewModel());
            //ViewModelFactory<APLPX.UI.WPF.ViewModels.Analytic.AnalyticDriverViewModel>.Register(ModuleFeatureStepType.PlanningAnalyticsValueDrivers, (DisplayEntities.Analytic a, DisplayEntities.ModuleFeature f) => new APLPX.UI.WPF.ViewModels.Analytic.AnalyticDriverViewModel(a,f));
            //ViewModelFactory<ViewModelBase>.Register(ModuleFeatureStepType.AdministrationHomeDashboard, () => new HomeViewModel());
            //ViewModelFactory<ViewModelBase>.Register(ModuleFeatureStepType.AdministrationHomeDashboard, () => new HomeViewModel());
            //var vm = ViewModelFactory<ViewModelBase>.Create(ModuleFeatureStepType.AdministrationHomeDashboard);

        }
    }

    public class MockApplicationProvider : NinjectModule
    {

        public override void Load()
        {
            Bind<IAnalyticService>().To<MOCKProxies.MockAnalyticClient>();
            Bind<IPricingEverydayService>().To<MOCKProxies.MockPricingEverydayClient>();
            Bind<IUserService>().To<MOCKProxies.MockUserClient>();
            Bind<EventAggregator>().ToSelf().InSingletonScope();

            Bind<MainViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf().InSingletonScope();
            Bind<SearchViewModel>().ToSelf().InTransientScope();
            Bind<AnalyticDisplayServices>().ToSelf().InSingletonScope()
    .WithConstructorArgument("analyticService", Kernel.Get<IAnalyticService>());

            Bind<PricingEverydayDisplayService>().ToSelf().InSingletonScope()
                .WithConstructorArgument("pricingService", Kernel.Get<IPricingEverydayService>());

        }
    }

    public class ViewModelFactory<T>
    {
        private ViewModelFactory() { }
        static readonly Dictionary<DTO.ModuleFeatureStepType, Func<T>> _dict = new Dictionary<DTO.ModuleFeatureStepType, Func<T>>();

        public static T Create(DTO.ModuleFeatureStepType stepType)
        {
            Func<T> constructor = null;
            if (_dict.TryGetValue(stepType, out constructor))
                return constructor();

            throw new ArgumentException("No type registered for this step");
        }
        public static void Register(DTO.ModuleFeatureStepType stepType, Func<T> constructor)
        {
            _dict.Add(stepType, constructor);
        }
    }



}
