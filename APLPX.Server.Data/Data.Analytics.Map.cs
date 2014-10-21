using System;
using System.Data;
using System.Collections.Generic;
using APLPX.Server.Entity;

namespace APLPX.Server.Data {

    class AnalyticMap
    {
        #region Load Identities...
        public void LoadListMapParameters(Session<NullT> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.loadIdentitiesMessage)
            }; service.sqlParameters.List = parameters;

        }

        public List<Server.Entity.Analytic> LoadListMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            List<Server.Entity.Analytic> list = new List<Analytic>(data.Rows.Count);
            //Record set...
            while (reader.Read()) {
                list.Add(
                    new Analytic (
                        Int32.Parse(reader[AnalyticMap.Names.analyticsId].ToString()),
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
                            Boolean.Parse(reader[AnalyticMap.Names.active].ToString())
                    )));
            }

            return list;
        }
        #endregion

        #region Save Identity...
        public void SaveIdentityMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.name, SqlDbType.VarChar, 105, ParameterDirection.Input, session.Data.Identity.Name),
                new SqlServiceParameter(AnalyticMap.Names.description, SqlDbType.VarChar, 255, ParameterDirection.Input, session.Data.Identity.Description),
                //TODO: Add notes to meta data
                //TODO: Add folder to meta data
                //TODO: Add shared to meta data
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.saveIdentityMessage)
            }; service.sqlParameters.List= parameters;
        }

        public Server.Entity.Analytic SaveIdentityMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            Server.Entity.Analytic analytic = new Analytic();
            //Single record...
            if (reader.Read()) {
                analytic.Id = Int32.Parse(reader[AnalyticMap.Names.id].ToString());
            }

            return analytic;
        }
        #endregion

        #region Load Filters...
        public void LoadFiltersMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.loadFilterMessage)
            }; service.sqlParameters.List = parameters;

        }
        
        public List<Entity.FilterGroup> LoadFiltersMapData(System.Data.DataTable data) {

            //Map the entity data...
            Boolean reading = true;
            Int32 rows = data.Rows.Count;
            String filterGroupNow = String.Empty;
            String filterGroupLast = String.Empty;
            List<Server.Entity.Filter> listFilters = new List<Entity.Filter>();
            List<Server.Entity.FilterGroup> listFilterGroups = new List<Entity.FilterGroup>();
            System.Data.DataTableReader reader = data.CreateDataReader();

            //From record set...
            while (reading) {
                reading = reader.Read();
                filterGroupNow = (reading) ? reader[AnalyticMap.Names.filterTypeName].ToString() : String.Empty;
                if (reading) {
                    listFilters.Add(
                        new Entity.Filter(
                            Int32.Parse(reader[AnalyticMap.Names.filterId].ToString()),
                            Int32.Parse(reader[AnalyticMap.Names.filterKey].ToString()),
                            reader[AnalyticMap.Names.filterCode].ToString(),
                            reader[AnalyticMap.Names.filterName].ToString(),
                            Boolean.Parse(reader[AnalyticMap.Names.filterIncluded].ToString())
                        ));
                    if (filterGroupLast != filterGroupNow) {
                        listFilterGroups.Add(
                            new Entity.FilterGroup(
                                reader[AnalyticMap.Names.filterTypeName].ToString(),
                                new List<Filter>()
                            ));
                    }
                }
                if (!(filterGroupLast.Equals(String.Empty) || filterGroupLast == filterGroupNow) ) {
                    if (filterGroupNow.Equals(String.Empty)) {
                        listFilterGroups[listFilterGroups.Count - 1].Filters = listFilters.GetRange(0, listFilters.Count);
                    }
                    else {
                        listFilterGroups[listFilterGroups.Count - 2].Filters = listFilters.GetRange(0, listFilters.Count - 1);
                        listFilters.RemoveRange(0, listFilters.Count - 1);
                    }
                }
                filterGroupLast = filterGroupNow;
            }

            return listFilterGroups;
        }
        #endregion

        #region Save Filters...
        public void SaveFiltersMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;

            //Build comma delimited key list...
            const System.Char delimiter = ',';
            System.Text.StringBuilder filterKeys = new System.Text.StringBuilder();
            foreach (Server.Entity.FilterGroup filterGroup in session.Data.FilterGroups) { 
                foreach (Server.Entity.Filter filter in filterGroup.Filters) {
                    if (!filter.IsSelected) { filterKeys.Append(filter.Key.ToString() + delimiter); }
                }
            }
            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.filters, SqlDbType.VarChar, 4000, ParameterDirection.Input, filterKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.saveFiltersMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Load Drivers...
        public void LoadDriversMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.loadDriversMessage)
            }; service.sqlParameters.List = parameters;

        }

        public List<Server.Entity.AnalyticDriver> LoadDriversMapData(System.Data.DataTable data) {

            //Map the entity data...
            Boolean reading = true;
            Boolean selected = false;
            Int32 rows = data.Rows.Count;
            String driverNow = String.Empty;
            String driverLast = String.Empty;
            String modeNow = String.Empty;
            String modeLast = String.Empty;
            List<Server.Entity.AnalyticDriver> listDrivers = new List<AnalyticDriver>();
            List<Server.Entity.AnalyticDriverMode> listModes = new List<AnalyticDriverMode>();
            List<Server.Entity.AnalyticDriverGroup> listGroups = new List<AnalyticDriverGroup>();
            System.Data.DataTableReader reader = data.CreateDataReader();

            //From record set...
            while (reading) {
                reading = reader.Read();
                driverNow = (reading) ? reader[AnalyticMap.Names.driverName].ToString() : String.Empty;
                modeNow = (reading) ? reader[AnalyticMap.Names.driverModeName].ToString() : String.Empty;

                if (reading) {
                    listGroups.Add(
                        new AnalyticDriverGroup(
                            Int32.Parse(reader[AnalyticMap.Names.driverGroupId].ToString()),
                            Int16.Parse(reader[AnalyticMap.Names.driverGroupValue].ToString()),
                            Decimal.Parse(reader[AnalyticMap.Names.driverGroupMinOutlier].ToString()),
                            Decimal.Parse(reader[AnalyticMap.Names.driverGroupMaxOutlier].ToString()),
                            0 //TODO: Add sort order to meta data
                        ));
                    if (modeLast != modeNow) {
                        listModes.Add(
                            new Entity.AnalyticDriverMode(
                               Int32.Parse(reader[AnalyticMap.Names.driverModeKey].ToString()),
                               reader[AnalyticMap.Names.driverModeName].ToString(), //Name
                               reader[AnalyticMap.Names.driverModeName].ToString(), //Tooltip
                               0, //TODO: add sort order to meta data
                               Boolean.Parse(reader[AnalyticMap.Names.driverModeIncluded].ToString()),
                               new List<AnalyticDriverGroup>()
                            ));
                    }
                    if (driverLast != driverNow) {
                        listDrivers.Add(
                            new Entity.AnalyticDriver(
                                Int32.Parse(reader[AnalyticMap.Names.driverId].ToString()),
                                Int32.Parse(reader[AnalyticMap.Names.driverKey].ToString()),
                                reader[AnalyticMap.Names.driverName].ToString(), //Name
                                reader[AnalyticMap.Names.driverName].ToString(), //Title
                                0, //TODO: Add sort order to meta data
                                Boolean.Parse(reader[AnalyticMap.Names.driverIncluded].ToString()),
                                new List<AnalyticDriverMode>()
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
                selected = Boolean.Parse(reader[AnalyticMap.Names.driverIncluded].ToString());
            }
            return listDrivers;
        }
        #endregion

        #region Save Drivers...
        public void SaveDriversMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;

            //Build comma delimited key list - type;mode;group;min;max, ...
            const System.Char splitter = ',';
            const System.Char delimiter = ';';
            System.Text.StringBuilder driverKeys = new System.Text.StringBuilder();
            foreach (Server.Entity.AnalyticDriver driver in session.Data.Drivers) {
                if (driver.IsSelected) {
                    foreach (Server.Entity.AnalyticDriverMode mode in driver.Modes) {
                        if (mode.IsSelected) { 
                            foreach (Server.Entity.AnalyticDriverGroup group in mode.Groups) {
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
                new SqlServiceParameter(AnalyticMap.Names.drivers, SqlDbType.VarChar, 4000, ParameterDirection.Input, driverKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.saveDriversMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Load Price lists...
        public void LoadPricelistsMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.loadPriceListsMessage)
            }; service.sqlParameters.List = parameters;

        }

        public List<Server.Entity.PriceListGroup> LoadPricelistsMapData(System.Data.DataTable data) {

            //Map the entity data...
            Boolean reading = true;
            Int32 rows = data.Rows.Count;
            String listTypeNow = String.Empty;
            String listTypeLast = String.Empty;
            List<Server.Entity.PriceListGroup> priceListGroups = new List<Entity.PriceListGroup>();
            List<Server.Entity.PriceList> priceLists = new List<Entity.PriceList>();
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
                            Boolean.Parse(reader[AnalyticMap.Names.priceListIncluded].ToString())
                        ));
                    if (listTypeLast != listTypeNow) {
                        priceListGroups.Add(
                            new Entity.PriceListGroup(
                                reader[AnalyticMap.Names.priceListTypeName].ToString(),
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
        #endregion

        #region Save Price lists...
        public void SavePricelistsMapParameters(Session<Server.Entity.Analytic> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = AnalyticMap.Names.updateCommand;

            //Build comma delimited key list...
            const System.Char delimiter = ',';
            System.Text.StringBuilder priceKeys = new System.Text.StringBuilder();
            foreach (Server.Entity.PriceListGroup group in session.Data.PriceListGroups) {
                foreach (Server.Entity.PriceList priceList in group.PriceLists) {
                    if (!priceList.IsSelected) { priceKeys.Append(priceList.Key.ToString() + delimiter); }
                }
            }
            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(AnalyticMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.pricelists, SqlDbType.VarChar, 4000, ParameterDirection.Input, priceKeys.ToString()),
                new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
                new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.savePriceListsMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Enumeration map...
        //Database names...
        public static class Names {
            //Select commands...
            public const String selectCommand = "dbo.aplAnalyticsSelect";
            public const String loadWorkflowMessage = "selectWorkflow";
            public const String loadIdentitiesMessage = "selectIdentities";
            public const String loadFilterMessage = "selectFilters";
            public const String loadDriversMessage = "selectDrivers";
            public const String loadPriceListsMessage = "selectPriceLists";

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

            //Default parameters...
            public const String id = "id";
            public const String name = "name";
            public const String description = "description";
            public const String filters = "filterKeys";
            public const String drivers = "driverKeys";
            public const String pricelists = "priceListKeys";
            public const String sqlSession = "session";
            public const String sqlMessage = "message";

            #region Fields Identity...
            public const String analyticsId = "analyticsId";
            public const String analyticsName = "analyticsName";
            public const String analyticsDescription = "analyticsDescription";
            public const String analyticsNotes = "analyticsNotes";
            public const String refreshedText = "refreshedText";
            public const String createdText = "createdText";
            public const String editedText = "editedText";
            public const String refreshed = "refreshed";
            public const String created = "created";
            public const String edited = "edited";
            public const String authorText = "authorText";
            public const String editorText = "editorText";
            public const String ownerText = "ownerText";
            public const String active = "active";
            #endregion

            #region Fields Filters...
            public const String filterId = "filterId";
            public const String filterKey = "filterKey";
            public const String filterCode = "filterCode";
            public const String filterName = "filterText";
            public const String filterIncluded = "included";
            public const String filterTypeName = "filterTypeText";
            #endregion

            #region Fields Drivers...
            public const String driverId = "driverId";
            public const String driverKey = "driverKey";
            public const String driverName = "driverText";
            public const String driverModeKey = "modeKey";
            public const String driverModeName = "modeText";
            public const String driverGroupId = "groupId";
            public const String driverGroupValue = "groupValue";
            public const String driverGroupMinOutlier = "minOutlier";
            public const String driverGroupMaxOutlier = "maxOutlier";
            public const String driverIncluded = "driverIncluded";
            public const String driverModeIncluded = "modeIncluded";
            #endregion

            #region Fields Pricelists...
            public const String priceListId = "listId";
            public const String priceListKey = "listKey";
            public const String priceListCode = "listCode";
            public const String priceListName = "listName";
            public const String priceListText = "listText";
            public const String priceListTypeId = "listTypeId";
            public const String priceListTypeName = "listTypeName";
            public const String priceListIncluded = "included";
            #endregion

            #region Fields Workflow...
            public const String workflowKey = "workflowKey";
            public const String workflowTitle = "workflowTitle";
            public const String workflowStepKey = "workflowStepKey";
            public const String workflowStepSort = "workflowStepSort";
            public const String workflowStepName = "workflowStepName";
            public const String workflowStepTitle = "workflowStepTitle";
            public const String workflowMessageTitle = "workflowMessageTitle";
            public const String workflowMessageSort = "workflowMessageSort";
            public const String workflowStepEnablePrevious = "workflowStepEnablePrevious";
            public const String workflowStepEnableNext = "workflowStepEnableNext";
            #endregion
        }
        //Database enumerations...
        public enum DataSets { entitydata = 0, workflow = 1 };
        #endregion

        #region Message map...
        //selectIdentities  - analyticsId,analyticsName,analyticsDescription,refreshedText,createdText,editedText,refreshed,created,edited,authorText,editorText,ownerText,active
        //selectDrivers     - analyticsId,driverId,driverKey,driverText,modeKey,modeText,groupId,groupValue,minOutlier,maxOutlier,driverIncluded,modeIncluded
        //selectFilters       - analyticsId,filterId,filterKey,filterCode,filterText,filterTypeId,filterTypeText,included
        //selectPriceLists  - analyticsId,listId,listKey,listCode,listName,listText,listTypeId,listTypeName,included
		//selectWorkFlow  -  workflowKey,workflowName,workflowTitle,workflowStepKey,workflowStepName,workflowStepTitle,workflowMessageTitle,workflowStepSort,workflowMessageSort,
        //                              workflowStepEnablePrevious,workflowStepEnableNext
        #endregion

        //TODO - Determine result view for validation; by workflow, validation messages, validation warnings, icons
    }

    #region OBSOLETE
    //#region Load workflow...
    //public void LoadWorkflowMapParameters(Session<Server.Entity.NullT> session, ref Server.Data.SqlService service) {

    //    //Map the command...
    //    service.SqlProcedure = AnalyticMap.Names.selectCommand;

    //    //Map the parameters...
    //    APLPX.Server.Data.SqlServiceParameter[] parameters = { 
    //        new SqlServiceParameter(AnalyticMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey),
    //        new SqlServiceParameter(AnalyticMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, AnalyticMap.Names.loadWorkflowMessage)
    //    }; service.sqlParameters.List = parameters;

    //}

    //public Server.Entity.Workflow LoadWorkflowMapData(System.Data.DataTable data, Server.Data.SqlService service) {

    //    //Map the entity data...
    //    Boolean reading = true;
    //    Boolean IsValid = true;
    //    Boolean IsActive = false;
    //    Int32 rows = data.Rows.Count;
    //    String stepNow = String.Empty;
    //    String stepLast = String.Empty;
    //    APLPX.Server.Entity.Workflow workflow = null;
    //    List<Server.Entity.Workflow.Step> listSteps = new List<Entity.Workflow.Step>();
    //    List<Server.Entity.Workflow.Error> listErrors = new List<Entity.Workflow.Error>();
    //    List<Server.Entity.Workflow.Advisor> listAdvisor = new List<Entity.Workflow.Advisor>();
    //    System.Data.DataTableReader reader = data.CreateDataReader();

    //    //From record set...
    //    while (reading) {
    //        reading = reader.Read();
    //        workflow = new Workflow { Title = reader[AnalyticMap.Names.workflowTitle].ToString(), Steps = listSteps };
    //        stepNow = (reading) ? reader[AnalyticMap.Names.workflowStepTitle].ToString() : String.Empty;
    //        if (reading) {
    //            listAdvisor.Add(new Entity.Workflow.Advisor(
    //                Int32.Parse(reader[AnalyticMap.Names.workflowMessageSort].ToString()),
    //                reader[AnalyticMap.Names.workflowMessageTitle].ToString()
    //                ));
    //            if (stepLast != stepNow) {
    //                listSteps.Add(new Entity.Workflow.Step(
    //                    Int16.Parse(reader[UserMap.Names.workflowStepSort].ToString()),
    //                    reader[AnalyticMap.Names.workflowStepName].ToString(),
    //                    reader[AnalyticMap.Names.workflowStepTitle].ToString(),
    //                    IsValid,
    //                    IsActive,
    //                    Boolean.Parse(reader[AnalyticMap.Names.workflowStepEnablePrevious].ToString()),
    //                    Boolean.Parse(reader[AnalyticMap.Names.workflowStepEnableNext].ToString()),
    //                    listErrors,
    //                    listAdvisor,
    //                    (Server.Entity.WorkflowStepType)Int32.Parse(reader[UserMap.Names.workflowStepKey].ToString())
    //                    ));
    //            }
    //        }
    //        if (!(stepLast.Equals(String.Empty) || stepLast == stepNow)) {
    //            if (stepNow.Equals(String.Empty)) {
    //                listSteps[listSteps.Count - 1].Advisors = listAdvisor.GetRange(0, listAdvisor.Count);
    //            }
    //            else {
    //                listSteps[listSteps.Count - 2].Advisors = listAdvisor.GetRange(0, listAdvisor.Count - 1);
    //                listAdvisor.RemoveRange(0, listAdvisor.Count - 1);
    //            }
    //        }
    //        stepLast = stepNow;
    //    }

    //    return workflow;
    //}
    //#endregion
    #endregion
}
