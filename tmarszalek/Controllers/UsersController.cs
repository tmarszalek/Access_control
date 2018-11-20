using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections;
using tmarszalek.Models;

namespace tmarszalek.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Użytkownicy";

            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                       "password=strongPASS123;server=localhost;" +
                                       "database=access_controll_database; " +
                                       "connection timeout=30");
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM users", myConnection);
            SqlDataReader rdr = command.ExecuteReader();
            var model = new List<User>();
            while(rdr.Read()){
                var user = new User();
                user.ID = rdr["id"].ToString();
                user.Name = rdr["user_name"].ToString();
                user.Surname = rdr["user_surname"].ToString();
                user.Number = rdr["user_number"].ToString();
                model.Add(user);
            }
            myConnection.Close();
            return View (model);
        }

        public ActionResult Details(int id)
        {
            return View ();
        }

        public ActionResult Create()
        {
            @ViewData["Text"] = "Dodawanie użytkownika";
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try {
                var name = collection["user_name"];
                var surname = collection["user_surname"];
                var number = collection["user_number"];
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                     "password=strongPASS123;server=localhost;" +
                                     "database=access_controll_database; " +
                                     "connection timeout=30");
                myConnection.Open();
                SqlCommand command = new SqlCommand(@"INSERT INTO users (user_name, user_surname, user_number) VALUES ('"+name+"','"+surname+"','"+number+"')", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
        
        public ActionResult Edit(int id)
        {
            ViewData["Text"] = "Edycja użytkownika";

            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                       "password=strongPASS123;server=localhost;" +
                                       "database=access_controll_database; " +
                                       "connection timeout=30");
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE id = " + id, myConnection);
            SqlDataReader rdr = command.ExecuteReader();
            var model = new List<User>();
            while (rdr.Read())
            {
                var user = new User();
                user.ID = rdr["id"].ToString();
                user.Name = rdr["user_name"].ToString();
                user.Surname = rdr["user_surname"].ToString();
                user.Number = rdr["user_number"].ToString();
                model.Add(user);
            }
            myConnection.Close();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var name = collection["user_name"];
                var surname = collection["user_surname"];
                var number = collection["user_number"];
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                     "password=strongPASS123;server=localhost;" +
                                     "database=access_controll_database; " +
                                     "connection timeout=30");
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE users SET user_name = '" + name + "', user_surname = '"+ surname+"', user_number = '"+number+"' WHERE id = " + id, myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                   "password=strongPASS123;server=localhost;" +
                                   "database=access_controll_database; " +
                                   "connection timeout=30");
            myConnection.Open();
            SqlCommand command = new SqlCommand(@"DELETE FROM users WHERE id = " + id, myConnection);
            command.ExecuteNonQuery();
            myConnection.Close();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try {
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}