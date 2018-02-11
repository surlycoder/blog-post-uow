using System.Collections.Generic;
using System.Linq;
using BloggingApp.Data;
using BloggingApp.Data.Entities;
using BloggingApp.Models;

namespace BloggingApp.Services
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IPostRepository _postRepository;
		private readonly IBlogAppContext _appContext;

		public BlogService(
			IBlogAppContext appContext,
			IBlogRepository blogRepository,
			IPostRepository postRepository)
		{
			_blogRepository = blogRepository;
			_postRepository = postRepository;
			_appContext = appContext;
		}

		public BlogDto CreateBlog(BlogDto blogToCreate)
		{
			_blogRepository.Create(blogToCreate);
			var blog = _blogRepository.GetById(blogToCreate.Id);

			return blog;
		}

		public PostDto CreatePostWithBlog(PostWithBlog postWithBlog)
		{
			PostDto post = null;

			_appContext.StartSession();
			try
			{
				var blog = new BlogDto()
				{
					Url = postWithBlog.BlogUrl
				};
				_blogRepository.Create(blog);

				post = new PostDto()
				{
					BlogId = blog.Id,
					Title = postWithBlog.Title,
					Content = postWithBlog.Content
				};
				_postRepository.Create(post);

				_appContext.CompleteSession();
			} catch
			{
				_appContext.RollbackSession();
				throw;
			}

			return post;
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
