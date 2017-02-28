using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NumbersToWordsWCFServer {
	[DataContract]
	public struct NTWResponse {
		[DataMember]
		public bool Success;
		[DataMember]
		public string Result;
		[DataMember]
		public string FailureReason;
	}
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class Service1 : IService1 {
		/*
		 * Write a program which converts currency (dollars) from numbers into word presentation.
		 * The maximum number is 999 999 999.
		 * The maximum number of cents is 99.
		 * Separator between dollars and cents is ‘,’ (comma).
		 * 
		 * "0" -> "zero dollars"
		 * "1" -> "one dollar"
		 * "25,10" -> "twenty-five dollars and ten cents"
		 * "0,1" -> "zero dollars and one cent"
		 * "45 100" -> "forty-five thousand one hundred dollars"
		 * "999 999 999,99" -> "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents"
		 */
		private string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
		private string[] tens = {"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
		private string[] elev = {"ten", "eleven", "twelve", "thirteen", "fourteen", "sixteen", "seventeen", "eighteen", "nineteen"};
		private string[] orders = {"", "thousand", "million"};

		public NTWResponse GetWordRepresentation(string value) {
			var ret = new NTWResponse() {FailureReason = "", Success = true, Result = ""};
			try
			{
				var integers = value.Trim().Split(',');
				var dollars = GetIntWords(integers[0],9, "Dollars");
				ret.Result = dollars == "one" ? "one dollar " : $"{dollars} dollars";
				if (integers.Length == 2)
				{
					var cents = GetIntWords(integers[1],2,"Cents");
					if (cents != "")
						ret.Result += cents == "one" ? " and one cent " : $" and {cents} cents";
				}
				ret.Result = ret.Result.Trim();
				return ret;
			}
			catch (Exception ex)
			{
				ret.FailureReason = ex.Message;
				ret.Success = false;
				return ret;				
			}
		}

		private string GetIntWords(string value, int maxCharacters, string part) {
			var words = new List<string>();

			if (value == "0") return "zero";
			value = value.Replace(" ", "");
			if (value.Length == 0) throw new ArgumentException("Input string was empty");

			if (value.Length > maxCharacters) throw new ArgumentException($"{part} part has too many characters");

			var sb = new StringBuilder();
			var len = value.Length;
			var orderIndex = 0;
			for (var i = 0; i<len; ++i)
			{
				var digit = value[len - i - 1] - '0';
				if (digit < 0 || digit > 9) throw new ArgumentException($"{part} part contains illegal character");
				if (i%3 == 0)
				{
					words.Add($"{orders[orderIndex++]} ");
					
					if (len - i - 2 >= 0 && value[len - i - 2] - '0' == 1) // preceeding digit is 1
					{
						words.Add($"{elev[digit]} ");
					}
					else if (len - i - 2 < 0 || !(value[len - i - 2] - '0' == 0 && digit == 0))
					{
						words.Add($"{ones[digit]} ");
					}
				}
				else if (i%3 == 1)
				{
					if (digit < 2) continue;
					words.Add($"{tens[digit]}-");
				}
				else
				{
					if (digit > 0)
					{
						words.Add($"{ones[digit]} hundred ");
					}
				}
			}
			for (var l = words.Count-1; l >= 0; --l)
			{
				sb.Append(words[l]);
			}
			return sb.ToString().Trim();
		}
	}
}
