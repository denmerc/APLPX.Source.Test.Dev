using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using APLPX.Entity;

namespace APLPX.Server.Data {

    class AnalyticMap
    {
        #region Load Analytic
        public void LoadMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command procedure...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadMetaMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.action, SqlDbType.Int, 0, ParameterDirection.Input, session.ClientCommand.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }

        public Entity.Analytic LoadMapData(System.Data.DataSet dataSet) {

            //Map the entity data...
            Entity.Analytic analytic = null;
            Entity.AnalyticIdentity analyticIdentity = null;
            List<Entity.AnalyticValueDriver> driverList = null;
            List<Entity.AnalyticValueDriverMode> driverModeList = null;
            List<Entity.ValueDriverGroup> driverGroupList = null;
            List<Entity.AnalyticResultValueDriverGroup> driverResultList = null;
            List<Entity.AnalyticPriceListGroup> priceGroupList = null;
            List<Entity.FilterGroup> filterGroupList = null;
            List<Entity.PriceList> priceLists = null;
            List<Entity.Filter> filterLists = null;

            #region Load Analytic...
            var queryIdentity = dataSet.Tables[AnalyticMap.Names.loadAnaltyicIdentityData].AsEnumerable()
                .Select(identity => new {
                    Id = identity.Field<int>(AnalyticMap.Names.analyticsId),
                    SearchId = identity.Field<int>(AnalyticMap.Names.analyticsSearchGroupId),
                    SearchGroup = identity.Field<string>(AnalyticMap.Names.analyticsSearchGroupKey),
                    IdentityName = identity.Field<string>(AnalyticMap.Names.analyticsName),
                    IdentityDescription = identity.Field<string>(AnalyticMap.Names.analyticsDescription),
                    IdentityNotes = identity.Field<string>(AnalyticMap.Names.analyticsNotes),
                    IdentityRefreshedText = identity.Field<string>(AnalyticMap.Names.refreshedText),
                    IdentityCreatedText = identity.Field<string>(AnalyticMap.Names.createdText),
                    IdentityEditedText = identity.Field<string>(AnalyticMap.Names.editedText),
                    IdentityRefreshed = identity.Field<DateTime>(AnalyticMap.Names.refreshed),
                    IdentityCreated = identity.Field<DateTime>(AnalyticMap.Names.created),
                    IdentityEdited = identity.Field<DateTime>(AnalyticMap.Names.edited),
                    IdentityAuthor = identity.Field<string>(AnalyticMap.Names.authorText),
                    IdentityEditor = identity.Field<string>(AnalyticMap.Names.editorText),
                    IdentityOwner = identity.Field<string>(AnalyticMap.Names.ownerText),
                    IdentityShared = identity.Field<bool>(AnalyticMap.Names.analyticsShared),
                    IdentityActive = identity.Field<bool>(AnalyticMap.Names.analyticsActive)
                }).Distinct();

            foreach (var identity in queryIdentity) {
                analyticIdentity = new AnalyticIdentity(
                    identity.IdentityName, identity.IdentityDescription, identity.IdentityNotes, 
                    identity.IdentityRefreshedText, identity.IdentityCreatedText, identity.IdentityEditedText, identity.IdentityRefreshed, identity.IdentityCreated, identity.IdentityEdited,
                    identity.IdentityAuthor, identity.IdentityEditor, identity.IdentityOwner, identity.IdentityShared, identity.IdentityActive);
                #region Load Drivers...
                driverList = new List<AnalyticValueDriver>();
                var queryDrivers = dataSet.Tables[AnalyticMap.Names.loadAnaltyicDriverData].AsEnumerable()
                    .Where(drivers => drivers.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(drivers => new {
                        Id = drivers.Field<int>(AnalyticMap.Names.driverId),
                        Key = drivers.Field<int>(AnalyticMap.Names.driverType),
                        Selected = drivers.Field<bool>(AnalyticMap.Names.driverSelected),
                        Name = drivers.Field<string>(AnalyticMap.Names.driverTypeName),
                        Title = drivers.Field<string>(AnalyticMap.Names.driverTypeText),
                        Sort = drivers.Field<short>(AnalyticMap.Names.driverSort),
                    }).Distinct();
                foreach (var driver in queryDrivers) {
                    driverModeList = new List<AnalyticValueDriverMode>();
                    driverResultList = new List<AnalyticResultValueDriverGroup>();
                    driverList.Add(new Entity.AnalyticValueDriver(driver.Id, driver.Key, driver.Selected, driver.Name, driver.Title, driver.Sort, driverResultList, driverModeList));
                    #region Load Driver modes...
                    var queryModes = dataSet.Tables[AnalyticMap.Names.loadAnaltyicDriverData].AsEnumerable()
                        .Where(modes =>
                            modes.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            modes.Field<int>(AnalyticMap.Names.driverType) == driver.Key
                        ).Select(modes => new {
                            Key = modes.Field<int>(AnalyticMap.Names.driverMode),
                            Selected = modes.Field<bool>(AnalyticMap.Names.driverModeSelected),
                            Name = modes.Field<string>(AnalyticMap.Names.driverModeName),
                            Title = modes.Field<string>(AnalyticMap.Names.driverModeText),
                            Sort = modes.Field<short>(AnalyticMap.Names.driverModeSort)
                        }).Distinct();
                    foreach (var mode in queryModes) {
                        driverGroupList = new List<Entity.ValueDriverGroup>();
                        driverModeList.Add(new Entity.AnalyticValueDriverMode(mode.Key, mode.Selected, mode.Name, mode.Title, mode.Sort, driverGroupList));
                        //Mode Groups...
                        var queryGroups = dataSet.Tables[AnalyticMap.Names.loadAnaltyicDriverData].AsEnumerable()
                            .Where(groups => 
                                groups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                                groups.Field<int>(AnalyticMap.Names.driverType) == driver.Key &&
                                groups.Field<int>(AnalyticMap.Names.driverMode) == mode.Key
                            ).Select(groups => new {
                               Id = groups.Field<int>(AnalyticMap.Names.driverGroupId),
                               Value = groups.Field<short>(AnalyticMap.Names.driverGroupValue),
                               Min = groups.Field<decimal>(AnalyticMap.Names.driverGroupLimitLower),
                               Max = groups.Field<decimal>(AnalyticMap.Names.driverGroupLimitUpper),
                               Sort = groups.Field<short>(AnalyticMap.Names.driverGroupSort)
                            }).Distinct();
                        foreach (var group in queryGroups) {
                            driverGroupList.Add(new Entity.ValueDriverGroup(group.Id, group.Value, group.Min, group.Max, group.Sort));
                        }
                    }
                    #endregion
                    #region Load Driver results...
                    var queryResults = dataSet.Tables[AnalyticMap.Names.loadAnaltyicResultData].AsEnumerable()
                        .Where(results =>
                            results.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            results.Field<int>(AnalyticMap.Names.resultDriverType) == driver.Key
                        ).Select(results => new {
                            Value = results.Field<short>(AnalyticMap.Names.resultDriverGroup),
                            Min = results.Field<decimal>(AnalyticMap.Names.resultMetricMinLimit),
                            Max = results.Field<decimal>(AnalyticMap.Names.resultMetricMaxLimit),
                            SkuCount = results.Field<int>(AnalyticMap.Names.resultSkuCount),
                            Sales = results.Field<string>(AnalyticMap.Names.resultCurrentSales) 
                        }).Distinct();
                    foreach (var result in queryResults) {
                        driverResultList.Add(new Entity.AnalyticResultValueDriverGroup(result.Value, result.Min, result.Max, result.SkuCount, result.Sales));
                    }
                    #endregion
                }
                #endregion
                #region Load Price Lists...
                priceGroupList = new List<AnalyticPriceListGroup>();
                var queryPriceGroups = dataSet.Tables[AnalyticMap.Names.loadAnaltyicPriceListData].AsEnumerable()
                    .Where(priceGroups => priceGroups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(priceGroups => new {
                        Key = priceGroups.Field<int>(AnalyticMap.Names.priceListType),
                        Name = priceGroups.Field<string>(AnalyticMap.Names.priceListTypeName),
                        Title = priceGroups.Field<string>(AnalyticMap.Names.priceListTypeText),
                        Sort = priceGroups.Field<short>(AnalyticMap.Names.priceListTypeSort)
                    }).Distinct();
                foreach (var priceGroup in queryPriceGroups) {
                    priceLists = new List<PriceList>();
                    priceGroupList.Add(new AnalyticPriceListGroup(priceGroup.Key, priceGroup.Name, priceGroup.Title, priceGroup.Sort, priceLists));
                    var queryPriceLists = dataSet.Tables[AnalyticMap.Names.loadAnaltyicPriceListData].AsEnumerable()
                        .Where(pl =>
                            pl.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            pl.Field<int>(AnalyticMap.Names.priceListType) == priceGroup.Key
                        ).Select(pl => new {
                            Id = pl.Field<int>(AnalyticMap.Names.priceListId),
                            Key = pl.Field<int>(AnalyticMap.Names.priceListKey),
                            Code = pl.Field<string>(AnalyticMap.Names.priceListCode),
                            Name = pl.Field<string>(AnalyticMap.Names.priceListName),
                            Title = pl.Field<string>(AnalyticMap.Names.priceListText),
                            Sort = pl.Field<short>(AnalyticMap.Names.priceListSort),
                            Selected = pl.Field<bool>(AnalyticMap.Names.priceListSelected)
                        }).Distinct();
                    foreach (var priceList in queryPriceLists) {
                        priceLists.Add(new PriceList(priceList.Id, priceList.Key, priceList.Code, priceList.Name, priceList.Sort, priceList.Selected));
                    }
                }
                #endregion
                #region Load Filters...
                filterGroupList = new List<FilterGroup>();
                var queryFilterGroups = dataSet.Tables[AnalyticMap.Names.loadAnaltyicFilterData].AsEnumerable()
                    .Where(filterGroups => filterGroups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(filterGroups => new {
                        Key = filterGroups.Field<int>(AnalyticMap.Names.filterType),
                        Sort = filterGroups.Field<short>(AnalyticMap.Names.filterTypeSort),
                        Name = filterGroups.Field<string>(AnalyticMap.Names.filterTypeText),
                    }).Distinct();
                foreach (var filterGroup in queryFilterGroups) {
                    filterLists = new List<Filter>();
                    filterGroupList.Add(new FilterGroup(filterGroup.Sort, filterGroup.Name, filterLists));
                    var queryFilterLists = dataSet.Tables[AnalyticMap.Names.loadAnaltyicFilterData].AsEnumerable()
                        .Where(fl =>
                            fl.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            fl.Field<int>(AnalyticMap.Names.filterType) == filterGroup.Key
                        ).Select(fl => new {
                            Id = fl.Field<int>(AnalyticMap.Names.filterId),
                            Key = fl.Field<int>(AnalyticMap.Names.filterKey),
                            Code = fl.Field<string>(AnalyticMap.Names.filterCode),
                            Name = fl.Field<string>(AnalyticMap.Names.filterName),
                            Sort = fl.Field<short>(AnalyticMap.Names.filterSort),
                            Selected = fl.Field<bool>(AnalyticMap.Names.filterSelected)
                        }).Distinct();
                    foreach (var filterList in queryFilterLists) {
                        filterLists.Add(new Filter(filterList.Id, filterList.Key, filterList.Code, filterList.Name, filterList.Selected, filterList.Sort));
                    }
                }
                #endregion

                analytic = new Entity.Analytic(identity.Id, identity.SearchId, identity.SearchGroup, analyticIdentity, driverList, priceGroupList, filterGroupList);
            }
            #endregion

            return analytic;
        }
        
        #endregion

        #region Load Identities...
        public void LoadListMapParameters(Session<NullT> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadIdentitiesMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;

        }

        public List<Entity.Analytic> LoadListMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            List<Entity.Analytic> list = new List<Analytic>(data.Rows.Count);
            //Record set...
            while (reader.Read()) {
                list.Add(
                    new Analytic (
                        Int32.Parse(reader[AnalyticMap.Names.analyticsId].ToString()),
                        Int32.Parse(reader[AnalyticMap.Names.analyticsSearchGroupId].ToString()),
                        reader[AnalyticMap.Names.analyticsSearchGroupKey].ToString(),
                        new AnalyticIdentity(
                            reader[AnalyticMap.Names.analyticsName].ToString(),
                            reader[AnalyticMap.Names.analyticsDescription].ToString(),
                            reader[AnalyticMap.Names.analyticsNotes].ToString(),
                            reader[AnalyticMap.Names.refreshedText].ToString(),
                            reader[AnalyticMap.Names.createdText].ToString(),
                            reader[AnalyticMap.Names.editedText].ToString(),
                            DateTime.Parse(reader[AnalyticMap.Names.refreshed].ToString()),
                            DateTime.Parse(reader[AnalyticMap.Names.created].ToString()),
                            DateTime.Parse(reader[AnalyticMap.Names.edited].ToString()),
                            reader[AnalyticMap.Names.authorText].ToString(),
                            reader[AnalyticMap.Names.editorText].ToString(),
                            reader[AnalyticMap.Names.ownerText].ToString(),
                            Boolean.Parse(reader[AnalyticMap.Names.analyticsShared].ToString()),
                            Boolean.Parse(reader[AnalyticMap.Names.analyticsActive].ToString())
                    )));
            }

            return list;
        }
        #endregion

        #region Load Identity...
        public void LoadIdentityMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadIdentityMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;

        }

        public Entity.Analytic LoadIdentityMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            Entity.Analytic analytic = null;
            //Record set...
            if (reader.Read()) {
                analytic = new Analytic (
                        Int32.Parse(reader[AnalyticMap.Names.analyticsId].ToString()),
                        Int32.Parse(reader[AnalyticMap.Names.analyticsSearchGroupId].ToString()),
                        reader[AnalyticMap.Names.analyticsSearchGroupKey].ToString(),
                        new AnalyticIdentity(
                            reader[AnalyticMap.Names.analyticsName].ToString(),
                            reader[AnalyticMap.Names.analyticsDescription].ToString(),
                            reader[AnalyticMap.Names.analyticsNotes].ToString(),
                            reader[AnalyticMap.Names.refreshedText].ToString(),
                            reader[AnalyticMap.Names.createdText].ToString(),
                            reader[AnalyticMap.Names.editedText].ToString(),
                            DateTime.Parse(reader[AnalyticMap.Names.refreshed].ToString()),
                            DateTime.Parse(reader[AnalyticMap.Names.created].ToString()),
                            DateTime.Parse(reader[AnalyticMap.Names.edited].ToString()),
                            reader[AnalyticMap.Names.authorText].ToString(),
                            reader[AnalyticMap.Names.editorText].ToString(),
                            reader[AnalyticMap.Names.ownerText].ToString(),
                            Boolean.Parse(reader[AnalyticMap.Names.analyticsShared].ToString()),
                            Boolean.Parse(reader[AnalyticMap.Names.analyticsActive].ToString())
                    ));
            }

            return analytic;
        }
        #endregion

        #region Save Identity...
        public void SaveIdentityMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;
            String commandMessage = AnalyticMap.Names.saveIdentityMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.active, SqlDbType.Bit, 0, ParameterDirection.Input, session.Data.Identity.Active.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.shared, SqlDbType.Bit, 0, ParameterDirection.Input, session.Data.Identity.Shared.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.searchId, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.SearchGroupId.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.name, SqlDbType.VarChar, 105, ParameterDirection.Input, session.Data.Identity.Name),
                new SqlServiceParameter(AnalyticMap.Names.description, SqlDbType.VarChar, 255, ParameterDirection.Input, session.Data.Identity.Description),
                new SqlServiceParameter(AnalyticMap.Names.notes, SqlDbType.VarChar, 2000, ParameterDirection.Input, session.Data.Identity.Notes),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List= parameters;
        }

        //OBSOLETE...
        //public Entity.Analytic SaveIdentityMapData(System.Data.DataTable data) {

        //    //Map the entity data...
        //    System.Data.DataTableReader reader = data.CreateDataReader();
        //    Entity.Analytic analytic = null;
        //    //Single record...
        //    if (reader.Read()) {
        //        analytic = new Analytic(Int32.Parse(reader[AnalyticMap.Names.id].ToString()));
        //    }

        //    return analytic;
        //}
        #endregion

        #region Load Filters...
        public void LoadFiltersMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadFilterMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;

        }

        public Entity.Analytic LoadFiltersMapData(System.Data.DataTable data) {
            Entity.Analytic analytic = null;
            List<Entity.FilterGroup> filterGroupList = null;
            List<Entity.Filter> filterLists = null;

            var queryIdentity = data.AsEnumerable()
                .Select(identity => new {
                    Id = identity.Field<int>(AnalyticMap.Names.analyticsId),
                }).Distinct();

            foreach (var identity in queryIdentity) {
                filterGroupList = new List<FilterGroup>();
                var queryFilterGroups = data.AsEnumerable()
                    .Where(filterGroups => filterGroups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(filterGroups => new {
                        Key = filterGroups.Field<int>(AnalyticMap.Names.filterType),
                        Sort = filterGroups.Field<short>(AnalyticMap.Names.filterTypeSort),
                        Name = filterGroups.Field<string>(AnalyticMap.Names.filterTypeText),
                    }).Distinct();
                foreach (var filterGroup in queryFilterGroups) {
                    filterLists = new List<Filter>();
                    filterGroupList.Add(new FilterGroup(filterGroup.Sort, filterGroup.Name, filterLists));
                    var queryFilterLists = data.AsEnumerable()
                        .Where(fl =>
                            fl.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            fl.Field<int>(AnalyticMap.Names.filterType) == filterGroup.Key
                        ).Select(fl => new {
                            Id = fl.Field<int>(AnalyticMap.Names.filterId),
                            Key = fl.Field<int>(AnalyticMap.Names.filterKey),
                            Code = fl.Field<string>(AnalyticMap.Names.filterCode),
                            Name = fl.Field<string>(AnalyticMap.Names.filterName),
                            Sort = fl.Field<short>(AnalyticMap.Names.filterSort),
                            Selected = fl.Field<bool>(AnalyticMap.Names.filterSelected)
                        }).Distinct();
                    foreach (var filterList in queryFilterLists) {
                        filterLists.Add(new Filter(filterList.Id, filterList.Key, filterList.Code, filterList.Name, filterList.Selected, filterList.Sort));
                    }
                }
                analytic = new Entity.Analytic(identity.Id, filterGroupList);
            }

            return analytic;
        }
        #endregion

        #region Save Filters...
        public void SaveFiltersMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;
            String commandMessage = AnalyticMap.Names.saveFiltersMessage;

            //Build comma delimited key list...
            const System.Char delimiter = ',';
            System.Text.StringBuilder filterKeys = new System.Text.StringBuilder();
            foreach (Entity.FilterGroup filterGroup in session.Data.FilterGroups) { 
                foreach (Entity.Filter filter in filterGroup.Filters) {
                    if (!filter.IsSelected) { filterKeys.Append(filter.Key.ToString() + delimiter); }
                }
            }
            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.filters, SqlDbType.VarChar, 4000, ParameterDirection.Input, filterKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Load Drivers...
        public void LoadDriversMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadDriversMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }

        public Entity.Analytic LoadDriversMapData(System.Data.DataSet dataSet) {
            Entity.Analytic analytic = null;
            List<Entity.AnalyticValueDriver> driverList = null;
            List<Entity.AnalyticValueDriverMode> driverModeList = null;
            List<Entity.ValueDriverGroup> driverGroupList = null;
            List<Entity.AnalyticResultValueDriverGroup> driverResultList = null;

            var queryIdentity = dataSet.Tables[AnalyticMap.Names.loadDriversData].AsEnumerable()
                .Select(identity => new {
                    Id = identity.Field<int>(AnalyticMap.Names.analyticsId),
                }).Distinct();

            foreach (var identity in queryIdentity) {
                driverList = new List<AnalyticValueDriver>();
                var queryDrivers = dataSet.Tables[AnalyticMap.Names.loadDriversData].AsEnumerable()
                    .Where(drivers => drivers.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(drivers => new {
                        Id = drivers.Field<int>(AnalyticMap.Names.driverId),
                        Key = drivers.Field<int>(AnalyticMap.Names.driverType),
                        Selected = drivers.Field<bool>(AnalyticMap.Names.driverSelected),
                        Name = drivers.Field<string>(AnalyticMap.Names.driverTypeName),
                        Title = drivers.Field<string>(AnalyticMap.Names.driverTypeText),
                        Sort = drivers.Field<short>(AnalyticMap.Names.driverSort),
                    }).Distinct();
                foreach (var driver in queryDrivers) {
                    driverModeList = new List<AnalyticValueDriverMode>();
                    driverResultList = new List<AnalyticResultValueDriverGroup>();
                    driverList.Add(new Entity.AnalyticValueDriver(driver.Id, driver.Key, driver.Selected, driver.Name, driver.Title, driver.Sort, driverResultList, driverModeList));
                    #region Load Driver modes & groups...
                    var queryModes = dataSet.Tables[AnalyticMap.Names.loadDriversData].AsEnumerable()
                        .Where(modes =>
                            modes.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            modes.Field<int>(AnalyticMap.Names.driverType) == driver.Key
                        ).Select(modes => new {
                            Key = modes.Field<int>(AnalyticMap.Names.driverMode),
                            Selected = modes.Field<bool>(AnalyticMap.Names.driverModeSelected),
                            Name = modes.Field<string>(AnalyticMap.Names.driverModeName),
                            Title = modes.Field<string>(AnalyticMap.Names.driverModeText),
                            Sort = modes.Field<short>(AnalyticMap.Names.driverModeSort)
                        }).Distinct();
                    foreach (var mode in queryModes) {
                        driverGroupList = new List<Entity.ValueDriverGroup>();
                        driverModeList.Add(new Entity.AnalyticValueDriverMode(mode.Key, mode.Selected, mode.Name, mode.Title, mode.Sort, driverGroupList));
                        //Mode Groups...
                        var queryGroups = dataSet.Tables[AnalyticMap.Names.loadDriversData].AsEnumerable()
                            .Where(groups =>
                                groups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                                groups.Field<int>(AnalyticMap.Names.driverType) == driver.Key &&
                                groups.Field<int>(AnalyticMap.Names.driverMode) == mode.Key
                            ).Select(groups => new {
                                Id = groups.Field<int>(AnalyticMap.Names.driverGroupId),
                                Value = groups.Field<short>(AnalyticMap.Names.driverGroupValue),
                                Min = groups.Field<decimal>(AnalyticMap.Names.driverGroupLimitLower),
                                Max = groups.Field<decimal>(AnalyticMap.Names.driverGroupLimitUpper),
                                Sort = groups.Field<short>(AnalyticMap.Names.driverGroupSort)
                            }).Distinct();
                        foreach (var group in queryGroups) {
                            driverGroupList.Add(new Entity.ValueDriverGroup(group.Id, group.Value, group.Min, group.Max, group.Sort));
                        }
                    }
                    #endregion
                    #region Load Driver results...
                    var queryResults = dataSet.Tables[AnalyticMap.Names.loadDriversResultData].AsEnumerable()
                        .Where(results =>
                            results.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            results.Field<int>(AnalyticMap.Names.resultDriverType) == driver.Key
                        ).Select(results => new {
                            Value = results.Field<short>(AnalyticMap.Names.resultDriverGroup),
                            Min = results.Field<decimal>(AnalyticMap.Names.resultMetricMinLimit),
                            Max = results.Field<decimal>(AnalyticMap.Names.resultMetricMaxLimit),
                            SkuCount = results.Field<int>(AnalyticMap.Names.resultSkuCount),
                            Sales = results.Field<string>(AnalyticMap.Names.resultCurrentSales)
                        }).Distinct();
                    foreach (var result in queryResults) {
                        driverResultList.Add(new Entity.AnalyticResultValueDriverGroup(result.Value, result.Min, result.Max, result.SkuCount, result.Sales));
                    }
                    #endregion
                }
                analytic = new Entity.Analytic(identity.Id, driverList);
            }
            return analytic;
        }
        /* OBSOLETE...
        public List<Entity.AnalyticValueDriver> LoadDriversMapData(System.Data.DataTable data) {

            //Map the entity data...
            Boolean reading = true;
            Boolean selected = false;
            Int32 rows = data.Rows.Count;
            String driverNow = String.Empty;
            String driverLast = String.Empty;
            String modeNow = String.Empty;
            String modeLast = String.Empty;
            List<Entity.AnalyticValueDriver> listDrivers = new List<AnalyticValueDriver>();
            List<Entity.AnalyticValueDriverMode> listModes = new List<AnalyticValueDriverMode>();
            List<Entity.ValueDriverGroup> listGroups = new List<ValueDriverGroup>();
            System.Data.DataTableReader reader = data.CreateDataReader();

            //From record set...
            while (reading) {
                reading = reader.Read();
                driverNow = (reading) ? reader[AnalyticMap.Names.driverTypeName].ToString() : String.Empty;
                modeNow = (reading) ? reader[AnalyticMap.Names.driverModeName].ToString() : String.Empty;

                if (reading) {
                    listGroups.Add(
                        new ValueDriverGroup(
                            Int32.Parse(reader[AnalyticMap.Names.driverDetailId].ToString()),
                            Int16.Parse(reader[AnalyticMap.Names.driverGroupValue].ToString()),
                            Int32.Parse(reader[AnalyticMap.Names.driverGroupLimitLower].ToString()),
                            Int32.Parse(reader[AnalyticMap.Names.driverGroupLimitUpper].ToString()),
                            Int16.Parse(reader[AnalyticMap.Names.driverGroupSort].ToString())
                        ));
                    if (modeLast != modeNow) {
                        listModes.Add(
                            new Entity.AnalyticValueDriverMode(
                               Int32.Parse(reader[AnalyticMap.Names.driverMode].ToString()),
                               Boolean.Parse(reader[AnalyticMap.Names.driverModeSelected].ToString()),
                               reader[AnalyticMap.Names.driverModeName].ToString(), //Name
                               reader[AnalyticMap.Names.driverModeText].ToString(), //Tooltip
                               Int16.Parse(reader[AnalyticMap.Names.driverModeSort].ToString()),
                               new List<ValueDriverGroup>()
                            ));
                    }
                    if (driverLast != driverNow) {
                        listDrivers.Add(
                            new Entity.AnalyticValueDriver(
                                Int32.Parse(reader[AnalyticMap.Names.driverId].ToString()),
                                Int32.Parse(reader[AnalyticMap.Names.driverType].ToString()),
                                Boolean.Parse(reader[AnalyticMap.Names.driverSelected].ToString()),
                                reader[AnalyticMap.Names.driverTypeName].ToString(), //Name
                                reader[AnalyticMap.Names.driverTypeText].ToString(), //Tooltip
                                Int16.Parse(reader[AnalyticMap.Names.driverSort].ToString()),
                                new List<AnalyticResultValueDriverGroup>(),
                                new List<AnalyticValueDriverMode>()
                            ));
                    }
                }

                if (!(modeLast.Equals(String.Empty) || modeLast == modeNow)) {
                    if (modeNow.Equals(String.Empty)) {
                        listModes[listModes.Count - 1].Groups = listGroups.GetRange(0, listGroups.Count);
                    }
                    else {
                        listModes[listModes.Count - 2].Groups = listGroups.GetRange(0, listGroups.Count - 1);
                        listGroups.RemoveRange(0, listGroups.Count - 1);
                    }
                }
                if (!(driverLast.Equals(String.Empty) || driverLast == driverNow)) {
                    if (driverNow.Equals(String.Empty)) {
                        listDrivers[listDrivers.Count - 1].Modes = listModes.GetRange(0, listModes.Count);
                    }
                    else {
                        listDrivers[listDrivers.Count - 2].Modes = listModes.GetRange(0, listModes.Count - 1);
                        listModes.RemoveRange(0, listModes.Count - 1);
                    }
                }
                driverLast = driverNow;
                modeLast = modeNow;
                selected = Boolean.Parse(reader[AnalyticMap.Names.driverSelected].ToString());
            }
            return listDrivers;
        }
        */
        #endregion

        #region Save Drivers...
        public void SaveDriversMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;
            String commandMessage = AnalyticMap.Names.saveDriversMessage;

            //Build comma delimited key list - type;mode;group;min;max, ...
            const Int16 baseOne = 1;
            const System.Char splitter = ',';
            const System.Char delimiter = ';';
            System.Text.StringBuilder driverKeys = new System.Text.StringBuilder();
            foreach (Entity.AnalyticValueDriver driver in session.Data.ValueDrivers) {
                if (driver.IsSelected) {
                    foreach (Entity.AnalyticValueDriverMode mode in driver.Modes) {
                        if (mode.IsSelected) {
                            foreach (Entity.ValueDriverGroup group in mode.Groups) {
                                driverKeys.Append((driver.RunResults.CompareTo(true) + baseOne).ToString() + delimiter); //Compare bool true=0, false=-1
                                driverKeys.Append(driver.Key.ToString() + delimiter);
                                driverKeys.Append(mode.Key.ToString() + delimiter);
                                driverKeys.Append(group.Value.ToString() + delimiter);
                                driverKeys.Append(group.MinOutlier.ToString() + delimiter);
                                driverKeys.Append(group.MaxOutlier.ToString() + splitter);
                            }
                        }
                    }
                }
            }
            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.action, SqlDbType.Int, 0, ParameterDirection.Input, session.ClientCommand.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.drivers, SqlDbType.VarChar, 4000, ParameterDirection.Input, driverKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Run Drivers...
        #endregion

        #region Load Price lists...
        public void LoadPricelistsMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;
            String commandMessage = AnalyticMap.Names.loadPriceListsMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }

        public Entity.Analytic LoadPricelistsMapData(System.Data.DataTable data) {
            Entity.Analytic analytic = null;
            List<Entity.AnalyticPriceListGroup> priceGroupList = null;
            List<Entity.PriceList> priceLists = null;

            var queryIdentity = data.AsEnumerable()
                .Select(identity => new {
                    Id = identity.Field<int>(AnalyticMap.Names.analyticsId),
                }).Distinct();

            foreach (var identity in queryIdentity) {
                priceGroupList = new List<AnalyticPriceListGroup>();
                var queryPriceGroups = data.AsEnumerable()
                    .Where(priceGroups => priceGroups.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id)
                    .Select(priceGroups => new {
                        Key = priceGroups.Field<int>(AnalyticMap.Names.priceListType),
                        Name = priceGroups.Field<string>(AnalyticMap.Names.priceListTypeName),
                        Title = priceGroups.Field<string>(AnalyticMap.Names.priceListTypeText),
                        Sort = priceGroups.Field<short>(AnalyticMap.Names.priceListTypeSort)
                    }).Distinct();
                foreach (var priceGroup in queryPriceGroups) {
                    priceLists = new List<PriceList>();
                    priceGroupList.Add(new AnalyticPriceListGroup(priceGroup.Key, priceGroup.Name, priceGroup.Title, priceGroup.Sort, priceLists));
                    var queryPriceLists = data.AsEnumerable()
                        .Where(pl =>
                            pl.Field<int>(AnalyticMap.Names.analyticsId) == identity.Id &&
                            pl.Field<int>(AnalyticMap.Names.priceListType) == priceGroup.Key
                        ).Select(pl => new {
                            Id = pl.Field<int>(AnalyticMap.Names.priceListId),
                            Key = pl.Field<int>(AnalyticMap.Names.priceListKey),
                            Code = pl.Field<string>(AnalyticMap.Names.priceListCode),
                            Name = pl.Field<string>(AnalyticMap.Names.priceListName),
                            Title = pl.Field<string>(AnalyticMap.Names.priceListText),
                            Sort = pl.Field<short>(AnalyticMap.Names.priceListSort),
                            Selected = pl.Field<bool>(AnalyticMap.Names.priceListSelected)
                        }).Distinct();
                    foreach (var priceList in queryPriceLists) {
                        priceLists.Add(new PriceList(priceList.Id, priceList.Key, priceList.Code, priceList.Name, priceList.Sort, priceList.Selected));
                    }
                }
                analytic = new Entity.Analytic(identity.Id, priceGroupList);
            }

            return analytic;
        }

        /* OBSOLETE...
        public List<Entity.AnalyticPriceListGroup> LoadPricelistsMapData(System.Data.DataTable data) {

            //Map the entity data...
            Boolean reading = true;
            Int32 rows = data.Rows.Count;
            String listTypeNow = String.Empty;
            String listTypeLast = String.Empty;
            List<Entity.AnalyticPriceListGroup> priceListGroups = new List<Entity.AnalyticPriceListGroup>();
            List<Entity.PriceList> priceLists = new List<Entity.PriceList>();
            System.Data.DataTableReader reader = data.CreateDataReader();

            //From record set...
            while (reading) {
                reading = reader.Read();
                listTypeNow = (reading) ? reader[AnalyticMap.Names.priceListTypeName].ToString() : String.Empty;
                if (reading) {
                    priceLists.Add(
                        new Entity.PriceList(
                            Int32.Parse(reader[AnalyticMap.Names.priceListId].ToString()),
                            Int32.Parse(reader[AnalyticMap.Names.priceListKey].ToString()),
                            reader[AnalyticMap.Names.priceListCode].ToString(),
                            reader[AnalyticMap.Names.priceListName].ToString(),
                            Int16.Parse(reader[AnalyticMap.Names.priceListSort].ToString()),
                            Boolean.Parse(reader[AnalyticMap.Names.priceListSelected].ToString())
                        ));
                    if (listTypeLast != listTypeNow) {
                        priceListGroups.Add(
                            new Entity.AnalyticPriceListGroup(
                                Int32.Parse(reader[AnalyticMap.Names.priceListType].ToString()),
                                reader[AnalyticMap.Names.priceListTypeName].ToString(),
                                reader[AnalyticMap.Names.priceListTypeText].ToString(),
                                Int16.Parse(reader[AnalyticMap.Names.priceListTypeSort].ToString()),
                                new List<PriceList>()
                            ));
                    }
                }
                if (!(listTypeLast.Equals(String.Empty) || listTypeLast == listTypeNow)) {
                    if (listTypeNow.Equals(String.Empty)) {
                        priceListGroups[priceListGroups.Count - 1].PriceLists = priceLists.GetRange(0, priceLists.Count);
                    }
                    else {
                        priceListGroups[priceListGroups.Count - 2].PriceLists = priceLists.GetRange(0, priceLists.Count - 1);
                        priceLists.RemoveRange(0, priceLists.Count - 1);
                    }
                }
                listTypeLast = listTypeNow;
            }
            return priceListGroups;
        }
        */
        #endregion

        #region Save Price lists...
        public void SavePricelistsMapParameters(Session<Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;
            String commandMessage = AnalyticMap.Names.savePriceListsMessage;

            //Build delimited key list...
            const System.Char delimiter = ',';
            System.Text.StringBuilder priceKeys = new System.Text.StringBuilder();
            foreach (Entity.AnalyticPriceListGroup group in session.Data.PriceListGroups) {
                foreach (Entity.PriceList priceList in group.PriceLists) {
                    if (priceList.IsSelected) { priceKeys.Append(priceList.Key.ToString() + delimiter); }
                }
            }
            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.pricelists, SqlDbType.VarChar, 4000, ParameterDirection.Input, priceKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, commandMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Enumeration map...
        public static class Names
        {
            #region Commands...
            //Select commands...
            public const String selectCommand = "dbo.aplAnalyticsSelect";
            public const String loadMetaMessage = "selectAnalytic";
            public const String loadFilterMessage = "selectFilters";
            public const String loadDriversMessage = "selectDrivers";
            public const String loadPriceListsMessage = "selectPriceLists";
            public const String loadIdentityMessage = "selectIdentity";
            public const String loadIdentitiesMessage = "selectIdentities";

            //Update commands...
            public const String updateCommand = "dbo.aplAnalyticsUpdate";
            public const String saveIdentityMessage = "updateIdentity";
            public const String saveFiltersMessage = "updateFilters";
            public const String saveDriversMessage = "updateDrivers";
            public const String savePriceListsMessage = "updatePriceLists";

            //Process commands...
            public const String runProcessMarkup = "updateProcessMarkup";
            public const String runProcessMovement = "updateProcessMover";
            public const String runProcessDaysOnHand = "updateProcessDaysOnHand";
            #endregion

            #region Defaults Parameters...
            public const String id = "id";
            public const String action = "action";
            public const String active = "active";
            public const String shared = "shared";
            public const String notes = "notes";
            public const String name = "name";
            public const String description = "description";
            public const String searchId = "searchId";
            public const String filters = "filterKeys";
            public const String drivers = "driverKeys";
            public const String pricelists = "priceListKeys";
            public const String sqlSession = "session";
            public const String sqlMessage = "message";
            #endregion

            #region Identity data...
            public const String analyticsId = "analyticsId";
            public const String analyticsSearchGroupId = "analyticsSearchGroupId";
            public const String analyticsSearchGroupKey = "analyticsSearchGroupKey";
            public const String analyticsName = "analyticsName";
            public const String analyticsDescription = "analyticsDescription";
            public const String analyticsNotes = "analyticsNotes";
            public const String analyticsActive = "analyticsActive";
            public const String analyticsShared = "analyticsShared";
            public const String refreshedText = "analyticsRefreshedText";
            public const String createdText = "analyticsCreatedText";
            public const String editedText = "analyticsEditedText";
            public const String refreshed = "analyticsRefreshed";
            public const String created = "analyticsCreated";
            public const String edited = "analyticsEdited";
            public const String authorText = "analyticsAuthorText";
            public const String editorText = "analyticsEditorText";
            public const String ownerText = "analyticsOwnerText";
            #endregion

            #region Filters data...
            public const String filterId = "filterId";
            public const String filterKey = "filterKey";
            public const String filterCode = "filterCode";
            public const String filterName = "filterText";
            public const String filterType = "filterType";
            public const String filterTypeText = "filterTypeText";
            public const String filterSelected = "filterSelected";
            public const String filterSort = "filterSort";
            public const String filterTypeSort = "filterTypeSort";
            #endregion

            #region Driver, mode, group data...
            public const String driverId = "driverId";
            public const String driverDetailId = "driverDetailId";
            public const String driverGroupId = "driverGroupId";
            public const String driverGroupValue = "driverGroupValue";
            public const String driverGroupLimitLower = "driverGroupLimitLower";
            public const String driverGroupLimitUpper = "driverGroupLimitUpper";
            public const String driverType = "driverType";
            public const String driverTypeName = "driverTypeName";
            public const String driverTypeText = "driverTypeText";
            public const String driverMode = "driverMode";
            public const String driverModeName = "driverModeName";
            public const String driverModeText = "driverModeText";
            public const String driverModeSelected = "driverModeSelected";
            public const String driverMetric = "driverMetricType";
            public const String driverMetricName = "driverMetricTypeName";
            public const String driverMetricText = "driverMetricTypeText";
            public const String driverSelected = "driverSelected";
            public const String driverSort = "driverSort";
            public const String driverModeSort = "driverModeSort";
            public const String driverGroupSort = "driverGroupSort";
            #endregion

            #region Price Lists data...
            public const String priceListId = "priceListId";
            public const String priceListKey = "priceListKey";
            public const String priceListCode = "priceListCode";
            public const String priceListName = "priceListName";
            public const String priceListText = "priceListText";
            public const String priceListSort = "priceListSort";
            public const String priceListType = "priceListType";
            public const String priceListTypeName = "priceListTypeName";
            public const String priceListTypeText = "priceListTypeText";
            public const String priceListTypeSort = "priceListTypeSort";
            public const String priceListFilterKey = "priceListFilterKey";
            public const String priceListFilterCode = "priceListFilterCode";
            public const String priceListFilterText = "priceListFilterText";
            public const String priceListSelected = "priceListSelected";
            #endregion

            #region Results data...
            public const String resultDriverType = "resultDriverType";
			public const String resultDriverTypeName = "resultDriverTypeName";
            public const String resultDriverGroup = "resultDriverGroupValue";
            public const String resultMetricMinLimit = "resultMetricMinLimit";
			public const String resultMetricMaxLimit = "resultMetricMaxLimit";
			public const String resultSkuCount = "resultSkuCount";
            public const String resultCurrentSales = "resultCurrentSales";
            #endregion

            #region Data set enumeration...
            public enum LoadAnalyticDataSet { entity = 0, identity = 0, drivers = 1, pricelists = 2, filters = 3, results = 4 };
            public const Int32 loadAnaltyicIdentityData = (Int32)LoadAnalyticDataSet.identity;
            public const Int32 loadAnaltyicDriverData = (Int32)LoadAnalyticDataSet.drivers;
            public const Int32 loadAnaltyicPriceListData = (Int32)LoadAnalyticDataSet.pricelists;
            public const Int32 loadAnaltyicFilterData = (Int32)LoadAnalyticDataSet.filters;
            public const Int32 loadAnaltyicResultData = (Int32)LoadAnalyticDataSet.results;
            public enum LoadDrivers { drivers = 0, results = 1 };
            public const Int32 loadDriversData = (Int32)LoadDrivers.drivers;
            public const Int32 loadDriversResultData = (Int32)LoadDrivers.results;
            #endregion
        }
        #endregion
    }
}
