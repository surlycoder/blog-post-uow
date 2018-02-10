using System.Data;

namespace BlogUoW.Data
{
	public interface IBlogContext
	{
		IDbConnection CreateConnection();
	}
}