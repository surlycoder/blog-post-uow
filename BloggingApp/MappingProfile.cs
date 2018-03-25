using AutoMapper;
using BloggingApp.Data.Entities;
using BloggingApp.Models;

namespace BloggingApp
{
	public class MappingProfile: Profile
    {
		public MappingProfile()
		{
			CreateMap<BlogDto, Blog>();
			CreateMap<PostDto, Post>()
				.ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Content));

			CreateMap<BlogForCreate, BlogDto>();
		}
    }
}
