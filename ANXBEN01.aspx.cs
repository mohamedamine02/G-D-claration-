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
    public partial class ANXBEN01 : System.Web.UI.Page
    {
        
        //private SqlCommand query;
        private SqlCommand com;
        GDLEntities1 db = new GDLEntities1();
        private object cmd;
        private object cn;

        
        SqlDataSource sq = new SqlDataSource();
        private string Exercice="3";
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
            gvData.Visible = true;
            //GridView1.Visible = false;
            

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
                String quer = "select * from T_ANXBEN01 where A108 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN01(A105,A106,A107,A108,A109,A110,A111,A112,A113,A114,A115,A116,A117,A118,A119,A120,A121,A122,A123,A124) values('"+row.Cells[0].Text.Replace(",",".")+"','" + row.Cells[1].Text.Replace(",", ".") + "','" + row.Cells[2].Text.Replace(",", ".") + "','" + row.Cells[3].Text.Replace(",", ".") + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "'," + row.Cells[7].Text.Replace(",", ".") + "," + row.Cells[8].Text.Replace(",", ".") + ",'" + row.Cells[9].Text.Replace(",", ".") + "','" + row.Cells[10].Text.Replace(",", ".") + "'," + row.Cells[11].Text.Replace(",", ".") + "," + row.Cells[12].Text.Replace(",", ".") + "," + row.Cells[13].Text.Replace(",", ".") + "," + row.Cells[14].Text.Replace(",", ".") + "," + row.Cells[15].Text.Replace(",", ".") + "," + row.Cells[16].Text.Replace(",", ".") + "," + row.Cells[17].Text.Replace(",", ".") + "," + row.Cells[18].Text.Replace(",", ".") + "," + row.Cells[19].Text.Replace(",", ".") + ")";


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
            string file_name;


            //if (open1.ShowDialog() == DialogResult.OK)
            //{

            string ch1;
            ch1 = "";

            string ConnectionString = Get_ConnexionString(); // chaine de connexion


            //-------------------Debut ANXDEB00 -------------------------------------------

            string E000 = "E1";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An1";
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
            req = " SELECT *   FROM   dbo.T_Exercice where ( id ='"+Exercice+"')";

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

            string file_name1 = "ANXEMP_1_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A106 = "";
            string A107 = "";
            string A108 = "";
            string A109 = "";
            string A110 = "";
            string A111 = "";
            string A112 = "";
            string A113 = "";
            string A114 = "";
            string A115 = "";
            string A116 = "";
            string A117 = "";
            string A118 = "";
            string A119 = "";
            string A120 = "";
            string A121 = "";
            string A122 = "";
            string A123 = "";
            string A124 = "";
           


            //Totaux
            decimal T107 = 0;
            decimal T108 = 0;
            decimal T109 = 0;
            decimal T110 = 0;
            decimal T111 = 0;
            decimal T112 = 0;
            decimal T113 = 0;
            decimal T114 = 0;

            String SQL;
            SQL = "SELECT A106,A107,A108,A109,A110,A111,A112,A113,A114,A115,A116,A117,A118,A119,A120,A121,A122,A123,A124";
            SQL += " FROM [dbo].[T_ANXBEN01]";
            SQL += " WHERE A105 = '" + Exercice+"'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A106 = mySqDataReader2["A106"].ToString();
                A107 = mySqDataReader2["A107"].ToString();
                A108 = mySqDataReader2["A108"].ToString().Replace("‘", "");
                A109 = mySqDataReader2["A109"].ToString().Replace("‘", "");
                A110 = mySqDataReader2["A110"].ToString().Replace("‘", "");
                A111 = mySqDataReader2["A111"].ToString().Replace("‘", "");
                A112 = mySqDataReader2["A112"].ToString().Replace("‘", "");
                A113 = mySqDataReader2["A113"].ToString().Replace("‘", "");
                A114 = mySqDataReader2["A114"].ToString().Replace("/", "").Replace("00:00:00", "").Trim(); //date
                A115 = mySqDataReader2["A115"].ToString().Replace("/", "").Replace("00:00:00", "").Trim(); //date
                A116 = mySqDataReader2["A116"].ToString().Replace(",", "").Replace(".", "").Trim();
                A117 = mySqDataReader2["A117"].ToString().Replace(",", "").Replace(".", "").Trim();
                A118 = mySqDataReader2["A118"].ToString().Replace(",", "").Replace(".", "").Trim();
                A119 = mySqDataReader2["A119"].ToString().Replace(",", "").Replace(".", "").Trim();
                A120 = mySqDataReader2["A120"].ToString().Replace(",", "").Replace(".", "").Trim();
                A121 = mySqDataReader2["A121"].ToString().Replace(",", "").Replace(".", "").Trim();
                A122 = mySqDataReader2["A122"].ToString().Replace(",", "").Replace(".", "").Trim();
                A123 = mySqDataReader2["A123"].ToString().Replace(",", "").Replace(".", "").Trim();
                A124 = mySqDataReader2["A124"].ToString().Replace(",", "").Replace(".", "").Trim();

                T107 += Convert.ToDecimal(mySqDataReader2["A117"]);
                T108 += Convert.ToDecimal(mySqDataReader2["A118"]);
                T109 += Convert.ToDecimal(mySqDataReader2["A119"]);
                T110 += Convert.ToDecimal(mySqDataReader2["A120"]);
                T111 += Convert.ToDecimal(mySqDataReader2["A121"]);
                T112 += Convert.ToDecimal(mySqDataReader2["A122"]);
                T113 += Convert.ToDecimal(mySqDataReader2["A123"]);
                T114 += Convert.ToDecimal(mySqDataReader2["A124"]);

                A106 = A106.PadLeft(6, '0');

                A107 = A107.PadLeft(1, '2');

                A108 = A108.PadRight(13, ' ');

                A109 = A109.PadRight(40, ' ');

                A110 = A110.PadRight(40, ' ');

                A111 = A111.PadRight(120, ' ');

                A113 = A113.PadLeft(2, '0');

                A114 = A114.PadLeft(8, '0');

                A115 = A115.PadLeft(8, '0');

                A116 = A116.PadLeft(3, '0');

                A117 = A117.PadLeft(15, '0');

                A118 = A118.PadLeft(15, '0');

                A119 = A119.PadLeft(15, '0');

                A120 = A120.PadLeft(15, '0');

                A121 = A121.PadLeft(15, '0');

                A122 = A122.PadLeft(15, '0');

                A123 = A123.PadLeft(15, '0');

                A124 = A124.PadLeft(15, '0');

                ch1 = "L1" + E001 + E002 + E003 + E004 + E005 + A106 + A107 + A108 + A109 + A110 + A111 + A112 + A113 + A114 + A115 + A116 + A117 + A118 + A119 + A120 + A121 + A122 + A123 + A124;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T106 = "";
            _T106 = _T106.PadRight(242, ' ');
            string _T115 = "";
            _T115 = _T115.PadRight(25, ' ');

            string _T107 = T107.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T108 = T108.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T109 = T109.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T110 = T110.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T111 = T111.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T112 = T112.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T113 = T113.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');

            string _T114 = T114.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');

            ch1 = "T1" + E001 + E002 + E003 + E004 + E005 + _T106 + _T107 + _T108 + _T109 + _T110 + _T111 + _T112 + _T113 + _T114 + _T115;
            objWriter.WriteLine(ch1);


            //-------------------Fin ANXFIN01 -------------------------------------------



            objWriter.Close();
            objWriter.Dispose();

            //MessageBox.Show("Fichier exporté avec succès : " + open1.SelectedPath + "\\" + file_name1, "Operation effecutée", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }





        protected void Button3_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



                //GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN01 where A108 = '" + TextBox3.Text + "'";


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

                    String A106Numérodeordre = TextBox1.Text;
                    String A107Typedidentifiantdubinificiaire = DropDownList1.SelectedValue;
                    String A108Identifiantbénéficiaire = TextBox3.Text;
                    String A109NometPrénomdubénéficiaire = TextBox4.Text;
                    String A110Emploioccupédubénéficiaire = TextBox5.Text;

                    String A111Dernièreadressedubénéficiaire = TextBox6.Text;
                    String A112Situationfamilialedubénéficiaire = DropDownList2.SelectedValue;

                    String A113Nombredenfantsàcharge = TextBox8.Text;

                    String A114Datedébutdelapériodedutravail = TextBox9.Text;

                    String A115Datefindelapériodedutravail = TextBox10.Text;

                    String A116Duréedelapériodeennombredejours = TextBox11.Text;
                    String A117Revenuimposable = TextBox12.Text;
                    String A118Valeurdesavantagesennature = TextBox13.Text;
                    String A119Totaldurevenubrutimposable = TextBox14.Text;
                    String A120Revenuréinvesti = TextBox15.Text;
                    String A121Montantdesretenuesopéréesselonlerégimecommun = TextBox16.Text;
                    String A122Montantdesretenuesopéréesautauxde20 = TextBox17.Text;
                    String A123Lacontributionsocialedesolidaritéduesurlestraitementssalairesrémunérationsindemnités = TextBox18.Text;
                    String A124Montantnetservi = TextBox19.Text;

                    T_ANXBEN01 an1 = new T_ANXBEN01();
                    an1.A106 = A106Numérodeordre;
                    an1.A107 = A107Typedidentifiantdubinificiaire;
                    an1.A108 = A108Identifiantbénéficiaire;
                    an1.A109 = A109NometPrénomdubénéficiaire;
                    an1.A110 = A110Emploioccupédubénéficiaire;
                    an1.A111 = A111Dernièreadressedubénéficiaire;
                    an1.A112 = A112Situationfamilialedubénéficiaire;
                    an1.A113 = A113Nombredenfantsàcharge;
                    an1.A114 = A114Datedébutdelapériodedutravail;
                    an1.A115 = A115Datefindelapériodedutravail;
                    an1.A116 = A116Duréedelapériodeennombredejours;
                    an1.A117 = Convert.ToInt32(A117Revenuimposable);
                    an1.A118 = Convert.ToInt32(A118Valeurdesavantagesennature);
                    an1.A119 = Convert.ToInt32(A119Totaldurevenubrutimposable);
                    an1.A120 = Convert.ToInt32(A120Revenuréinvesti);
                    an1.A121 = Convert.ToInt32(A121Montantdesretenuesopéréesselonlerégimecommun);
                    an1.A122 = Convert.ToInt32(A122Montantdesretenuesopéréesautauxde20);
                    an1.A123 = Convert.ToInt32(A123Lacontributionsocialedesolidaritéduesurlestraitementssalairesrémunérationsindemnités);
                    an1.A124 = Convert.ToInt32(A124Montantnetservi);
                    an1.T_Exercice = db.T_Exercice.Find(3);
                    an1.idUser =Int32.Parse(Session["idUser"].ToString());
                    db.T_ANXBEN01.Add(an1);
                    db.SaveChanges();
                    ModelState.Clear();



                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('le clé "+ TextBox3.Text + " est existant !')</script>");
                }
            //}
            con.Close();
        }

        private class open1
        {
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
            String myquery = "Select * from T_ANXBEN01";
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
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A117)", "").ToString();
            GridView1.FooterRow.Cells[13].Text = dt.Compute("Sum(A118)", "").ToString();
            GridView1.FooterRow.Cells[14].Text = dt.Compute("Sum(A119)", "").ToString();
            GridView1.FooterRow.Cells[15].Text = dt.Compute("Sum(A120)", "").ToString();
            GridView1.FooterRow.Cells[16].Text = dt.Compute("Sum(A121)", "").ToString();
            GridView1.FooterRow.Cells[17].Text = dt.Compute("Sum(A122)", "").ToString();
            GridView1.FooterRow.Cells[18].Text = dt.Compute("Sum(A123)", "").ToString();
            GridView1.FooterRow.Cells[21].Text = dt.Compute("Sum(A124)", "").ToString();
            GridView1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
    

   

        
       
    

