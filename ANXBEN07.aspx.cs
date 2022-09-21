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
    public partial class ANXBEN07 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        //private string A705;
        private string A706;
        private string A707;
        private string A708;
        private string A709;
        private string A710;
        private string A711;
        private string A712;
        private double A713;
        private string A714;
        private string A715;
        //int A705;
        //int ANXBEN07_ID;
        //private int idUser;
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

        //protected void btnImportFromCSV_Click(object sender, EventArgs e)
        //{


        //    string path = Path.GetFileName(FileUpload1.FileName);
        //    path = path.Replace(" ", "");
        //    FileUpload1.SaveAs(Server.MapPath("~/ExcelFile/") + path);
        //    String ExcelPath = Server.MapPath("~/ExcelFile/") + path;
        //    OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
        //    mycon.Open();
        //    OleDbCommand cmd = new OleDbCommand("SELECT * FROM `Feuil2$`", mycon);
        //    OleDbDataReader dr = cmd.ExecuteReader();

        //    OleDbDataAdapter dt = new OleDbDataAdapter(cmd.CommandText, mycon);

        //    DataSet ds = new DataSet();
        //    dt.Fill(ds);
        //    gvData.DataSource = ds;
        //    gvData.DataBind();
        //    gvData.Visible = true;
        //    //GridView1.Visible = false;


        //    //Label4.Text = "Data Has Been Updated Successfully";
        //    mycon.Close();

        //}

       

        protected void Button1_Click1(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



            //    GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN07 where A708 = '" + TextBox3.Text + "'";


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
                    String A706Numerodordre = TextBox1.Text;
                    String A707Typedidentifiant = DropDownList1.SelectedValue;
                    String A708Identifiantbénéficiaire = TextBox3.Text;
                    String A709Nometprénomouraisonsociale = TextBox4.Text; ;
                    String A710Activité = TextBox5.Text;
                    String A711Derniéreadressedubénéficiaire = TextBox6.Text;
                    String A712typedemontantspayés = DropDownList2.SelectedValue;
                    String A713Monantspayés = TextBox8.Text;
                    String A714Retenueàlasource = TextBox9.Text;
                    String A715Montantnetservi = TextBox10.Text;

                    T_ANXBEN07 an7 = new T_ANXBEN07();
                    an7.A706 = A706Numerodordre;
                    an7.A707 = A707Typedidentifiant;
                    an7.A708 = A708Identifiantbénéficiaire;
                    an7.A709 = A709Nometprénomouraisonsociale;
                    an7.A710 = A710Activité;
                    an7.A711 = A711Derniéreadressedubénéficiaire;
                    an7.A712 = A712typedemontantspayés;
                    an7.A713 = Convert.ToDecimal(A713Monantspayés);
                    an7.A714 = Convert.ToDecimal(A714Retenueàlasource);
                    an7.A715 = Convert.ToDecimal(A715Montantnetservi);
                    an7.T_Utilisateur = db.T_Utilisateur.Find(2003);
                    an7.T_Exercice = db.T_Exercice.Find(3);
                    db.T_ANXBEN07.Add(an7);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            string file_name;


            string ch1;
            ch1 = "";

            string ConnectionString = Get_ConnexionString(); // chaine de connexion


            //-------------------Debut ANXDEB00 -------------------------------------------

            string E000 = "E7";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An7";
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
            req = " SELECT * FROM dbo.T_Exercice where ( id ='" + Exercice + "')";

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

            string file_name1 = "ANXEMP_7_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A706 = "";
            string A707 = "";
            string A708 = "";
            string A709 = "";
            string A710 = "";
            string A711 = "";
            string A712 = "";
            string A713 = "";
            string A714 = "";
            string A715 = "";
            string A716 = "";




            // Totaux 

            decimal T707 = 0;
            decimal T708 = 0;
            decimal T709 = 0;
            decimal T710 = 0;



            String SQL;
            SQL = "SELECT A706,A707,A708,A709,A710,A711,A712,A713,A714,A715";
            SQL += " FROM [dbo].[T_ANXBEN07]";
            SQL += " WHERE A705 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A706 = mySqDataReader2["A706"].ToString();
                A707 = mySqDataReader2["A707"].ToString();
                A708 = mySqDataReader2["A708"].ToString();
                A709 = mySqDataReader2["A709"].ToString().Replace("‘", "");
                A710 = mySqDataReader2["A710"].ToString().Replace("‘", "");
                A711 = mySqDataReader2["A711"].ToString().Replace("‘", "");
                A712 = mySqDataReader2["A712"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A713 = mySqDataReader2["A713"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A714 = mySqDataReader2["A714"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A715 = mySqDataReader2["A715"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");



                T707 += Convert.ToDecimal(mySqDataReader2["A713"]);
                T708 += Convert.ToDecimal(mySqDataReader2["A714"]);
                T709 += Convert.ToDecimal(mySqDataReader2["A715"]);






                A706 = A706.PadLeft(6, '0');

                A707 = A707.PadLeft(1, '1');

                A708 = A708.PadRight(13, ' ');

                A709 = A709.PadRight(40, ' ');

                A710 = A710.PadRight(40, ' ');

                A711 = A711.PadRight(120, ' ');
                A712 = A712.PadLeft(2, '0');
                A713 = A713.PadLeft(15, '0');
                A714 = A714.PadLeft(15, '0');
                A715 = A715.PadLeft(15, '0');
                A716 = A716.PadLeft(120, ' ');




                ch1 = "L7" + E001 + E002 + E003 + E004 + E005 + A706 + A707 + A708 + A709 + A710 + A711 + A712 + A713 + A714 + A715 + A716;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T706 = "";
            _T706 = _T706.PadRight(229, ' ');
            

            string _T707 = T707.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T708 = T708.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T709 = T709.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T710 = T710.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');





            ch1 = "T1" + E001 + E002 + E003 + E004 + E005 + _T706 + _T707 + _T708 + _T709 ;
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            String myquery = "Select * from T_ANXBEN07";
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

            GridView1.FooterRow.Cells[8].Text = dt.Compute("Sum(A713)", "").ToString();
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A714)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A715)", "").ToString();




            GridView1.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
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
            gvData.Visible = true;
            //GridView1.Visible = false;


            //Label4.Text = "Data Has Been Updated Successfully";
            mycon.Close();
        }

       

        protected void Button6_Click1(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            for (int i = 1; i < gvData.Rows.Count; i++)
            {



                GridViewRow row = gvData.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN07 where A708 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN07(A705,A706,A707,A708,A709,A710,A711,A712,A713,A714,A715) values('" + row.Cells[0].Text + "','" + row.Cells[1].Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "'," + row.Cells[8].Text.Replace(",", ".") + "," + row.Cells[9].Text.Replace(",", ".") + "," + row.Cells[10].Text.Replace(",", ".") + ")";


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
    }
}   


       
   
