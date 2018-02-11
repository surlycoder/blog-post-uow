using System.Collections.Generic;
using BloggingApp.Data.Entities;
using BloggingApp.Models;

namespace BloggingApp.Services
{
	public interface IBlogService
	{
		List<BlogDto> GetBlogs();
		BlogDto GetBlogById(int id);
		BlogDto CreateBlog(BlogDto blog);
		List<PostDto> GetPostsForBlog(int blogId);
		PostDto GetPost(int id);
		IList<PostDto> GetPosts();
		PostDto CreatePostWithBlog(PostWithBlog postWithBlog);
	}
}