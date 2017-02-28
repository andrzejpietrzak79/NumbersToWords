using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumbersToWordsWCFServer;

namespace NumbersToWordsTest {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			var service = new Service1();
			var result = service.GetWordRepresentation("0").Result;
			Assert.IsTrue(result == "zero dollars", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod2() {
			var service = new Service1();
			var result = service.GetWordRepresentation("1").Result;
			Assert.IsTrue(result == "one dollar", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod3() {
			var service = new Service1();
			var result = service.GetWordRepresentation("25,10").Result;
			Assert.IsTrue(result == "twenty-five dollars and ten cents", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod4() {
			var service = new Service1();
			var result = service.GetWordRepresentation("0,1").Result;
			Assert.IsTrue(result == "zero dollars and one cent", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod5() {
			var service = new Service1();
			var result = service.GetWordRepresentation("45 100").Result;
			Assert.IsTrue(result == "forty-five thousand one hundred dollars", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod6() {
			var service = new Service1();
			var result = service.GetWordRepresentation("999 999 999,99").Result;
			Assert.IsTrue(result == "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod7() {
			var service = new Service1();
			var result = service.GetWordRepresentation("111,13").Result;
			Assert.IsTrue(result == "one hundred eleven dollars and thirteen cents", $"Result was: '{result}'");
		}
		[TestMethod]
		public void TestMethod8() {
			var service = new Service1();
			var result = service.GetWordRepresentation("101,00").Result;
			Assert.IsTrue(result == "one hundred one dollars", $"Result was: '{result}'");
		}
	}
}
