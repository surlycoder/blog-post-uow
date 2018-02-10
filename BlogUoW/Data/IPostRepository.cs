using System.Collections.Generic;

namespace BlogUoW.Data
{
	public interface IPostRepository
    {
		PostDto GetById( int id );
		IEnumerable<PostDto> GetListByBlogId( int blogId );
		void Create( PostDto blog );
	}
}
