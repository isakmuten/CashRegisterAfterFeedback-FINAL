
using System.Net.Http.Headers;

namespace CashRegister.Views {


	public class ColorScheme {
		static int colorScheme = int.Parse(System.Environment.GetEnvironmentVariable("CashRegisterColorScheme"));

		public static ConsoleColor[] GetColorScheme() {

			ConsoleColor[] colors = new ConsoleColor[3];

			if (colorScheme == Constants.RedGray) {
				colors[0] = ConsoleColor.Red;	 
				colors[1] = ConsoleColor.Red;    
				colors[2] = ConsoleColor.Gray;   
			} else if (colorScheme.Equals(Constants.YellowGray)) {
				colors[0] = ConsoleColor.Yellow; 
				colors[1] = ConsoleColor.Yellow;    
				colors[2] = ConsoleColor.DarkGray;    
			} else if (colorScheme.Equals(Constants.GreenGray)) {
				colors[0] = ConsoleColor.DarkGreen; 
				colors[1] = ConsoleColor.DarkGreen;    
				colors[2] = ConsoleColor.Gray;    
			}

			return colors;
		}
	}
}