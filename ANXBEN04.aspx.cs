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
    public partial class ANXBEN04 : System.Web.UI.Page
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
                String quer = "select * from T_ANXBEN04 where A408 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN04(A405,A406,A407,A408,A409,A410,A411,A412,A413,A414,A415,A416,A417,A418,A419,A420,A421,A422,A423,A424,A425,A426,A427,idUser) values(" + row.Cells[0].Text.Replace(",", ".") + "," + row.Cells[1].Text.Replace(",", ".") + "," + row.Cells[2].Text.Replace(",", ".") + "," + row.Cells[3].Text.Replace(",", ".") + ",'" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text.Replace(",", ".") + "'," + row.Cells[8].Text.Replace(",", ".") + "," + row.Cells[9].Text.Replace(",", ".") + "," + row.Cells[10].Text.Replace(",", ".") + "," + row.Cells[11].Text.Replace(",", ".") + "," + row.Cells[12].Text.Replace(",", ".") + "," + row.Cells[13].Text.Replace(",", ".") + "," + row.Cells[14].Text.Replace(",", ".") + "," + row.Cells[15].Text.Replace(",", ".") + "," + row.Cells[16].Text.Replace(",", ".") + "," + row.Cells[17].Text.Replace(",", ".") + "," + row.Cells[18].Text.Replace(",", ".") + "," + row.Cells[19].Text.Replace(",", ".") + "," + row.Cells[20].Text.Replace(",", ".") + "," + row.Cells[21].Text.Replace(",", ".") + "," + row.Cells[22].Text.Replace(",", ".") + "," + row.Cells[23].Text.Replace(",", ".") + ")";




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
                String quer = "select * from T_ANXBEN04 where A408 = '" + TextBox3.Text + "'";


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
                    String A406Numérodordre = TextBox1.Text;
                    String A407Naturedelidentifiant = DropDownList1.SelectedValue;
                    String A408Identifiantbénéficiaire = TextBox3.Text;
                    String A409NometPrénomouraisonsocial = TextBox4.Text;
                    String A410Activitédubénéficiaire = TextBox5.Text;
                    String A411Dernièreadressedubénéficiaire = TextBox6.Text;
                    String A412TypedesMontantsservisautitredes = DropDownList2.SelectedValue;
                    String A413TauxdesMontantsservisautitredeshonorairescommissionsmontantsservisauxnonrésidentsétablisenTunisieetquineprocèdentpasaudépôtdeladéclarationdexistence = TextBox8.Text;
                    String A414MontantbrutserviautitredeshonorairescommissionsmontantsservisauxnonrésidentsétablisenTunisieetquineprocèdentpasaudépôtdeladéclarationdexistence = TextBox9.Text;
                    String A415Tauxdeshonorairesservisauxpersonnesnonrésidentesquiréalisentdestravauxdeconstructionoudesopérationsdemontageoudesservicesdecontrôlesconnexesoudautresservicespourunepériodenedépassantpas6mois = TextBox10.Text;
                    String A416Montantdeshonorairesservisauxpersonnesnonrésidentesquiréalisentdestravauxdeconstructionoudesopérationsdemontageoudesservicesdecontrôlesconnexesoudautresservicespourunepériodenedépassantpas6mois = TextBox11.Text;
                    String A417Tauxdeplusvalueimmobilière = TextBox12.Text;
                    String A418Montantdelaplusvalueimmobilière = TextBox13.Text;
                    String A419Tauxdeplusvaluedecessiondesactionsdespartssocialesoudespartsdefondsprévuesparlalégislation = TextBox14.Text;
                    String A420Montantplusvaluedecessiondesactionsdespartssocialesoudespartsdefondsprévuesparlalégislation = TextBox15.Text;
                    String A421Tauxdesrevenusdevaleursmobilièresycomprisjetonsdeprésenceactionsetpartssociales = TextBox16.Text;
                    String A422Montantdesrevenusdevaleursmobilièresycomprisjetonsdeprésenceactionsetpartssociales = TextBox17.Text;
                    String A423Typedesmontantsservisautitredes = DropDownList3.SelectedValue;
                    String A424Montantbrutdeshonorairescommissionscourtagesloyersetrémunérationsdesactivitésnoncommercialesprovenantdesopérationsdexportation = TextBox19.Text;
                    String A425Montantdesrémunérationsourevenusservisàdespersonnesrésidentesouétabliesdansdesparadisfiscaux = TextBox20.Text;
                    String A426Montantdesretenuesopérées = TextBox21.Text;
                    String A427Montantnetservi = TextBox22.Text;

                    T_ANXBEN04 an4 = new T_ANXBEN04();
                    an4.A406 = A406Numérodordre;
                    an4.A407 = A407Naturedelidentifiant;
                    an4.A408 = A408Identifiantbénéficiaire;
                    an4.A409 = A409NometPrénomouraisonsocial;
                    an4.A410 = A410Activitédubénéficiaire;
                    an4.A411 = A411Dernièreadressedubénéficiaire;
                    an4.A412 = A412TypedesMontantsservisautitredes;
                    an4.A413 = Convert.ToDecimal(A413TauxdesMontantsservisautitredeshonorairescommissionsmontantsservisauxnonrésidentsétablisenTunisieetquineprocèdentpasaudépôtdeladéclarationdexistence);
                    an4.A414 = Convert.ToDecimal(A414MontantbrutserviautitredeshonorairescommissionsmontantsservisauxnonrésidentsétablisenTunisieetquineprocèdentpasaudépôtdeladéclarationdexistence);
                    an4.A415 = Convert.ToDecimal(A415Tauxdeshonorairesservisauxpersonnesnonrésidentesquiréalisentdestravauxdeconstructionoudesopérationsdemontageoudesservicesdecontrôlesconnexesoudautresservicespourunepériodenedépassantpas6mois);
                    an4.A416 = Convert.ToDecimal(A416Montantdeshonorairesservisauxpersonnesnonrésidentesquiréalisentdestravauxdeconstructionoudesopérationsdemontageoudesservicesdecontrôlesconnexesoudautresservicespourunepériodenedépassantpas6mois);
                    an4.A417 = Convert.ToDecimal(A417Tauxdeplusvalueimmobilière);
                    an4.A418 = Convert.ToDecimal(A418Montantdelaplusvalueimmobilière);
                    an4.A419 = Convert.ToDecimal(A419Tauxdeplusvaluedecessiondesactionsdespartssocialesoudespartsdefondsprévuesparlalégislation);
                    an4.A420 = Convert.ToDecimal(A420Montantplusvaluedecessiondesactionsdespartssocialesoudespartsdefondsprévuesparlalégislation);
                    an4.A421 = Convert.ToDecimal(A421Tauxdesrevenusdevaleursmobilièresycomprisjetonsdeprésenceactionsetpartssociales);
                    an4.A422 = Convert.ToDecimal(A422Montantdesrevenusdevaleursmobilièresycomprisjetonsdeprésenceactionsetpartssociales);
                    an4.A423 = A423Typedesmontantsservisautitredes;
                    an4.A424 = Convert.ToDecimal(A424Montantbrutdeshonorairescommissionscourtagesloyersetrémunérationsdesactivitésnoncommercialesprovenantdesopérationsdexportation);
                    an4.A425 = Convert.ToDecimal(A425Montantdesrémunérationsourevenusservisàdespersonnesrésidentesouétabliesdansdesparadisfiscaux);
                    an4.A426 = Convert.ToDecimal(A426Montantdesretenuesopérées);
                    an4.A427 = Convert.ToDecimal(A427Montantnetservi);
                    an4.T_Utilisateur = db.T_Utilisateur.Find(2003);
                    an4.T_Exercice = db.T_Exercice.Find(3);
                    db.T_ANXBEN04.Add(an4);
                    db.SaveChanges();
                    ModelState.Clear();
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    gvData.Visible = false;
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

            string E000 = "E4";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An4";
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

            string file_name1 = "ANXEMP_4_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A406 = "";
            string A407 = "";
            string A408 = "";
            string A409 = "";
            string A410 = "";
            string A411 = "";
            string A412 = "";
            string A413 = "";
            string A414 = "";
            string A415 = "";
            string A416 = "";
            string A417 = "";
            string A418 = "";
            string A419 = "";
            string A420 = "";
            string A421 = "";
            string A422 = "";
            string A423 = "";
            string A424 = "";
            string A425 = "";
            string A426 = "";
            string A427 = "";
            string A428 = "";

            //Totaux
            decimal T408 = 0;
            decimal T410 = 0;
            decimal T412 = 0;
            decimal T414 = 0;
            decimal T416 = 0;
            decimal T418 = 0;
            decimal T419 = 0;
            decimal T420 = 0;
            decimal T421 = 0;



            String SQL;
            SQL = "SELECT A406,A407,A408,A409,A410,A411,A412,A413,A414,A415,A416,A417,A418,A419,A420,A421,A422,A423,A424,A425,A426,A427";
            SQL += " FROM [dbo].[T_ANXBEN04]";
            SQL += " WHERE A405 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A406 = mySqDataReader2["A406"].ToString();
                A407 = mySqDataReader2["A407"].ToString();
                A408 = mySqDataReader2["A408"].ToString();
                A409 = mySqDataReader2["A409"].ToString().Replace("‘", "");
                A410 = mySqDataReader2["A410"].ToString().Replace("‘", "");
                A411 = mySqDataReader2["A411"].ToString().Replace("‘", "");
                A412 = mySqDataReader2["A412"].ToString();
                A413 = mySqDataReader2["A413"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A414 = mySqDataReader2["A414"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A415 = mySqDataReader2["A415"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A416 = mySqDataReader2["A416"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A417 = mySqDataReader2["A417"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A418 = mySqDataReader2["A418"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A419 = mySqDataReader2["A419"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A420 = mySqDataReader2["A420"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A421 = mySqDataReader2["A421"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A422 = mySqDataReader2["A422"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A423 = mySqDataReader2["A423"].ToString();
                A424 = mySqDataReader2["A424"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A425 = mySqDataReader2["A425"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A426 = mySqDataReader2["A426"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A427 = mySqDataReader2["A427"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");


                T408 += Convert.ToDecimal(mySqDataReader2["A414"]);
                T410 += Convert.ToDecimal(mySqDataReader2["A416"]);

                T412 += Convert.ToDecimal(mySqDataReader2["A418"]);
                T414 += Convert.ToDecimal(mySqDataReader2["A420"]);
                T416 += Convert.ToDecimal(mySqDataReader2["A422"]);
                T418 += Convert.ToDecimal(mySqDataReader2["A424"]);
                T419 += Convert.ToDecimal(mySqDataReader2["A425"]);
                T420 += Convert.ToDecimal(mySqDataReader2["A426"]);
                T421 += Convert.ToDecimal(mySqDataReader2["A427"]);


                A406 = A406.PadLeft(6, '0');

                A407 = A407.PadLeft(1, '3');

                A408 = A408.PadRight(13, ' ');

                A409 = A409.PadRight(40, ' ');

                A410 = A410.PadRight(40, ' ');

                A411 = A411.PadRight(120, ' ');

                A412 = A412.PadRight(1, '0');

                A413 = A413.PadLeft(5, '0');

                A414 = A414.PadLeft(15, '0');

                A415 = A415.PadLeft(5, '0');

                A416 = A416.PadLeft(15, '0');

                A417 = A417.PadLeft(5, '0');

                A418 = A418.PadLeft(15, '0');

                A419 = A419.PadLeft(5, '0');

                A420 = A420.PadLeft(15, '0');

                A421 = A421.PadLeft(5, '0');

                A422 = A422.PadLeft(15, '0');

                A423 = A423.PadLeft(1, '0');

                A424 = A424.PadLeft(15, '0');

                A425 = A425.PadLeft(15, '0');

                A426 = A426.PadLeft(15, '0');

                A427 = A427.PadLeft(15, '0');

                A428 = A428.PadRight(5, ' ');

                ch1 = "L4" + E001 + E002 + E003 + E004 + E005 + A406 + A407 + A408 + A409 + A410 + A411 + A412 + A413 + A414 + A415 + A416 + A417 + A418 + A419 + A420 + A421 + A422 + A423 + A424 + A425 + A426 + A427;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T406 = "";
            _T406 = _T406.PadRight(222, ' ');
            string _T407 = "0000";
            string _T409 = "0000";
            string _T411 = "0000";
            string _T413 = "0000";
            string _T415 = "00000";
            string _T408 = T408.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T410 = T410.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T412 = T412.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T414 = T414.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T416 = T416.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T418 = T418.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T419 = T419.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T420 = T420.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T421 = T421.ToString().Replace(",", "").Replace(".", "").PadLeft(40, '0');

            string _T417 = "";
            _T417 = _T417.PadRight(1, ' ');

            string _T422 = "";
            _T422 = _T422.PadRight(5, ' ');

            ch1 = "T4" + E001 + E002 + E003 + E004 + E005 + _T406  + _T408  + _T410  + _T412  + _T414  + _T416   + _T418 + _T419 + _T420 + _T421 + _T422 ;
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
            String myquery = "Select * from T_ANXBEN04";
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
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A413)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A414)", "").ToString();
            GridView1.FooterRow.Cells[11].Text = dt.Compute("Sum(A415)", "").ToString();
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A416)", "").ToString();
            GridView1.FooterRow.Cells[13].Text = dt.Compute("Sum(A417)", "").ToString();
            GridView1.FooterRow.Cells[14].Text = dt.Compute("Sum(A418)", "").ToString();
            GridView1.FooterRow.Cells[15].Text = dt.Compute("Sum(A419)", "").ToString();
            GridView1.FooterRow.Cells[16].Text = dt.Compute("Sum(A420)", "").ToString();
            GridView1.FooterRow.Cells[17].Text = dt.Compute("Sum(A421)", "").ToString();
            GridView1.FooterRow.Cells[18].Text = dt.Compute("Sum(A422)", "").ToString();
            GridView1.FooterRow.Cells[20].Text = dt.Compute("Sum(A424)", "").ToString();
            GridView1.FooterRow.Cells[21].Text = dt.Compute("Sum(A425)", "").ToString();
            GridView1.FooterRow.Cells[22].Text = dt.Compute("Sum(A426)", "").ToString();
            GridView1.FooterRow.Cells[23].Text = dt.Compute("Sum(A427)", "").ToString();

            GridView1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }

       
 }
