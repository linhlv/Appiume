using System.ComponentModel.DataAnnotations;
using Appiume.Web.IoT.Core.Users;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Account
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Not required for single-tenant applications.
        /// </summary>
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public bool IsExternalLogin { get; set; }

        public RegisterViewModel()
        {
            TenancyName = Tenant.DefaultTenantName;
        }
    }
}