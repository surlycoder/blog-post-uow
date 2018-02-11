using BloggingApp.Models;
using BloggingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Controllers
{
	[Route("api/posts")]
	public class PostsController : Controller
	{
		private readonly IBlogService _blogService;

		public PostsController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		[HttpGet()]
		public IActionResult Get()
		{
			var posts = _blogService.GetPosts();

			return Ok(posts);
		}

		[HttpGet("{id}", Name = nameof(GetById))]
		public IActionResult GetById(int id)
		{
			var blog = _blogService.GetPost(id);

			if ( blog == null )
			{
				return NotFound();
			}

			return Ok(blog);
		}

		[HttpGet("~/api/blogs/{blogId}/posts")]
		public IActionResult GetPostsForBlog(int blogId)
		{
			var blog = _blogService.GetBlogById(blogId);
			if ( blog == null )
			{
				return NotFound();
			}

			var posts = _blogService.GetPostsForBlog(blogId);

			return Ok(posts);
		}

		[HttpPost]
		public IActionResult CreatePostWithBlog([FromBody]PostWithBlog postWithBlog)
		{
			var post = _blogService.CreatePostWithBlog(postWithBlog);

			return CreatedAtRoute(nameof(GetById),
				new { id = post.Id },
				post);
		}
	}
}