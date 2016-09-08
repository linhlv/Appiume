using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class GetUserActivitiesInput : IInputDto, ILimitedResultRequest
    {
        private const int MaxMaxResultCount = 100;

        [Range(1, int.MaxValue)]
        public long UserId { get; set; }
        
        [Range(1, MaxMaxResultCount)]
        public int MaxResultCount { get; set; }

        public int BeforeId { get; set; }

        public GetUserActivitiesInput()
        {
            BeforeId = int.MaxValue;
        }
    }
}