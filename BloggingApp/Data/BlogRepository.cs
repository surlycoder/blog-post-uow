using System.Collections.Generic;
using System.Data;
using BloggingApp.Data.Entities;

namespace BloggingApp.Data
{
	public class BlogRepository : IBlogRepository
	{
		private readonly IBlogAppContext _blogAppContext;

		public BlogRepository(IBlogAppContext blogAppContext)
		{
			_blogAppContext = blogAppContext;
		}

		public BlogDto GetById(int id)
		{
			BlogDto blog = null;

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"SELECT BlogId, Url 
										FROM Blogs 
										WHERE BlogId = @BlogId";

				var param = command.CreateParameter();
				param.ParameterName = "BlogId";
				param.Value = id;
				command.Parameters.Add(param);

				using ( var reader = command.ExecuteReader() )
				{
					if ( reader.Read() )
					{
						blog = CreateBlogFromReader(reader);
					}
				}
			}

			return blog;
		}

		public IEnumerable<BlogDto> GetAll()
		{
			IList<BlogDto> blogs = new List<BlogDto>();

			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"SELECT BlogId, Url
										FROM Blogs";

				using ( var reader = command.ExecuteReader() )
				{
					while ( reader.Read() )
					{
						blogs.Add(CreateBlogFromReader(reader));
					}
				}
			}

			return blogs;
		}

		public void Create(BlogDto blog)
		{
			using ( var command = _blogAppContext.CreateCommand() )
			{
				command.CommandText = @"INSERT INTO Blogs (Url)
										OUTPUT Inserted.BlogId
										VALUES (@Url)";

				var param = command.CreateParameter();
				param.ParameterName = "Url";
				param.Value = blog.Url;
				command.Parameters.Add(param);

				blog.Id = (int)command.ExecuteScalar();
			}
		}

		private BlogDto CreateBlogFromReader(IDataReader reader)
		{
			return new BlogDto()
			{
				Id = reader.GetInt32(0),
				Url = reader.GetString(1)
			};
		}
	}
}
