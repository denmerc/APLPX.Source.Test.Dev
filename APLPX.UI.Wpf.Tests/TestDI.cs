using System;
using System.Windows;
using System.Windows.Markup;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using APLPX.UI.WPF;
using APLPX.UI.WPF.ViewModels;
using APLPX.Client;
using Ninject.Parameters;
using APLPX.Client.Contracts;
using APLPX.UI.WPF.Events;
using APLPX.Entity;

namespace APLPX.UI.Wpf.Tests
{
    /// <summary>
    /// Summary description for TestDI
    /// </summary>
    [TestClass]
    public class TestDI
    {
        public TestDI()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
            IKernel kernel = new StandardKernel();

            //mock

            var accentColors = new List<AccentColorMenuData>();
            var appThemes = new List<AppThemeMenuData>();

            kernel.Bind<IAnalyticService>().To<AnalyticClient>();
            kernel.Bind<IPricingEverydayService>().To<PricingEverydayClient>();
            kernel.Bind<IUserService>().To<UserClient>();
            kernel.Bind<EventAggregator>().ToSelf().InSingletonScope();
            

            var analyticClient =  kernel.Get<IAnalyticService>();
            var pricingEverydayClient =  kernel.Get<IPricingEverydayService>();
            var userClient =  kernel.Get<IUserService>();
            var eventAggregator = kernel.Get<EventAggregator>();

            kernel.Bind<MainViewModel>().ToSelf()
                .WithConstructorArgument("session", new Session<NullT>())
                .WithConstructorArgument("analyticService", analyticClient)
                .WithConstructorArgument("pricingService", pricingEverydayClient)
                .WithConstructorArgument("userService", userClient)
                .WithConstructorArgument("eventManager", eventAggregator)
                .WithPropertyValue("AccentColors", accentColors )
                .WithPropertyValue("AppThemes", appThemes)
                
                ;
            var main = kernel.Get<MainViewModel>(
                new ConstructorArgument("session", new Session<NullT>()),
                new ConstructorArgument("analyticService", analyticClient), 
                new ConstructorArgument ("pricingService", pricingEverydayClient),
                new ConstructorArgument("userService", userClient),
                new ConstructorArgument("eventManager", eventAggregator)
                );
            

        }
    }
}
