using CineMaster.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineMaster.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\damian_pileckas\git\nt1\CineMaster2\CineMaster\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        private Database1Entities db = new Database1Entities();

        public ActionResult Index()
        {
            
            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null) { 
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout2.cshtml";
                return view;
            }
            else
            {
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout.cshtml";
                return view;

            }

        }

        public ActionResult Registro()
        {



            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null)
            {

                return RedirectToAction("Create", "Cliente", new { area = "" });
            }
            else
            {
                ViewBag.Message = "Your contact page.";
                return RedirectToAction("Create", "Cliente", new { area = "" });
            }
            //return DependencyResolver.Current.GetService<ClientesController>().Create(cliente);
        }

        public ActionResult Contact()
        {
           
            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null)
            {
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout2.cshtml";
                return view;
            }
            else
            {
                var view = View();
                return view;
            }


        }

        public ActionResult Compra()
        {
            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null)
            {
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout2.cshtml";
                return view;
            }
            else
            {
                var view = View();
                return view;
            }

        }

        public ActionResult LoginOFF()
        {
            Session["id"] = null;
            Session["nom"] = null;
            Session["dni"] = null;
            Session["tarjeta"] = null;
            Session["contra"] = null;
            Session["email"] = null;



            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null)
            {

                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });

            }
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Dni_Cliente,Contrasenia")] Cliente cliente)
        {
            //validar contraseña como con el dni 
            String sql = "Select * from Cliente where Dni_Cliente ='" + cliente.Dni_Cliente + "' and Contrasenia = '" + cliente.Contrasenia + "' ";
            con.Open();
            SqlCommand command = new SqlCommand(sql, con);

            SqlDataReader leer = command.ExecuteReader();
            

            if (leer.Read())
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable tabla = new DataTable();
                da.Fill(tabla);
                Session["id"] = tabla.Rows[0]["Cliente_ID"].ToString();
                Session["nom"] = tabla.Rows[0]["Nombre_Cliente"].ToString();
                Session["dni"] = cliente.Dni_Cliente;
                Session["tarjeta"] = tabla.Rows[0]["Nro_Tarjeta"].ToString();
                Session["contra"] = cliente.Contrasenia;
                Session["email"] = tabla.Rows[0]["Email"].ToString();

               
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                return View("Error");
            }
        }
    }
}