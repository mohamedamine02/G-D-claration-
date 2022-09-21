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
    public partial class ANXBEN06 : System.Web.UI.Page
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
                String quer = "select * from T_ANXBEN06 where A608 = '" + row.Cells[3].Text.Replace(",", ".") + "'";


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
                    String query = "insert into T_ANXBEN06(A605,A606,A607,A608,A609,A610,A611,A612,A613,A614,A615,A616,A617,A618,A619) values (" + row.Cells[0].Text + "," + row.Cells[1].Text + "," + row.Cells[2].Text + "," + row.Cells[3].Text + ",'" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "'," + row.Cells[7].Text.Replace(",", ".") + "," + row.Cells[8].Text.Replace(",", ".") + "," + row.Cells[9].Text.Replace(",", ".") + "," + row.Cells[10].Text.Replace(",", ".") + "," + row.Cells[11].Text.Replace(",", ".") + "," + row.Cells[12].Text.Replace(",", ".") + "," + row.Cells[13].Text.Replace(",", ".") + "," + row.Cells[14].Text.Replace(",", ".") + ")";






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
                String quer = "select * from T_ANXBEN06 where A608 = '" + TextBox3.Text + "'";


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
                    String A606Numérodordre = TextBox1.Text;
                    String A607Typedidentifiant = DropDownList1.SelectedItem.Value;
                    String A608Identifiantbénéficiaire = TextBox3.Text;
                    String A609Nometprénomouraisonsociale = TextBox4.Text;
                    String A610Activité = TextBox5.Text;
                    String A611Dernireadressedubénéficiaire = TextBox6.Text;
                    String A612Montantesristournescommercialesetnoncommercialesservies = TextBox7.Text;
                    String A613MontantdesventesauPPsoumisesàlimpotsurlerevenuselonlerégimeforfaitaire = TextBox8.Text;
                    String A614MontantdelavanceduesurlesventesauPPsoumisesàlimpotsurlerevenuselonlerégimeforfaitaire = TextBox9.Text;
                    String A615Montantdesrevenusautitredesjeuxdeparidehasardetdeloterie = TextBox10.Text;
                    String A616Retenueàlasourcesurlesrevenusautitredesjeuxdeparidehasardetdeloterie = TextBox11.Text;
                    String A617Montantdesventesdesentreprisesindustriellesetdecommerceauprofitdesintervenantsdansladistributiondebiensetdeproduitsetservicesquinedépassentpas20000DTparan = TextBox12.Text;
                    String A618Retenuesàlasourcesurlesventesdesentreprisesindustriellesetdecommerceauprofitdesintervenantsdansladistributiondebiensetdeproduitsetservicesquinedépassentpas20000DTparan = TextBox13.Text;
                    String A619Montantperçusenespècesautitredesmarchandisesvenduesetservicesrendus = TextBox14.Text;
                    T_ANXBEN06 an6 = new T_ANXBEN06();
                    an6.A606 = A606Numérodordre;
                    an6.A607 = A607Typedidentifiant;
                    an6.A608 = A608Identifiantbénéficiaire;
                    an6.A609 = A609Nometprénomouraisonsociale;
                    an6.A610 = A610Activité;
                    an6.A611 = A611Dernireadressedubénéficiaire;
                    an6.A612 = Convert.ToDecimal(A612Montantesristournescommercialesetnoncommercialesservies);
                    an6.A613 = Convert.ToDecimal(A613MontantdesventesauPPsoumisesàlimpotsurlerevenuselonlerégimeforfaitaire);
                    an6.A614 = Convert.ToDecimal(A614MontantdelavanceduesurlesventesauPPsoumisesàlimpotsurlerevenuselonlerégimeforfaitaire);
                    an6.A615 = Convert.ToDecimal(A615Montantdesrevenusautitredesjeuxdeparidehasardetdeloterie);
                    an6.A616 = Convert.ToDecimal(A616Retenueàlasourcesurlesrevenusautitredesjeuxdeparidehasardetdeloterie);
                    an6.A617 = Convert.ToDecimal(A617Montantdesventesdesentreprisesindustriellesetdecommerceauprofitdesintervenantsdansladistributiondebiensetdeproduitsetservicesquinedépassentpas20000DTparan);
                    an6.A618 = Convert.ToDecimal(A618Retenuesàlasourcesurlesventesdesentreprisesindustriellesetdecommerceauprofitdesintervenantsdansladistributiondebiensetdeproduitsetservicesquinedépassentpas20000DTparan);
                    an6.A619 = Convert.ToDecimal(A619Montantperçusenespècesautitredesmarchandisesvenduesetservicesrendus);
                    an6.T_Exercice = db.T_Exercice.Find(3);
                    db.T_ANXBEN06.Add(an6);
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

            string E000 = "E6";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";
            string E006 = "An6";
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

            string file_name1 = "ANXEMP_6_" + E005.Substring(0) + "_1.txt";
            //file_name = (open + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + E006 + E007 + E008.PadLeft(6, '0') + E009.PadRight(40, ' ') + E010.PadRight(40, ' ') + E011.PadRight(40, ' ') + E012.PadRight(72, ' ') + E013 + E014 + E015.PadRight(177, ' ');
            objWriter.WriteLine(ch1);

            //-------------------Fin ANXDEB00 -------------------------------------------

            //-------------------Debut ANXBEN01 -------------------------------------------

            string A606 = "";
            string A607 = "";
            string A608 = "";
            string A609 = "";
            string A610 = "";
            string A611 = "";
            string A612 = "";
            string A613 = "";
            string A614 = "";
            string A615 = "";
            string A616 = "";
            string A617 = "";
            string A618 = "";
            string A619 = "";
            string A620 = "";


            // Totaux

            decimal T607 = 0;
            decimal T608 = 0;
            decimal T609 = 0;
            decimal T610 = 0;
            decimal T611 = 0;
            decimal T612 = 0;
            decimal T613 = 0;
            decimal T614 = 0;

            String SQL;
            SQL = "SELECT A606,A607,A608,A609,A610,A611,A612,A613,A614,A615,A616,A617,A618,A619";
            SQL += " FROM [dbo].[T_ANXBEN06]";
            SQL += " WHERE A605 = '" + Exercice + "'";

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                A606 = mySqDataReader2["A606"].ToString();
                A607 = mySqDataReader2["A607"].ToString();
                A608 = mySqDataReader2["A608"].ToString();
                A609 = mySqDataReader2["A609"].ToString().Replace("‘", "");
                A610 = mySqDataReader2["A610"].ToString().Replace("‘", "");
                A611 = mySqDataReader2["A611"].ToString().Replace("‘", "");
                A612 = mySqDataReader2["A612"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A613 = mySqDataReader2["A613"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A614 = mySqDataReader2["A614"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A615 = mySqDataReader2["A615"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A616 = mySqDataReader2["A616"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A617 = mySqDataReader2["A617"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A618 = mySqDataReader2["A618"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                A619 = mySqDataReader2["A619"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");


                T607 += Convert.ToDecimal(mySqDataReader2["A612"]);
                T608 += Convert.ToDecimal(mySqDataReader2["A613"]);
                T609 += Convert.ToDecimal(mySqDataReader2["A614"]);
                T610 += Convert.ToDecimal(mySqDataReader2["A615"]);
                T611 += Convert.ToDecimal(mySqDataReader2["A616"]);
                T612 += Convert.ToDecimal(mySqDataReader2["A617"]);
                T613 += Convert.ToDecimal(mySqDataReader2["A618"]);
                T614 += Convert.ToDecimal(mySqDataReader2["A619"]);



                A606 = A606.PadLeft(6, '0');
                A607 = A607.PadLeft(1, '1');

                A608 = A608.PadRight(13, ' ');

                A609 = A609.PadRight(40, ' ');

                A610 = A610.PadRight(40, ' ');

                A611 = A611.PadRight(120, ' ');
                A612 = A612.PadLeft(15, '0');
                A613 = A613.PadLeft(15, '0');
                A614 = A614.PadLeft(15, '0');
                A615 = A615.PadLeft(15, '0');
                A616 = A616.PadLeft(15, '0');
                A617 = A617.PadLeft(15, '0');
                A618 = A618.PadLeft(15, '0');
                A619 = A619.PadLeft(15, '0');
                A620 = A620.PadLeft(47, ' ');







                ch1 = "L6" + E001 + E002 + E003 + E004 + E005 + A606 + A607 + A608 + A609 + A610 + A611 + A612 + A613 + A614 + A615 + A616 + A617 + A618 + A619 + A620;
                objWriter.WriteLine(ch1);


            }

            myConnection2.Close();



            //-------------------Fin ANXBEN01 -------------------------------------------

            //-------------------Debut ANXFIN01 -------------------------------------------

            string _T606 = "";
            _T606 = _T606.PadRight(220, ' ');
            string _T607 = T607.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T608 = T608.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T609 = T609.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T610 = T610.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T611 = T611.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T612 = T612.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T613 = T613.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');
            string _T614 = T614.ToString().Replace(",", "").Replace(".", "").PadLeft(15, '0');


            string _T615 = "";
            _T615 = _T615.PadRight(47, ' ');

            ch1 = "T6" + E001 + E002 + E003 + E004 + E005 + _T606 + _T607 + _T608 + _T609 + _T610 + _T611 + _T612 + _T613 + _T614 + _T615;
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

        protected void Selecte(object sender, GridViewSelectEventArgs e)
        {
            int s = e.NewSelectedIndex;
            TextBox1.Text = GridView1.Rows[s].Cells[2].Text;
            DropDownList1.SelectedValue = GridView1.Rows[s].Cells[3].Text;
            TextBox3.Text = GridView1.Rows[s].Cells[4].Text;
            TextBox4.Text = GridView1.Rows[s].Cells[5].Text;
            TextBox5.Text = GridView1.Rows[s].Cells[6].Text;
            TextBox6.Text = GridView1.Rows[s].Cells[7].Text;
            TextBox7.Text = GridView1.Rows[s].Cells[8].Text;
            TextBox8.Text = GridView1.Rows[s].Cells[9].Text;
            TextBox9.Text = GridView1.Rows[s].Cells[10].Text;
            TextBox10.Text = GridView1.Rows[s].Cells[11].Text;
            TextBox11.Text = GridView1.Rows[s].Cells[12].Text;
            TextBox12.Text = GridView1.Rows[s].Cells[13].Text;
            TextBox13.Text = GridView1.Rows[s].Cells[14].Text;
            TextBox14.Text = GridView1.Rows[s].Cells[15].Text;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            String myquery = "update T_ANXBEN06 set  A607 = '"+DropDownList1.SelectedValue+"',A608= '"+TextBox3.Text+"',A609 ='"+TextBox4.Text+"',A610 ='" +TextBox5.Text+"',A611 = '"+TextBox6.Text+ "',A612 = '" + TextBox7.Text + "',A613 = '" + TextBox8.Text + "',A614 = '" + TextBox9.Text + "',A615= '" + TextBox10.Text + "',A616 = '" + TextBox11.Text + "',A617 = '" + TextBox12.Text + "',A618= '" + TextBox13.Text + "',A619= '" + TextBox14.Text + "' where A606= '" + TextBox1.Text + "' ";
            SqlConnection con = new SqlConnection(mycon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            ModelState.Clear();
            GridView1.DataBind();
            con.Close();

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String mycon = "Data Source=DESKTOP-B9MQU2G\\SAGE100; Initial Catalog=GDL; Integrated Security=true";
            String myquery = "Select * from T_ANXBEN06";
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
            GridView1.FooterRow.Cells[8].Text = dt.Compute("Sum(A612)", "").ToString();
            GridView1.FooterRow.Cells[9].Text = dt.Compute("Sum(A613)", "").ToString();
            GridView1.FooterRow.Cells[10].Text = dt.Compute("Sum(A614)", "").ToString();
            GridView1.FooterRow.Cells[11].Text = dt.Compute("Sum(A615)", "").ToString();
            GridView1.FooterRow.Cells[12].Text = dt.Compute("Sum(A616)", "").ToString();
            GridView1.FooterRow.Cells[13].Text = dt.Compute("Sum(A617)", "").ToString();
            GridView1.FooterRow.Cells[14].Text = dt.Compute("Sum(A618)", "").ToString();
            GridView1.FooterRow.Cells[15].Text = dt.Compute("Sum(A619)", "").ToString();

            GridView1.Visible = true;
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }

       
 }
