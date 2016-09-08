using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class ResetPasswordInput : IInputDto
    {
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Compare("Password", ErrorMessage = "Passwords do no match!")]
        public string PasswordRepeat { get; set; }

        public string PasswordResetCode { get; set; }
    }
}