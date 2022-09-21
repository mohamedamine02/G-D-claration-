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
    public partial class Autorisationsoc : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            T_Autorisation_Soc autsoc = new T_Autorisation_Soc();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string idSoc = TextBox1.Text;
            string idUser = TextBox2.Text;
            string Flag = TextBox3.Text;
            T_Autorisation_Soc autsoc = new T_Autorisation_Soc();
            autsoc.idSoc = Convert.ToInt32 (idSoc);
            autsoc.idUser = Convert.ToInt32(idUser);
            autsoc.Flag = Convert.ToBoolean(Flag);
            db.T_Autorisation_Soc.Add(autsoc);
            db.SaveChanges();
            ModelState.Clear();
            GridView1.DataBind();


        }
    }
   
}
