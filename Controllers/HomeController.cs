using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fetchDataUsingAjax.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace fetchDataUsingAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly SqlConnection con;
        readonly IConfigurationRoot configuration;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<PersonAddress> PersonAddresses = new List<PersonAddress>();
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            con = new SqlConnection();
            configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
            con.ConnectionString = configuration["ConnectionString"];
        }

        public List<PersonAddress> fetchTableData()
        {
            if(PersonAddresses.Count > 0)
            {
                PersonAddresses.Clear();
            }
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT TOP (100) [AddressID],[AddressLine1],[City],[StateProvinceID],[PostalCode] FROM [AdventureWorks2012].[Person].[Address]";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                PersonAddresses.Add(new PersonAddress()
                {
                    AddressId = dr["AddressID"].ToString(),
                    Address = dr["AddressLine1"].ToString(),
                    City = dr["City"].ToString(),
                    ProvinceId = dr["StateProvinceID"].ToString(),
                    PostalCode = dr["PostalCode"].ToString()
                });
            }
            con.Close();
            return PersonAddresses;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
