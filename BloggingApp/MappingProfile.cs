using System.Data;
using AutoMapper;
using BloggingApp.Data.Entities;
using BloggingApp.Models;

namespace BloggingApp
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<BlogDto, Blog>();
			CreateMap<PostDto, Post>()
				.ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Content));

			CreateMap<BlogForCreate, BlogDto>();

			CreateMap<IDataRecord, BlogDto>()
				.ForMember(dest => dest.Id,
						   opt => opt.MapFrom(
						   src => src.GetInt32(src.GetOrdinal("BlogId"))))
				.ForMember(dest => dest.Url,
						   opt => opt.MapFrom(
						   src => src.GetString(src.GetOrdinal("Url"))));
			CreateMap<IDataRecord, PostDto>();
		}
	}
}
