using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    public class GetTasksByImportanceInput : IInputDto, ILimitedResultRequest
    {
        private const int MaxMaxResultCount = 100;

        [Range(1, long.MaxValue)]
        public long AssignedUserId { get; set; }

        [Range(1, MaxMaxResultCount)]
        public int MaxResultCount { get; set; }

        public GetTasksByImportanceInput()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}