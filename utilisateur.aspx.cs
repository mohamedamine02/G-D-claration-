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
    public partial class WebForm3 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        private bool IsPost;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == "admin")
                {
                    structure.Visible = true;
                }
                else
                {
                    structure.Visible = false;

                }
                //Label1.Text = Session["Login"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
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

                    string idUser = TextBox1.Text;
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}