using Application.Dtos.BlogPostDtos;
using Application.Models;
using AutoMapper;

namespace Application.Mappers
{
    public class DtoMappers : Profile
    {
        public DtoMappers()
        {
            CreateMap<BlogPostModel, BlogPostDto>().ReverseMap();
        }
    }
}
