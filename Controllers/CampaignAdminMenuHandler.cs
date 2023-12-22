
using CashRegister.Views;
using CashRegister.Models;
using CashRegister.Log;
using System.Diagnostics;
using System.Text;
using System.Reflection.Metadata;

namespace CashRegister.Controllers {

	public class CampaignAdminMenuHandler : Handler {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static CampaignAdminMenuHandler? instance;
		private static DBHandler model = DBHandler.GetInstance();
		private bool isRunning = false;

		public static CampaignAdminMenuHandler GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new CampaignAdminMenuHandler();
			return instance;
		}

		public override void HandleUserInput() {
			Logger.Log(new StackTrace(), "");

			isRunning = true;

			try {
				while (isRunning) {
					ConsoleBox view = CampaignAdminView.GetInstance().DrawView();
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
										AddCampaign();
										break;
									case "2":
										RemoveCampaign();
										break;
									case "3":
										EditCampaign();
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

		private void AddCampaign() {
			Logger.Log(new StackTrace(), "");

			Campaign campaign = new Campaign();

			isRunning = true;

			while (isRunning) {
				ConsoleBox view = CampaignAdminView.GetInstance().DrawAddCampaignView();
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					CampaignAdminMenuHandler.GetInstance().HandleUserInput();
					return;
				}

				campaign.id = input;
				if (CheckIdInput(campaign, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddCampaign();
				}

				campaign.name = view.GetInput(view);
				if (CheckNameInput(campaign, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddCampaign();
				}

				campaign.productId = view.GetInput(view);
				if (CheckProductIdInput(campaign, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddCampaign();
				}

				campaign.startDate = view.GetInput(view);
				if (CheckStartDateInput(campaign, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddCampaign();
				}

				campaign.endDate = view.GetInput(view);
				if (CheckEndDateInput(campaign, view)) {
					view.MoveNext(true);

				} else {
					view.MoveNext(false);
					AddCampaign();
				}

				campaign.newPrice = view.GetInput(view);
				if (CheckNewPriceInput(campaign, view)) {
					Product product = model.GetProductById(campaign.productId);
					Product newProduct = product;
					newProduct.hasCampaign = "true";
					model.UpdateProduct(product, newProduct);

					model.InsertCampaignToDb(campaign);
					view.SetSuccessMessage();
					isRunning = false;
					HandleUserInput();
				} else {
					view.MoveNext(false);
					AddCampaign();
				}
			}
		}

		private void EditCampaign() {
			Logger.Log(new StackTrace(), "");

			Campaign campaign = new Campaign();
			Campaign newCampaign = new Campaign();

			isRunning = true;
			ConsoleBox view = CampaignAdminView.GetInstance().DrawEditCampaignView();

			while (isRunning) {
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					CampaignAdminMenuHandler.GetInstance().HandleUserInput();
				}

				if (model.CampaignExists(input)) {
					campaign = model.GetCampaignById(input);
					Logger.Log(new StackTrace(), campaign.ToString());
				} else {
					view.SetErrorMessage(Constants.InvalidCampaignIdMessage, Constants.CampaignDoesNotExist);
					EditCampaign();
				}

				view = CampaignAdminView.GetInstance().DrawUpdateCampaignView(campaign);

				newCampaign.id = view._input;

				if (newCampaign.id.Equals("0")) {
					isRunning = false;
					CampaignAdminMenuHandler.GetInstance().HandleUserInput();
				}

				if (newCampaign.id.Length == 0) {
					newCampaign.id = campaign.id;
					view.MoveNext(true);

				} else if (CheckIdInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();

				}

				newCampaign.name = view.GetInput(view);

				if (newCampaign.name.Length == 0) {
					newCampaign.name = campaign.name;
					view.MoveNext(true);

				} else if (CheckNameInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();

				}

				newCampaign.productId = view.GetInput(view);

				if (newCampaign.productId.Length == 0) {
					newCampaign.productId = campaign.productId;
					view.MoveNext(true);

				} else if (CheckProductIdInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();
				}

				newCampaign.startDate = view.GetInput(view);

				if (newCampaign.startDate.Length == 0) {
					newCampaign.startDate = campaign.startDate;
					view.MoveNext(true);

				} else if (CheckStartDateInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();
				}

				newCampaign.endDate = view.GetInput(view);

				if (newCampaign.endDate.Length == 0) {
					newCampaign.endDate = campaign.endDate;
					view.MoveNext(true);

				} else if (CheckEndDateInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();
				}

				newCampaign.newPrice = view.GetInput(view);

				if (newCampaign.newPrice.Length == 0) {
					newCampaign.newPrice = campaign.newPrice;
					view.MoveNext(true);

				} else if (CheckNewPriceInput(newCampaign, campaign, view)) {
					view.MoveNext(true);
				} else {
					view.MoveNext(false);
					newCampaign = new Campaign();
					EditCampaign();

				}

				model.UpdateCampaign(campaign, newCampaign);
				ConsoleBox.PrintCampaignListInBox(0);

				view.SetEditSuccessMessage(Constants.EditCampaignSuccessMessage);
				view._isRunning = false;

				HandleUserInput();
			}
		}

		private void RemoveCampaign() {
			Logger.Log(new StackTrace(), "");

			Campaign campaign = new Campaign();

			isRunning = true;
			ConsoleBox view = CampaignAdminView.GetInstance().DrawRemoveCampaignView();

			while (isRunning) {
				string input = view._input;

				if (input.Equals("0")) {
					isRunning = false;
					CampaignAdminMenuHandler.GetInstance().HandleUserInput();
				}

				campaign.id = view._input;

				if (CheckRemoveCampaignId(campaign, view)) {
					try {
						campaign = model.GetCampaignById(campaign.id);
						//Product product = model.GetProductById(campaign.productId);
						//Product newProduct = product;
						//newProduct.hasCampaign = "false";
						model.GetProductById(campaign.productId).hasCampaign = "false";
						model.RemoveCampaign(campaign);

						ColorMessage.SetCampaignSuccessfullyRemoved(view._cursor, campaign);
						view._isRunning = false;
						HandleUserInput();

					} catch (System.Exception e) {

						view.SetErrorMessage(e.Message, Constants.ProductDeleteErrorMessage);
						RemoveCampaign();
					}

				} else {
					RemoveCampaign();
				}
			}
		}
		
		private bool CheckProductIdInput(Campaign newCampaign, ConsoleBox view) {

			return CheckProductIdInput(newCampaign, new Campaign(), view);

		}

		private bool CheckProductIdInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			try {
				if (newCampaign == null) {
					if (newCampaign.productId.Equals(campaign.productId)) {
						view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.NameExistsMessage);
						return false;
					}
				}

				int id = System.Convert.ToInt32(newCampaign.productId);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (newCampaign.id.Length > 3) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;
		}

		private bool CheckStartDateInput(Campaign newCampaign, ConsoleBox view) {

			return CheckStartDateInput(newCampaign, new Campaign(), view);
		}

		private bool CheckStartDateInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			try {
				DateTime startDate;
				string[] formats = new[] { "yyyy-MM-dd" };
				if (!DateTime.TryParse(newCampaign.startDate, out startDate)) {
					view.SetErrorMessage(Constants.InvalidStartDateFormatMessage, Constants.InvalidDateFormatMessage);
					return false;
				}
			} catch (System.Exception e) {

				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidDateFormatMessage);
				return false;
			}

			return true;
		}

		private bool CheckEndDateInput(Campaign newCampaign, ConsoleBox view) {

			return CheckStartDateInput(newCampaign, new Campaign(), view);
		}

		private bool CheckEndDateInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			try {
				DateTime endDate;
				string[] formats = new[] { "yyyy-MM-dd" };
				if (!DateTime.TryParseExact(newCampaign.endDate, formats,
					System.Globalization.CultureInfo.InvariantCulture,
					System.Globalization.DateTimeStyles.None, out endDate)) {
					view.SetErrorMessage(Constants.InvalidEndDateFormatMessage, Constants.InvalidDateFormatMessage);
					return false;	
				}
			} catch (FormatException e) {

				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidDateFormatMessage);
				return false;
			}
			return true;
		}

		private bool CheckIdInput(Campaign newCampaign, ConsoleBox view) {

			return CheckIdInput(newCampaign, new Campaign(), view);
		}

		private bool CheckIdInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			try {
				if (newCampaign.id.Length > 3) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (model.GetCampaignById(newCampaign.id) == null) {
					if (newCampaign.id.Equals(campaign.id)) {

						view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.NameExistsMessage);
						return false;
					}
				}

				int id = System.Convert.ToInt32(newCampaign.id);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (model.CampaignExists(newCampaign.id)) {
					view.SetErrorMessage(Constants.InvalidCampaignIdMessage, Constants.CampaignExistsErrorMessage);
					return false;
				}
			} catch (FormatException e) {
				view.SetErrorMessage(Constants.InvalidFormatMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;
		}

		private bool CheckNameInput(Campaign newCampaign, ConsoleBox view) {

			return CheckNameInput(newCampaign, new Campaign(), view);
		}

		private bool CheckNameInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			try {
				if (newCampaign.name == "") {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
					return false;
				}

				if (!newCampaign.name.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x))) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
					return false;
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidNameFormatMessage);
				return false;
			}
			return true;
		}

		private bool CheckNewPriceInput(Campaign newCampaign, ConsoleBox view) {
			
			return CheckNewPriceInput(newCampaign, new Campaign(), view);

		}

		private bool CheckNewPriceInput(Campaign newCampaign, Campaign campaign, ConsoleBox view) {
			
			try {
				string periodPrice = newCampaign.newPrice;
				periodPrice.Replace(',', '.');
					
				if (double.Parse(periodPrice) < 0) {
					view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceRangeMessage);
					return false;
				}

				if (double.Parse(periodPrice) == 0) {
					view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceRangeMessage);
					return false;
				}

				if (newCampaign.newPrice.LastIndexOf(',') == -1) {
					view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceDecimalFormatMessage);
					return false;
				}

				int result = newCampaign.newPrice.LastIndexOf(',');
				if (result == -1 || newCampaign.newPrice.Length - result - 1 != 2) {
					view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidPriceDecimalFormatMessage);
					return false;
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidPriceFormatMessage, Constants.InvalidNumberFormatMessage);
				e.ToString();
				return false;
			}
			return true;
		}

		private bool CheckRemoveCampaignId(Campaign campaign, ConsoleBox view) {
			try {

				int id = System.Convert.ToInt32(campaign.id);
				if (id < 1 || id > 999) {
					view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
					return false;
				}

				if (!model.CampaignExists(campaign.id)) {
					view.SetErrorMessage(Constants.InvalidCampaignIdMessage, Constants.CampaignDoesNotExist);
					return false;
				}
			} catch (System.Exception e) {
				view.SetErrorMessage(Constants.InvalidInputStringMessage, Constants.InvalidIdRangeMessage);
				return false;
			}
			return true;
		}

	}
}

