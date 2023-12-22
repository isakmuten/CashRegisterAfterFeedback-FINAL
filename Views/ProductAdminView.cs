using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;


namespace CashRegister.Views {

	public class ProductAdminView : ConsoleView {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static ProductAdminView? instance;
		private bool isRunning = false;

		static ConsoleBox contentBox;
		static ConsoleBox headerBox;
		static ConsoleBox view;

		private ProductAdminView() {

		}

		public static ProductAdminView GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new ProductAdminView();
			return instance;
		}

		public override ConsoleBox DrawView() {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetProductAdminMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}

		public ConsoleBox DrawAddProductView() {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetAddProductMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}

		public ConsoleBox DrawRemoveProductView() {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetRemoveProductMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}

		public ConsoleBox DrawEditProductView() {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetEditProductMenu(), ColorScheme.GetColorScheme()[2]);

			return view;
		}

		public ConsoleBox DrawUpdateProductView(Product product) {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetUpdateProductMenu(product), ColorScheme.GetColorScheme()[2]);

			return view;
		}
	}
}