using System.Collections.Generic;
using BloggingApp.Data.Entities;

namespace BloggingApp.Data
{
	public interface IBlogRepository
    {
		BlogDto GetById( int id );
		IEnumerable<BlogDto> GetAll();
		void Create( BlogDto blog );
    }
}
