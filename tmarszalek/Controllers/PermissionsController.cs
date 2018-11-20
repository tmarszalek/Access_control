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
    public class PermissionsController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Uprawnienia";
            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                       "password=strongPASS123;server=localhost;" +
                                       "database=access_controll_database; " +
                                       "connection timeout=30");
            myConnection.Open();
            SqlCommand users_command = new SqlCommand("SELECT * FROM users", myConnection);

            SqlDataReader users_rdr = users_command.ExecuteReader();
            var model1 = new List<User>();
            while (users_rdr.Read())
            {
                var user = new User();
                user.ID = users_rdr["id"].ToString();
                user.Name = users_rdr["user_name"].ToString();
                user.Surname = users_rdr["user_surname"].ToString();
                user.Number = users_rdr["user_number"].ToString();
                model1.Add(user);
            }
           
            myConnection.Close();
            myConnection.Open();
            SqlCommand rooms_command = new SqlCommand("SELECT * FROM rooms", myConnection);

            SqlDataReader rooms_rdr = rooms_command.ExecuteReader();
            var model2 = new List<Room>();
            while (rooms_rdr.Read())
            {
                var room = new Room();
                room.ID = rooms_rdr["id"].ToString();
                room.Name = rooms_rdr["room_name"].ToString();
                model2.Add(room);
            }

            RoomsUsers multi_model = new RoomsUsers();
            multi_model.Users = model1;
            multi_model.Rooms = model2;

            myConnection.Close();

            return View (multi_model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var user_id = collection["user_name"];
                var room_id = collection["user_surname"];
                SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                     "password=strongPASS123;server=localhost;" +
                                     "database=access_controll_database; " +
                                     "connection timeout=30");
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO permissions (user_id, room_id) VALUES ('" + user_id + "','" + room_id + "')", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();

                ViewBag.Message = "Uprawnienie zostało dodane!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
