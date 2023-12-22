
using CashRegister.Models;
using CashRegister.Log;
using CashRegister.Exception;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace CashRegister.Controllers {

	public class DBHandler {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static string receiptPath { get; } = @"../../../RECEIPT_";
		private static string receiptIndexPath { get; } = "../../../ReceiptIndex.txt";
		private static string dbFilePath { get; } = "../../../CashRegisterDB.csv";
		private static List<Product> productList = new();
		private static List<Campaign> campaignList = new();
		private static DBHandler? instance;
		private static readonly object thisLock = new();

		private static string campaignTableHeader = "\n" +
					"# Campaign Table\n" +
					"# CampaignId; Name; ProductId; StartDate; EndDate; CampaignPrice\n" +
					"CAMPAIGN_START\n";
		private static string campaignTableFooter = "CAMPAIGN_END";

		private static string productTableHeader = "\n" +
					"# Product Table\n" +
					"# ProductId; Name; Price; Unit, hasCampaign\n" +
					"PRODUCT_START\n";
		private static string productTableFooter = "PRODUCT_END";


		public static DBHandler GetInstance() {

			if (instance != null) {
				return instance;
			}
			instance = new DBHandler();
			return instance;
		}

		public static void InitDB() {
			Logger.Log(new StackTrace(), "");

			try {
				string initialDBTables =
					campaignTableHeader +
					"001; Campaign 1; 001; 2023-11-02; 2024-12-12; 20,00\n" +
					"002; Campaign 2; 002; 2023-11-02; 2024-12-12; 10,00\n" +
					"003; Campaign 3; 003; 2023-11-03; 2024-12-13; 10,00\n" +
					"004; Campaign 4; 004; 2023-11-04; 2024-12-14; 40,00\n" +
					campaignTableFooter + "\n" +

					productTableHeader +
					"001; Tandborste; 5,00; st; true\n" +
					"002; Mjölk; 12,00; st; true\n" +
					"003; Ägg; 1,75; st; true\n" +
					"004; Pasta; 7,25; kg; true\n" +
					"005; Tomater; 28,90; kg; false\n" +
					"006; Bröd; 9,75; st; false\n" +
					"007; Kyckling; 51,00; kg; false\n" +
					"008; Tvål; 6,20; st; false\n" +
					"009; Kaffe; 32,80; st; false\n" +
					"010; Potatis; 10,50; kg; false\n" +
					"011; Apelsiner; 25,20; kg; false\n" +
					"012; Yoghurt; 13,25; st; false\n" +
					"013; Rispaket; 7,10; st; false\n" +
					"014; Köttfärs; 78,50; kg; false\n" +
					"015; Tandkräm; 9,00; st; false\n" +
					"016; Cornflakes; 14,20; st; false\n" +
					"017; Laxfilé; 63,80; kg; false\n" +
					"018; Diskmedel; 8,25; st; false\n" +
					"019; Te; 29,50; st; false\n" +
					"020; Jordnötssmör; 14,50; st; false" + "\n" +
					productTableFooter + "\n";

				// If the db file exists, then use it
				// Else, create it.
				if (!File.Exists(dbFilePath)) {
					try {
						// Writes the initial product data to disk if the file dosen't exists 
						File.WriteAllText(dbFilePath, initialDBTables);

					} catch (System.Exception e) {
						throw new CashRegisterException(e, Constants.CashRegisterFileWriteError, Constants.DatabaseError);
					}
				}

				string[] lines;
				try {
					lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.CashRegisterFileReadError, Constants.DatabaseError);
				}
				productList = GetAllFromProductTable(lines);
				campaignList = GetAllFromCampaignTable(lines);

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.DBInitError, Constants.DatabaseError);
			}
		}

		public static string[] GetDBFile(Encoding enc) {
			Logger.Log(new StackTrace(), "");

			string[] str = File.ReadAllLines(dbFilePath, enc);
			return str;
		}

		public static List<Product> GetAllFromProductTable(string[] lines) {
			Logger.Log(new StackTrace(), "");

			string[] cashRegisterTable = GetDBTable(Constants.ProductMessage, lines);
			
			// Product table
			productList.Clear();

			for (int i = 0; i < cashRegisterTable.Length; i++) {

				try {
					string[] row = cashRegisterTable[i].Split("; ");
					string idCol = row[0];
					string nameCol = row[1];
					string priceCol = row[2];
					string unitCol = row[3];
					string hasCampaignCol = row[4];

					Product product = new(idCol, nameCol, priceCol, unitCol, hasCampaignCol);
					productList.Add(product);

				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.MallFormedProduct, Constants.DatabaseError);
				}
			}
			return productList;
		}

		public static List<Campaign> GetAllFromCampaignTable(string[] lines) {
			Logger.Log(new StackTrace(), "");

			string[] cashRegisterTable = GetDBTable(Constants.CampaignMessage, lines);

			// Campaign table
			campaignList.Clear();

			for (int i = 0; i < cashRegisterTable.Length; i++) {
				try {

					string[] row = cashRegisterTable[i].Split("; ");
					string idCol = row[0];
					string nameCol = row[1];
					string productIdCol = row[2];
					string startDateCol = row[3];
					string endDateCol = row[4];
					string newPriceCol = row[5];

					Campaign campaign = new(idCol, nameCol, productIdCol, startDateCol, endDateCol, newPriceCol);
					campaignList.Add(campaign);

				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.MallFormedCampaign, Constants.DatabaseError);
				}
			}
			return campaignList;

		}

		private static string[] GetDBTable(string tableName, string[] tableData) {
			Logger.Log(new StackTrace(), "");

			string[] lines;
			try {
				string startTag = tableName + "_START";
				string endTag = tableName + "_END";

				// Find start and end tags
				int start = 0;
				int end = 0;

				for (int i = 0; i < tableData.Length; i++) {
					if (tableData[i].StartsWith(startTag)) {
						start = i + 1;
					}
					if (tableData[i].StartsWith(endTag)) {
						end = i;
					}
				}
				lines = new string[end - start];
				int endLine = end - start;

				// Get lines between tags
				for (int i = 0; i < endLine; i++) {
					lines[i] = tableData[start + i];
				}

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.DBFileSearchError, Constants.DatabaseError);
			}
			return lines;
		}

		public List<Product> GetProductList() {
			Logger.Log(new StackTrace(), "");

			return productList;
		}

		public Product GetProductFromDB(string id) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Product> products = GetProductList();
				foreach (Product product in products) {
					if (product.id == id) {
						return product;
					}
				}
				return null;
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.GetProductFromDBError, Constants.DatabaseError);
			}
		}

		// Appends all receipts to a text file
		public static void WriteReceiptToFile(string output) {
			Logger.Log(new StackTrace(), "");

			try {
				// Maintains single access to a file
				lock (thisLock) {
					File.AppendAllText(@receiptPath + "" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "\n" + output);
				}
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.ReceiptError, Constants.DatabaseError);
			}
		}

		// Inserts a new product into the database/file
		public void InsertProductToDb(Product product) {
			Logger.Log(new StackTrace(), product.ToString());

			// Adds product to file
			string[] lines;
			try {
				lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.DBReadError, Constants.DatabaseError);
			}

			try {
				List<Product> list = GetAllFromProductTable(lines);
				AppendProductToTable(Constants.ProductMessage, lines, product);
				InitDB();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.InsertCampaignError, Constants.DatabaseError);
			}
		}


		// Convenient method
		public Product GetProductById(string id) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Product> pList = ReadAllProducts();
				foreach (Product p in pList) {
					if (id.Equals(p.id)) {

						return p;
					}
				}
				return new Product();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.GetProductByIdError, Constants.DatabaseError);
			}
		}

		public Product GetProductByName(string name) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Product> pList = ReadAllProducts();
				foreach (Product p in pList) {
					if (name.Equals(p.name)) {

						return p;
					}
				}
				return new Product();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.GetProductByIdError, Constants.DatabaseError);
			}
		}

		// Reads all products from the db file in order to return a runtime list.
		// Serves as a cache to minimize file I/O operations
		private static List<Product> ReadAllProducts() {
			Logger.Log(new StackTrace(), "");

			productList.Clear();
			string[] lines = GetDBFile(Encoding.GetEncoding("UTF-8"));

			try {
				productList = GetAllFromProductTable(lines);

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.ProductReadError, Constants.DatabaseError);
			}
			return productList;
		}

		public void UpdateProductHasCampaign(Product orig, Product update) {

			string[] lines;
			try {
				lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
			}

			string line = "";
			for (int i = 0; i < lines.Length; i++) {

				try {
					if (lines[i].StartsWith(orig.id)) {
						line = lines[i];
					}
				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
				}
			}

			try {
				// Modified row is saved to disk
				string f = File.ReadAllText(dbFilePath);

				// Update original product hasCampaign
				string origLine = orig.id + "; " + orig.name + "; " + orig.price + "; " + orig.unit + "; " + orig.hasCampaign;
				string origLineUpdate = orig.id + "; " + orig.name + "; " + orig.price + "; " + orig.unit + "; " + update.hasCampaign;
				string replaced = f.Replace(origLine, origLineUpdate);
				File.WriteAllText(dbFilePath, replaced);


				try {
					lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
				}

				line = "";
				for (int i = 0; i < lines.Length; i++) {

					try {
						if (lines[i].StartsWith(update.id)) {
							line = lines[i];
						}
					} catch (System.Exception e) {
						throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
					}
				}

				f = File.ReadAllText(dbFilePath);
				string newLineOrig = update.id + "; " + update.name + "; " + update.price + "; " + update.unit + "; " + update.hasCampaign;
				string newLineUpdate = update.id + "; " + update.name + "; " + update.price + "; " + update.unit + "; " + "true";
				replaced = f.Replace(newLineOrig, newLineUpdate);
				File.WriteAllText(dbFilePath, replaced);

				ReadAllProducts();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
			}

		}

		// Modifies an existing product in the database/file
		public void UpdateProduct(Product orig, Product update) {
			Logger.Log(new StackTrace(), "");

			string[] lines;
			try {
				lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
			}

			string line = "";
			for (int i = 0; i < lines.Length; i++) {

				try {
					if (lines[i].StartsWith(orig.id)) {
						line = lines[i];
					}
				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
				}
			}

			try {
				// Modified row is saved to disk
				File.WriteAllText(dbFilePath, File.ReadAllText(dbFilePath).Replace(line,
					update.id + "; " + update.name + "; " + update.price + "; " + update.unit + "; " + orig.hasCampaign));

				ReadAllProducts();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
			}

		}

		public static List<Campaign> GetCampaignsByProductId(string productId) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Campaign> cList = ReadAllCampaigns();
				List<Campaign> campaigns = new List<Campaign>();

				foreach (Campaign c in cList) {
					if (productId.Equals(c.productId)) {
						if (c.IsActive()) {
							campaigns.Add(c);
						}
					}
				}
				return campaigns;

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.GetCampaignsByProductId, Constants.DatabaseError);
			}
		}

		private static List<Campaign> ReadAllCampaigns() {
			Logger.Log(new StackTrace(), "");

			campaignList.Clear();
			string[] lines = GetDBFile(Encoding.GetEncoding("UTF-8"));

			try {
				campaignList = GetAllFromCampaignTable(lines);

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.CampaignReadError, Constants.DatabaseError);
			}
			return campaignList;
		}

		public List<Campaign> GetCampaignsList() {
			Logger.Log(new StackTrace(), "");

			return campaignList;
		}

		public Campaign GetCampaignById(string id) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Campaign> cList = GetCampaignsList();
				foreach (Campaign c in cList) {
					if (id.Equals(c.id)) {
						return c;
					}
				}

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.GetCampaignByIDError, Constants.DatabaseError);
			}
			return null;
		}

		public void UpdateCampaign(Campaign orig, Campaign update) {
			Logger.Log(new StackTrace(), "");

			string[] lines;
			try {
				lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
				string r = "";
				string line = "";
				string newRow = update.id + "; " + update.name + "; " + update.productId + "; " + update.startDate + "; " + update.endDate + "; " + update.newPrice;
				bool idFound = false;
				bool productTableHeaderFound = false;
				bool campaignTableFooterFound = false;
				bool campaignTableHeaderFound = false;

				for (int i = 0; i < lines.Length; i++) {

					if (line.Equals(campaignTableHeader) && campaignTableHeaderFound == false) {
						campaignTableHeaderFound = true;
						
					}

					if (!lines[i].Contains("PRODUCT_END")) {

						if (lines[i].StartsWith(orig.id) && (idFound == false)) {
							idFound = true;
							lines[i] = newRow;							
						} else if (line.Equals(productTableHeader) && productTableHeaderFound == false) {
							productTableHeaderFound = true;
							line += productTableHeader;
						} else if (line.Equals(campaignTableFooter) && campaignTableFooterFound == false) {
							campaignTableFooterFound = true;
							line += campaignTableFooter;
						}

						line += lines[i] + "\n";						
					}
				}
				line += productTableFooter;
				File.WriteAllText(dbFilePath, line);		

				ReadAllCampaigns();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.UpdateProductError, Constants.DatabaseError);
			}
		}

		public void InsertCampaignToDb(Campaign campaign) {
			Logger.Log(new StackTrace(), "");

			// Adds new campaign to the dbfile
			try {
				string[] lines;
				try {
					lines = GetDBFile(Encoding.GetEncoding("UTF-8"));
				} catch (System.Exception e) {
					throw new CashRegisterException(e, Constants.DBReadError, Constants.DatabaseError);
				}

				List<Campaign> list = GetAllFromCampaignTable(lines);
				AppendCampaignToTable(Constants.CampaignMessage, lines, campaign);
				InitDB();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.InsertCampaignError, Constants.DatabaseError);
			}
		}
	
		private static void AppendCampaignToTable(string table, string[] lines, Campaign cashRegisterObject) {
			Logger.Log(new StackTrace(), "");

			// Make room for row to be appended
			string newLines = "";
			try {
				for (int i = 0; i < lines.Length; i++) {

					newLines += lines[i].Trim() + "\n";
					if (lines[i].Equals(table + "_END")) {

						Campaign c = cashRegisterObject;

						int index = newLines.IndexOf(table + "_END");
						newLines = newLines.Remove(index);
						newLines +=		 c.id
								+ "; " + c.name
								+ "; " + c.productId
								+ "; " + c.startDate
								+ "; " + c.endDate
								+ "; " + c.newPrice + "\n";
						newLines += table + "_END" + "\n";
					}
				}
				File.WriteAllText(dbFilePath, newLines.ToString());

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.AppendToTableError, Constants.DatabaseError);
			}
		}

		private static void AppendProductToTable(string table, string[] lines, Product cashRegisterObject) {
			Logger.Log(new StackTrace(), "");

			// Make room for row to be appended
			string newLines = "";
			try {
				for (int i = 0; i < lines.Length; i++) {

					newLines += lines[i].Trim() + "\n";
					if (lines[i].Equals(table + "_END")) {

						Product p = cashRegisterObject;

						int index = newLines.IndexOf(table + "_END");
						newLines = newLines.Remove(index);
						newLines +=		 p.id
								+ "; " + p.name
								+ "; " + p.price
								+ "; " + p.unit
								+ "; " + p.hasCampaign + "\n";
						newLines += table + "_END" + "\n";
							
					}
				}
				File.WriteAllText(dbFilePath, newLines.ToString());
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.AppendToTableError, Constants.DatabaseError);
			}
		}

		public void RemoveCampaign(Campaign campaign) {
			string[] dbLines = GetDBFile(Encoding.GetEncoding("UTF-8"));
			RemoveFromCampaignTable(dbLines, campaign);
		}
		public void RemoveProduct(Product product) {
			string[] dbLines = GetDBFile(Encoding.GetEncoding("UTF-8"));
			RemoveFromProductTable(dbLines, product);
		}

		public static void RemoveFromProductTable(string[] tableData, Product cashRegisterObject) {
			Logger.Log(new StackTrace(), "");

			try {
				string productStartTag = "PRODUCT_START";
				string productEndTag = "PRODUCT_END";
				string campaignStartTag = "CAMPAIGN_START";
				string campaignEndTag = "CAMPAIGN_END";

				// Find start and end tags
				int start = 0;
				int end = 0;

				for (int i = 0; i < tableData.Length; i++) {
					if (tableData[i].StartsWith(campaignStartTag)) {
						start = i + 1;
					}
					if (tableData[i].StartsWith(campaignEndTag)) {
						end = i;
					}
				}
				string[] campaignLines = new string[end - start];
				int endLine = end - start;

				string newCamapaignLines = "";
				// Get lines between tags
				for (int i = 0; i < endLine; i++) {
					campaignLines[i] = tableData[start + i];
					if (campaignLines[i].StartsWith(cashRegisterObject.id)) {
						newCamapaignLines += "";
					} else {
						newCamapaignLines += campaignLines[i] + "\n";
					}
				}
				newCamapaignLines = (campaignTableHeader + newCamapaignLines + campaignTableFooter);


				start = 0;
				end = 0;

				for (int i = 0; i < tableData.Length; i++) {
					if (tableData[i].StartsWith(productStartTag)) {
						start = i + 1;
					}
					if (tableData[i].StartsWith(productEndTag)) {
						end = i;
					}
				}
				string[] productLines = new string[end - start];
				endLine = end - start;

				string newProductLines = "";
				// Get lines between tags
				for (int i = 0; i < endLine; i++) {
					productLines[i] = tableData[start + i];
					if (productLines[i].StartsWith(cashRegisterObject.id)) {
						newProductLines += "";
					} else {
						newProductLines += productLines[i] + "\n";

					}
				}
				newProductLines = (productTableHeader + newProductLines + productTableFooter);

				string newLines = newCamapaignLines + "\n" + newProductLines;
				File.WriteAllText(dbFilePath, newLines.ToString());

				ReadAllCampaigns();
				ReadAllProducts();

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.DBFileSearchError, Constants.DatabaseError);
			}
		}

		public static void RemoveFromCampaignTable(string[] tableData, Campaign campaign) {
				Logger.Log(new StackTrace(), "");

			try {
				string productStartTag = "PRODUCT_START";
				string productEndTag = "PRODUCT_END";
				string campaignStartTag = "CAMPAIGN_START";
				string campaignEndTag = "CAMPAIGN_END";

				// Find start and end tags
				int start = 0;
				int end = 0;

				for (int i = 0; i < tableData.Length; i++) {
					if (tableData[i].StartsWith(campaignStartTag)) {
						start = i + 1;
					}
					if (tableData[i].StartsWith(campaignEndTag)) {
						end = i;
					}
				}
				string[] campaignLines = new string[end - start];
				int endLine = end - start;

				string newCamapaignLines = "";
				// Get lines between tags
				for (int j = 0; j < endLine; j++) {
					campaignLines[j] = tableData[start + j];
					string[] splitRow = campaignLines[j].Split("; ");

					if (!splitRow[0].Equals(campaign.id)) {
						newCamapaignLines += "\n" + campaignLines[j] + "\n";
					}

					newCamapaignLines = (campaignTableHeader + newCamapaignLines + campaignTableFooter);


					start = 0;
					end = 0;

					for (int k = 0; k < tableData.Length; k++) {
						if (tableData[k].StartsWith(productStartTag)) {
							start = k + 1;
						}
						if (tableData[k].StartsWith(productEndTag)) {
							end = k;
						}
					}
					string[] productLines = new string[end - start];
					endLine = end - start;

					string newProductLines = "";
					// Get lines between tags
					for (int l = 0; l < endLine; l++) {
						productLines[l] = tableData[start + l];
						newProductLines += productLines[l] + "\n";
					}

					newProductLines = (productTableHeader + newProductLines + productTableFooter);

					string newLines = newCamapaignLines + "\n" + newProductLines;
					File.WriteAllText(dbFilePath, newLines.ToString());
				}
			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.DBFileSearchError, Constants.DatabaseError);
			}
			ReadAllCampaigns();
			ReadAllProducts();

		}

		public bool ProductExists(string id) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Product> objectList = GetProductList();

				for (int i = 0; i < objectList.Count; i++) {
					if (id.Equals(objectList.ElementAt(i).id)) {
						return true;
					}
				}

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.ProductExistsError, Constants.DatabaseError);
			}
			return false;
		}

		public bool CampaignExists(string id) {
			Logger.Log(new StackTrace(), "");

			try {
				List<Campaign> objectList = GetCampaignsList();

				for (int i = 0; i < objectList.Count; i++) {
					if (id.Equals(objectList.ElementAt(i).id)) {
						return true;
					}
				}

			} catch (System.Exception e) {
				throw new CashRegisterException(e, Constants.CampaignExistsError, Constants.DatabaseError);
			}
			return false;
		}

		public void EditCampaign(Campaign campaign, Campaign newCampaign) {
			UpdateCampaign(campaign, newCampaign);
		}
	}
}