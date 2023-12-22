using CashRegister.Log;
using CashRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Views {

	public class Menus {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();

		private static Menus? instance;

		public static Menus GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new Menus();
			return instance;
		}


		public static List<string> GetMainMenu() {


			List<string> menu = new List<string>();
			menu.Add("Cash Register - Main Menu\n\n");
			menu.Add("");
			menu.Add("\t1. New Customer\n");
			menu.Add("\t2. Product Admin\n");
			menu.Add("\t3. Campaign Admin\n");
			menu.Add("\n\t0. Exit\n");
			menu.Add("");
			menu.Add("\n\t> ");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetCustomerMenu() {

			List<string> menu = new List<string>();
			menu.Add("Cash Register - Customer Menu\n\n");
			menu.Add("");
			menu.Add("\tCommands: \n");
			menu.Add("\t- <productId> <amount>\n");
			menu.Add("\t- PAY (Calculates and prints a receipt)\n");
			menu.Add("\t- 0 (Exit to Main menu)\n");
			menu.Add("");
			menu.Add("\n\t> ");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetProductAdminMenu() {

			List<string> menu = new List<string>();
			menu.Add("Cash Register - Product Admin Menu\n\n");
			menu.Add("");
			menu.Add("\t1. Add product\n");
			menu.Add("\t2. Remove product\n");
			menu.Add("\t3. Edit product\n");
			menu.Add("\n\t0. Exit\n");
			menu.Add("");
			menu.Add("\n\t> ");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetAddProductMenu() {

			List<string> menu = new List<string>();
			menu.Add("Product Admin Menu - Add New Product");
			menu.Add("");
			string format = "{0,-14}{1,16}";
			menu.Add(string.Format(format, "\tProduct Id", "(001 - 999) : "));
			menu.Add(string.Format(format, "\tProduct Name", "(A-Z), (0-9) : "));
			menu.Add(string.Format(format, "\tProduct Price", "(eg. 10,00) : "));
			menu.Add(string.Format(format, "\tProduct Unit", "(eg. kg/st) : "));

			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetRemoveProductMenu() {

			List<string> menu = new List<string>();
			menu.Add("Product Admin Menu - Remove Product");
			menu.Add("");
			menu.Add("\tProduct Id (001 - 999): ");
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}


		public static List<string> GetUpdateProductMenu(Product product) {

			List<string> menu = new List<string>();
			string format = "{0,-14}{1,16}";

			menu.Add("Product Admin Menu - Edit Product");
			menu.Add("");
			menu.Add(string.Format(format, "\tProduct Id", "(" + product.id + ") : "));
			menu.Add(string.Format(format, "\tProduct Name", "(" + product.name + ") : "));
			menu.Add(string.Format(format, "\tProduct Price", "(" + product.price + ") : "));
			menu.Add(string.Format(format, "\tProduct Unit", "(" + product.unit + ") : "));
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
			
		}

		public static List<string> GetEditProductMenu() {

			List<string> menu = new List<string>();
			menu.Add("Product Admin Menu - Edit Product");
			menu.Add("");
			menu.Add("\tProduct Id: ");
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;

		}

		public static List<string> GetCampaignAdminMenu() {

			List<string> menu = new List<string>();
			menu.Add("Cash Register - Campaign Admin Menu\n\n");
			menu.Add("");
			menu.Add("\t1. Add Campaign\n");
			menu.Add("\t2. Remove Campaign\n");
			menu.Add("\t3. Edit Campaign\n");
			menu.Add("\n\t0. Exit\n");
			menu.Add("");
			menu.Add("\n\t> ");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetAddCampaignMenu() {

			List<string> menu = new List<string>();
			menu.Add("Campaign Admin Menu - Add New Campaign");
			menu.Add("");
			string format = "{0,-14}{1,16}";
			menu.Add(string.Format(format, "\tCampaign Id", "(001 - 999) : "));
			menu.Add(string.Format(format, "\tCampaign Name", "(A-Z), (0-9) : "));
			menu.Add(string.Format(format, "\tProduct Id", "(001 - 999) : "));
			menu.Add(string.Format(format, "\tStart Date", "(yyyy-MM-dd) : "));
			menu.Add(string.Format(format, "\tEnd Date", "(yyyy-MM-dd) : "));
			menu.Add(string.Format(format, "\tNew Price", "(eg. 10,00) : "));

			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetRemoveCampaignMenu() {

			List<string> menu = new List<string>();
			menu.Add("Campaign Admin Menu - Remove Campaign");
			menu.Add("");
			menu.Add("\tCampaign Id (001 - 999): ");
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetListCampaignByIdMenu() {

			List<string> menu = new List<string>();
			menu.Add("Campaign Admin Menu - List Campaigns By Id\n\n");
			menu.Add("");
			menu.Add("\tCampaign Id: \n");
			menu.Add("");
			menu.Add("\n\t0. Exit\n");
			menu.Add("");
			menu.Add("\n\t> ");
			menu.Add("");
			menu.Add("");

			return menu;
		}

		public static List<string> GetEditCampaignMenu() {

			List<string> menu = new List<string>();
			menu.Add("Campaign Admin Menu - Edit Campaign");
			menu.Add("");
			menu.Add("\tCampaign Id: ");
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;

		}

		public static List<string> GetUpdateCampaignMenu(Campaign campaign) {

			List<string> menu = new List<string>();
			string format = "{0,-14}{1,16}";

			menu.Add("Campaign Admin Menu - Edit Campaign");
			menu.Add("");
			menu.Add(string.Format(format, "\tCampaign Id", "(" + campaign.id + ") : "));
			menu.Add(string.Format(format, "\tCampaign Name", "(" + campaign.name + ") : "));
			menu.Add(string.Format(format, "\tProduct Id", "(" + campaign.productId + ") : "));
			menu.Add(string.Format(format, "\tStart Date", "(" + campaign.startDate + ") : "));
			menu.Add(string.Format(format, "\tEnd Date", "(" + campaign.endDate + ") : "));
			menu.Add(string.Format(format, "\tNew Price", "(" + campaign.newPrice + ") : "));
			menu.Add("");
			menu.Add("\t0. Exit");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");
			menu.Add("");

			return menu;

		}


		public static List<string> GetReceiptBox(List<string> receipt) {
			receipt.Add("");

			return receipt;
		}

		public static List<string> GetErrorBox(string error) {

			List<string> errorList = new List<string>();
			errorList.Add("Cash Register Error\n\n");
			errorList.Add("");
			errorList.Add("\t\t" + error + " \n");
			errorList.Add("");
			errorList.Add("");
			errorList.Add("");
			errorList.Add("");
			errorList.Add("");

			return errorList;
		}

		public static List<string> GetDisplayProductMenu(Product p) {

			List<string> list = new List<string>();
			list.Add("Edit Product - " + p.name + "\n\n");
			list.Add("");
			list.Add("\tId:    " + p.id);
			list.Add("\tName:  " + p.name);
			list.Add("\tPrice: " + p.price);
			list.Add("\tUnit:  " + p.unit);
			list.Add("");
			list.Add("\tNew Id: ");
			list.Add("");

			return list;
		}

		public static List<string> GetDisplayCampaignMenu(Campaign c) {

			List<string> list = new List<string>();
			list.Add("Edit Campaign - " + c.name + "\n\n");
			list.Add("");
			list.Add("\tId:    " + c.id);
			list.Add("\tName:  " + c.name);
			list.Add("\tProduct Id: " + c.productId);
			list.Add("\tStart Date:  " + c.startDate);
			list.Add("\tEnd Date:  " + c.endDate);
			list.Add("\tNew Price:  " + c.newPrice);
			list.Add("");
			list.Add("\tNew Id: ");
			list.Add("");

			return list;
		}
	}
}
