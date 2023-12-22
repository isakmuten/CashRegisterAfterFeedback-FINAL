using System.Diagnostics;

namespace CashRegister.Exception {

    public class CashRegisterException : ApplicationException {

        private static Log.CashRegisterLogger Logger = Log.CashRegisterLogger.GetInstance();

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorSource { get; set; }
        public System.Exception Exception { get; set; }

        // Error level
        public CashRegisterException(System.Exception e, int errorCode, int errorSource) {
			Exception = e;
            ErrorMessage = e.Message;
            ErrorCode = errorCode;
            ErrorSource = errorSource;
            Logger.Log(new StackTrace(), ToString());

        }

        public CashRegisterException(string message, int errorCode, int errorSource) {
			ErrorMessage = message;
            ErrorCode = errorCode;
            ErrorSource = errorSource;
            Logger.Log(new StackTrace(), ToString());

        }

        public override string ToString() {
            string message = "\n\n\tCustomException.ErrorCode: [\n" +
                "\t   ErrorCode:    " + ErrorCode + "\n" +
                "\t   ErrorSource:  " + ErrorSource + "\n" +
                "\t   ErrorMessage: " + ErrorMessage + "\n" +
                "\t   Exception:    " + base.Message + "\n\t]";

            return message;
        }
    }
}