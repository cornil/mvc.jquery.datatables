﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.JQuery.Datatables.Example.Controllers
{
    public class HomeController : Controller
    {
        private static List<User> _users;
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public enum PositionTypes
        {
            Engineer,
            Tester,
            Manager
        }

        public DataTablesResult<UserView> GetUsers(DataTablesParam dataTableParam)
        {
            var users = Users().AsQueryable();

            return DataTablesResult.Create(users, dataTableParam, user => new UserView()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Position = user.Position == null ? "" : user.Position.ToString(),
                Hired = user.Hired
            });
        }
        public DataTablesResult GetUsersUntyped(DataTablesParam dataTableParam)
        {
            var users = Users();

            return DataTablesResult.Create(users, dataTableParam);
        }

        private static List<User> Users()
        {
            var r = new Random();
            var domains = "gmail.com,yahoo.com,hotmail.com".Split(',').ToArray();
            var positions = new List<PositionTypes?> { null, PositionTypes.Engineer, PositionTypes.Tester, PositionTypes.Manager };
            return _users ?? (_users = new List<User>
                (
                Enumerable.Range(1, 100).Select(i =>
                                                new User()
                                                {
                                                    Id = i,
                                                    Email = "user" + i + "@" + domains[i%domains.Length],
                                                    Name = "User" + i,
                                                    Position = positions[i%positions.Count],
                                                    Hired = DateTimeOffset.UtcNow.AddDays(-1 * 365 * 3 * r.NextDouble())
                                                })
                ));
            return _users;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public HomeController.PositionTypes? Position { get; set; }

        public DateTimeOffset Hired { get; set; }
    }

    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public DateTimeOffset Hired { get; set; }
    }
}