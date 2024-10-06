using Application.Dtos.BlogPostDtos;
using Application.Models;
using AutoMapper;

namespace Application.Mappers
{
    public class DtoMappers : Profile
    {
        public DtoMappers()
        {
            CreateMap<BlogPostDto, BlogPostModel>().ReverseMap();
            CreateMap<CreateBlogPostDto, BlogPostModel>().ReverseMap();
            CreateMap<UpdateBlogPostDto, BlogPostModel>().ReverseMap();
        }
    }
}
