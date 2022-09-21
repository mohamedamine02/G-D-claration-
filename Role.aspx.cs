using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
namespace creation.Views.Home
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                //Label1.Text = Session["Login"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string RoleID = TextBox1.Text;
            string Role = TextBox2.Text;
            string idUser = TextBox3.Text;
            T_Roles r = new T_Roles();
            r.Role = Role;
            r.idUser =Convert.ToInt32( idUser);
            db.T_Roles.Add(r);
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