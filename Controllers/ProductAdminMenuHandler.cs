
using CashRegister.Views;
using CashRegister.Models;
using CashRegister.Log;
using System.Diagnostics;
using System.Globalization;

namespace CashRegister.Controllers {


	public class ProductAdminMenuHandler : Handler {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static ProductAdminMenuHandler? instance;
		private static DBHandler model = DBHandler.GetInstance();
		private bool isRunning = false;

		public static ProductAdminMenuHandler GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new ProductAdminMenuHandler();
			return instance;
		}

		public override void HandleUserInput() {
			Logger.Log(new StackTrace(), "");

			List<Product> regProducts = new();

			isRunning = true;

			try {
				while (isRunning) {
					ConsoleBox view = ProductAdminView.GetInstance().DrawView();
					string input = view._input;

					if (input.Equals("0")) {
						isRunning = false;
						MainMenuHandler.GetInstance().HandleUserInput();
						return;
					}
					if (input != null) {
						if (input.Split().Length == 1) {
							if (input.Length == 1) {

								switch (input) {
									case "1":
										isRunning = false;
										AddProduct();
										break;
									case "2":
										isRunning = false;
										RemoveProduct();
										break;
									case "3":
										isRunning = false;
										EditProduct();
										break;
									case "0":
										isRunning = false;
										MainMenuHandler.GetInstance().HandleUserInput();
										break;
									default:
										break;
								}
							} else {
								view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidInputStringMessage);
							}
						} else {
							view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidInputStringMessage);
						}
					} else {
						view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidInputStringMessage);
					}
				}
			} catch (System.Exception e) {

			}
		}

		private void AddProduct() {
			Logger.Log(new StackTrace(), "");

			Product product = new Product();

			isRunning = true;

			while (isRunning) {
				ConsoleBox view = ProductAdminView.GetInstance().DrawAddProductView();
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					ProductAdminMenuHandler.GetInstance().HandleUserInput();
					return;
				}

				product.id = view._input;
				if (CheckIdInput(product, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddProduct();
				}

				product.name = view.GetInput(view);
				if (CheckNameInput(product, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddProduct();
				}

				product.price = view.GetInput(view);
				if (CheckPriceInput(product, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddProduct();
				}

				product.unit = view.GetInput(view);
				if (CheckUnitInput(product, view)) {
					product.hasCampaign = "false";					
					
					model.InsertProductToDb(product);
					view.SetSuccessMessage();
					isRunning = false;
					HandleUserInput();
				} else {
					view.MoveNext(false);
					AddProduct();
				}
			}
		}

		private void EditProduct() {
			Logger.Log(new StackTrace(), "");

			Product product = new Product();
			Product newProduct = new Product();

			isRunning = true;
			ConsoleBox view = ProductAdminView.GetInstance().DrawEditProductView();

			while (isRunning) {
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					ProductAdminMenuHandler.GetInstance().HandleUserInput();
				}

				if (model.ProductExists(input)) {
					product = model.GetProductById(input);
					Logger.Log(new StackTrace(), product.ToString());
				} else {
					view.SetErrorMessage(Constants.InvalidProductIdMessage, Constants.ProductDoesNotExist);
					EditProduct();
				}


				view = ProductAdminView.GetInstance().DrawUpdateProductView(product);

				newProduct.id = view._input;

				if (newProduct.id.Equals("0")) {
					isRunning = false;
					ProductAdminMenuHandler.GetInstance().HandleUserInput();
				}

				if (newProduct.id.Length == 0) {
					newProduct.id = product.id;
					view.MoveNext(true);

				} else if (CheckIdInput(newProduct, product, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newProduct = new Product();
					EditProduct();

				}

				newProduct.name = view.GetInput(view);

				if (newProduct.name.Length == 0) {
					newProduct.name = product.name;
					view.MoveNext(true);

				} else if (CheckNameInput(newProduct, product, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newProduct = new Product();
					EditProduct();

				}

				newProduct.price = view.GetInput(view);

				if (newProduct.price.Length == 0) {
					newProduct.price = product.price;
					view.MoveNext(true);

				} else if (CheckPriceInput(newProduct, product, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newProduct = new Product();
					EditProduct();

				}

				newProduct.unit = view.GetInput(view);

				if (newProduct.unit.Length == 0) {
					newProduct.unit = product.unit;
					view.MoveNext(true);

				} else if (CheckUnitInput(newProduct, product, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newProduct = new Product();
					EditProduct();

				}

				if (newProduct.unit.Length == 0) {
					newProduct.unit = product.unit;
					product.hasCampaign = "false";

					model.UpdateProduct(product, newProduct);

					view.SetEditSuccessMessage(Constants.EditProductSuccessMessage);
					view._isRunning = false;
					
					HandleUserInput();

				} else if (CheckUnitInput(newProduct, view)) {
					model.UpdateProduct(product, newProduct);
					view.SetEditSuccessMessage(Constants.EditProductSuccessMessage);
					view._isRunning = false;
					ConsoleBox.PrintProductListInBox(0);
					HandleUserInput();
				} else {
					view.MoveNext(false);
					EditProduct();
				}
			}
		}

		private void RemoveProduct() {
			Logger.Log(new StackTrace(), "");

			Product product = new Product();

			isRunning = true;
			ConsoleBox view = ProductAdminView.GetInstance().DrawRemoveProductView();

			while (isRunning) {
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					ProductAdminMenuHandler.GetInstance().HandleUserInput();
				}

				product.id = view._input;

				if (CheckRemoveProductId(product, view)) {
					try {
						product = model.GetProductById(product.id);
						model.RemoveProduct(product);
						ColorMessage.SetProductSuccessfullyRemoved(view._cursor, product);
						view._isRunning = false;
						HandleUserInput();

					} catch (System.Exception e) {

						view.SetErrorMessage(e.Message, Constants.ProductDeleteErrorMessage);
						RemoveProduct();

					}

				} else {

				}
			}
		}


		private bool CheckEditUnitInput(Product product, ConsoleBox view) {
			if (product.unit.Equals("")) {
				return true;
			} else {
				return CheckUnitInput(product, view);
			}
		}

		private bool CheckEditPriceInput(Product product, ConsoleBox view) {
			if (product.price.Equals("")) {
				return true;
			} else {
				return CheckPriceInput(product, view);
			}
		}

		private bool CheckEditNameInput(Product product, Product update, ConsoleBox view) {
			if (update.name.Equals("")) {
				return true;
			} else {
				return CheckNameInput(product, update, view);
			}
		}

		private bool CheckEditProductIdInput(Product product, Product update, ConsoleBox view) {
			try {
				if (update.id.Equals("")) {
					return true;
				}

				int id = System.Convert.ToInt32(product.id);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (!model.ProductExists(product.id)) {
					view.SetErrorMessage(Constants.InvalidProductIdMessage, Constants.ProductDoesNotExist);
					return false;
				}
			} catch (System.Exception e) {

				view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;

		}

		private bool CheckRemoveProductId(Product product, ConsoleBox view) {
			try {

				int id = System.Convert.ToInt32(product.id);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (!model.ProductExists(product.id)) {
					view.SetErrorMessage(Constants.InvalidProductIdMessage, Constants.ProductDoesNotExist);
					return false;
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;
		}

		private bool CheckIdInput(Product newProduct, ConsoleBox view) {
			return CheckIdInput(newProduct, new Product(), view);
		}

		private bool CheckIdInput(Product newProduct, Product product, ConsoleBox view) {

			try {
				if (newProduct.id.Length > 3) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}


				if (model.GetProductById(newProduct.id) == null) {
					if (newProduct.id.Equals(product.id)) {

						view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.NameExistsMessage);
						return false;
					}
				}

				int id = System.Convert.ToInt32(newProduct.id);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (model.ProductExists(newProduct.id)) {
					view.SetErrorMessage(Constants.InvalidProductIdMessage, Constants.ProductExistsErrorMessage);
					return false;
				}
			} catch (FormatException e) {
				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;
		}
		private bool CheckUnitInput(Product newProduct, ConsoleBox view) {

			return CheckUnitInput(newProduct, new Product(), view);
		}

		private bool CheckUnitInput(Product newProduct, Product product, ConsoleBox view) {

			try {
				if (newProduct.unit != "kg" & newProduct.unit != "st") {
					view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidUnitFormatMessage);
					return false;
				}
			} catch (System.Exception e) {

				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidUnitFormatMessage);
				return false;
			}

			return true;
		}

		private bool CheckPriceInput(Product newProduct, ConsoleBox view) {

			return CheckPriceInput(newProduct, new Product(), view);
		}

		private bool CheckPriceInput(Product newProduct, Product product, ConsoleBox view) {

			try {
				if (double.TryParse(newProduct.price, NumberStyles.Number, CultureInfo.InvariantCulture, out double productPrice)) {
					if (productPrice < 0) {
						view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceRangeMessage);
						return false;
					}

					if (productPrice == 0) {
						view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceRangeMessage);
						return false;
					}

					if (newProduct.price.LastIndexOf(',') == -1) {
						view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceDecimalFormatMessage);
						return false;
					}

					int decimals = newProduct.price.LastIndexOf(',');
					if (decimals == -1 || newProduct.price.Length - decimals - 1 != 2) {
						view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceDecimalFormatMessage);
						return false;
					}
				} else {
					view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceRangeMessage);
					return false;
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidNumberFormatMessage);
				return false;
			}
			return true;
		}

		private bool CheckNameInput(Product newProduct, ConsoleBox view) {

			return CheckNameInput(newProduct, new Product(), view);
		}

		private bool CheckNameInput(Product newProduct, Product product, ConsoleBox view) {
			//////
			try {
				//Product tempProduct = model.GetProductByName(newProduct.name);

				if (model.GetProductByName(newProduct.name) == null) {

					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.NameExistsMessage);
					return false;
				}

				if (newProduct.name == "") {

					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
					return false;
				}

				if (!newProduct.name.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x))) {

					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
					return false;
				}

			} catch (System.Exception e) {

				view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
				return false;
			}
			return true;
		}
	}
}


