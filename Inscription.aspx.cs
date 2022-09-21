using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace creation
{
    public partial class Inscription : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



            //    GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "Select * From T_Utilisateur where Login = '" + TextBox4.Text + "'";


                cmd1.CommandText = quer;
                cmd1.Connection = con;
                SqlDataReader dr = cmd1.ExecuteReader();
                bool test = true;

                if (dr.HasRows == false)
                {

                    test = false;

                }
                dr.Close();
                if (test == false)
                {

                    
                    string Nom = TextBox2.Text;
                    string Prenom = TextBox3.Text;
                    string Login = TextBox4.Text;
                    string MotDePasse = TextBox5.Text;
                    T_Utilisateur User = new T_Utilisateur();

                    User.Nom = Nom;
                    User.Prenom = Prenom;
                    User.Login = Login;
                    User.MotDePasse = MotDePasse;
                    db.T_Utilisateur.Add(User);

                    db.SaveChanges();

                    ModelState.Clear();
                    GridView1.DataBind();

                }
            else
            {
                Response.Write("<script>alert('le Login " + TextBox4.Text + " est existant !')</script>");
            }
            //}
            con.Close();
        }

        

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
    
}