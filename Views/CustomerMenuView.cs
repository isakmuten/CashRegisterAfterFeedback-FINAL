using CashRegister.Log;
using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;

namespace CashRegister.Views {
	
	public class CustomerMenuView : ConsoleView {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();

		private static CustomerMenuView? instance;
		private bool isRunning = false;

		static ConsoleBox contentBox;
		static ConsoleBox headerBox;
		static ConsoleBox view;

		private CustomerMenuView() {

		}

		public static CustomerMenuView GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new CustomerMenuView();
			return instance;
		}

		public override ConsoleBox DrawView() {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawMenuBox(headerBox._pos, Menus.GetCustomerMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}
	}
}