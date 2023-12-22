
using CashRegister.Controllers;
using CashRegister.Log;
using System.Diagnostics;

namespace CashRegister.Models {

    public class Product : IComparable {

        public string hasCampaign { set; get; } = "false";

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		public string id { get; set; }
		public string name { get; set; }
		public string unit { get; set; }
		public string price { get; set; }
		public string productCampaignPrice { get; set; } = "0";
		public string amount { get; set; } = "0";

		public Product() {


		}

        // For receipt handeling
        public Product(string id, string name, string price, string unit, string hasCampaign) {

            this.id = id;
            if (name.Length > 12) {
				name = name.Substring(0, 12);
			}
            this.name = name;
            this.price = price;
            this.unit = unit;
            this.hasCampaign = hasCampaign;
        }

        // For registering products
        public Product(string id, string amount) {

            this.id = id;
            this.amount = amount;
        }


        public override string ToString() {

            string ret = GetType().FullName + " [ \n\tid = " + id + "\n\tname = " + name + "\n\tprice = " + price + "\n\tunit = " + unit + "\n\tamount = " + amount + "\n]";

            return ret;

        }

        public string CalculatePrice() {
			Logger.Log(new StackTrace(), "");

			List<Campaign> campaign = DBHandler.GetCampaignsByProductId(this.id);

            foreach (Campaign item in campaign) {

                if (double.Parse(item.newPrice) < double.Parse(this.price)) {
                
                    if (item.IsActive()) {

                        this.price = "" + double.Parse(item.newPrice);

                    }

                }
            }
            return "" + this.price;
        }

        public int CompareTo(Object? obj) {
            return System.Convert.ToInt32(id.CompareTo(((Product)obj).id));
        }

	}
}
