using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using APLPX.Entity;

namespace APLPX.Server.Data {

    class UserMap {

        #region Initialize...
        public void InitializeMapParameters(Session<Entity.NullT> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = UserMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //Shared client key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, UserMap.Names.initializeMessage)
            }; service.sqlParameters.List = parameters;

        }

        public Entity.Session<NullT> InitializeMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            Entity.Session<NullT> init = new Session<NullT>();
            //Single record...
            if (reader.Read()) {
                init.SessionOk = false;
                init.ClientMessage = String.Empty;
                init.ServerMessage = String.Empty;
                init.SqlKey = reader[UserMap.Names.privateKey].ToString();
                init.AppOnline = Boolean.Parse(reader[UserMap.Names.appOnline].ToString());
                init.SqlAuthorization = Boolean.Parse(reader[UserMap.Names.sqlAuthorization].ToString());
                init.WinAuthorization = Boolean.Parse(reader[UserMap.Names.winAuthorization].ToString());
            }

            if (reader != null) reader.Dispose();
            return init;
        }
        #endregion

        #region Authenticate...
        public void AuthenticateMapParameters(Session<Entity.NullT> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = UserMap.Names.selectCommand;
            String authenticateMessage = (session.SqlAuthorization) ? UserMap.Names.authenticateSqlUserMessage : ( (session.WinAuthorization) ? UserMap.Names.authenticateWinUserMessage : String.Empty );

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.login, SqlDbType.VarChar, 100, ParameterDirection.Input, session.User.Credential.Login),
                new SqlServiceParameter(UserMap.Names.password, SqlDbType.VarChar, 100, ParameterDirection.Input, session.User.Credential.OldPassword),
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //Tenant private client key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, authenticateMessage)
            }; service.sqlParameters.List = parameters;

        }

        public Entity.User AuthenticateMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            Entity.User user = null;
            //Record set...
            if (reader.Read()) {
                user = new User(
                    Int32.Parse(reader[UserMap.Names.id].ToString()),
                    reader[UserMap.Names.sqlSession].ToString(),
                    new UserRole(
                        Int32.Parse(reader[UserMap.Names.roleId].ToString()),
                        reader[UserMap.Names.roleName].ToString(),
                        reader[UserMap.Names.roleDescription].ToString()
                    ),
                    new UserIdentity(
                        reader[UserMap.Names.email].ToString(),
                        reader[UserMap.Names.userGreeting].ToString(),
                        reader[UserMap.Names.userName].ToString(),
                        reader[UserMap.Names.firstName].ToString(),
                        reader[UserMap.Names.lastName].ToString(),
                        reader[UserMap.Names.lastLoginText].ToString(),
                        reader[UserMap.Names.createdText].ToString(),
                        reader[UserMap.Names.editedText].ToString(),
                        DateTime.Parse(reader[UserMap.Names.lastLogin].ToString()),
                        DateTime.Parse(reader[UserMap.Names.created].ToString()),
                        DateTime.Parse(reader[UserMap.Names.edited].ToString()),
                        reader[UserMap.Names.editor].ToString(),
                        Boolean.Parse(reader[UserMap.Names.active].ToString())
                     ));
            }
            if (reader != null) reader.Dispose();
            return user;
        }

        public List<Entity.Module> AuthenticateMapWorkflowModules(System.Data.DataTable moduleData, DataTable searchData) {

            List<Module> moduleList = null;
            List<ModuleFeature> featureList = null;
            List<ModuleFeatureStep> stepList = null;
            List<ModuleFeatureStepAction> actionList = null;
            List<ModuleFeatureStepAdvisor> advisorList = null;
            //List<ModuleFeatureStepError> errorList = null; //TODO Dave: review and add step errors
            List<FeatureSearchGroup> searchGroupList = null;

            moduleList = new List<Module>();
            //Modules...
            var queryModules = moduleData.AsEnumerable()
                .Select(modules => new {
                    Name = modules.Field<string>(UserMap.Names.workflowModuleName),
                    Title = modules.Field<string>(UserMap.Names.workflowModuleTitle),
                    Sort = modules.Field<short>(UserMap.Names.workflowModuleSort),
                    Type = modules.Field<ModuleType>(UserMap.Names.workflowModuleType)
                }).Distinct();
            #region Load Modules...
            foreach (var module in queryModules) {
                featureList = new List<ModuleFeature>();
                moduleList.Add(new Entity.Module (module.Name, module.Title, module.Sort, module.Type, featureList));

                //Featuers...
                var queryFeatures = moduleData.AsEnumerable()
                    .Where(features => features.Field<ModuleType>(UserMap.Names.workflowModuleType) == module.Type)
                    .Select(features => new {
                        Name = features.Field<string>(UserMap.Names.workflowFeatureName),
                        Title = features.Field<string>(UserMap.Names.workflowFeatureTitle),
                        Sort = features.Field<short>(UserMap.Names.workflowFeatureSort),
                        Type = features.Field<ModuleFeatureType>(UserMap.Names.workflowFeatureType),
                        SearchType = features.Field<ModuleFeatureSearchGroupType>(UserMap.Names.workflowFeatureSearchType),
                        LandingStepType = features.Field<ModuleFeatureStepType>(UserMap.Names.workflowFeatureLandingStepType),
                        ActionStepType = features.Field<ModuleFeatureStepType>(UserMap.Names.workflowFeatureActionStepType)
                    }).Distinct();
                #region Load Features...
                foreach (var feature in queryFeatures) {
                    stepList = new List<ModuleFeatureStep>();
                    searchGroupList = new List<FeatureSearchGroup>();
                    featureList.Add(new Entity.ModuleFeature (feature.Name, feature.Title, feature.Sort, feature.Type, feature.LandingStepType, feature.ActionStepType, stepList, searchGroupList));

                    //Steps...
                    var querySteps = moduleData.AsEnumerable()
                        .Where(steps => steps.Field<ModuleFeatureType>(UserMap.Names.workflowFeatureType) == feature.Type)
                        .Select(steps => new {
                            Name = steps.Field<string>(UserMap.Names.workflowStepName),
                            Title = steps.Field<string>(UserMap.Names.workflowStepTitle),
                            Sort = steps.Field<short>(UserMap.Names.workflowStepSort),
                            Type = steps.Field<ModuleFeatureStepType>(UserMap.Names.workflowStepType)
                        }).Distinct();
                    #region Load Steps...
                    foreach (var step in querySteps) {
                        actionList = new List<ModuleFeatureStepAction>();
                        advisorList = new List<ModuleFeatureStepAdvisor>();
                        stepList.Add(new Entity.ModuleFeatureStep(step.Name, step.Title, step.Sort, step.Type, actionList, advisorList));

                        //Step actions...
                        var queryActions = moduleData.AsEnumerable()
                            .Where(actions => actions.Field<ModuleFeatureStepType>(UserMap.Names.workflowStepType) == step.Type)
                            .Select(actions => new {
                                Name = actions.Field<string>(UserMap.Names.workflowStepActionName),
                                ParentName = actions.Field<string>(UserMap.Names.workflowStepActionParentName),
                                Title = actions.Field<string>(UserMap.Names.workflowModuleTitle),
                                Sort = actions.Field<short>(UserMap.Names.workflowStepActionSort),
                                Type = actions.Field<ModuleFeatureStepActionType>(UserMap.Names.workflowStepActionType)
                            }).Distinct();
                        foreach (var action in queryActions) {
                            actionList.Add(new Entity.ModuleFeatureStepAction(action.Name, action.ParentName, action.Title, action.Sort, action.Type));
                        }

                        //Step advisors...
                        var queryAdvisors = moduleData.AsEnumerable()
                            .Where(advisors => advisors.Field<ModuleFeatureStepType>(UserMap.Names.workflowStepType) == step.Type)
                            .Select(advisors => new {
                                Sort = advisors.Field<short>(UserMap.Names.workflowStepAdvisorMessageSort),
                                Message = advisors.Field<string>(UserMap.Names.workflowStepAdvisorMessageTitle)
                            }).Distinct();
                        foreach (var advisor in queryAdvisors) {
                            advisorList.Add(new Entity.ModuleFeatureStepAdvisor(advisor.Sort, advisor.Message));
                        }
                        //Note: Step errors collection is initially NULL...
                    }
                    #endregion

                    //Search Groups...
                    var querySearchGroups = searchData.AsEnumerable()
                        .Where(searchGroups => searchGroups.Field<ModuleFeatureSearchGroupType>(UserMap.Names.workflowFeatureSearchType) == feature.SearchType)
                        .Select(searchGroups => new {
                            Name = searchGroups.Field<string>(UserMap.Names.workflowFeatureSearchName),
                            ItemCount = searchGroups.Field<short>(UserMap.Names.workflowFeatureSearchItemCount),
                            SearchId = searchGroups.Field<int>(UserMap.Names.workflowFeatureSearchId),
                            SearchGroup = searchGroups.Field<string>(UserMap.Names.workflowFeatureSearchGroup),
                            ParentName = searchGroups.Field<string>(UserMap.Names.workflowFeatureSearchParentName),
                            IsNameChanged = searchGroups.Field<bool>(UserMap.Names.workflowFeatureSearchIsNameChanged),
                            IsSearchKeyChanged = searchGroups.Field<bool>(UserMap.Names.workflowFeatureSearchIsSearchGroupChanged),
                            CanNameChange = searchGroups.Field<bool>(UserMap.Names.workflowFeatureSearchCanNameChange),
                            CanSearchKeyChange = searchGroups.Field<bool>(UserMap.Names.workflowFeatureSearchCanSearchGroupChange),
                            Sort = searchGroups.Field<short>(UserMap.Names.workflowFeatureSearchSort)
                        }).Distinct();
                    foreach (var searchGroup in querySearchGroups) {
                        searchGroupList.Add(new Entity.FeatureSearchGroup(searchGroup.Name, searchGroup.ItemCount, searchGroup.SearchId, searchGroup.SearchGroup, searchGroup.ParentName,
                            searchGroup.IsNameChanged, searchGroup.IsSearchKeyChanged, searchGroup.CanNameChange, searchGroup.CanSearchKeyChange, searchGroup.Sort));
                    }
                }
                #endregion
            }
            #endregion

            return moduleList;          
        }
        #endregion

        #region Save Password...
        public void SavePasswordMapParameters(Session<Entity.NullT> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = UserMap.Names.updateCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.login, SqlDbType.VarChar, 100, ParameterDirection.Input, session.User.Credential.Login),
                new SqlServiceParameter(UserMap.Names.password, SqlDbType.VarChar, 100, ParameterDirection.Input, session.User.Credential.NewPassword),
                new SqlServiceParameter(UserMap.Names.oldPassword, SqlDbType.VarChar, 100, ParameterDirection.Input, session.User.Credential.OldPassword),
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //logged in user session key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, UserMap.Names.savePasswordMessage)
            }; service.sqlParameters.List = parameters;
        }
        #endregion

        #region Load List...
        public void LoadListMapParameters(Session<List<Entity.User>> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = UserMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //session key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, UserMap.Names.loadIdentitiesMessage)
            }; service.sqlParameters.List = parameters;
        }

        public List<Entity.User> LoadListMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            List<Entity.User> list = new List<User>(data.Rows.Count);
            //Record set...
            while (reader.Read()) {
                list.Add(
                    new User(
                        Int32.Parse(reader[UserMap.Names.id].ToString()),
                        reader[UserMap.Names.sqlSession].ToString(),
                        new UserRole(
                            Int32.Parse(reader[UserMap.Names.roleId].ToString()),
                            reader[UserMap.Names.roleName].ToString(),
                            reader[UserMap.Names.roleDescription].ToString()
                        ),
                        new UserIdentity(
                            reader[UserMap.Names.email].ToString(),
                            reader[UserMap.Names.userGreeting].ToString(),
                            reader[UserMap.Names.userName].ToString(),
                            reader[UserMap.Names.firstName].ToString(),
                            reader[UserMap.Names.lastName].ToString(),
                            reader[UserMap.Names.lastLoginText].ToString(),
                            reader[UserMap.Names.createdText].ToString(),
                            reader[UserMap.Names.editedText].ToString(),
                            DateTime.Parse(reader[UserMap.Names.lastLogin].ToString()),
                            DateTime.Parse(reader[UserMap.Names.created].ToString()),
                            DateTime.Parse(reader[UserMap.Names.edited].ToString()),
                            reader[UserMap.Names.editor].ToString(),
                            Boolean.Parse(reader[UserMap.Names.active].ToString())
                    )));
            }

            if (reader != null) reader.Dispose();
            return list;
        }
       #endregion

        #region Load User...
        public void LoadUserMapParameters(Session<Entity.User> session, ref Server.Data.SqlService service) {

            //Map the command...
            service.SqlProcedure = UserMap.Names.selectCommand;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.User.Id.ToString()),
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //session key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, UserMap.Names.loadIdentityMessage)
            }; service.sqlParameters.List = parameters;
        }

        public Entity.User LoadUserMapData(System.Data.DataSet dataSet) {

            //Map the entity data...
            System.Data.DataTableReader reader = dataSet.Tables[(Int32)UserMap.DataSets.entitydata].CreateDataReader();
            Entity.User user = null;
            //Single record...
            if (reader.Read()) {
                List<Entity.SQLEnumeration> roleTypes = EnumerationMapData(dataSet.Tables[(Int32)UserMap.DataSets.enumeration]);
                user = new User(
                    Int32.Parse(reader[UserMap.Names.id].ToString()),
                    reader[UserMap.Names.sqlSession].ToString(),
                    new UserRole(
                        Int32.Parse(reader[UserMap.Names.roleId].ToString()),
                        reader[UserMap.Names.roleName].ToString(),
                        reader[UserMap.Names.roleDescription].ToString()
                    ),
                    new UserIdentity(
                        reader[UserMap.Names.email].ToString(),
                        reader[UserMap.Names.userGreeting].ToString(),
                        reader[UserMap.Names.userName].ToString(),
                        reader[UserMap.Names.firstName].ToString(),
                        reader[UserMap.Names.lastName].ToString(),
                        reader[UserMap.Names.lastLoginText].ToString(),
                        reader[UserMap.Names.createdText].ToString(),
                        reader[UserMap.Names.editedText].ToString(),
                        DateTime.Parse(reader[UserMap.Names.lastLogin].ToString()),
                        DateTime.Parse(reader[UserMap.Names.created].ToString()),
                        DateTime.Parse(reader[UserMap.Names.edited].ToString()),
                        reader[UserMap.Names.editor].ToString(),
                        Boolean.Parse(reader[UserMap.Names.active].ToString())
                     ),
                     new UserCredential(),
                     roleTypes
                     );
            }
            if (reader != null) reader.Dispose();
            return user;
        }
        #endregion

        #region Save User...
        public void SaveUserMapParameters(Session<Entity.User> session, ref Server.Data.SqlService service) {

            //Map the command...
            Int16 insertId = 0;
            service.SqlProcedure = UserMap.Names.updateCommand;
            String updateCommandMessage = (session.Data.Id == insertId) ? UserMap.Names.saveIdentityInsertMessage : UserMap.Names.saveIdentityUpdateMessage;

            //Map the parameters...
            APLPX.Server.Data.SqlServiceParameter[] parameters = { 
                new SqlServiceParameter(UserMap.Names.id, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Id.ToString()),
                new SqlServiceParameter(UserMap.Names.roleId, SqlDbType.Int, 0, ParameterDirection.Input, session.Data.Role.Id.ToString()),
                new SqlServiceParameter(UserMap.Names.login, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Credential.Login),
                new SqlServiceParameter(UserMap.Names.firstName, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Identity.FirstName),
                new SqlServiceParameter(UserMap.Names.lastName, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Identity.LastName),
                new SqlServiceParameter(UserMap.Names.email, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Identity.Email),
                new SqlServiceParameter(UserMap.Names.password, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Credential.NewPassword),
                new SqlServiceParameter(UserMap.Names.oldPassword, SqlDbType.VarChar, 100, ParameterDirection.Input, session.Data.Credential.OldPassword),
                new SqlServiceParameter(UserMap.Names.sqlSession, SqlDbType.VarChar, 50, ParameterDirection.Input, session.SqlKey), //logged on user session key
                new SqlServiceParameter(UserMap.Names.sqlMessage, SqlDbType.VarChar, 500, ParameterDirection.InputOutput, updateCommandMessage)
            }; service.sqlParameters.List = parameters;
        }
        
        public Entity.User SaveUserMapData(System.Data.DataTable data) {

            //Map the entity data...
            System.Data.DataTableReader reader = data.CreateDataReader();
            Entity.User user = null;
            //Single record...
            if (reader.Read()) {
                user = new User(
                    Int32.Parse(reader[UserMap.Names.id].ToString()),
                    reader[UserMap.Names.sqlSession].ToString(),
                    new UserRole(
                        Int32.Parse(reader[UserMap.Names.roleId].ToString()),
                        reader[UserMap.Names.roleName].ToString(),
                        reader[UserMap.Names.roleDescription].ToString()
                    ),
                    new UserIdentity(
                        reader[UserMap.Names.email].ToString(),
                        reader[UserMap.Names.userGreeting].ToString(),
                        reader[UserMap.Names.userName].ToString(),
                        reader[UserMap.Names.firstName].ToString(),
                        reader[UserMap.Names.lastName].ToString(),
                        reader[UserMap.Names.lastLoginText].ToString(),
                        reader[UserMap.Names.createdText].ToString(),
                        reader[UserMap.Names.editedText].ToString(),
                        DateTime.Parse(reader[UserMap.Names.lastLogin].ToString()),
                        DateTime.Parse(reader[UserMap.Names.created].ToString()),
                        DateTime.Parse(reader[UserMap.Names.edited].ToString()),
                        reader[UserMap.Names.editor].ToString(),
                        Boolean.Parse(reader[UserMap.Names.active].ToString())
                     ));
            }

            if (reader != null) reader.Dispose();
            return user;
        }
        #endregion

        #region Load Enumeration...
        public List<Entity.SQLEnumeration> EnumerationMapData(System.Data.DataTable data) {
            System.Data.DataTableReader reader = data.CreateDataReader();
            List<Entity.SQLEnumeration> list = new List<SQLEnumeration>(data.Rows.Count);

            while (reader.Read()) {
                list.Add(new SQLEnumeration(
                    Int16.Parse(reader[UserMap.Names.enumValue].ToString()),
                    reader[UserMap.Names.enumName].ToString(),
                    reader[UserMap.Names.enumDescription].ToString(),
                    Int16.Parse(reader[UserMap.Names.enumSort].ToString())
                ));
            }

            if (reader != null) reader.Dispose();
            return list;

        } 
        #endregion

        #region Entity map...
        //Data set names...
        public class Names {
            #region Select commands...
            public const String selectCommand = "dbo.aplUserSelect";
            public const String initializeMessage = "selectInitialize";
            public const String loadIdentityMessage = "selectIdentity";
            public const String loadIdentitiesMessage = "selectIdentities";
            public const String loadAuthenticatedUserMessage = "selectUser";
            public const String authenticateSqlUserMessage = "selectSqlUser";
            public const String authenticateWinUserMessage = "selectWinUser";
            #endregion

            #region Update commands...
            public const String updateCommand = "dbo.aplUserUpdate";
            public const String savePasswordMessage = "updatePassword";
            public const String saveIdentityInsertMessage = "insertIdentity";
            public const String saveIdentityUpdateMessage = "updateIdentity";
            #endregion

            #region Default parameters...
            public const String id = "id";
            public const String sqlSession = "session";
            public const String sqlMessage = "message";
            #endregion

            #region Fields Identity...
            public const String userId = "id";
            public const String login = "login";
            public const String active = "active";
            public const String password = "password";
            public const String oldPassword = "passwordold";
            public const String roleId = "role";
            public const String roleName = "roleName";
            public const String roleDescription = "roleText";
            public const String email = "email";
            public const String userName = "username";
            public const String userGreeting = "usergreeting";
            public const String firstName = "firstname";
            public const String lastName = "lastname";
            public const String lastLoginText = "lastLoginText";
            public const String createdText = "createdText";
            public const String editedText = "editedText";
            public const String lastLogin = "lastLogin";
            public const String created = "created";
            public const String edited = "edited";
            public const String editor = "lastEditor";
            public const String groupId= "groupId";
            public const String groupName = "groupName";
            public const String groupText = "groupTitle";
            public const String viewId = "viewId";
            public const String viewDefault = "viewDefault";
            public const String viewName = "viewName";
            public const String viewText = "viewTitle";
            public const String viewIsReadOnly = "viewReadonly";
            public const String viewDescription = "viewDescription";
            public const String mapId = "mapId";
            public const String entityId = "entityId";
            public const String nodeId = "nodeId";
            public const String nodeHeader = "nodeHeader";
            public const String nodeName = "nodeName";
            public const String nodeText = "nodeTitle";
            public const Int32 nodeEntityZero = 0;
            #endregion

            #region Fields Session...
            //public const String sqlKey = "sqlKey";
            public const String appOnline = "appOnline";
            public const String privateKey = "privateKey";
            public const String sqlAuthorization = "sqlAuthorization";
            public const String winAuthorization = "winAuthorization";
            public const String sharedKey = "72B9ED08-5D12-48FD-9CF7-56A3CA30E660";
            #endregion

            #region Fields Modules Features Steps Actions Advisors...
            public const String workflowModuleType = "moduleType";
            public const String workflowModuleName = "moduleName";
            public const String workflowModuleTitle = "moduleTitle";
            public const String workflowModuleSort = "moduleSort"; 
            
            public const String workflowFeatureType = "featureType";
            public const String workflowFeatureName = "featureName";
            public const String workflowFeatureTitle = "featureTitle";
            public const String workflowFeatureSort = "featureSort";
            public const String workflowFeatureActionStepType = "actionStepType";
            public const String workflowFeatureLandingStepType = "landingStepType";
                        
            public const String workflowFeatureSearchType = "searchFeatureType";
            public const String workflowFeatureSearchId = "searchGroupId";
            public const String workflowFeatureSearchGroup = "searchGroupKey";
            public const String workflowFeatureSearchName = "searchGroupName";
            public const String workflowFeatureSearchParentName = "searchGroupParentName";
            public const String workflowFeatureSearchCanNameChange = "canNameChange";
            public const String workflowFeatureSearchIsNameChanged = "isNameChanged";
            public const String workflowFeatureSearchCanSearchGroupChange = "canSearchGroupChange";
            public const String workflowFeatureSearchIsSearchGroupChanged = "isSearchGroupChanged";
            public const String workflowFeatureSearchItemCount = "itemCount";
            public const String workflowFeatureSearchSort = "sort";

            public const String workflowStepType = "stepType";
            public const String workflowStepName = "stepName";
            public const String workflowStepTitle = "stepTitle";
            public const String workflowStepSort = "stepSort";

            public const String workflowStepActionParentName = "actionParentName";
            public const String workflowStepActionName = "actionName";
            public const String workflowStepActionTitle = "actionTitle";
            public const String workflowStepActionType = "actionType";
            public const String workflowStepActionSort = "actionSort";

            public const String workflowStepAdvisorMessageName = "messageName";
            public const String workflowStepAdvisorMessageTitle = "messageTitle";
            public const String workflowStepAdvisorMessageSort = "messageSort";

            public const String workflowStepEnablePrevious = "workflowStepEnablePrevious";
            public const String workflowStepEnableNext = "workflowStepEnableNext";
            #endregion

            #region Fields Enumerations...
            public const String enumSort = "sort";
            public const String enumValue = "value";
            public const String enumName = "name";
            public const String enumDescription = "description";
            #endregion
        }

        //Data set enumerations...
        public enum DataSets { entitydata=0, workflowModules=1, enumeration=1, workflowSearchGroups=2 };
        #endregion
    }
}
