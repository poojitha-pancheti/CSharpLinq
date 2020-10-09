using CSharp.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace CSharp.Biz.Tests
{
    
    [TestClass()]
    public class CompanyRepositoryTests
    {
        [TestMethod()]
        public void RetrieveTest()
        {
            var repository = new CompanyRepository();

            var expected = new List<Company>();
            expected.Add(new Company()
            { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" });
            expected.Add(new Company()
            { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" });

            var actual = repository.Retrieve();

            CollectionAssert.AreEqual(expected, actual.ToList());

        }

        [TestClass()]
        public class ComapnyRepositoryTests
        {
            [TestMethod()]
            public void RetrieveAllTest()
            {
                var repository = new CompanyRepository();

                var expected = new List<Company>()
                {
                    { new Company() 
                    {VendorId = 22, CompanyName = "Toy Block Inc", Email = "blocks@abc.com"}},
                    { new Company()
                    {VendorId = 28, CompanyName = "Car Toys", Email = "car@abc.com"} } ,
                    { new Company()
                    { VendorId = 31, CompanyName = "Toys for fun", Email = "fun@abc.com" }},
                    {new Company()
                    {VendorId = 35, CompanyName = "JK Ltd", Email = "jk@jk.com" }}


                };
                var companies = repository.RetrieveAll();
                //Query Syntax 
                //var companyQuery = from v in companies
                //                   where v.CompanyName.Contains("Toy")
                //                   orderby v.CompanyName
                //                   select v;

                //Method Syntax
                //var companyQuery = companies.Where(FilterCompanies)
                //    .OrderBy(OrderCompaniesByName);

                //Lambda Expression
                var companyQuery = companies.Where(c => c.CompanyName.Contains("Toy"))
                    .OrderBy(c=>c.CompanyName);
                CollectionAssert.AreEqual(expected, companyQuery.ToList());
            
            }
            //Methods for Method syntax
            //private bool FilterCompanies(Company c) =>
            //  c.CompanyName.Contains("Toy");

            //private string OrderCompaniesByName(Company c) => c.CompanyName;

        }

        [TestMethod()]
        public void RetrieveWithIteratorTest()
        {
            var repository = new CompanyRepository();

            var expected = new List<Company>()
            {
                {new Company()
                   { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" }},


                {new Company()
                   { VendorId = 2, CompanyName = "XYZ Inc", Email = "xyz@xyz.com"} }


            };
            var vendorIterator = repository.RetrieveWithIterator();
            foreach (var item in vendorIterator)
            {
                Console.WriteLine(item);

            }
            var actual = vendorIterator.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void RetrieveWithKeysTest()
        {
            var repository = new CompanyRepository();

            var expected = new Dictionary<string, Company>()
            {
            {"ABC Corp" , new Company()
                {VendorId = 5, CompanyName = "ABC Corp" , Email = "abc@abc.com" }},
                { "XYZ Inc" , new Company()
                {VendorId = 8, CompanyName = "XYZ Inc", Email = "xyz@xyz.com" }}
                };
            var actual = repository.RetrieveWithKeys();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}

