using System.Diagnostics;

namespace CashRegister.Log {

	public interface ILogger {
		void Log(StackTrace trace, string message);
	}
}