using Florin_API.DTOs.Auth;
using Florin_API.DTOs.User;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class UserMapper
{
    public static User ToEntity(this RegisterDTO dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password
        };
    }

    public static User ToEntity(this LoginDTO dto)
    {
        return new User
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }

    public static UserDTO ToDTO(this User entity)
    {
        return new UserDTO
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };
    }
}
