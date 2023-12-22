using CashRegister.Log;
using CashRegister.Controllers;
using System;
using System.Diagnostics;

namespace CashRegister.Models {

    // Manages the CashRegister reciept functionallity
    public class Receipt : CashRegisterObject {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		private static DBHandler model = DBHandler.GetInstance();
        private string receiptIndexPath { get; } = "../../../ReceiptIndex.txt";
        private static int receiptCount { get; set; }
		private static Receipt? instance;

        public Receipt() {

		}

        // Provides a single handle to the Receipt class
        public static Receipt GetInstance() {

            if (instance != null) {
                return instance;
            }
            instance = new Receipt();
            return instance;

        }

        // Receipt. Receives customer purshaced products (customerProducts) which are
        // matched against the Product database/file. The customer receipt is calculated
        // and displayed for the Cashier and also printed to a text file called RECEIPT_<Todays date>.txt. 
        public List<string> CreateReceipt(List<Product> customerProducts) {
			Logger.Log(new StackTrace(), "");

			// Creates a file to save an incremented index number which will serve as a unique
			// identifier for every new reciept.             
			if (!File.Exists(receiptIndexPath)) {

                File.WriteAllText(receiptIndexPath, "0");
                receiptCount = 0;

            } else {

                string[] lines = File.ReadAllLines(receiptIndexPath);
                string count = lines[0];
                receiptCount = int.Parse(count);

            }

            string receiptDevider      = "-------------------------------------------------------------";
            string headerTextFormat = "{0,-12}{1,49}\n";
            string headerText = string.Format(headerTextFormat, "KVITTO #" + receiptCount, DateTime.Now);

            double finalTotal = 0;
            string footerFormat = "{0,-7}{1,54}";

            string tableData = "";

            string headerFormat = "{0,-5}{1,-5}{2,-12}{3,11}{4,14}{5,14}\n";
            string tableFormat =  "{0,-5}{1,-5}{2,-12}{3,11}{4,14}{5,14}\n";

            // Customer-bought products are matched against the product database/file to get the price in
            // order to calculate the total price which will be displayed on a recept in a receipt file
            for (int i = 0; i < customerProducts.Count; i++) {

                Product product = customerProducts.ElementAt(i);

                // Get product info from database/file
                Product listItem = (Product)model.GetProductFromDB(product.id);

                //Checks if product has a campaign
                //Applying new price if the given product ID matches with campaign.productID

                string newPrice = "";
                string campaignId = "";
                newPrice = listItem.CalculatePrice();

                List<Campaign> campaigns = DBHandler.GetCampaignsByProductId(listItem.id);     
                
                foreach (Campaign campaign in campaigns) {

                    campaignId += " " + campaign.id;

                }


                // Calculate the receipt                                
                double price = double.Parse(listItem.price);
                int amount = int.Parse(product.amount);

                double result = amount * price;
                double total = 0;

                total += result;

                finalTotal = total + finalTotal;

                string longNamePart1;
                //string longNamePart2;
                if (listItem.name.Length > 12) {

                    longNamePart1 = listItem.name = listItem.name[..12];
                    //longNamePart2 = listItem.name = listItem.name.Substring(12);
                    listItem.name = longNamePart1;

                }
                
                // Append and Format table data 
                tableData += string.Format(tableFormat, product.id, product.amount, listItem.name, campaignId, 
                                           total.ToString("####.00") + " Kr", price.ToString("###.00") + " Kr" + "/" + listItem.unit);
            }

            string header = string.Format(headerFormat, "Id", "Nr#", "Namn", "Campaign", "Totalt", "Pris");
            string output = header + receiptDevider + "\n" + tableData;

            output = headerText + "                                                             \nARTIKLAR:                                                    \n" + output;


            string footer = string.Format(footerFormat, "TOTAL", finalTotal.ToString("####.00") + " Kr");
            output += receiptDevider + "\n" + footer + "\n                                                             \n                                                             \n                                                             \n";

            string newOutput = "";

			try {
                newOutput = output.Replace(",", ".");

            } catch (System.Exception e) {

                Console.WriteLine(e.StackTrace.ToString());
            }

			Logger.Log(new StackTrace(), "");

			// Saves the reciept to file
			DBHandler.WriteReceiptToFile(newOutput);

            // Increment receipt count
            UpdateReceiptCount();

            string[] toList = newOutput.Split('\n');
            List<string> outputList = new List<string>();  
            for (int i = 0; i < toList.Length; i++) {
                outputList.Add(toList[i]);
			}

			return outputList;
        }

        // Increments the reciept count
        private void UpdateReceiptCount() {

            receiptCount++;
            File.WriteAllText(receiptIndexPath, "" + receiptCount);

        }
    }
}
