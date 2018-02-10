using System.Collections.Generic;
using System.Data;

namespace BlogUoW.Data
{
	public class BlogRepository : IBlogRepository
	{
		private readonly IBlogAppContext _blogAppContext;

		public BlogRepository( IBlogAppContext blogAppContext )
		{
			_blogAppContext = blogAppContext;
		}

		public BlogDto GetById( int id )
		{
			BlogDto blog = null;

			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = "SELECT BlogId, Url FROM Blogs WHERE BlogId = @BlogId";

				var param = command.CreateParameter();
				param.ParameterName = "BlogId";
				param.Value = id;
				command.Parameters.Add( param );

				connection.Open();

				using ( var reader = command.ExecuteReader() )
				{
					if ( reader.Read() )
					{
						blog = CreateBlogFromReader( reader );
					}
				}
			}

			return blog;
		}

		public IEnumerable<BlogDto> GetAll()
		{
			IList<BlogDto> blogs = new List<BlogDto>();

			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = "SELECT BlogId, Url FROM Blogs";

				connection.Open();

				using ( var reader = command.ExecuteReader() )
				{
					while ( reader.Read() )
					{
						blogs.Add( CreateBlogFromReader( reader ) );
					}
				}
			}

			return blogs;
		}

		public void Create( BlogDto blog )
		{
			using ( var connection = _blogAppContext.CreateConnection() )
			using ( var command = connection.CreateCommand() )
			{
				command.Connection = connection;
				command.CommandText = @"INSERT INTO Blogs (Url)
										OUTPUT Inserted.BlogId
										VALUES (@Url)";

				var param = command.CreateParameter();
				param.ParameterName = "Url";
				param.Value = blog.Url;
				command.Parameters.Add( param );

				connection.Open();

				blog.Id = (int)command.ExecuteScalar();
			}
		}

		private BlogDto CreateBlogFromReader( IDataReader reader )
		{
			return new BlogDto()
			{
				Id = reader.GetInt32( 0 ),
				Url = reader.GetString( 1 )
			};
		}
	}
}
