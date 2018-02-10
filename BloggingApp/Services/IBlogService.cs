using System.Collections.Generic;
using BloggingApp.Data.Entities;

namespace BloggingApp.Services
{
	public interface IBlogService
	{
		List<BlogDto> GetBlogs();
		BlogDto GetBlogById( int id );
		BlogDto CreateBlog( BlogDto blog );
	}
}