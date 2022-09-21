using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace creation
{
    public partial class Login : System.Web.UI.Page
    {
        //private object configurationManager;
        //private object checkuser;
        //private object checkMotDePasse;
        ////string connectionString = GetConnectionString();
        GDLEntities1 db = new GDLEntities1();
        //private object ViewBag;

       

        protected void Page_Load(object sender, EventArgs e)
        {
            T_Utilisateur u1 = new T_Utilisateur();
        }

        

        private void RedirectToAction(string v)
        {
            throw new NotImplementedException();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From T_Utilisateur where Login = '" + TextBox1.Text + "' and MotDePasse = '" + TextBox2.Text+"'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                Session["Login"] = TextBox1.Text;
                while (dr.Read())
                {
                    Session["idUser"] = dr["idUser"].ToString();
                        }
                Response.Redirect("Acceuil.aspx");
            }
            else
            
                
            {
                    Response.Write("<script>alert('utilisateur ne pas inscrit !')</script>");
                
                //Response.Write( "utilisateurnepasinscrit");
                
            }
           
            con.Close();
        }

       

       

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }
    }
}   