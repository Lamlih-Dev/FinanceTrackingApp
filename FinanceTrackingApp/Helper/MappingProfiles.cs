using AutoMapper;
using FinanceTrackingApp.Dto;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Models.Type, TypeDto>();
            CreateMap<TypeDto, Models.Type>();
            CreateMap<Bank, BankDto>();
            CreateMap<BankDto, Bank>();
            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<BankAccountDto, BankAccount>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
