using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Views;
using System.Diagnostics;
using System.Globalization;

namespace CashRegister.Views {

	public class CampaignAdminView : ConsoleView {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private int color = int.Parse(System.Environment.GetEnvironmentVariable("CashRegisterColorScheme"));

		private static CampaignAdminView? instance;
		private bool isRunning = false;

		static ConsoleBox contentBox;
		static ConsoleBox headerBox;
		static ConsoleBox view;

		private CampaignAdminView() {

		}

		public static CampaignAdminView GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new CampaignAdminView();
			return instance;
		}

		public override ConsoleBox DrawView() {

			Console.Clear();
			ConsoleBox.PrintCampaignListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetCampaigns(), ColorScheme.GetColorScheme()[1]);

			ConsoleBox.PrintProductListInBox(contentBox._pos);
			contentBox = ConsoleBox.DrawContentBox(contentBox._pos + 2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[0]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetCampaignAdminMenu(), ColorScheme.GetColorScheme()[2]);

			return view;

		}

		public ConsoleBox DrawAddCampaignView() {

			Console.Clear();
			ConsoleBox.PrintCampaignListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetCampaigns(), ColorScheme.GetColorScheme()[1]);

			ConsoleBox.PrintProductListInBox(contentBox._pos);
			contentBox = ConsoleBox.DrawContentBox(contentBox._pos + 2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawMenuBox(headerBox._pos, Menus.GetAddCampaignMenu(), ColorScheme.GetColorScheme()[2]);

			return view;

		}

		public ConsoleBox DrawRemoveCampaignView() {

			Console.Clear();
			ConsoleBox.PrintCampaignListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetCampaigns(), ColorScheme.GetColorScheme()[1]);

			ConsoleBox.PrintProductListInBox(contentBox._pos);
			contentBox = ConsoleBox.DrawContentBox(contentBox._pos + 2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawMenuBox(headerBox._pos, Menus.GetRemoveCampaignMenu(), ColorScheme.GetColorScheme()[2]);

			return view;

		}

		public ConsoleBox DrawEditCampaignView() {

			Console.Clear();
			ConsoleBox.PrintCampaignListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetCampaigns(), ColorScheme.GetColorScheme()[1]);

			ConsoleBox.PrintProductListInBox(contentBox._pos);
			contentBox = ConsoleBox.DrawContentBox(contentBox._pos + 2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetEditCampaignMenu(), ColorScheme.GetColorScheme()[2]);

			return view;

		}

		public ConsoleBox DrawUpdateCampaignView(Campaign campaign) {

			Console.Clear();
			ConsoleBox.PrintCampaignListInBox(0);
			contentBox = ConsoleBox.DrawContentBox(2, ConsoleBox.GetCampaigns(), ColorScheme.GetColorScheme()[1]);

			ConsoleBox.PrintProductListInBox(contentBox._pos);
			contentBox = ConsoleBox.DrawContentBox(contentBox._pos + 2, ConsoleBox.GetProducts(), ColorScheme.GetColorScheme()[1]);

			headerBox = ConsoleBox.DrawHeaderBox(0, ColorScheme.GetColorScheme()[1]);
			view = ConsoleBox.DrawSelectionBox(headerBox._pos, Menus.GetUpdateCampaignMenu(campaign), ColorScheme.GetColorScheme()[2]);

			return view;

		}

	}
}
