using System.Collections.Generic;
using BloggingApp.Models;

namespace BloggingApp.Services
{
	public interface IBlogService
	{
		List<Blog> GetBlogs();
		Blog GetBlogById(int id);
		Blog CreateBlog(BlogForCreate blog);
		List<Post> GetPostsForBlog(int blogId);
		Post GetPost(int id);
		IList<Post> GetPosts();
		Post CreatePostWithBlog(PostWithBlog postWithBlog);
	}
}