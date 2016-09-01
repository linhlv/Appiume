using System.ComponentModel.DataAnnotations;
using Appiume.Web.Modules.EventCloud.Core.MultiTenancy;

namespace Appiume.Web.Modules.EventCloud.WebMvc.Models.Account
{
    public class RegisterTenantViewModel
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Appiume.Web.Modules.EventCloud.Core.Users.User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(Appiume.Web.Modules.EventCloud.Core.Users.User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(Appiume.Web.Modules.EventCloud.Core.Users.User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(Appiume.Web.Modules.EventCloud.Core.Users.User.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}