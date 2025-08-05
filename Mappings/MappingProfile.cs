using AutoMapper;
using Florin_API.DTOs;
using Florin_API.Models;

namespace Florin_API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Authentication mappings
        CreateMap<RegisterDTO, User>()
            .ForMember(destination => destination.Password, options => options.Ignore());
        CreateMap<LoginDTO, User>()
            .ForMember(destination => destination.Password, options => options.Ignore());
        CreateMap<User, UserDTO>();

        // Category mappings
        CreateMap<Category, CategoryDTO>();
        CreateMap<CreateCategoryDTO, Category>()
            .ForMember(destination => destination.User, options => options.Ignore())
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Transactions, options => options.Ignore());
        CreateMap<UpdateCategoryDTO, Category>()
            .ForMember(destination => destination.Transactions, options => options.Ignore());

        // Transaction mappings
        CreateMap<Transaction, TransactionDTO>()
            .ForMember(destination => destination.Category, options => options.MapFrom(source => source.Category));
        CreateMap<CreateTransactionDTO, Transaction>()
            .ForMember(destination => destination.User, options => options.Ignore())
            .ForMember(destination => destination.Category, options => options.Ignore())
            .ForMember(destination => destination.Id, options => options.Ignore());
        CreateMap<UpdateTransactionDTO, Transaction>()
            .ForMember(destination => destination.User, options => options.Ignore())
            .ForMember(destination => destination.Category, options => options.Ignore());
    }
}
