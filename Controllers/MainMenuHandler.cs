
using CashRegister.Controllers;
using CashRegister.Log;
using CashRegister.Views;
using CashRegister.Exception;

using System.Collections.Generic;
using System.Diagnostics;

namespace CashRegister.Controllers {

	public class MainMenuHandler : Handler {

        private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
        private static MainMenuHandler? instance;
        private static DBHandler model = DBHandler.GetInstance();
		private bool isRunning = false;

		public static MainMenuHandler GetInstance() {

            if (instance != null) {
                return instance;
            }
            instance = new MainMenuHandler();
            return instance;
        }

        public override void HandleUserInput() {
			Logger.Log(new StackTrace(), "");

			isRunning = true;

			try {

				while (isRunning) {
					string input = MainMenuView.GetInstance().DrawView()._input;

					if (input.Equals("0")) {
						Environment.Exit(0);
					} else {

						switch (input) {

							case "1":
								try {
									CustomerMenuHandler.GetInstance().HandleUserInput();
								} catch (System.Exception e) {
									throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
								}
								break;
							case "2":
								try {
									ProductAdminMenuHandler.GetInstance().HandleUserInput();
								} catch (System.Exception e) {
									throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
								}
								break;
							case "3":
								try {
									CampaignAdminMenuHandler.GetInstance().HandleUserInput();
								} catch (System.Exception e) {
									throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
								}
								break;
							case "0":
								Environment.Exit(0);
								break;
							default:
								break;
						}
					}
				}
			} catch (System.Exception e) {
				e.ToString();
				//throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
			}
        }
    }

}
	
