using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
		private readonly IMapper _mapper;

		public BlogService(
			IBlogAppContext appContext,
			IBlogRepository blogRepository,
			IPostRepository postRepository,
			IMapper mapper)
		{
			_blogRepository = blogRepository;
			_postRepository = postRepository;
			_appContext = appContext;
			_mapper = mapper;
		}

		public Blog CreateBlog(BlogForCreate blogToCreate)
		{
			var blogDto = _mapper.Map<BlogDto>(blogToCreate);
			_blogRepository.Create(blogDto);
			var blog = _blogRepository.GetById(blogDto.Id);

			return _mapper.Map<Blog>(blog);
		}

		public Post CreatePostWithBlog(PostWithBlog postWithBlog)
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
			}
			catch
			{
				_appContext.RollbackSession();
				throw;
			}

			return _mapper.Map<Post>(post);
		}

		public Blog GetBlogById(int id)
		{
			var blog = _blogRepository.GetById(id);
			return _mapper.Map<Blog>(blog);
		}

		public List<Blog> GetBlogs()
		{
			var blogs = _blogRepository.GetAll().ToList();
			return _mapper.Map<List<Blog>>(blogs);
		}

		public Post GetPost(int id)
		{
			var post = _postRepository.GetById(id);
			return _mapper.Map<Post>(post);
		}

		public IList<Post> GetPosts()
		{
			var posts = _postRepository.GetAll().ToList();
			return _mapper.Map<IList<Post>>(posts);
		}

		public List<Post> GetPostsForBlog(int blogId)
		{
			var posts = _postRepository.GetListByBlogId(blogId).ToList();
			return _mapper.Map<List<Post>>(posts);
		}
	}
}
