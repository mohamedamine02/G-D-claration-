using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using creation.Models;
using System.Data.Entity;
using ExcelDataReader;
using ClosedXML.Excel;
using System.IO;
using ExcelLibrary.SpreadSheet;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace creation
{
    public partial class Tableau_Recap : System.Web.UI.Page
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
        private void populateDatabaseData()
        {
            using (GDLEntities1 db = new GDLEntities1())

            {
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileName(FileUpload1.FileName);
            path = path.Replace(" ", "");
            FileUpload1.SaveAs(Server.MapPath("~/ExcelFile/") + path);
            String ExcelPath = Server.MapPath("~/ExcelFile/") + path;
            OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
            mycon.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM `Feuil1$`", mycon);
            OleDbDataReader dr = cmd.ExecuteReader();

            OleDbDataAdapter dt = new OleDbDataAdapter(cmd.CommandText, mycon);

            DataSet ds = new DataSet();
            dt.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Visible = true;

            //Label4.Text = "Data Has Been Updated Successfully";
            mycon.Close();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                String query = "insert into T_Requettes(Code,Libele,RequeteAss,RequetRet,Taux) values('" + row.Cells[0].Text+ "','" + row.Cells[1].Text.Replace("'", "‘") + "','" + row.Cells[2].Text.Replace("'", "‘") + "','" + row.Cells[3].Text.Replace("&gt;", ">").Replace(",", "*") + "'," + row.Cells[4].Text.Replace(",","*") +")";
                //Response.Write("<script> alert('" + row.Cells[3].Text.Replace("&gt;",">").Replace("'", " ") + "')</script>");
                String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
                SqlConnection con = new SqlConnection(mycon);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}