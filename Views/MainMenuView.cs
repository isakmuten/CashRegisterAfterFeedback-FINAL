using CashRegister.Log;
using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace CashRegister.Views {

	public class MainMenuView : ConsoleView {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();

		private static MainMenuView? instance;

		static ConsoleBox contentBox;
		static ConsoleBox headerBox;
		static ConsoleBox view;

		private MainMenuView() {

		}

		public static MainMenuView GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new MainMenuView();
			return instance;
		}

		public override ConsoleBox DrawView() {

			Console.Clear();
			headerBox = ConsoleBox.DrawHeaderBox(0, Constants.HeaderTitle, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawMenuBox(headerBox._pos, Constants.MainMenuBoxTitle, Menus.GetMainMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}
	}
}
