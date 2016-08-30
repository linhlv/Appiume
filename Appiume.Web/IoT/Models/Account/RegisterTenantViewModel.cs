using System.ComponentModel.DataAnnotations;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Models.Account
{
    public class RegisterTenantViewModel
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Appiume.Web.IoT.Core.Users.User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(Appiume.Web.IoT.Core.Users.User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(Appiume.Web.IoT.Core.Users.User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(Appiume.Web.IoT.Core.Users.User.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}