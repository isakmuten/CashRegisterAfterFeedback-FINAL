using System.Diagnostics;

namespace CashRegister.Log {

	public class CashRegisterLogger : ILogger {

		public static string LogfilePathtPath = "../../../CashRegisterLog_" + DateTime.Now.ToString("yyyy-MM-dd");
		public static CashRegisterLogger Logger = new CashRegisterLogger();
		string logLevel = System.Environment.GetEnvironmentVariable("CashRegisterLogLevel");

		public static CashRegisterLogger GetInstance() {

			if (Logger != null) {
				return Logger;
			}
			Logger = new CashRegisterLogger();

			return Logger;
		}

		public void Log(StackTrace trace, string message) {

			var stackTrace = new StackTrace();
			var currentMethod = stackTrace.GetFrame(1).GetMethod();
			string traceString = "";

			if (trace != null) {
				
				var callingMethod = trace.GetFrame(1).GetMethod();
				string callingPath = callingMethod.DeclaringType?.FullName + "." + callingMethod.Name;

				string[] traceArray = trace.ToString().Split("at ");
				traceString = "Trace [";
				for (int i = 0; i < traceArray.Length; i++) {
					traceString += "\t\t" + traceArray[i].Trim() + "\n";
				}
				traceString += "\t]\n";
			}

			string logEntry = "";
			if (logLevel.Equals("DEBUG")) {
				logEntry = $"{logLevel} {DateTime.Now} {currentMethod.DeclaringType?.FullName}.{currentMethod.Name} : {message}\n\t{traceString}";
			} else {
				logEntry = $"{logLevel} {DateTime.Now} {currentMethod.DeclaringType?.FullName}.{currentMethod.Name} : {message}";
			}
			File.AppendAllText(LogfilePathtPath, logEntry + "\n");
		}
	}
}

