using System;

namespace Appiume.Apm.Tenancy.Configuration
{
    public interface IApmTenancyEntityTypes
    {
        /// <summary>
        /// User type of the application.
        /// </summary>
        Type User { get; set; }

        /// <summary>
        /// Role type of the application.
        /// </summary>
        Type Role { get; set; }

        /// <summary>
        /// Tenant type of the application.
        /// </summary>
        Type Tenant { get; set; }
    }
}