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
using System.Configuration;

namespace creation
{
    public partial class ANXBEN03 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        T_ANXBEN03 an3 = new T_ANXBEN03();
        private string Exercice = "3";
        private StreamWriter bjWriter;
        private string open;

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
        private void populateDatabaseData()
        {
            using (GDLEntities1 db = new GDLEntities1())

            {
                gvData.DataBind();
            }
        }

        protected void btnImportFromCSV_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileName(FileUpload1.FileName);
            path = path.Replace(" ", "");
            FileUpload1.SaveAs(Server.MapPath("~/ExcelFile/") + path);
            String ExcelPath = Server.MapPath("~/ExcelFile/") + path;
            OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
            mycon.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM `Feuil2$`", mycon);
            OleDbDataReader dr = cmd.ExecuteReader();

            OleDbDataAdapter dt = new OleDbDataAdapter(cmd.CommandText, mycon);

            DataSet ds = new DataSet();
            dt.Fill(ds);
            gvData.DataSource = ds;
            gvData.DataBind();

            //Label4.Text = "Data Has Been Updated Successfully";
            mycon.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            for (int i = 1; i < gvData.Rows.Count; i++)
            {



                GridViewRow row = gvData.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN03 where A308 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN03(A305,A306,A307,A308,A309,A310,A311,A312,A313,A314,A315,A316,idUser) values(" + row.Cells[0].Text.Replace(",", ".") + "," + row.Cells[1].Text.Replace(",", ".") + "," + row.Cells[2].Text.Replace(",", ".") + "," + row.Cells[3].Text.Replace(",", ".") + ",'" + row.Cells[4].Text + "','" + row.Cells[5].Text.Replace(",", ".") + "','" + row.Cells[6].Text.Replace(",", ".") + "'," + row.Cells[7].Text.Replace(",", ".") + "," + row.Cells[8].Text.Replace(",", ".") + "," + row.Cells[9].Text.Replace(",", ".") + "," + row.Cells[10].Text.Replace(",", ".") + "," + row.Cells[11].Text.Replace(",", ".") + "," + row.Cells[12].Text.Replace(",", ".") + ")";



                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }

            }
            gvData.Visible = true;

            GridView1.Visible = true;
            GridView1.DataBind();

            gvData.DataSource = SqlDataSource1;

            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



            //    GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN03 where A308 = '" + TextBox3.Text + "'";


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
                    String A306Numérodordre = TextBox1.Text;
                    String A307Typedidentifiant = DropDownList1.SelectedValue;
                    String A308Identifiantdubénéficiaire = TextBox3.Text;
                    String A309Nometprénomouraisonsocialedubénéficiaire = TextBox4.Text;
                    String A310Activitédubénéficiaire = TextBox5.Text;
                    String A311dernièreadressedubénéficiaire = TextBox6.Text;

                    String A312InteretsdescomptesspéciauxdépargneouvertsauprèsdesbanquesetdelaCENT = TextBox7.Text;
                    String A313Interetsdesautrescapitauxmobiliers = TextBox8.Text;
                    String A314InteretsdespretspayésauxétablissementsbancairesnonétablisenTunisie = TextBox9.Text;

                    String A315Montantdesretenuesopérées = TextBox10.Text;

                    String A316Montantnetservi = TextBox11.Text;

                    T_ANXBEN03 an3 = new T_ANXBEN03();
                    an3.A306 = A306Numérodordre;
                    an3.A307 = A307Typedidentifiant;
                    an3.A308 = A308Identifiantdubénéficiaire;
                    an3.A309 = A309Nometprénomouraisonsocialedubénéficiaire;
                    an3.A310 = A310Activitédubénéficiaire;
                    an3.A311 = A311dernièreadressedubénéficiaire;
                    an3.A312 = Convert.ToDecimal(A312InteretsdescomptesspéciauxdépargneouvertsauprèsdesbanquesetdelaCENT);
                    an3.A313 = Convert.ToDecimal(A313Interetsdesautrescapitauxmobiliers);
                    an3.A314 = Convert.ToDecimal(A314InteretsdespretspayésauxétablissementsbancairesnonétablisenTunisie);
                    an3.A315 = Convert.ToDecimal(A315Montantdesretenuesopérées);
                    an3.A316 = Convert.ToDecimal(A316Montantnetservi);
                    an3.T_Exercice = db.T_Exercice.Find(3);
                    an3.T_Utilisateur = db.T_Utilisateur.Find(2003);
                    db.T_ANXBEN03.Add(an3);
                    db.SaveChanges();

                    ModelState.Clear();
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
            else
            {
                Response.Write("<script>alert('le clé " + TextBox3.Text + " est existant !')</script>");
            }
            //}
            con.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string file_name;


            //if (open1.ShowDialog() == DialogResult.OK)
            //{

            string ch1;
            ch1 = "";

            string ConnectionString = Get_ConnexionString(); // chaine de connexion


            //-------------------Debut ANXDEB00 -------------------------------------------

            string E000 = "E3";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An3";
            string E007 = "0";
            string E008 = "0";
            string E009 = "";
            string E010 = "";
            string E011 = "";
            string E012 = "";
            string E013 = "";
            string E014 = "";
            string E015 = "";

            // donnée exercice
            String req = "".ToString();
            req = " SELECT *   FROM   dbo.T_Exercice where ( id ='" + Exercice + "')";

            SqlCommand myCommand;
            SqlConnection myConnection;

            myConnection = new SqlConnection(ConnectionString);

            myConnection.Open();

            myCommand = new SqlCommand(req, myConnection);
            SqlDataReader mySqDataReader = myCommand.ExecuteReader();

            string idSoc = "".ToString();

            while (mySqDataReader.Read())
            {
                E005 = mySqDataReader["Exercice"].ToString();
                idSoc = mySqDataReader["idSoc"].ToString();
                E007 = mySqDataReader["CodeActe"].ToString();
                E007 = E007.PadRight(1, '0');
            }

            myConnection.Close();

            //donnée societé
            req = " SELECT *   FROM   dbo.T_Soc where ( idSoc ='" + idSoc + "')";

            SqlCommand myCommand1;
            SqlConnection myConnection1;

            myConnection1 = new SqlConnection(ConnectionString);

            myConnection1.Open();

            myCommand1 = new SqlCommand(req, myConnection1);
            SqlDataReader mySqDataReader1 = myCommand1.ExecuteReader();

            while (mySqDataReader1.Read())
            {
                E001 = mySqDataReader1["NumMatriculeFiscal"].ToString();
                E001 = E001.PadLeft(7, '0');
                E002 = mySqDataReader1["CleMatriculeFiscal"].ToString();
                E003 = mySqDataReader1["CodeCategorie"].ToString();
                E004 = mySqDataReader1["NumEtablissement"].ToString();
                E009 = mySqDataReader1["RaisonSociale"].ToString();
                E010 = mySqDataReader1["Activite"].ToString();
                E011 = mySqDataReader1["Ville"].ToString();
                E012 = mySqDataReader1["Rue"].ToString();
                E013 = mySqDataReader1["Numero"].ToString();
                E013 = E013.PadRight(4, ' ');
                E014 = mySqDataReader1["CodePostal"].ToString();
                E014 = E014.PadRight(4, ' ');
            }

            myConnection1.Close();

            string file_name1 = "ANXEMP_3_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A306 = "";
            string A307 = "";
            string A308 = "";
            string A309 = "";
            string A310 = "";
            string A311 = "";
            string A312 = "";
            string A313 = "";
            string A314 = "";
            string A315 = "";
            string A316 = "";
            string A317 = "";


            //Totaux
            decimal T307 = 0;
            decimal T308 = 0;
            decimal T309 = 0;
            decimal T310 = 0;
            decimal T311 = 0;



            String SQL;
            SQL = "SELECT A306,A307,A308,A309,A310,A311,A312,A313,A314,A315,A316";
            SQL += " FROM [dbo].[T_ANXBEN03]";
            SQL += " WHERE A305 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A306 = mySqDataReader2["A306"].ToString();
                A307 = mySqDataReader2["A307"].ToString();
                A308 = mySqDataReader2["A308"].ToString();
                A309 = mySqDataReader2["A309"].ToString().ToString().Replace("‘", "");
                A310 = mySqDataReader2["A310"].ToString().ToString().Replace("‘", "");
                A311 = mySqDataReader2["A311"].ToString().ToString().Replace("‘", "");
                A312 = mySqDataReader2["A312"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A313 = mySqDataReader2["A313"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A314 = mySqDataReader2["A314"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A315 = mySqDataReader2["A315"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A316 = mySqDataReader2["A316"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");


                T307 += Convert.ToDecimal(mySqDataReader2["A312"]);
                T308 += Convert.ToDecimal(mySqDataReader2["A313"]);
                T309 += Convert.ToDecimal(mySqDataReader2["A314"]);
                T310 += Convert.ToDecimal(mySqDataReader2["A315"]);
                T311 += Convert.ToDecimal(mySqDataReader2["A316"]);


                A306 = A306.PadLeft(6, '0');

                A307 = A307.PadLeft(1, '2');

                A308 = A308.PadRight(13, ' ');

                A309 = A309.PadRight(40, ' ');

                A310 = A310.PadRight(40, ' ');

                A311 = A311.PadRight(120, ' ');
                A312 = A312.PadLeft(15, '0');
                A313 = A313.PadLeft(15, '0');

                A314 = A314.PadLeft(15, '0');

                A315 = A315.PadLeft(15, '0');

                A316 = A316.PadLeft(15, '0');
                A317 = A317.PadRight(92, ' ');




                ch1 = "L3" + E001 + E002 + E003 + E004 + E005 + A306 + A307 + A308 + A309 + A310 + A311 + A312 + A313 + A314 + A315 + A316;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T306 = "";
            _T306 = _T306.PadRight(220, ' ');
            string _T312 = "";
            _T312 = _T312.PadRight(92, ' ');

            string _T307 = T307.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T308 = T308.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T309 = T309.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T310 = T310.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T311 = T311.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');



            ch1 = "T3" + E001 + E002 + E003 + E004 + E005 + _T306 + _T307 + _T308 + _T309 + _T310 + _T311 + _T312;
            objWriter.WriteLine(ch1);


            //-------------------Fin ANXFIN01 -------------------------------------------



            objWriter.Close();
            objWriter.Dispose();


        }
        public string Get_ConnexionString()
        {

            string ch = "";
            ch = ConfigurationManager.ConnectionStrings["GDLConnectionString"].ConnectionString.ToString();
            return ch;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            String myquery = "Select * from T_ANXBEN03";
            SqlConnection con = new SqlConnection(mycon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(dt);
            //GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
            GridView1.FooterRow.Cells[0].Text = "Total";
            GridView1.FooterRow.Cells[8].Text = dt.Compute("Sum(A312)", "").ToString();
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A313)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A314)", "").ToString();
            GridView1.FooterRow.Cells[11].Text = dt.Compute("Sum(A315)", "").ToString();
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A316)", "").ToString();


            GridView1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
