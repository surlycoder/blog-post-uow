using System;
using System.Collections.Generic;
using System.Data;
using BloggingApp.Data.Entities;

namespace BloggingApp.Data
{
	public class PostRepository : IPostRepository
	{
		private readonly IBlogAppContext _blogAppContext;

		public PostRepository( IBlogAppContext blogAppContext )
		{
			_blogAppContext = blogAppContext;
		}

		public PostDto GetById( int id )
		{
			PostDto post = null;

			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts 
										WHERE PostId = @PostId";

				var param = command.CreateParameter();
				param.ParameterName = "PostId";
				param.Value = id;
				command.Parameters.Add( param );

				connection.Open();

				using ( var reader = command.ExecuteReader() )
				{
					if ( reader.Read() )
					{
						post = CreatePostFromReader( reader );
					}
				}
			}

			return post;
		}

		public IEnumerable<PostDto> GetAll()
		{
			IList<PostDto> posts = new List<PostDto>();

			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts";

				connection.Open();

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

		public IEnumerable<PostDto> GetListByBlogId( int blogId )
		{
			IList<PostDto> posts = new List<PostDto>();

			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = @"SELECT PostId, BlogId, Content, Title 
										FROM Posts
										WHERE BlogId = @BlogId";

				var param = command.CreateParameter();
				param.ParameterName = "BlogId";
				param.Value = blogId;
				command.Parameters.Add( param );

				connection.Open();

				using ( var reader = command.ExecuteReader() )
				{
					while ( reader.Read() )
					{
						posts.Add( CreatePostFromReader( reader ) );
					}
				}
			}

			return posts;
		}

		public void Create( PostDto post )
		{
			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = @"INSERT INTO Posts (BlogId, Content, Title)
									  OUTPUT Inserted.PostId
									  VALUES (@BlogId, @Content, @Title)";
				var param1 = command.CreateParameter();
				param1.ParameterName = "BlogId";
				param1.Value = post.BlogId;
				command.Parameters.Add( param1 );

				var param2 = command.CreateParameter();
				param2.ParameterName = "Content";
				param2.Value = post.Content;
				command.Parameters.Add( param2 );

				var param3 = command.CreateParameter();
				param3.ParameterName = "Title";
				param3.Value = post.Title;
				command.Parameters.Add( param3 );

				connection.Open();

				post.Id = (int)command.ExecuteScalar();
			}
		}

		private PostDto CreatePostFromReader( IDataReader reader )
		{
			return new PostDto()
			{
				Id = reader.GetInt32( 0 ),
				BlogId = reader.GetInt32( 1 ),
				Content = reader.GetString( 2 ),
				Title = reader.GetString( 3 )
			};
		}
	}
}
