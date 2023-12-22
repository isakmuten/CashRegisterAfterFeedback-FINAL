
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Views;
using CashRegister.Exception;

using System.Collections.Generic;
using System.Diagnostics;

namespace CashRegister.Controllers {

	public class CustomerMenuHandler : Handler {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static CustomerMenuHandler? instance;
		private static DBHandler model = DBHandler.GetInstance();
		private bool isRunning = false;

		public static CustomerMenuHandler GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new CustomerMenuHandler();
			return instance;
		}

		public override void HandleUserInput() {
			Logger.Log(new StackTrace(), "");

			List<Product> regProducts = new();
			isRunning = true;

			string inputOption = "";

			try {
				while (isRunning) {
					ConsoleBox view = CustomerMenuView.GetInstance().DrawView();
					string input = view._input;

					if (input.Equals("0")) {
						isRunning = false;
						MainMenuHandler.GetInstance().HandleUserInput();
						return;
					}
					
					if (input != null) {
						if (input.Split().Length == 1) { 
							 if (input.Length == 3) { 							
								if (input.Equals("PAY")) {
									inputOption = "2";
								} else {
									view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidInputStringMessage);
									break;
								}
							} else {
								input = "";
								view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidInputStringMessage);
							}
						} else if (input.Split().Length == 2) { // check for <productId> <amount>
							inputOption = "1";
						} else {
							break;
						}				
					} else { 
						break; 
					}

					switch (inputOption) {

						case "1":
							try {
								CheckInput(input.Split(), regProducts, view);
							} catch (System.Exception e) {
								throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
							}
							break;
						case "2":
							try {
								HandlePay(regProducts, view);
							} catch (System.Exception e) {
								throw new CashRegisterException(e, Constants.MainMenuError, Constants.ViewError);
							}
							break;
						default:
							break;
					}
				}
			} catch (System.Exception e) {

			}
		}

		private void HandlePay(List<Product> regProducts, ConsoleBox view) {
			Logger.Log(new StackTrace(), "");

			if (regProducts.Count() > 0) {

				try {
					Receipt receipt = Receipt.GetInstance();
					ReceiptView.GetInstance().DrawView(receipt.CreateReceipt(regProducts));
					regProducts.Clear();	
					Console.ReadLine();
					Console.BackgroundColor = ConsoleColor.Black;

					return;
				} catch (System.Exception e) {
					view.SetErrorMessage(e.Message, Constants.ErrorInPurchaseMessage);
				}
			} else {
				view.SetErrorMessage(Constants.NoProductsInPurchase, Constants.NoProductsInPurchase);
			}
		}

		public void CheckInput(string[] values, List<Product> regProducts, ConsoleBox view) {
			Logger.Log(new StackTrace(), "");

			Product product;
			string id = values[0];
			string amount = values[1];

			try {
				if (System.Convert.ToInt16(id) > 0 && int.Parse(amount) > 0) {

					product = new(id, "" + amount);
					if (model.ProductExists(product.id)) {
						regProducts.Add(product);
					} else {
						view.SetErrorMessage("Product id: " + product.id + " does not exist.", Constants.ProductDoesNotExist);
					}
				} else {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, "[" + values[0] + " " + values[1] + "]");
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidInputStringMessage, "[" + values[0] + " " + values[1] + "]");
			}
		}
	}
}


