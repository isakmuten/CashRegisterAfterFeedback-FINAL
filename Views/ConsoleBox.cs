using CashRegister.Controllers;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CashRegister.Views {


	public class ConsoleBox {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static DBHandler model = DBHandler.GetInstance();

		public int _x;
		public int _y;
		public int _w;
		public int _h;
		public int _p;
		public List<string> _list;
		public int _pos;
		public bool _isRunning = false;
		public int[] _cursor;
		public string _input;
		public bool _next = false;

		private ConsoleBox() {

		}

		public ConsoleBox(int x, int y, int w, int h, int p, List<string> list) {
			_x = x;
			_y = y;
			_w = w;
			_h = h;
			_p = p;
			_list = list;
			_pos = _y + _h;
			_isRunning = true;
			_input = "";
		}

		public static ConsoleBox DrawReceiptBox(int pos, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;

			int x = 0;
			int y = pos + 2;
			int w = 61;
			int h = list.Count-2;
			int p = 0;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box.DrawBox();

			return box;
		}

		public static ConsoleBox DrawHeaderBox(int pos, string title, ConsoleColor colorScheme) {

			Console.ForegroundColor = (ConsoleColor)colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			List<string> list = new List<string>();
			list.Add(title);
			int x = 0;
			int y = 0;
			int w = 120 - 4;
			int h = 2;
			int p = 0;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box.DrawBox();

			return box;

		}

		public static ConsoleBox DrawHeaderBox(int pos, ConsoleColor colorScheme) {

			Console.ForegroundColor = (ConsoleColor)colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			List<string> list = new List<string>();
			list.Add(" Cash Register Application");
			int x = 0;
			int y = 0;
			int w = 120 - 4;
			int h = 2;
			int p = 0;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box.DrawBox();

			return box;

		}

		public static ConsoleBox DrawContentBox(int pos, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = (ConsoleColor)colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			int x = 64;
			int y = pos + 2;
			int w = 52;
			int h = list.Count;
			int p = 2;
			List<string> l = new List<string>();
			l.Add("");
			ConsoleBox box = new ConsoleBox(x, y, w, h, p, l);

			//ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box.DrawBox();
			return box;

		}

		public static ConsoleBox DrawMenuBox(int pos, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			// Menu box
			int x = 0;
			int y = pos + 2;
			int w = 61;
			int h = list.Count;
			int p = 1;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box._cursor = box.DrawBox();

			Console.SetCursorPosition(box._cursor[0] + 2, box._cursor[1]);
			box._input = Console.ReadLine();

			return box;

		}

		public static ConsoleBox DrawMenuBox(int pos, string title, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			List<string> newList = new List<string>();
			newList.Add(title);
			for (int i = 0; i < list.Count; i++) {
				newList.Add(list[i]);
			}

			// Menu box
			int x = 0;
			int y = pos + 2;
			int w = 61;
			int h = list.Count;
			int p = 1;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, newList);
			box._cursor = box.DrawBox();

			Console.SetCursorPosition(box._cursor[0] + 2, box._cursor[1]);
			box._input = Console.ReadLine();

			return box;

		}

		public static ConsoleBox DrawSelectionBox(int pos, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			// Menu box
			int x = 0;
			int y = pos + 2;
			int w = 61;
			int h = list.Count;
			int p = 1;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box._cursor = box.DrawBox();

			Console.SetCursorPosition(box._cursor[0] + 2, box._cursor[1]);

			box._input = Console.ReadLine();

			return box;
		}

		public static ConsoleBox DrawCampaignSelectionBox(int pos, List<string> list, ConsoleColor colorScheme) {

			Console.ForegroundColor = colorScheme;
			Console.BackgroundColor = ConsoleColor.Black;

			// Menu box
			int x = 0;
			int y = pos + 2;
			int w = 61;
			int h = list.Count;
			int p = 1;

			ConsoleBox box = new ConsoleBox(x, y, w, h, p, list);
			box._cursor = box.DrawBox();

			Console.SetCursorPosition(box._cursor[0] + 2, box._cursor[1]);

			box._input = Console.ReadLine();

			return box;
		}

		private static int GetLongestString(string[] lines) {

			int longestLine = 0;
			int currentLine = 0;
			for (int i = 0; i < lines.Length; i++) {

				currentLine = lines[i].Length;
				if (currentLine > longestLine) {
					longestLine = currentLine;
				}

			}
			return longestLine;
		}

		public int[] DrawBox() {
			return DrawBox(_x, _y, _w, _h, _p, _list);
		}

		private static int[] DrawBox(int x, int y, int width, int height, int padding, List<string> content) {

			DrawTopHorizLine(x, y, width, padding);
			int[] cursor = DrawVerticalLines(x, y, width, height, padding, content);
			DrawBottomHorizLine(x, y, width, height, padding);

			return cursor;

		}

		private static void DrawTopHorizLine(int x, int y, int width, int padding) {

			// Draw top horizontal line of the box (including top left/right corners)
			// Start with top left corner
			Console.SetCursorPosition(x, y);
			Console.Write('╔');

			// Add horizontal line
			int longestLine = width;
			int i;
			for (i = 0; i < longestLine /*+ (padding * 2)*/; i++) {

				Console.SetCursorPosition(x + 1 + i, y);  // 1 = gap between boxes
				Console.Write("═");

				int xValue = x + 1 + longestLine; //+ (padding * 2);
				if (xValue < Console.BufferWidth) {
					Console.SetCursorPosition(xValue, y);
					Console.Write('╗');
				}
			}
		}

		private static void DrawBottomHorizLine(int x, int y, int width, int height, int padding) {

			int longestLine = width;
			int i;
			// for vertical padding (bottom part)
			if (padding > 0) {

				for (i = height; i < height + padding; i++) {

					// Left vertical line part
					Console.SetCursorPosition(x, y + 1 + i);
					Console.Write('║');

					// right vertical line part
					Console.SetCursorPosition((x + longestLine + 1/* + (padding * 2)*/), y + 1 + i);
					Console.Write('║');

				}

				// Draw bottom line of the box (including bottom left/right corners)
				Console.SetCursorPosition(x, y + 1 + i);
				Console.Write('╚');

				for (int j = 0; j < longestLine /*+ (padding * 2)*/; j++) {

					Console.SetCursorPosition(x + 1 + j, y + 1 + i);
					Console.Write("═");

				}
				Console.SetCursorPosition(x + 1 + longestLine /*+ (padding * 2)*/, y + 1 + i);
				Console.Write('╝');

			} else {

				// Draw bottom line of the box (including bottom left/right corners)
				Console.SetCursorPosition(x, y + 1 + height);
				Console.Write('╚');

				for (i = 0; i < longestLine + (padding * 2); i++) {

					Console.SetCursorPosition(x + 1 + i, y + 1 + height);
					Console.Write("═");
				}

				Console.SetCursorPosition(x + 1 + longestLine + (padding * 2), y + 1 + height);
				Console.Write('╝');
			}
		}
		
		private static int[] DrawVerticalLines(int x, int y, int width, int height, int padding, List<string> content) {
			ConsoleColor currentFg = Console.ForegroundColor;
			ConsoleColor currentBg = Console.BackgroundColor;

			int[] cursor = new int[2];
			int longestLine = width;

			// Draw Vertical lines and content

			for (int i = 0; i < height; i++) {

				// for vertical padding (top part)
				int j;
				if (i == 0 && padding > 0) {

					for (j = 0; j < padding * 2; j++) {
						// Left vertical line part					
						Console.SetCursorPosition(x, y + 1 + j);
						Console.Write('║');

						// right vertical line part
						Console.SetCursorPosition((x + longestLine + 1), y + 1 + j);
						Console.Write('║');
					}
					i = j;
				}


				// Left vertical line part
				Console.SetCursorPosition(x, y + 1 + i);
				Console.Write('║');

				// Actual text
				Console.SetCursorPosition(x + 1 + padding, y + 1 + 0);

				if (i == (height - 2)) {

					for (int k = 0; k < content.Count; k++) {

						Console.SetCursorPosition(x + 1 + padding, y + 1 + k);
						if (content.ElementAt(k).IndexOf(':') != -1) {
							Console.Write(content.ElementAt(k));
							cursor[0] = content.ElementAt(k).IndexOf(':') + 7;
							cursor[1] = y + 3;
							Console.ForegroundColor = currentFg;
							Console.BackgroundColor = currentBg;
						} else if (content.ElementAt(k).IndexOf('>') != -1) {
							Console.Write(content.ElementAt(k));
							cursor[0] = 10;
							cursor[1] = y + k + 2;
							Console.ForegroundColor = currentFg;
							Console.BackgroundColor = currentBg;
						} else if (content.ElementAt(k).IndexOf("HeaderTitle ") != -1) {
							Console.ForegroundColor = ConsoleColor.White;
							Console.BackgroundColor = ConsoleColor.Black;
							string title = content.ElementAt(k).Substring(11);
							Console.Write(title);
							k++;
							Console.ForegroundColor = currentFg;
							Console.BackgroundColor = currentBg;
						} else if (content.ElementAt(k).IndexOf("MenuTitle ") != -1) {
							Console.ForegroundColor = ConsoleColor.White;
							Console.BackgroundColor = ConsoleColor.Black;
							string title = content.ElementAt(k).Substring(9);
							Console.Write(title);
							k++;
							Console.ForegroundColor = currentFg;
							Console.BackgroundColor = currentBg;
						} else {
							Console.ForegroundColor = currentFg;
							Console.Write(content.ElementAt(k) + "\n");
						}
					}
				}
				// Right vertical line part	
				Console.SetCursorPosition(x + longestLine + 1, y + 1 + i);
				Console.Write('║');
			}
			return cursor;
		}

		public static void PrintCampaignListInBox(int pos) {

			Console.ForegroundColor = ConsoleColor.White;
			List<string> l2 = ConsoleBox.GetCampaigns();
			string line = "\n";
			for (int i = 0; i < l2.Count; i++) {
				if (i == 4) {
					line += "\n";
				}
				line = l2.ElementAt(i);
				Console.SetCursorPosition(67, pos + 5 + i);
				Console.WriteLine(line);
			}
		}

		public static List<string> GetCampaigns() {
			// Read file with correct encoding (å, ä, ö ..)
			string[] lines = DBHandler.GetDBFile(Encoding.GetEncoding("UTF-8"));
			//string[] lines = DBHandler.GetDBFile(Encoding.GetEncoding("ISO-8859-1"));
			List<Campaign> fileContent = DBHandler.GetAllFromCampaignTable(lines);

			fileContent.Sort(); 
			
			List<string> list = new List<string>();
			list.Add("Campaign List\n");
			list.Add("\n");

			string headerFormat = "{0,2}{1,7}{2,9}{3,6}{4,9}{5,15}\n";
			string colFormat = "{0,2}{1,11}{2,4}{3,11}{4,11}{5,8}\n";

			string header = string.Format(headerFormat, "Id", "Name", "PId", "Start", "End", "Price");
			list.Add(header);
			list.Add("------------------------------------------------");

			for (int i = 0; i < fileContent.Count; i++) {
				string id = fileContent.ElementAt(i).id;
				string name = "" + fileContent.ElementAt(i).name;
				string productId = "" + fileContent.ElementAt(i).productId;
				string startDate = "" + fileContent.ElementAt(i).startDate;
				string endDate = fileContent.ElementAt(i).endDate;
				string newPrice = fileContent.ElementAt(i).newPrice;

				string newRow = string.Format(colFormat, id, name, productId, startDate, endDate, newPrice);
				list.Add(newRow);
			}
			return list;
		}

		public static void PrintProductListInBox(int pos) {

			Console.ForegroundColor = ConsoleColor.White;
			List<string> l2 = ConsoleBox.GetProducts();
			string line = "\n";
			for (int i = 0; i < l2.Count; i++) {
				if (i == 4) {
					line += "\n";
				}
				line = l2.ElementAt(i);
				Console.SetCursorPosition(67, pos + 5 + i);
				Console.WriteLine(line);
			}
		}

		public static List<string> GetProducts() {
			// Read file with correct encoding (å, ä, ö ..)			
			//string[] lines = DBHandler.GetDBFile(Encoding.GetEncoding("ISO-8859-1"));
			string[] lines = DBHandler.GetDBFile(Encoding.GetEncoding("UTF-8"));
			List<Product> fileContent = DBHandler.GetAllFromProductTable(lines);

			fileContent.Sort();

			List<string> list = new List<string>();
			list.Add("Product List\n");
			list.Add("\n");
			//                        id  name   price unit camp
			string headerFormat = "{0,-6}{1,-12}{2,9}{3,9}{4,12}\n";
			string colFormat = "{0,-5}{1,-10}{2,12}{3,9}{4,12}\n";

			string header = string.Format(headerFormat, "Id", "Name", "Price", "Unit", "HasCampaign");
			list.Add(header);
			list.Add("------------------------------------------------");

			for (int i = 0; i < fileContent.Count; i++) {
				string id = fileContent.ElementAt(i).id;
				string name = fileContent.ElementAt(i).name;
				string price = fileContent.ElementAt(i).price;
				double dblPrice = double.Parse(price);
				string unit = fileContent.ElementAt(i).unit;
				string hasCampaign = fileContent.ElementAt(i).hasCampaign.ToString().ToLower();

				string newRow = string.Format(colFormat, id, GetMaxLength(name), dblPrice.ToString("###.00"), unit, hasCampaign);
				list.Add(newRow);
			}

			return list;
		}

		private static string GetMaxLength(string name) {
			Encoding utf8 = Encoding.GetEncoding("UTF-8");
			byte[] utfBytes = utf8.GetBytes(name);
			name = utf8.GetString(utfBytes, 0, utfBytes.Length);

			if (name.Length > 10) {
				return name.Substring(0, 10);
			}

			return name;
		}

		public void SetSuccessMessage() {
			ColorMessage.SetSuccessMessage(_cursor);
		}

		public void SetEditSuccessMessage(string message) {
			ColorMessage.SetEditSuccessMessage(_cursor, message);
		}

		public void SetErrorMessage(string errorMessage, string infoMessage) {

			_cursor[0] = 8;
			_cursor[1] = (_h+2);
			ColorMessage.SetErrorMessage(_cursor, errorMessage, infoMessage);	
		}

		public string GetInput(ConsoleBox view) {

			_cursor[0] = 37;
			Console.SetCursorPosition(_cursor[0], _cursor[1]);
			return Console.ReadLine();
		}

		public string GetEditCampaignInput(ConsoleBox view) {

			_cursor[0] = 37;
			Console.SetCursorPosition(_cursor[0], _cursor[1]);
			return Console.ReadLine();
		}

		public void MoveNext(bool moveNext) {
				
			if (moveNext) {
				_cursor[1]++;
				Console.SetCursorPosition(_cursor[0], _cursor[1]);
			}
			//else {
			//	Console.SetCursorPosition(_cursor[0], _cursor[1]);
			//}
		}

		public string GetEditProductInput(ConsoleBox view) {
			_cursor[0] = 36;
			Console.SetCursorPosition(_cursor[0], _cursor[1]);
			return Console.ReadLine();
		}
	}
}