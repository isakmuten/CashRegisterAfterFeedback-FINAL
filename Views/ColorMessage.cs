using CashRegister.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CashRegister.Views {

	public class ColorMessage {

		public static string ErrorMessage;
		public static string ExceptionErrorMessage;
		public static string PressToContinueMessage = "Press any key to continue...";


		public ColorMessage() {

		}

		public static void SetErrorMessage(string errorMessage) {
			Console.SetCursorPosition(8, 11);

			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			string message = "Error message: " + errorMessage + "\n";

			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(8, 13);

		}


		public static void SetColorID(Product product) {

			Console.SetCursorPosition(10, 8);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(">tbd<");
			Console.ForegroundColor = ConsoleColor.White;



			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write("->" + product.id);
			Console.BackgroundColor = ConsoleColor.Black;

			Console.SetCursorPosition(product.id.Length, 3);




		}
		public static void SetColorName(Product product) {
			int inputLines = 7;
			Console.SetCursorPosition(27, 4);
			int originalCursorPosition = Console.CursorLeft;

			Console.SetCursorPosition(originalCursorPosition, 4);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write(product.name);
			Console.BackgroundColor = ConsoleColor.Black;

			Console.SetCursorPosition(originalCursorPosition + product.name.Length, 4);
			Console.SetCursorPosition(0, Console.WindowTop + inputLines);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\nPlease only use characters (A-Z), (0-9).");
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void SetColorPrice(Product product) {
			int inputLines = 7;
			Console.SetCursorPosition(27, 5);
			int originalCursorPosition = Console.CursorLeft;

			Console.SetCursorPosition(originalCursorPosition, 5);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write(product.price);
			Console.BackgroundColor = ConsoleColor.Black;

			Console.SetCursorPosition(originalCursorPosition + product.price.Length, 5);
			Console.SetCursorPosition(0, Console.WindowTop + inputLines);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\nPlease use the right format (##,##)");
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void SetColorUnit(Product product) {
			int inputLines = 7;
			Console.SetCursorPosition(22, 6);
			int originalCursorPosition = Console.CursorLeft;

			Console.SetCursorPosition(originalCursorPosition, 6);
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write(product.unit);
			Console.BackgroundColor = ConsoleColor.Black;

			Console.SetCursorPosition(originalCursorPosition + product.unit.Length, 6);
			Console.SetCursorPosition(0, Console.WindowTop + inputLines);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\nPlease use the right Unit format.");
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void SetSuccessMessage(int[] cursor) {
			Console.ForegroundColor = ConsoleColor.Green;
			cursor[0] = 8;
			cursor[1] = 14;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.WriteLine("Sucessfully Added!");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			cursor[1] = cursor[1] + 1;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
		}

		public static void SetErrorMessage(int[] cursor, string errorMessage, string infoMessage) {

			Console.SetCursorPosition(cursor[0], cursor[1]);

			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.White;
			string message = "Error: " + errorMessage;
			Console.Write(message);

			cursor[0] = 8;
			cursor[1]++;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(infoMessage);

			cursor[0] = 8;
			cursor[1]++;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(Constants.PressToContinueMessage);

			Console.ReadLine();

		}

		public static void SetProductSuccessfullyRemoved(int[] cursor, Product o) {
			cursor[0] = 8;
			cursor[1] = cursor[1] + 4;

			Console.SetCursorPosition(cursor[0], cursor[1]);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Successfully removed!");

			cursor[0] = 8;
			cursor[1]++;

			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(Constants.PressToContinueMessage);

			Console.ReadLine();
		}
		public static void SetCampaignSuccessfullyRemoved(int[] cursor, Campaign o) {
			cursor[0] = 8;
			cursor[1] = cursor[1] + 4;

			Console.SetCursorPosition(cursor[0], cursor[1]);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Successfully removed!");

			cursor[0] = 8;
			cursor[1]++;

			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(Constants.PressToContinueMessage);

			Console.ReadLine();
		}

		internal static void SetEditSuccessMessage(int[] cursor, string message) {
			Console.ForegroundColor = ConsoleColor.Green;
			cursor[0] = 8;
			cursor[1] = 14;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			cursor[1] = cursor[1] + 1;
			Console.SetCursorPosition(cursor[0], cursor[1]);
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
		}
	}
}

