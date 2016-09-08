using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Dewey.Application.Activities.Dto;
using Appiume.Web.Dewey.Application.Friendships.Dto;
using Appiume.Web.Dewey.Application.Tasks.Dtos;
using Appiume.Web.Dewey.Core.Activities;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Mapping
{
    public static class DtoMapper
    {
        public static void Map()
        {
            //TODO: Check unnecessary ReverseMaps
            AutoMapper.Mapper.CreateMap<Task, TaskDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Task, TaskWithAssignedUserDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<Friendship, FriendshipDto>().ReverseMap();

            AutoMapper.Mapper
                .CreateMap<Activity, ActivityDto>()
                .Include<CreateTaskActivity, CreateTaskActivityDto>()
                .Include<CompleteTaskActivity, CompleteTaskActivityDto>();
            AutoMapper.Mapper.CreateMap<CreateTaskActivity, CreateTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.UseValue(1));
            AutoMapper.Mapper.CreateMap<CompleteTaskActivity, CompleteTaskActivityDto>().ForMember(t => t.ActivityType, tt => tt.UseValue(2));

            AutoMapper.Mapper.CreateMap<UserFollowedActivity, UserFollowedActivityDto>();
        }
    }
}