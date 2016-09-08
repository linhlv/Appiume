
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public static class UserDtosMapper
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<User, UserDto>()
                .ForMember(
                    user => user.ProfileImage,
                    configuration => configuration.ResolveUsing(
                        user => user.ProfileImage == null
                                    //TODO: How to implement this?
                                    ? ""
                                    : "ProfileImages/" + user.ProfileImage
                                         )
                ).ReverseMap();

            AutoMapper.Mapper.CreateMap<RegisterUserInput, User>();

            AutoMapper.Mapper.CreateMap<User, UserDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<RegisterUserInput, User>();
        }
    }
}
