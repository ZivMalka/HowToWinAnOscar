using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("ViewActors", "Actors", new { UserName = Session["UserName"].ToString() });
            }
            else
            {
                return View();

            }
        }

        [HttpPost]
        public ActionResult Login(Users account)
        {
            var AccountLoggedIn = DBManager.GetUsersCollection().AsQueryable<Users>().SingleOrDefault(x => x.UserName == account.UserName && x.Password == account.Password);
            if (AccountLoggedIn != null)
            {
                ViewBag.Message = "Logged In";
                ViewBag.TriedOnce = "yes";

                Session["UserName"] = account.UserName;
                return RedirectToAction("Index", "Home", new { UserName = account.UserName });

            }
            else
            {
                ViewBag.triedOnce = "Yes";
                return View();

            }
        }

        public ActionResult Index()
        {
            return View();
        }

        //Return all signed users
        public ActionResult ViewUsers()
        {
            var users = DBManager.GetUsersCollection().AsQueryable().ToList();
            return View(users);

        }
        //Return user details
        public ActionResult Details(String id)
        {
            var productId = new ObjectId(id);
            var product = DBManager.GetUsersCollection().AsQueryable<Users>().SingleOrDefault(x => x.ID == productId);
            return View(product);
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        //Create
        [HttpPost]
        public ActionResult Create(Users user)
        {
            try
            {
                DBManager.GetUsersCollection().InsertOne(user);
                return RedirectToAction("ViewUsers");
            }
            catch
            {
                return View();
            }
        }

        //edit user details
        public ActionResult Edit(string id)
        {
            var userId = new ObjectId(id);
            var user = DBManager.GetUsersCollection().AsQueryable<Users>().SingleOrDefault(x => x.ID == userId);
            return View(user);
        }

        //edit function for users
        [HttpPost]
        public ActionResult Edit(string id, Users user)
        {
            try
            {
                var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<Users>.Update
                        .Set("UserName", user.UserName)
                        .Set("Password", user.Password)
                        .Set("Email", user.Email)
                        .Set("PhoneNo", user.PhoneNo)
                        .Set("Address", user.Address);
                var result = DBManager.GetUsersCollection().UpdateOne(filter, update);
                return RedirectToAction("ViewUsers");
            }
            catch
            {
                return View();
            }
        }

        //delete user from database
        public ActionResult Delete(string id)
        {
            var userId = new ObjectId(id);
            var user = DBManager.GetUsersCollection().AsQueryable<Users>().SingleOrDefault(x => x.ID == userId);
            return View(user);
        }

        //delete function for users
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                DBManager.GetUsersCollection().DeleteOne(Builders<Users>.Filter.Eq("_id", ObjectId.Parse(id)));
                return RedirectToAction("ViewUsers");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Homepage()
        {
            return View();
        }
    }
}