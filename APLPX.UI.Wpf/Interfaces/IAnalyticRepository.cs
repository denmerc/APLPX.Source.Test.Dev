using System;
using System.Collections.Generic;
using APLPX.Client.Entity;


namespace APLPX.UI.WPF.Interfaces
{
    /// <summary>
    /// Interface for the Analytic repository.
    /// </summary>
    public interface IAnalyticRepository
    {
        void Save<T>(T item) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Delete<T>(T item) where T : class, new();
        T Single<T>() where T : class, new();

        IEnumerable<T> All<T>() where T : class, new();

        IEnumerable<T> All<T>(int page, int pageSize) where T : class, new();

        Session<List<AnalyticIdentity>> LoadList(Session<NullT> session);

        Session<AnalyticIdentity> SaveIdentity(Session<AnalyticIdentity> session);

        Session<List<Filter>> LoadFilters(Session<AnalyticIdentity> session);

        Session<List<Filter>> SaveFilters(Session<Analytic> session);

        Session<List<AnalyticValueDriver>> LoadDrivers(Session<AnalyticIdentity> session);

        Session<List<AnalyticValueDriver>> SaveDrivers(Session<Analytic> session);

    }
}
