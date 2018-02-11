using System.Collections.Generic;
using System.Linq;
using BloggingApp.Data;
using BloggingApp.Data.Entities;

namespace BloggingApp.Services
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IPostRepository _postRepository;

		public BlogService(IBlogRepository blogRepository, IPostRepository postRepository)
		{
			_blogRepository = blogRepository;
			_postRepository = postRepository;
		}

		public BlogDto CreateBlog(BlogDto blogToCreate)
		{
			_blogRepository.Create(blogToCreate);
			var blog = _blogRepository.GetById(blogToCreate.Id);

			return blog;
		}

		public BlogDto GetBlogById(int id)
		{
			return _blogRepository.GetById(id);
		}

		public List<BlogDto> GetBlogs()
		{
			return _blogRepository.GetAll().ToList();
		}

		public PostDto GetPost(int id)
		{
			return _postRepository.GetById(id);
		}

		public IList<PostDto> GetPosts()
		{
			return _postRepository.GetAll().ToList();
		}

		public List<PostDto> GetPostsForBlog(int blogId)
		{
			return _postRepository.GetListByBlogId(blogId).ToList();
		}
	}
}
