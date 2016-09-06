using System;
using System.ComponentModel.DataAnnotations;
using Appiume.Web.Dewey.Core.Events;

namespace Appiume.Web.Dewey.Application.Events.Dtos
{
    public class CreateEventInput
    {
        [Required]
        [StringLength(Event.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Event.MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }
    }
}