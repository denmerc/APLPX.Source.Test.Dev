using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using APLPX.Entity;

namespace APLPX.Server.Data
{
    class PricingEverydayMap
    {

        #region Load workflow...
        #endregion

        #region Load Identities...
        #endregion

        #region Save Identity...
        #endregion

        #region Load Filters...
        #endregion

        #region Save Filters...
        #endregion

        #region Load Drivers...
        #endregion

        #region Save Drivers...
        #endregion

        #region Load Price lists...
        #endregion

        #region Save Price lists...
        #endregion

        #region Enumeration map...
        //Database names...
        public static class Names
        {
            //Select commands...
            public const String selectCommand = "dbo.aplPricingSelect";
            public const String loadWorkflowMessage = "selectWorkflow";
            public const String loadIdentitiesMessage = "selectIdentities";
            public const String loadFilterMessage = "selectFilters";
            public const String loadDriversMessage = "selectDrivers";
            public const String loadPriceListsMessage = "selectPriceLists";

            //Update commands...
            public const String updateCommand = "dbo.aplPricingUpdate";
            public const String saveIdentityMessage = "updateIdentity";
            public const String saveFiltersMessage = "updateFilters";
            public const String saveDriversMessage = "updateDrivers";
            public const String savePriceListsMessage = "updatePriceLists";

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
            public const String pricingId = "pricingId";
            public const String pricingName = "pricingName";
            public const String pricingDescription = "pricingDescription";
            public const String pricingNotes = "pricingNotes";
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
            #endregion

            #region Fields Pricelists...
            #endregion

            #region Fields Workflow...
            #endregion
        }

        //Database enumerations...
        public enum DataSets { entitydata = 0, workflow = 1 };
        #endregion

        #region Message map...
        #endregion
    }
}
