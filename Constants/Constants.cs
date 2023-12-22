

using System.Numerics;

    public class Constants { 

        // Strings and messages
        public const string InvalidInputTypeMessage = "Invalid input type";
        public const string InvalidFormatMessage = "Invalid input format";
		public const string InvalidPriceRangeMessage = "Invalid price range";
		public const string InvalidPriceFormatMessage = "Invalid price format";
		public const string NotInRangeMessage = "The value is not within a valid range";
        public const string ValueExitsMessage = "The value already exists";
        public const string InvalidProductIdMessage = "Invalid Id";
        public const string InvalidUnitFormatMessage = "Invalid unit format, valid formats: (st, kg)";
        public const string InvalidPriceSeparatorFormatMessage = "Invalid price format, valid decimal separator: (" + "," + ")";
        public const string InvalidPriceDecimalFormatMessage = "The price shall have two decimals";
        public const string InvalidNameFormatMessage = "Please use valid characters (A-Z, a-z, 0-9 and space) ";
	    public const string NameExistsMessage = "Name already exists";
	    public const string InvalidDateFormatMessage = "Valid date format: yyyy-MM-dd, eg. 2023-12-31";
        public const string InvalidCampaignIdMessage = "Invalid Campaign Id";
        public const string ProductIdMessage = "id";
        public const string ProductNameMessage = "name";
        public const string ProductPriceMessage = "price";
        public const string ProductUnitMessage = "unit";
        public const string ProductMessage = "PRODUCT";
        public const string CampaignMessage = "CAMPAIGN";
        public const string ReadDBErrorMessage = "Failed while reading db file";
        public const string ReceiptErrorMessage = "Failed to create recepit";
        public const string NoProductsInPurchase = "No products in purchase";
        public const string InvalidInputStringMessage = "Invalid input";
        public const string ProductExistsErrorMessage = "Product Id already exists";
		public const string InvalidIdRangeMessage = "Please enter an Id between 001-999";
		public const string PressToContinueMessage = "Press any key to continue...";
        public const string InvalidNumberFormatMessage = "Invalid number format";
        public const string ProductDeleteErrorMessage = "Failed to delete product";
		public const string ProductDoesNotExist = "Product id does not exist";
		public const string CampaignExistsErrorMessage = "Campaign Id already exists";
		public const string CampaignDoesNotExist = "Campaign id does not exist";
		public const string EditCampaignSuccessMessage = "Campaign updated!";
		public const string EditProductSuccessMessage = "Product updated!";
		public const string InvalidStartDateFormatMessage = "Invalid Start date format";
		public const string InvalidEndDateFormatMessage = "Invalid End date format";
	    public const string ErrorInPurchaseMessage = "Error in purchase";

		// Error Codes
		public const int InvalidInputNumber = 1;
        public const int InvalidInputString = 2;
        public const int InvalidRange = 3;
        public const int InvalidId = 4;
        public const int InvalidUnit = 5;
        public const int InvalidPrice = 6;
        public const int InvalidName = 7;
        public const int ReceiptError = 9;
        public const int InsertProductError = 10;
        public const int UpdateProductError = 11;
        public const int ProductReadError = 12;
        public const int MallFormedProduct = 13;
        public const int MainMenuError = 14;
        public const int CampaignError = 15;
        public const int ProductError = 16; 
        public const int DBInitError = 17;
        public const int DBInstanceError = 18;
        public const int CampaignDBInitError = 19;
        public const int CampaignDBInstanceError = 20;
        public const int CampaignReadError = 21;
        public const int InsertCampaignError = 22;
        public const int MallFormedCampaign = 23;
        public const int UpdateCampaignError = 24;
        public const int CashRegisterFileReadError = 25;
        public const int CashRegisterFileWriteError = 26;        
        public const int ProductRegistrationError = 27;
        public const int MenuHandlerError = 28;
        public const int DBDeleteReadError = 29;
        public const int DBReadError = 30;
        public const int DBFileSearchError = 31;
        public const int ErrorDuringPay = 32;
        public const int RemoveCampaignError = 33;
        public const int GetCampaignByIDError = 34;
        public const int AppendToTableError = 35;
        public const int DeleteCampaignError = 36;
        public const int CampaignExistsError = 37;
        public const int HasProductCampaignError = 38;
        public const int GetCampaignsByProductId = 39;
        public const int GetProductByIdError = 40;
        public const int GetProductFromDBError = 41;
        public const int ProductExistsError = 42;


        // Number ranges
        public const int MaxId = 999;
        public const int MinId = 001;

        // Source constants
        public const int DatabaseError = 100;
        public const int ControllerError = 200;
        public const int ViewError = 300;
        public const int MainError = 400;
        public const int AdminError = 500;
        public const int CampaignAdminError = 600;
        public const int ReceiptInstanceError = 700;
        public const int ProductAdminInstanceError = 800;
        public const int MenuHandlerInstanceError = 900;
        public const int CampaignAdminInstanceError = 1000;
        public const int ProductAdminError = 1100;

		// Color scheme constants
		public const int RedGray = 1;
		public const int YellowGray = 2;
		public const int GreenGray = 3;

		public const int ContentBox = 1;
		public const int HeaderBox = 2;
		public const int MenuBox = 3;
		public const int Foreground = 4;
		public const int Background = 5;

	public const string HeaderTitle = "HeaderTitle CashRegister Application";
	public const string MainMenuBoxTitle = "MenuTitle CashRegister - Main Menu";
	public const string ProductListBoxTitle = "ListTitle Products";

	// Product variables
	public const int ProductId = 1;
		public const int ProductName = 2;
		public const int ProductPrice = 3;
		public const int ProductUnit = 4;

        
	}

