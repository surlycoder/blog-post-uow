using System.Collections.Generic;

namespace BloggingApp.Data.Entities
{
	public class BlogPostsDto
    {
		public BlogDto Blog { get; set; }
		public IList<PostDto> Posts { get; set; }
    }
}
