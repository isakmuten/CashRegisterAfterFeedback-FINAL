namespace CashRegister.Models {

    public class CashRegisterObject /*: IComparable */{

        // Common
        public string id { get; set; }
        public string name { get; set; }

        // Product (Id, Name, )
        public string unit { get; set; }
        public string price { get; set; }

        // Register products (Id, Amount)
        public string productCampaignPrice { get; set; } = "0";
        public string amount { get; set; } = "0";

        // Campaign
        public string productId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string newPrice { get; set; }
        public bool isActive { get; set; }


		public CashRegisterObject() { }

		// Campaign
		public CashRegisterObject(string id, string name, string productId, string startDate, string endDate, string newPrice) {

            this.id = id;
            this.name = name;
            this.productId = productId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.newPrice = newPrice;

        }

        // Product
        public CashRegisterObject(string id, string name, string amount, string unit, string price) {

            this.id = id;
            this.name = name;
            this.unit = unit;
            this.price = price;
            this.amount = amount;

        }

		//public int CompareTo(object? obj) {
		//	return System.Convert.ToInt32(id.CompareTo(((CashRegisterObject)obj).id) );
		//}
	}
}