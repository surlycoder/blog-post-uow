using System.Collections.Generic;

namespace BloggingApp.Models
{
	public class BlogPosts
    {
		public Blog Blog { get; set; }
		public IList<Post> Posts { get; set; }
	}
}
