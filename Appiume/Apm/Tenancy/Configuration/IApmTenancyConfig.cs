namespace Appiume.Apm.Tenancy.Configuration
{
    /// <summary>
    /// Configuration options for zero module.
    /// </summary>
    public interface IApmTenancyConfig
    {
        /// <summary>
        /// Gets role management config.
        /// </summary>
        IRoleManagementConfig RoleManagement { get; }

        /// <summary>
        /// Gets user management config.
        /// </summary>
        IUserManagementConfig UserManagement { get; }

        /// <summary>
        /// Gets language management config.
        /// </summary>
        ILanguageManagementConfig LanguageManagement { get; }

        /// <summary>
        /// Gets entity type config.
        /// </summary>
        IApmTenancyEntityTypes EntityTypes { get; }
    }
}