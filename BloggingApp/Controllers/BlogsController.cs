﻿using BloggingApp.Data.Entities;
using BloggingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Controllers
{
	[Route( "api/[controller]" )]
	public class BlogsController : Controller
	{
		private readonly IBlogService _blogService;

		public BlogsController( IBlogService blogService )
		{
			_blogService = blogService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var blogs = _blogService.GetBlogs();

			return Ok( blogs );
		}

		[HttpGet( "{id}" )]
		public IActionResult Get( int id )
		{
			var blog = _blogService.GetBlogById( id );

			return Ok( blog );
		}

		[HttpPost]
		public IActionResult Post( [FromBody]BlogDto blogToCreate )
		{
			var blog = _blogService.CreateBlog( blogToCreate );

			return Ok( blog );
		}

		[HttpPut( "{id}" )]
		public void Put( int id, [FromBody]string value )
		{
		}

		[HttpDelete( "{id}" )]
		public void Delete( int id )
		{
		}
	}
}