﻿using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using BloggingApp.Data.Entities;

namespace BloggingApp.Data
{
	public class PostRepository : IPostRepository
	{
		private readonly IBlogAppContext _blogAppContext;
		private readonly IMapper _mapper;

		public PostRepository(IBlogAppContext blogAppContext,
			IMapper mapper)
		{
			_blogAppContext = blogAppContext;
			_mapper = mapper;
		}

		public PostDto GetById(int id)
		{
			PostDto post = null;

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts 
										WHERE PostId = @PostId";

				var param = command.CreateParameter();
				param.ParameterName = "PostId";
				param.Value = id;
				command.Parameters.Add(param);

				using ( var reader = command.ExecuteReader() )
				{
					if ( reader.Read() )
					{
						//post = CreatePostFromReader(reader);
						post = _mapper.Map<IDataReader, PostDto>(reader);
					}
				}
			}

			return post;
		}

		public IEnumerable<PostDto> GetAll()
		{
			IList<PostDto> posts = new List<PostDto>();

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts";

				using ( var reader = command.ExecuteReader() )
				{
					while ( reader.Read() )
					{
						posts.Add(CreatePostFromReader(reader));
					}
				}
			}

			return posts;
		}

		public IEnumerable<PostDto> GetListByBlogId(int blogId)
		{
			IList<PostDto> posts = new List<PostDto>();

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts
										WHERE BlogId = @BlogId";

				var param = command.CreateParameter();
				param.ParameterName = "BlogId";
				param.Value = blogId;
				command.Parameters.Add(param);

				using ( var reader = command.ExecuteReader() )
				{
					while ( reader.Read() )
					{
						posts.Add(CreatePostFromReader(reader));
					}
				}
			}

			return posts;
		}

		public void Create(PostDto post)
		{
			//throw new Exception("Something bad happened!");

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"INSERT INTO Posts (BlogId, Content, Title)
										OUTPUT Inserted.PostId
										VALUES (@BlogId, @Content, @Title)";

				var param1 = command.CreateParameter();
				param1.ParameterName = "BlogId";
				param1.Value = post.BlogId;
				command.Parameters.Add(param1);

				var param2 = command.CreateParameter();
				param2.ParameterName = "Content";
				param2.Value = post.Content;
				command.Parameters.Add(param2);

				var param3 = command.CreateParameter();
				param3.ParameterName = "Title";
				param3.Value = post.Title;
				command.Parameters.Add(param3);

				post.Id = (int)command.ExecuteScalar();
			}
		}

		private PostDto CreatePostFromReader(IDataReader reader)
		{
			return new PostDto()
			{
				Id = reader.GetInt32(0),
				BlogId = reader.GetInt32(1),
				Content = reader.GetString(2),
				Title = reader.GetString(3)
			};
		}
	}
}
