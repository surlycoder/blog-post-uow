﻿using BloggingApp.Models;
using BloggingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Controllers
{
	public class BlogsController : BaseController
	{
		private readonly IBlogService _blogService;

		public BlogsController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var blogs = _blogService.GetBlogs();

			return Ok(blogs);
		}

		[HttpGet("{id}", Name = nameof(Get))]
		public IActionResult Get(int id)
		{
			var blog = _blogService.GetBlogById(id);

			//var blog = _blogService.GetBlogPostsById(id);

			if ( blog == null )
			{
				return NotFound();
			}

			return Ok(blog);
		}

		[HttpPost]
		public IActionResult Post([FromBody]BlogForCreate blogToCreate)
		{
			var blog = _blogService.CreateBlog(blogToCreate);

			return CreatedAtRoute(nameof(Get),
				new { id = blog.Id },
				blog);
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
