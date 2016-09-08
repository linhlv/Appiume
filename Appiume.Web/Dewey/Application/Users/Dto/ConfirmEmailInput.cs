using System.ComponentModel.DataAnnotations;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class ConfirmEmailInput
    {
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}