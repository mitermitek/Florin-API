using Florin_API.DTOs.Requests;
using Florin_API.DTOs.Responses;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class UserMapper
{
    public static User ToEntity(this RegisterRequest request)
    {
        return new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };
    }

    public static User ToEntity(this LoginRequest request)
    {
        return new User
        {
            Email = request.Email,
            Password = request.Password
        };
    }

    public static UserResponse ToResponse(this User entity)
    {
        return new UserResponse
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };
    }
}
