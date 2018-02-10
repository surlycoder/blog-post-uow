using System.Collections.Generic;
using BloggingApp.Data.Entities;

namespace BloggingApp.Data
{
	public interface IPostRepository
    {
		PostDto GetById( int id );
		IEnumerable<PostDto> GetListByBlogId( int blogId );
		void Create( PostDto blog );
	}
}
