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
    public partial class ANXBEN05 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
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
                String quer = "select * from T_ANXBEN05 where A508 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN05(A505,A506,A507,A508,A509,A510,A511,A512,A513,A514,A515,A516,A517,A518,A519,A520,idUser) values('" + row.Cells[0].Text.Replace(",", ".") + "','" + row.Cells[1].Text.Replace(",", ".") + "','" + row.Cells[2].Text.Replace(",", ".") + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "'," + row.Cells[7].Text.Replace(",", ".") + "," + row.Cells[8].Text.Replace(",", ".") + "," + row.Cells[9].Text.Replace(",", ".") + "," + row.Cells[10].Text.Replace(",", ".") + "," + row.Cells[11].Text.Replace(",", ".") + "," + row.Cells[12].Text.Replace(",", ".") + "," + row.Cells[13].Text.Replace(",", ".") + "," + row.Cells[14].Text.Replace(",", ".") + "," + row.Cells[15].Text.Replace(",", ".") + "," + row.Cells[16].Text.Replace(",", ".")+")";





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

       

        protected void Button2_Click1(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



            //    GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN05 where A508 = '" + TextBox3.Text + "'";


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
                    String A506Numérodordre = TextBox1.Text;
                    string A507Typedidentifiant = DropDownList1.SelectedValue;
                    String A508Identifiantbénéficiaire = TextBox3.Text;
                    String A509NometPrénomouraisonsocial = TextBox4.Text;
                    String A510Activité = TextBox5.Text;
                    String A511Dernièreadressedubénéficiaire = TextBox6.Text;
                    String A512Totaldesmontantségauxousupérieursà1000DycomprislaTVAprovenantdesventesdesentreprisesbénéficiantdeladéductiondesdeuxtiersdesrevenusetdesventesdessociétéssoumisesàlimpôtsurlessociétésauxtauxde10 = TextBox7.Text;
                    String A513Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesopérationsdexportetdesventesdesentreprisessoumisesàlISoutauxde10 = TextBox8.Text;
                    String A514Montantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesautresopérations = TextBox9.Text;
                    String A515Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesautresopérations = TextBox10.Text;

                    String A516Montantspayéségauxousupérieursà1000DycomprislaTVApayésparlesentreprisesetlesétablissementspublicsetsoumisàlaretenueàlasourceautitredelaTVA = TextBox11.Text;
                    String A517Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayésparlesentreprisesetlesétablissementspublicsetsoumisàlaretenueàlasourceautitredelaTVA = TextBox12.Text;
                    String A518MontantsservisautitredesopérationsréaliséesaveclespersonnesnayantpasdétablissementenTunisieetdontlaretenueàlasourceautitredelaTVAestde100 = TextBox13.Text;
                    String A519RetenuesopéréessurlesmontantsservisautitredesopérationsréaliséesaveclespersonnesnayantpasdétablissementenTunisieetdontlaretenueàlasourceautitredelaTVAestde100 = TextBox14.Text;
                    String A520MontantNetservi = TextBox15.Text;

                    T_ANXBEN05 an5 = new T_ANXBEN05();
                    an5.A506 = A506Numérodordre;
                    an5.A507 = A507Typedidentifiant;
                    an5.A508 = A508Identifiantbénéficiaire;
                    an5.A509 = A509NometPrénomouraisonsocial;
                    an5.A510 = A510Activité;
                    an5.A511 = A511Dernièreadressedubénéficiaire;
                    an5.A512 = Convert.ToDecimal(A512Totaldesmontantségauxousupérieursà1000DycomprislaTVAprovenantdesventesdesentreprisesbénéficiantdeladéductiondesdeuxtiersdesrevenusetdesventesdessociétéssoumisesàlimpôtsurlessociétésauxtauxde10);
                    an5.A513 = Convert.ToDecimal(A513Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesopérationsdexportetdesventesdesentreprisessoumisesàlISoutauxde10);
                    an5.A514 = Convert.ToDecimal(A514Montantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesautresopérations);
                    an5.A515 = Convert.ToDecimal(A515Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayéautitredesautresopérations);
                    an5.A516 = Convert.ToDecimal(A516Montantspayéségauxousupérieursà1000DycomprislaTVApayésparlesentreprisesetlesétablissementspublicsetsoumisàlaretenueàlasourceautitredelaTVA);
                    an5.A517 = Convert.ToDecimal(A517Retenuesurlesmontantspayéségauxousupérieursà1000DycomprislaTVApayésparlesentreprisesetlesétablissementspublicsetsoumisàlaretenueàlasourceautitredelaTVA);
                    an5.A518 = Convert.ToDecimal(A518MontantsservisautitredesopérationsréaliséesaveclespersonnesnayantpasdétablissementenTunisieetdontlaretenueàlasourceautitredelaTVAestde100);
                    an5.A519 = Convert.ToDecimal(A519RetenuesopéréessurlesmontantsservisautitredesopérationsréaliséesaveclespersonnesnayantpasdétablissementenTunisieetdontlaretenueàlasourceautitredelaTVAestde100);
                    an5.A520 = Convert.ToDecimal(A520MontantNetservi);
                    an5.T_Utilisateur = db.T_Utilisateur.Find(2003);
                    an5.T_Exercice = db.T_Exercice.Find(3);
                    db.T_ANXBEN05.Add(an5);
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

            string E000 = "E5";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An5";
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

            string file_name1 = "ANXEMP_5_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A506 = "";
            string A507 = "";
            string A508 = "";
            string A509 = "";
            string A510 = "";
            string A511 = "";
            string A512 = "";
            string A513 = "";
            string A514 = "";
            string A515 = "";
            string A516 = "";
            string A517 = "";
            string A518 = "";
            string A519 = "";
            string A520 = "";
            

            //Totaux
            decimal T507 = 0;
            decimal T508 = 0;
            decimal T509 = 0;
            decimal T510 = 0;
            decimal T511 = 0;
            decimal T512 = 0;
            decimal T513 = 0;
            decimal T514 = 0;
            decimal T515 = 0;

            String SQL;
            SQL = "SELECT A506,A507,A508,A509,A510,A511,A512,A513,A514,A515,A516,A517,A518,A519,A520";
            SQL += " FROM [dbo].[T_ANXBEN05]";
            SQL += " WHERE A505 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A506 = mySqDataReader2["A506"].ToString();
                A507 = mySqDataReader2["A507"].ToString();
                A508 = mySqDataReader2["A508"].ToString();
                A509 = mySqDataReader2["A509"].ToString().Replace("‘", "");
                A510 = mySqDataReader2["A510"].ToString().Replace("‘", "");
                A511 = mySqDataReader2["A511"].ToString().Replace("‘", "");
                A512 = mySqDataReader2["A512"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A513 = mySqDataReader2["A513"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A514 = mySqDataReader2["A514"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A515 = mySqDataReader2["A515"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A516 = mySqDataReader2["A516"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A517 = mySqDataReader2["A517"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A518 = mySqDataReader2["A518"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A519 = mySqDataReader2["A519"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A520 = mySqDataReader2["A520"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");


                T507 += Convert.ToDecimal(mySqDataReader2["A512"]);
                T508 += Convert.ToDecimal(mySqDataReader2["A513"]);
                T509 += Convert.ToDecimal(mySqDataReader2["A514"]);
                T510 += Convert.ToDecimal(mySqDataReader2["A515"]);
                T511 += Convert.ToDecimal(mySqDataReader2["A516"]);
                T512 += Convert.ToDecimal(mySqDataReader2["A517"]);
                T513 += Convert.ToDecimal(mySqDataReader2["A518"]);
                T514 += Convert.ToDecimal(mySqDataReader2["A519"]);
                T515 += Convert.ToDecimal(mySqDataReader2["A520"]);

                A506 = A506.PadLeft(6, '0');

                A507 = A507.PadLeft(1, '1');

                A508 = A508.PadRight(13, ' ');

                A509 = A509.PadRight(40, ' ');

                A510 = A510.PadRight(40, ' ');

                A511 = A511.PadRight(120, ' ');
                A512 = A512.PadLeft(15, '0');
                A513 = A513.PadLeft(15, '0');

                A514 = A514.PadLeft(15, '0');

                A515 = A515.PadLeft(15, '0');

                A516 = A516.PadLeft(15, '0');
                A517 = A517.PadLeft(15, '0');
                A518 = A518.PadLeft(15, '0');
                A519 = A519.PadLeft(15, '0');

                A520 = A520.PadLeft(15, '0');
                





                ch1 = "L5" + E001 + E002 + E003 + E004 + E005 + A506 + A507 + A508 + A509 + A510 + A511 + A512 + A513 + A514 + A515 + A516 + A517 + A518 + A519 + A520 ;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T506 = "";
            _T506 = _T506.PadRight(220, ' ');
            string _T507 = T507.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T508 = T508.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T509 = T509.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T510 = T510.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T511 = T511.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T512 = T512.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T513 = T513.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T514 = T514.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T515 = T515.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');

            string _T516 = "";
            _T516 = _T516.PadRight(32, ' ');

            ch1 = "T5" + E001 + E002 + E003 + E004 + E005 + _T506 + _T507 + _T508 + _T509 + _T510 + _T511 + _T512 + _T513 + _T514 + _T515 + _T516;
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
            String myquery = "Select * from T_ANXBEN05";
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
            GridView1.FooterRow.Cells[7].Text = dt.Compute("Sum(A512)", "").ToString();
            GridView1.FooterRow.Cells[8].Text = dt.Compute("Sum(A513)", "").ToString();
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A514)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A515)", "").ToString();
            GridView1.FooterRow.Cells[11].Text = dt.Compute("Sum(A516)", "").ToString();
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A517)", "").ToString();
            GridView1.FooterRow.Cells[13].Text = dt.Compute("Sum(A518)", "").ToString();
            GridView1.FooterRow.Cells[14].Text = dt.Compute("Sum(A519)", "").ToString();
            GridView1.FooterRow.Cells[15].Text = dt.Compute("Sum(A520)", "").ToString();
            GridView1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }

       
    }
