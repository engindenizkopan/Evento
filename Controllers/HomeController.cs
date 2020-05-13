using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        meet_up db = new meet_up();
        SqlConnection sqlConn = null;
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserPartial()
        {
            return View(db.users.ToList());
        }

        public ActionResult AboutPhoto()
        {
            return View();
        }

        public ActionResult AboutWebDevelopers()
        {
            return View();
        }

        public ActionResult AboutLawCommunity()
        {
            return View();
        }

        [HttpPost]
        public void SaveUser(Users usersModel)
        {
            ConnectionDb();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Users VALUES(@gelenname, @gelensurname, @gelenage, @geleninterests, @gelencity, @gelenusername, @gelenmail, @gelenpassword); ", sqlConn);
            sqlCommand.Parameters.AddWithValue("@gelenname", usersModel.name);
            sqlCommand.Parameters.AddWithValue("@gelensurname", usersModel.surname);
            sqlCommand.Parameters.AddWithValue("@gelenage", usersModel.age);
            sqlCommand.Parameters.AddWithValue("@geleninterests", usersModel.interests);
            sqlCommand.Parameters.AddWithValue("@gelencity", usersModel.city);
            sqlCommand.Parameters.AddWithValue("@gelenusername", usersModel.userName);
            sqlCommand.Parameters.AddWithValue("@gelenmail", usersModel.mail);
            sqlCommand.Parameters.AddWithValue("@gelenpassword", usersModel.password);
            //sqlCommand.Parameters.AddWithValue("@gelenmail",usersModel.password)
            int dr = sqlCommand.ExecuteNonQuery();
            Response.Redirect("#");
        }

        [HttpPost]
        public void SaveEvent(userEvents userEventsModel)
        {
            ConnectionDb();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO userEvents VALUES(@geleneventName, @geleneventDate, @geleneventTime, @geleneventLocation, @geleneventCategory, @geleneventDetails, @geleneventQuota, @gelencurrentQuota); ", sqlConn);
            sqlCommand.Parameters.AddWithValue("@geleneventName", userEventsModel.eventName);
            sqlCommand.Parameters.AddWithValue("@geleneventDate", userEventsModel.eventDate);
            sqlCommand.Parameters.AddWithValue("@geleneventTime", userEventsModel.eventTime);
            sqlCommand.Parameters.AddWithValue("@geleneventLocation", userEventsModel.eventLocation);
            sqlCommand.Parameters.AddWithValue("@geleneventCategory", userEventsModel.eventCategory);
            sqlCommand.Parameters.AddWithValue("@geleneventDetails", userEventsModel.eventDetails);
            sqlCommand.Parameters.AddWithValue("@geleneventQuota", userEventsModel.eventQuota);
            sqlCommand.Parameters.AddWithValue("@gelencurrentQuota", 0);
            int dr = sqlCommand.ExecuteNonQuery();
            Response.Redirect("#");
        }


        [HttpPost]
        public void DeleteParticipant(string itemEventName2)
        {
            ConnectionDb();
            string s1 = Session["name"].ToString().Trim();
            //string s2 = Session["mail"].ToString().Trim();
           
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM userANDevent WHERE userName='"+s1 +"' and eventName='"+itemEventName2+"'", sqlConn);
            SqlCommand sqlUpdate = new SqlCommand("UPDATE [dbo].[userEvents] SET [currentQuota] = [currentQuota] - 1 WHERE[eventName] = '" + itemEventName2 + "'",sqlConn);
            int dr = sqlCommand.ExecuteNonQuery();
            int dr2 = sqlUpdate.ExecuteNonQuery();
            Response.Redirect("#");
        }



        public ActionResult ShowParticipants(userANDevent userANDeventModel, string itemEventName)
        {
            ConnectionDb();
            string s2 = Session["name"].ToString().Trim();
            SqlCommand check = new SqlCommand("SELECT [userName],[eventName] FROM [dbo].[userANDevent] where eventName='" + itemEventName + "' and userName='"+s2+"'", sqlConn);
            SqlDataReader readDB2 = check.ExecuteReader();
            if (!readDB2.HasRows)
            {
                readDB2.Close();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO userANDevent VALUES(@gelenuserName, @geleneventName); ", sqlConn);
                SqlCommand sqlUpdate = new SqlCommand("UPDATE [dbo].[userEvents] SET [currentQuota] = [currentQuota] + 1 WHERE[eventName] = '" + itemEventName + "'", sqlConn);
                sqlCommand.Parameters.AddWithValue("@gelenuserName", Session["name"].ToString());
                sqlCommand.Parameters.AddWithValue("@geleneventName", itemEventName);
                int dr = sqlCommand.ExecuteNonQuery();
                int dr2 = sqlUpdate.ExecuteNonQuery();
            }
            else
            {
                readDB2.Close();
            }




            SqlCommand cmnd = new SqlCommand("SELECT [userName],[eventName] FROM [dbo].[userANDevent] where eventName='"+ itemEventName+"'", sqlConn);
            cmnd.CommandType = System.Data.CommandType.Text;
            cmnd.Connection = sqlConn;

            //string temp = "";
            SqlDataReader readDB = cmnd.ExecuteReader();
            
            List<userANDevent> userANDeventModelList = new List<userANDevent>();
            while (readDB.Read())
            {
                userANDevent userANDeventModels = new userANDevent();

                userANDeventModels.userName = readDB["userName"].ToString();
                userANDeventModels.eventName = readDB["eventName"].ToString();
                userANDeventModelList.Add(userANDeventModels);
                //temp += readDB["eventName"].ToString();
                //temp += readDB["eventDate"].ToString();
                //temp += readDB["eventTime"].ToString();
                //temp += readDB["eventLocation"].ToString();
                //temp += readDB["eventCategory"].ToString();
                //temp += readDB["eventDetails"].ToString();
                //temp += "<br/>";
            }
            readDB.Close();
            return View(userANDeventModelList);
        }

        [HttpPost]
        public ActionResult LogoutUser()
        {
            Session["mail"] = null;
            Session["userName"] = null;
            return View("Index");
        }

        public ActionResult ShowEvents()
        {
            ConnectionDb();
            SqlCommand cmnd = new SqlCommand("SELECT [eventName],[eventDate],[eventTime],[eventLocation],[eventCategory],[eventDetails],[eventQuota],[currentQuota] FROM[dbo].[userEvents]", sqlConn);
            cmnd.CommandType = System.Data.CommandType.Text;
            cmnd.Connection = sqlConn;

            string temp = "";
            SqlDataReader readDB = cmnd.ExecuteReader();
            List<userEvents> eventsModelList = new List<userEvents>();
            while (readDB.Read())
            {
                userEvents eventsModel = new userEvents();

                eventsModel.eventName= readDB["eventName"].ToString();
                eventsModel.eventDate = readDB["eventDate"].ToString();
                eventsModel.eventTime = readDB["eventTime"].ToString();
                eventsModel.eventLocation = readDB["eventLocation"].ToString();
                eventsModel.eventCategory = readDB["eventCategory"].ToString();
                eventsModel.eventDetails = readDB["eventDetails"].ToString();
                eventsModel.eventQuota = (int)readDB["eventQuota"];
                eventsModel.currentQuota = (int)readDB["currentQuota"];
                eventsModelList.Add(eventsModel);
             
            }
            string lbl_test = temp;
            return View(eventsModelList);
        }

        public void LoginUser(Users usersModel)
        {
            Session["ActiveUser"] = null;
            ConnectionDb();
            SqlCommand sqlCommand = new SqlCommand("Select * from Users WHERE mail = @mail and password = @password", sqlConn);
            sqlCommand.Parameters.AddWithValue("@mail", usersModel.mail);
            sqlCommand.Parameters.AddWithValue("@password", usersModel.password);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            if (dr.Read())
            {
                //Session["mail"] = usersModel.mail;
                string mail = usersModel.mail;
                string name = dr["name"].ToString();
                //Response.Redirect("../Shared/userLayout");
                Session.Add("mail", mail);
                Session.Add("name", name);
                Response.Redirect("getSession");
            }
            else
            {
                Response.Redirect("#");
            }
        }
        public ActionResult getSession()
        {
            if(Session["mail"] == null && Session["name"] == null)
                return View();

            string mail = Session["mail"].ToString();
            string name = Session["name"].ToString();
            ViewBag.mail = mail;
            ViewBag.name = name;
            return View();
        }
        public void ConnectionDb()
        {
            string connStr = "Server=ASUS\\SQLEXPRESS;Database=meet-up;Integrated Security=True";
            sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
        }

        public ActionResult AfterLogin()
        {
            return View();
        }
    }
}