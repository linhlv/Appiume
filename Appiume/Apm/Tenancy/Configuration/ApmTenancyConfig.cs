namespace Appiume.Apm.Tenancy.Configuration
{
    internal class ApmTenancyConfig : IApmTenancyConfig
    {
        public IRoleManagementConfig RoleManagement
        {
            get { return _roleManagementConfig; }
        }
        private readonly IRoleManagementConfig _roleManagementConfig;

        public IUserManagementConfig UserManagement
        {
            get { return _userManagementConfig; }
        }
        private readonly IUserManagementConfig _userManagementConfig;

        public ILanguageManagementConfig LanguageManagement
        {
            get { return _languageManagement; }
        }
        private readonly ILanguageManagementConfig _languageManagement;

        public IApmTenancyEntityTypes EntityTypes
        {
            get { return _entityTypes; }
        }
        private readonly IApmTenancyEntityTypes _entityTypes;


        public ApmTenancyConfig(
            IRoleManagementConfig roleManagementConfig,
            IUserManagementConfig userManagementConfig,
            ILanguageManagementConfig languageManagement,
            IApmTenancyEntityTypes entityTypes)
        {
            _entityTypes = entityTypes;
            _roleManagementConfig = roleManagementConfig;
            _userManagementConfig = userManagementConfig;
            _languageManagement = languageManagement;
        }
    }
}