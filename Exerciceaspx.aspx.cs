using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
namespace creation
{
    public partial class Exerciceaspx : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
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
            string idSoc = TextBox1.Text;
            string idUser = TextBox2.Text;
            string Exercice = TextBox3.Text;
            string DateDebutExercice = TextBox4.Text;
            string DateClotureExercice = TextBox5.Text;
            string flag = TextBox6.Text;
            string CodeActe = TextBox7.Text;
            T_Exercice ex = new T_Exercice();
            ex.idSoc = Convert.ToInt32(idSoc);
            ex.idUser = Convert.ToInt32(idUser);
            ex.Exercice = Exercice;
            ex.DateDebutExercice = Convert.ToDateTime(DateDebutExercice);
            ex.DateClotureExercice = Convert.ToDateTime(DateClotureExercice);
            /*ex.flag = Convert.ToBoolean (flag)*/;
            ex.CodeActe = CodeActe;
            db.T_Exercice.Add(ex);
            db.SaveChanges();
            ModelState.Clear();
            GridView1.DataBind();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}