using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;

namespace CashRegister.Views {

	public abstract class ConsoleView {

		public ConsoleView() {

		}

		public abstract ConsoleBox DrawView();

	}
}