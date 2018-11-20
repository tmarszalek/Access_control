using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using tmarszalek.Models;
using System.Collections;

namespace tmarszalek.Controllers
{
    public class RoomsController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Pomieszczenia";
            SqlConnection myConnection = new SqlConnection("user id=sa;" +
                                     "password=strongPASS123;server=localhost;" +
                                     "database=access_controll_database; " +
                                     "connection timeout=30");
            myConnection.Open();
            //SqlCommand command = new SqlCommand("SELECT r.*, COUNT(DISTINCT reg.room_id) as people FROM rooms as r LEFT JOIN registry as reg ON r.id = reg.room_id", myConnection);
            SqlCommand command = new SqlCommand("SELECT * FROM rooms", myConnection);
            SqlDataReader rdr = command.ExecuteReader();

            var model = new List<Room>();
            while (rdr.Read())
            {
                var room = new Room();
                room.ID = rdr["id"].ToString();
                room.Name = rdr["room_name"].ToString();
                //room.Free = (int)rdr["people"];
                room.Places = (int)rdr["number_of_places"];
                room.Hours = rdr["access_hours"].ToString();
                model.Add(room);
            }
            myConnection.Close();

            return View(model);
        }
    }
}
