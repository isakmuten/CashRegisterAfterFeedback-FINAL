using CashRegister.Log;
using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;

namespace CashRegister.Views {

	public class ReceiptView : ConsoleView {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();

		private static ReceiptView? instance;
		private bool isRunning = false;

		static ConsoleBox contentBox;
		static ConsoleBox headerBox;
		static ConsoleBox receiptBox;

		private ReceiptView() {

		}

		public static ReceiptView GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new ReceiptView();
			return instance;
		}

		public override ConsoleBox DrawView() {
			return receiptBox;
		}
		
		public ConsoleBox DrawView(List<string> receipt) {

			Console.Clear();
			ConsoleBox.PrintProductListInBox(0); 
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);
			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			receiptBox = ConsoleBox.DrawReceiptBox(headerBox._pos, Menus.GetReceiptBox(receipt), ConsoleColor.Black);

			return receiptBox;
		}

	}
}