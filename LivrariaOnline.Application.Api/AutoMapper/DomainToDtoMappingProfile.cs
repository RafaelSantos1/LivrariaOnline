using AutoMapper;
using LivrariaOnline.Application.Api.Dto;
using LivrariaOnline.Domain.Entities;

namespace LivrariaOnline.Application.Api.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {

        public DomainToDtoMappingProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
