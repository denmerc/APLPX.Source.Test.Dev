using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APLX.UI.WPF.Data;
using APLPX.Client.Entity;

namespace APLPX.UI.Wpf.Tests
{
    [TestClass]
    public class MockRepos
    {
        [TestMethod]
        public void LoadList()
        {
            var repo = new MockAnalyticRepository();
            var list = repo.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            Assert.IsNotNull(list);
        }

        public void LoadFilters()
        {
            var repo = new MockAnalyticRepository();

            var session = new Session<Analytic.Identity>();
            var id = new Analytic.Identity();
            session.Data = id;
            var list = repo.LoadFilters(session);
            Assert.IsNotNull(list);
        }
    }
}
