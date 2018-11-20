using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Mvc.Ajax;
using tmarszalek.Models;

namespace tmarszalek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Kontrola dostępu - panel główny";
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

            var Msg = TempData["Message"] as string;
            ViewBag.Message = Msg;
            return View(multi_model);
        }
        protected void permissionspage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users/Index.cshtml");
            Server.Transfer("Users/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            var user_id = collection["user_id"];
            var room_id = collection["room_id"];
            var action = collection["action"];
            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                    "password=strongPASS123;server=localhost;" +
                                    "database=access_controll_database; " +
                                    "connection timeout=30");
            myConnection.Open();
            SqlCommand perm_check = new SqlCommand("SELECT COUNT(*) FROM permissions WHERE user_id = '"+ user_id + "' AND room_id = " +room_id, myConnection);
            var result = perm_check.ExecuteScalar().ToString();
            myConnection.Close();

            if (result != null){
                myConnection.Open();
                if (action == "1")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO registry (user_id, room_id, active) VALUES ('" + user_id + "','" + room_id + "','" + action + "')", myConnection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand command = new SqlCommand("UPDATE registry SET active = 0 WHERE user_id = '" + user_id + "' AND room_id = '" + room_id + "'", myConnection);
                    command.ExecuteNonQuery();
                }
                TempData["Message"] = "Akcja została zarejestrowana";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Użytkownik nie posiada uprawnienia do pomieszczenia!";
            return RedirectToAction("Index");

        }
    }

}
