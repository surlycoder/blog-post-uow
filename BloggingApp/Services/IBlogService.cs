using System.Collections.Generic;
using BloggingApp.Data.Entities;

namespace BloggingApp.Services
{
	public interface IBlogService
	{
		List<BlogDto> GetBlogs();
	}
}