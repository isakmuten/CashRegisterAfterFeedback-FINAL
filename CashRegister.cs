
using CashRegister.Controllers;
using CashRegister.Log;

namespace CashRegister {

    public class CashRegister {
		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();

		private static void Main(string[] args) {

			System.Environment.SetEnvironmentVariable("CashRegisterLogLevel", "DEBUG");
			System.Environment.SetEnvironmentVariable("CashRegisterColorScheme", "" + Constants.GreenGray);


			try {
			} catch (System.Exception e) {

				Console.WriteLine(e.ToString());
			}

			try {              

                // Init DB
                DBHandler dBHandler = new DBHandler();
                DBHandler.InitDB();

                // Show main menu
                MainMenuHandler.GetInstance().HandleUserInput();

            } catch (System.Exception e) {
				Console.WriteLine("In Main, Exiting : " + e);
				_ = Console.ReadLine();
            }
        }
    }
}
