using System.Data;

namespace BlogUoW.Data
{
	public interface IBlogAppContext
	{
		IDbConnection CreateConnection();
	}
}