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
    public partial class Autorisation : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            T_Autorisation aut = new T_Autorisation();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string User_ID = TextBox1.Text;
            string Role_ID = TextBox2.Text;
            string Annexe = TextBox3.Text;
            T_Autorisation aut = new T_Autorisation();
            aut.User_ID = Convert.ToInt32 (User_ID);
            aut.Role_ID = Convert.ToInt32(Role_ID);
            aut.Annexe = Annexe;
            db.T_Autorisation.Add(aut);
            db.SaveChanges();
            ModelState.Clear();
            GridView1.DataBind();

        }
    }
}