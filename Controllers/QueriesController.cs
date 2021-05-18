using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheGStore;
using TheGStore.Models;

namespace TheGStore.Controllers
{
    public class QueriesController : Controller
    {
        private const string S1_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S1.sql";
        private const string S2_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S2.sql";
        private const string S3_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S3.sql";
        private const string S4_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S4.sql";
        private const string S5_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S5.sql";
        private const string S6_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\S6.sql";

        private const string A1_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\A1.sql";
        private const string A2_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\A2.sql";
        private const string A3_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\A3.sql";

        private const string T1_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\T1.sql";
        private const string T2_PATH = @"C:\Users\PoixoN\Desktop\БД Лаба 2\Queries\T2.sql";

        public IConfiguration Configuration { get; }
        private readonly TheGStoreDbContext _context;

        public QueriesController(TheGStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index(int errorCode)
        {
            var customers = _context.Customers.Select(c => c.FirstName).Distinct().ToList();
            if (errorCode == 1)
            {
                ViewBag.ErrorFlag = 1;
                ViewBag.PriceError = "Введіть коректну вартість";
            }
            if (errorCode == 2)
            {
                ViewBag.ErrorFlag = 2;
                ViewBag.ProdNameError = "Поле необхідно заповнити";
            }

            var empty = new SelectList(new List<string> { "--Пусто--" });
            var anyCusts = _context.Customers.Any();
            var anyDevs = _context.Developers.Any();

            ViewBag.DevIds = anyDevs ? new SelectList(_context.Developers, "Id", "Id") : empty;
            ViewBag.DevNames = anyDevs ? new SelectList(_context.Developers, "Name", "Name") : empty;
            ViewBag.CustNames = anyCusts ? new SelectList(customers) : empty;
            ViewBag.CustEmails = anyCusts ? new SelectList(_context.Customers, "Email", "Email") : empty;
            ViewBag.CustLastNames = anyCusts ? new SelectList(_context.Customers, "LastName", "LastName") : empty;
            ViewBag.Countries = _context.Countries.Any() ? new SelectList(_context.Countries, "Name", "Name") : empty;
            ViewBag.GenreNames = _context.Genres.Any() ? new SelectList(_context.Genres, "Name", "Name") : empty;
            ViewBag.StatusNames = _context.Statuses.Any() ? new SelectList(_context.Statuses, "Name", "Name") : empty;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S1_PATH);
            query = query.Replace("X", "N\'" + queryModel.StatusName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S1";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.CountryNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = Resourses.ERROR_CustomersNotFound;
                            }
                        }
                    }
                    else
                    {
                        queryModel.ErrorFlag = 1;
                        queryModel.Error = Resourses.ERROR_AvgPrice;

                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S2_PATH);
            query = query.Replace("X", "N\'" + queryModel.DevName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S2";
            queryModel.CustLastNames = new List<string>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustLastNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_CustomersNotFound;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S3_PATH);
            query = query.Replace("X", "N\'" + queryModel.DevName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S3";
            queryModel.ProdPrices = new List<decimal>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.StatusNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_GameNotExists;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery4(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S4_PATH);
            query = query.Replace(" X", "N\'" + queryModel.DevName.ToString() + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S4";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        queryModel.MaxPrice = Convert.ToDecimal(result);
                    }
                    else
                    {
                        queryModel.ErrorFlag = 1;
                        queryModel.Error = Resourses.ERROR_AvgPrice;
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery5(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(S5_PATH);
                query = query.Replace("X", queryModel.Price.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryId = "S5";
                queryModel.DevNames = new List<string>();

                using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.DevNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = Resourses.ERROR_DevNotExists;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery6(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S6_PATH);
            query = query.Replace("X", "N\'" + queryModel.GenreName + "\'");
            query = query.Replace("Y", queryModel.Price.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "S6";
            queryModel.CustNames = new List<string>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_DevNotExists;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A1_PATH);
            query = query.Replace("K", queryModel.DevId.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A1";
            queryModel.CountryNames = new List<string>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CountryNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_CountryNotExists;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A2_PATH);
            query = query.Replace("Y", "N\'" + queryModel.CustEmail.ToString() + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A2";
            queryModel.CustLastNames = new List<string>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustLastNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_CustomersNotFound;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        public IActionResult AdvancedQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A3_PATH);
            query = query.Replace("Y", "N\'" + queryModel.CustName.ToString() + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');
            queryModel.QueryId = "A3";
            queryModel.CustLastNames = new List<string>();
            queryModel.CustEmails = new List<string>();

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustLastNames.Add(reader.GetString(0));
                            queryModel.CustEmails.Add(reader.GetString(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = Resourses.ERROR_CustomersNotFound;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        public IActionResult TeacherQuery1(Query queryModel)
        {
            throw new NotImplementedException();
        }

        public IActionResult TeacherQuery2(Query queryModel)
        {
            throw new NotImplementedException();
        }

        public IActionResult Result(Query queryResult)
        {
            return View(queryResult);
        }
    }
}