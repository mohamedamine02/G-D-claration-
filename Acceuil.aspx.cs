using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace creation
{
    public partial class Acceuil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["Login"] != null)
            {
                Label1.Text = Session["Login"].ToString();
                if (Session["Login"].ToString() == "admin")
                {
                   
                    structure.Visible = true;
                }
                else
                {
                    structure.Visible = false;

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("utilisateur.aspx");
        }
    }
}