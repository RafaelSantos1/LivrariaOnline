using AutoMapper;
using LivrariaOnline.Application.Api.Dto;
using LivrariaOnline.Domain.Entities;

namespace LivrariaOnline.Application.Api.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<BookDto, Book>();
        }
    }
}
