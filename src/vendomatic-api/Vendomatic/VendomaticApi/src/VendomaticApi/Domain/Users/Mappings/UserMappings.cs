namespace VendomaticApi.Domain.Users.Mappings;

using VendomaticApi.Domain.Users.Dtos;
using VendomaticApi.Domain.Users;
using Mapster;

public sealed class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(x => x.Email, y => y.Email.Value);
        config.NewConfig<UserForCreationDto, User>()
            .TwoWays();
        config.NewConfig<UserForUpdateDto, User>()
            .TwoWays();
    }
}