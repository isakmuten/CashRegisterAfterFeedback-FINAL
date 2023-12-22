using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Controllers { 

	public abstract class Handler {

		protected Handler() { }

		public abstract void HandleUserInput();	
	}
}
