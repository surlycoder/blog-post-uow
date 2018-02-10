using System.Collections.Generic;
using System.Linq;
using BloggingApp.Data;
using BloggingApp.Data.Entities;

namespace BloggingApp.Services
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;

		public BlogService( IBlogRepository blogRepository )
		{
			_blogRepository = blogRepository;
		}

		public BlogDto CreateBlog( BlogDto blogToCreate )
		{
			_blogRepository.Create( blogToCreate );
			var blog = _blogRepository.GetById( blogToCreate.Id );

			return blog;
		}

		public BlogDto GetBlogById( int id )
		{
			return _blogRepository.GetById( id );
		}

		public List<BlogDto> GetBlogs()
		{
			return _blogRepository.GetAll().ToList();
		}
	}
}
