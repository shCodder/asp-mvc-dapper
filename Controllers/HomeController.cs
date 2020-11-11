using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webEducation.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace webEducation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(config.GetConnectionString("DefaultConnection"));
            }
        }

        public IActionResult Index()
        {
            var items = GetUsers();
            return View(items);
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

        private List<User> GetUsers()
        {
            using (var db = Connection)
            {
                var result = db.Query<User>("select * from users").ToList();
                return result;
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Ballance { get; set; }
        public DateTime Created { get; set; }

    }
}
