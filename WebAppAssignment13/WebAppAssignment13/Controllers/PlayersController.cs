using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAssignment13.Models;

namespace WebAppAssignment13.Controllers
{
    public class PlayersController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["PlyersConStr"].ConnectionString;
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader srdr;
        // GET: Players
        public ActionResult Index()
        {

            List<Players> player = new List<Players>();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players");
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    player.Add(
                        new Players
                        {
                            PlayerId = (int)(srdr["PlayerId"]),
                            FirstName = (string)srdr["FirstName"],
                            LastName = (string)srdr["LastName"],
                            JerseyNumber = (int)(srdr["JerseyNumber"]),
                           Position = (string)srdr["Position"],
                            Team = (string)srdr["Team"]
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }

            return View(player);
        }

        // GET: Players/Details/5
        public ActionResult Details(int Id)
        {
            Players player = new Players();
            con = new SqlConnection(conString);
            try
            {
                cmd = new SqlCommand("select * from Players where Id = @id");
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    player.PlayerId = (int)(srdr["PlayerId"]);
                    player.FirstName = (string)srdr["FirstName"];
                    player.LastName = (string)srdr["LastName"];
                    player.JerseyNumber = (int)(srdr["JerseyNumber"]);
                    player.Position = (string)srdr["Position"];
                    player.Team = (string)srdr["Team"];
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

            return View(player);

        }
        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(Players player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("insert into Players values (@id,@fname,@lname,@jno,@pos,@team)");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", player.PlayerId);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jno", player.JerseyNumber);
                cmd.Parameters.AddWithValue("@pos", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }

        }
    

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {

           Players player = new Players();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where Id = @id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    player.PlayerId = (int)(srdr["PlayerId"]);
                    player.FirstName = (string)srdr["FirstName"];
                    player.LastName = (string)srdr["LastName"];
                    player.JerseyNumber = (int)(srdr["JerseyNumber"]);
                    player.Position = (string)srdr["Position"];
                    player.Team = (string)srdr["Team"];
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            //finally { con.Close(); }
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Players player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("update Product set Name=@name, Price=@price where Id=@id");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", player.PlayerId);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jno", player.JerseyNumber);
                cmd.Parameters.AddWithValue("@pos", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally { con.Close(); }
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {

            Players player = new Players();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                   player =
                        new Players
                        {
                            PlayerId = (int)(srdr["PlayerId"]),
                            FirstName = (string)srdr["FirstName"],
                            LastName = (string)srdr["LastName"],
                            JerseyNumber = (int)(srdr["JerseyNumber"]),
                            Position = (string)srdr["Position"],
                            Team = (string)srdr["Team"]
                        };

                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }

            return View(player);
        }

    

    // POST: Players/Delete/5
    [HttpPost]
        public ActionResult Delete(int id,Players player)
        {

            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("delete from Players where PlayerId=@id");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id",player.PlayerId);

                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }

        }
    }
}
