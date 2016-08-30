using System.ComponentModel.DataAnnotations;
using Appiume.Web.IoT.Core.Users;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Account
{
    public class RegisterTenantViewModel
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}