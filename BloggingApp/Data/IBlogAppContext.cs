using System.Data;

namespace BloggingApp.Data
{
	public interface IBlogAppContext
	{
		IDbCommand CreateCommand();
		void StartSession();
		void CompleteSession();
		void RollbackSession();
	}
}