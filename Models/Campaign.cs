

using CashRegister.Log;
using System.Diagnostics;

namespace CashRegister.Models {

    public class Campaign : IComparable {

		private static CashRegisterLogger Logger = CashRegisterLogger.GetInstance();
		public string id { get; set; }
		public string name { get; set; }
		public string productId { get; set; }
		public string startDate { get; set; }
		public string endDate { get; set; }
		public string newPrice { get; set; }
		public bool isActive { get; set; }

		public Campaign() {

        }

        public Campaign(string id, string name, string productId, string startDate, string endDate, string newPrice) {

            this.id = id;
            this.name = name;
            this.productId = productId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.newPrice = newPrice;

        }

        public override string ToString() {
            _ = GetType().FullName + "[ " +
            "\n\tid = " + id +
            "\n\tname = " + name +
            "\n\tproductId = " + productId +
            "\n\tstartDate = " + startDate +
            "\n\tendDate = " + endDate +
            "\n\tnewPrice = " + newPrice +
            "\n]";

            return "";

        }

        public bool IsActive() {
			Logger.Log(new StackTrace(), "");

			DateTime today = DateTime.Now;
            DateTime start = DateTime.Parse(this.startDate);
            DateTime end = DateTime.Parse(this.endDate);

            bool result = false;

            if(today == start) {
                result = true;
            } else if (today >= end) { 
                result = false;
            } else if (start < today && today < end) {
                result = true;
            }

            return result;
        }

        public int CompareTo(object? obj) {
            return System.Convert.ToInt32(id.CompareTo(((Campaign)obj).id));
        }

    }
}
