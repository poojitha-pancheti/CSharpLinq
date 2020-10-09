using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.Biz;
using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Common;
using System.Linq;

namespace CSharp.Biz.Tests
{
    [TestClass()]
    public class CompanyTests
    {

        [TestMethod()]
        public void PlaceOrderTest()
        {

            var vendor = new Company();
            var product = new Collections(1, "Saw", "");
            var expected = new OperationResult<bool>(true,
                "Order from Acme, Inc\r\nProduct: Saw\r\nQuantity: 12" +
                                     "\r\nInstructions: standard delivery");


            var actual = vendor.PlaceOrder(product, 12);


            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void ToStringTest()
        {

            var vendor = new Company();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";
            var expected = "Vendor: ABC Corp (1)";


            var actual = vendor.ToString();


            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            var CompanyRepository = new CompanyRepository();
            var companiesCollection = CompanyRepository.Retrieve();

            var expected = new List<string>()
            {"Message sent : Important message for: ABC Corp" ,
            "Message sent : Important message for: XYZ Inc"};

            var companies = companiesCollection.ToList();
            Console.WriteLine(companies.Count);

            var actual = Company.SendEmail(companies, "Text message");

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendEmailTestArray()
        {
            var CompanyRepository = new CompanyRepository();
            var companiesCollection = CompanyRepository.Retrieve();

            var expected = new List<string>()
            {"Message sent : Important message for: ABC Corp" ,
            "Message sent : Important message for: XYZ Inc"};

            var companies = companiesCollection.ToArray();

            Console.WriteLine(companies.Length);

            var actual = Company.SendEmail(companies, "Text message");

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendEmailTestDictationary()
        {
            var CompanyRepository = new CompanyRepository();
            var companiesCollection = CompanyRepository.Retrieve();

            var expected = new List<string>()
            {"Message sent : Important message for: ABC Corp" ,
            "Message sent : Important message for: XYZ Inc"};

            var companies = companiesCollection.ToDictionary(v => v.CompanyName);

            Console.WriteLine(companies.Values);

            var actual = Company.SendEmail(companies.Values, "Text message");

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
