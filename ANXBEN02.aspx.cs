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
    public partial class ANXBEN02 : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        private object lblMessage;

        T_ANXBEN02 an2 = new T_ANXBEN02();
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
            T_ANXBEN02 an2 = new T_ANXBEN02();

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
            dr.Close();
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
                String quer = "select * from T_ANXBEN02 where A208 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN02(A205,A206,A207,A208,A209,A210,A211,A212,A213,A214,A215,A216,A217,A218,A219,A220,A221,A222,A223,A224) values('" + row.Cells[0].Text.Replace(",", ".") + "','" + row.Cells[1].Text + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + row.Cells[8].Text.Replace(",", ".") + "','" + row.Cells[9].Text.Replace(",", ".") + "','" + row.Cells[10].Text.Replace(",", ".") + "','" + row.Cells[11].Text.Replace(",", ".") + "','" + row.Cells[12].Text.Replace(",", ".") + "','" + row.Cells[13].Text.Replace(",", ".") + "','" + row.Cells[14].Text.Replace(",", ".") + "','" + row.Cells[15].Text.Replace(",", ".") + "'," + row.Cells[16].Text.Replace(",", ".") + "," + row.Cells[17].Text.Replace(",", ".") + "," + row.Cells[18].Text.Replace(",", ".") + "," + row.Cells[19].Text.Replace(",", ".") + ")";

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





        protected void Button3_Click(object sender, EventArgs e)
        {
            string file_name;


            //if (open1.ShowDialog() == DialogResult.OK)
            //{

            string ch1;
            ch1 = "";

            string ConnectionString = Get_ConnexionString(); // chaine de connexion


            //-------------------Debut ANXDEB00 -------------------------------------------

            string E000 = "E2";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An2";
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
            mySqDataReader.Close();
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
            mySqDataReader1.Close();
            myConnection1.Close();

            string file_name1 = "ANXEMP_2_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A206 = "";
            string A207 = "";
            string A208 = "";
            string A209 = "";
            string A210 = "";
            string A211 = "";
            string A212 = "";
            string A213 = "";
            string A214 = "";
            string A215 = "";
            string A216 = "";
            string A217 = "";
            string A218 = "";
            string A219 = "";
            string A220 = "";
            string A221 = "";
            string A222 = "";
            string A223 = "";
            string A224 = "";



            //Totaux
            decimal T207 = 0;
            decimal T208 = 0;
            decimal T209 = 0;
            decimal T210 = 0;
            decimal T211 = 0;
            decimal T212 = 0;
            decimal T213 = 0;
            decimal T214 = 0;
            decimal T216 = 0;
            decimal T217 = 0;
            decimal T218 = 0;

            String SQL;
            SQL = "SELECT A206,A207,A208,A209,A210,A211,A212,A213,A214,A215,A216,A217,A218,A219,A220,A221,A222,A223,A224";
            SQL += " FROM [dbo].[T_ANXBEN02]";
            SQL += " WHERE A205 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A206 = mySqDataReader2["A206"].ToString();
                A207 = mySqDataReader2["A207"].ToString();
                A208 = mySqDataReader2["A208"].ToString().Replace("‘", "");
                A209 = mySqDataReader2["A209"].ToString().Replace("‘", "");
                A210 = mySqDataReader2["A210"].ToString().Replace("‘", "");
                A211 = mySqDataReader2["A211"].ToString().Replace("‘", "");
                A212 = mySqDataReader2["A212"].ToString().Replace("‘", "");
                A213 = mySqDataReader2["A213"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A214 = mySqDataReader2["A214"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A215 = mySqDataReader2["A215"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A216 = mySqDataReader2["A216"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A217 = mySqDataReader2["A217"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A218 = mySqDataReader2["A218"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A219 = mySqDataReader2["A219"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A220 = mySqDataReader2["A220"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A221 = mySqDataReader2["A221"].ToString();
                A222 = mySqDataReader2["A222"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A223 = mySqDataReader2["A223"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A224 = mySqDataReader2["A224"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                T207 += Convert.ToDecimal(mySqDataReader2["A213"]);
                T208 += Convert.ToDecimal(mySqDataReader2["A214"]);
                T209 += Convert.ToDecimal(mySqDataReader2["A215"]);
                T210 += Convert.ToDecimal(mySqDataReader2["A216"]);
                T211 += Convert.ToDecimal(mySqDataReader2["A217"]);
                T212 += Convert.ToDecimal(mySqDataReader2["A218"]);
                T213 += Convert.ToDecimal(mySqDataReader2["A219"]);
                T214 += Convert.ToDecimal(mySqDataReader2["A220"]);
                T216 += Convert.ToDecimal(mySqDataReader2["A222"]);
                T217 += Convert.ToDecimal(mySqDataReader2["A223"]);
                T218 += Convert.ToDecimal(mySqDataReader2["A224"]);

                A206 = A206.PadLeft(6, '0');

                A207 = A207.PadLeft(1, '2');

                A208 = A208.PadRight(13, ' ');

                A209 = A209.PadRight(40, ' ');

                A210 = A210.PadRight(40, ' ');

                A211 = A211.PadRight(120, ' ');
                A212 = A212.PadRight(1, '0');
                A213 = A213.PadLeft(15, '0');

                A214 = A214.PadLeft(15, '0');

                A215 = A215.PadLeft(15, '0');

                A216 = A216.PadLeft(15, '0');

                A217 = A217.PadLeft(15, '0');

                A218 = A218.PadLeft(15, '0');

                A219 = A219.PadLeft(15, '0');

                A220 = A220.PadLeft(15, '0');

                A221 = A221.PadLeft(1, '0');

                A222 = A222.PadLeft(15, '0');

                A223 = A223.PadLeft(15, '0');

                A224 = A224.PadLeft(15, '0');

                ch1 = "L2" + E001 + E002 + E003 + E004 + E005 + A206 + A207 + A208 + A209 + A210 + A211 + A212 + A213 + A214 + A215 + A216 + A217 + A218 + A219 + A220 + A221 + A222 + A223 + A224;
                objWriter.WriteLine(ch1);


            }
            mySqDataReader2.Close();
            myConnection2.Close();



            //-------------------Fin ANXBEN02 -------------------------------------------

            //-------------------Debut ANXFIN02 -------------------------------------------

            string _T206 = "";
            _T206 = _T206.PadRight(221, ' ');

            string _T207 = T207.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T208 = T208.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T209 = T209.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T210 = T210.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T211 = T211.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T212 = T212.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T213 = T213.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T214 = T214.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T216 = T216.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T217 = T217.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T218 = T218.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');

            ch1 = "T2" + E001 + E002 + E003 + E004 + E005 + _T206 + _T207 + _T208 + _T209 + _T210 + _T211 + _T212 + _T213 + _T214 + " " + _T216 + _T217 + _T218;
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
            String myquery = "Select * from T_ANXBEN02";
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
            GridView1.FooterRow.Cells[8].Text = dt.Compute("Sum(A213)", "").ToString();
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A214)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A215)", "").ToString();
            GridView1.FooterRow.Cells[11].Text = dt.Compute("Sum(A216)", "").ToString();
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A217)", "").ToString();
            GridView1.FooterRow.Cells[13].Text = dt.Compute("Sum(A218)", "").ToString();
            GridView1.FooterRow.Cells[14].Text = dt.Compute("Sum(A219)", "").ToString();
            GridView1.FooterRow.Cells[15].Text = dt.Compute("Sum(A220)", "").ToString();
            GridView1.FooterRow.Cells[17].Text = dt.Compute("Sum(A222)", "").ToString();
            GridView1.FooterRow.Cells[18].Text = dt.Compute("Sum(A223)", "").ToString();
            GridView1.FooterRow.Cells[19].Text = dt.Compute("Sum(A224)", "").ToString();

            GridView1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();


            //for (int i = 1; i < GridView1.Rows.Count; i++)
            //{



                //GridViewRow row = GridView1.Rows[i];
                SqlCommand cmd1 = new SqlCommand();
                String quer = "select * from T_ANXBEN02 where A208 = '" + TextBox3.Text + "'";


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
                    String A206Numérodordre = TextBox1.Text;
            String A607Typedidentifiant = DropDownList1.SelectedValue;

            String A208matriculebénéficiaireounumérodelaCIN = TextBox3.Text;
            String A209Nometprénomouraisonsocialedubénéficiaire = TextBox4.Text;
            String A210Activitédubénéficiaire = TextBox5.Text;

            String A211dernièreadressedubénéficiaire = TextBox6.Text;
            String A212Typedesmontantsservisautitredes = DropDownList3.SelectedValue;

            String A213montantbrutdeshonorairescommissionscourtagesloyersrémunérationspayéesencontrepartiedelaperformancedanslaprestationetrémunérationsdesactivitésnoncommercialesservisàdespersonnesrésidentesetétablies = TextBox8.Text;

            String A214Honorairesservisauxsociétésetauxpersonnesphysiquessoumisesaurégimeréel = TextBox9.Text;

            String A215Rémunérationsetprimesattribuéesauxmembresdeconseilsdecomitésetdecommissionsactionsetpartssociales = TextBox10.Text;

            String A216Rémunérationspayéesauxsalariésetauxnonsalariésencontrepartieduntravailoccasionnelouaccidentelendehorsdeleuractivitéprincipale = TextBox11.Text;
            String A217plusvalueimmobilièreprixdelimmeubledéclarédanslacte = TextBox12.Text;
            String A218Loyersdeshotelspayésauxsociétésetauxpersonnesphysiquessoumisesaurégimeréel = TextBox13.Text;
            String A219Rémunérationsserviesauxartistesetcréateurs = TextBox14.Text;
            String A220Honorairesservisauxbureauxdétudesexportateurs = TextBox15.Text;
            String A221Typedesmontantsservisautitredes = DropDownList2.SelectedValue;
            String A222Lemontantbrutdeshonorairescommissionscourtagesloyersetrémunérationsdesactivitésnoncommercialesprovenantdesopérationsdexportation = TextBox17.Text;
            String A223Montantdesretenuesopérées = TextBox18.Text;
            String A224Montantnetservi = TextBox19.Text;
            T_ANXBEN02 an2 = new T_ANXBEN02();
            an2.A206 = A206Numérodordre;
            an2.A207 = A607Typedidentifiant;
            an2.A208 = A208matriculebénéficiaireounumérodelaCIN;
            an2.A209 = A209Nometprénomouraisonsocialedubénéficiaire;
            an2.A210 = A210Activitédubénéficiaire;
            an2.A211 = A211dernièreadressedubénéficiaire;
            an2.A212 = A212Typedesmontantsservisautitredes;
            an2.A213 = Convert.ToDecimal(A213montantbrutdeshonorairescommissionscourtagesloyersrémunérationspayéesencontrepartiedelaperformancedanslaprestationetrémunérationsdesactivitésnoncommercialesservisàdespersonnesrésidentesetétablies);
            an2.A214 = Convert.ToDecimal(A214Honorairesservisauxsociétésetauxpersonnesphysiquessoumisesaurégimeréel);
            an2.A215 = Convert.ToDecimal(A215Rémunérationsetprimesattribuéesauxmembresdeconseilsdecomitésetdecommissionsactionsetpartssociales);
            an2.A216 = Convert.ToDecimal(A216Rémunérationspayéesauxsalariésetauxnonsalariésencontrepartieduntravailoccasionnelouaccidentelendehorsdeleuractivitéprincipale);
            an2.A217 = Convert.ToDecimal(A217plusvalueimmobilièreprixdelimmeubledéclarédanslacte);
            an2.A218 = Convert.ToInt32(A218Loyersdeshotelspayésauxsociétésetauxpersonnesphysiquessoumisesaurégimeréel);
            an2.A219 = Convert.ToInt32(A219Rémunérationsserviesauxartistesetcréateurs);
            an2.A220 = Convert.ToInt32(A220Honorairesservisauxbureauxdétudesexportateurs);
            an2.A221 = A221Typedesmontantsservisautitredes;
            an2.A222 = Convert.ToInt32(A222Lemontantbrutdeshonorairescommissionscourtagesloyersetrémunérationsdesactivitésnoncommercialesprovenantdesopérationsdexportation);
            an2.A223 = Convert.ToInt32(A223Montantdesretenuesopérées);
            an2.A224 = Convert.ToInt32(A224Montantnetservi);
            an2.T_Exercice = db.T_Exercice.Find(3);
                    an2.T_Utilisateur = db.T_Utilisateur.Find(2003);

            db.T_ANXBEN02.Add(an2);
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
    }


 }

