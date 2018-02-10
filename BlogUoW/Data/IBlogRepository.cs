using System.Collections.Generic;

namespace BlogUoW.Data
{
	public interface IBlogRepository
    {
		BlogDto GetById( int id );
		IEnumerable<BlogDto> GetAll();
		void Create( BlogDto blog );
    }
}
