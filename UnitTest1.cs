using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCSharp.Pages;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace SeleniumCSharp
{
    public class ZipCode
    { 
        public string? postCode { get; set; }
        public string? country { get; set; }
        public string? countryAbrebiation { get; set; }
        public string[]? places { get; set; }

    }
    public class Tests : DriverHelper
    {
        //public IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setup");
        }

        
        [Test]
        public void GetItems()
        {
            var url = $"https://api.zippopotam.us/us/94105";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                Stream strReader = response.GetResponseStream();

                if (strReader == null) return;
                StreamReader sr = new StreamReader(strReader);
                string str = sr.ReadToEnd();

                ZipCode? zipCode = JsonSerializer.Deserialize<ZipCode>(str);

                Assert.That(zipCode?.places[0], Is.EqualTo("San Francisco"));

            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); return; }

        }
    }
}