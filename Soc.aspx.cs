using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
using System.Data.SqlClient;
namespace creation.Views
{
    public partial class WebForm2 : System.Web.UI.Page
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
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            
                string idSoc = TextBox1.Text;
                string NumMatriculeFiscal = TextBox2.Text;
                string CleMatriculeFiscal = TextBox3.Text;
                string CodeTVA = TextBox4.Text;
                string CodeCategorie = TextBox5.Text;
                string NumEtablissement = TextBox6.Text;
                string RaisonSociale = TextBox7.Text;
                string Activite = TextBox8.Text;
                string Ville = TextBox9.Text;
                string Rue = TextBox10.Text;
                string Numero = TextBox11.Text;
                string CodePostal = TextBox12.Text;
                string idUser = TextBox13.Text;
                //string flag = TextBox14.Text;
                T_Soc s = new T_Soc();
                s.NumMatriculeFiscal = NumMatriculeFiscal;
                s.CleMatriculeFiscal = CleMatriculeFiscal;
                s.CodeTVA = CodeTVA;
                s.CodeCategorie = CodeCategorie;
                s.NumEtablissement = NumEtablissement;
                s.RaisonSociale = RaisonSociale;
                s.Activite = Activite;
                s.Ville = Ville;
                s.Rue = Rue;
                s.Numero = Numero;
                s.CodePostal = CodePostal;
                s.idUser = Convert.ToInt32(idUser);
                //s.flag = Convert.ToBoolean(flag);
                db.T_Soc.Add(s);
                //db.SaveChangesAsync();
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