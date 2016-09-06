using System.ComponentModel.DataAnnotations;
using Appiume.Web.Dewey.Core.MultiTenancy;

namespace Appiume.Web.Dewey.WebMvc.Models.Account
{
    public class RegisterTenantViewModel
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Appiume.Web.Dewey.Core.Users.User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(Appiume.Web.Dewey.Core.Users.User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(Appiume.Web.Dewey.Core.Users.User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(Appiume.Web.Dewey.Core.Users.User.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}