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
    public partial class Tableau_récap : System.Web.UI.Page
    {
        GDLEntities1 db = new GDLEntities1();
        private string Exercice = "";
        private StreamWriter bjWriter;
        private string open;
        private object dataGridView1;
        private object label1;
        private object open1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                //if (Session["Login"].ToString() == "admin")
                //{
                //    structure.Visible = true;
                //}
                //else
                //{
                //    structure.Visible = false;

                //}
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            string ConnectionString = Get_ConnexionString(); // chaine de connexion

            decimal DECEMP37; //Total général des retenues opérées.
            DECEMP37 = 0;


            DataTable table = new DataTable();

            table.Columns.Add("Tot_Ass", typeof(String));
            table.Columns.Add("Tau_Ret", typeof(String));
            table.Columns.Add("Tot_Ret", typeof(String));
            table.Columns.Add("Code", typeof(String));
            table.Columns.Add("Libele", typeof(String));


            String SQL8;
            SQL8 = "SELECT *";
            SQL8 += " FROM [dbo].[T_Requettes]";


            SqlConnection myConnection8;
            SqlCommand myCommand8;

            myConnection8 = new SqlConnection(ConnectionString);

            myConnection8.Open();

            myCommand8 = new SqlCommand(SQL8, myConnection8);
            SqlDataReader mySqDataReader8 = myCommand8.ExecuteReader();

            while (mySqDataReader8.Read())
            {
                string Code = mySqDataReader8["Code"].ToString();
                string Libele = mySqDataReader8["Libele"].ToString();
                string RequeteAss = mySqDataReader8["RequeteAss"].ToString();
                //RequeteAss = "SELECT  SUM(A119) AS A119 FROM   dbo.T_ANXBEN01".ToString();
                string RequetRet = mySqDataReader8["RequetRet"].ToString();
                string Taux = mySqDataReader8["Taux"].ToString();

                decimal DECEMP_Ret = 0; //Retenue
                decimal DECEMP_Ass = 0; //Assiette

                if (RequeteAss != "")
                {
                    DECEMP_Ass = Executer_SQL(RequeteAss);
                }

                if (RequetRet != "")
                {
                    DECEMP_Ret = Executer_SQL(RequetRet);

                    DECEMP37 += DECEMP_Ret;
                }

                table.Rows.Add(DECEMP_Ass, DECEMP_Ret, Taux.ToString() , Code, Libele);


            }

            myConnection8.Close();


            GridView1.DataSource = table;
            GridView1.DataBind();
            //label1.Text = DECEMP37.ToString();


        }
        
        decimal Executer_SQL(string SQL)
        {
            decimal Total = 0;

            if (SQL != "&nbsp;")
            {

                string ConnectionString = Get_ConnexionString(); // chaine de connexion

                SQL = SQL.Replace("*", ",");
                SQL += "" + 3;

                //  MessageBox.Show(SQL);

                //SqlConnection myConnection9;
                SqlCommand myCommand9;

                SqlConnection myConnection9 = new SqlConnection(ConnectionString);

                myConnection9.Open();

                myCommand9 = new SqlCommand(SQL, myConnection9);
                SqlDataReader mySqDataReader9 = myCommand9.ExecuteReader();

                while (mySqDataReader9.Read())
                {
                    Total = Convert.ToDecimal(mySqDataReader9[0].ToString());
                }
                myConnection9.Close();

            }

            return Total;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = Get_ConnexionString(); // chaine de connexion

            //--------------Supprimer les enregistrement de l'exercice en cours---------------

            SqlConnection con1 = new SqlConnection(ConnectionString);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM [dbo].[T_DECEMP0N] WHERE Exercice = " + 3, con1);

            SqlDataReader reader1 = cmd1.ExecuteReader();

            con1.Close();

            //----------------Fin Suppression-----------------------------

            foreach (GridViewRow item in GridView1.Rows)
            {

                int n = item.RowIndex;

                //------------Ajout les nouvelles données----------------------

                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();

                string SQL = "";
                SQL = "INSERT INTO T_DECEMP0N";
                SQL += "(";
                SQL += "Code,Libele,Tot_Ass , Tau_Ret , Tot_Ret , Exercice";
                SQL += ")";
                SQL += "VALUES ";
                SQL += "(";
                SQL += "'" + GridView1.Rows[n].Cells[3].Text.ToString() + "',";
                SQL += "'" + GridView1.Rows[n].Cells[4].Text.Replace("'", "‘") + "',";

                if (GridView1.Rows[n].Cells[0].Text.ToString() == "")
                {
                    SQL += "0,";
                }
                else
                {
                    SQL += "" + GridView1.Rows[n].Cells[0].Text.ToString().Replace(",", ".") + ",";
                }

                if (GridView1.Rows[n].Cells[1].Text.ToString().Replace("%", "").Trim() == "")
                {
                    SQL += "0,";
                }
                else
                {
                    SQL += "" + GridView1.Rows[n].Cells[1].Text.ToString().Replace(",", ".").Replace("%", "") + ",";
                }

                if (GridView1.Rows[n].Cells[2].Text.ToString() == "")
                {
                    SQL += "0,";
                }
                else
                {
                    SQL += "'" + GridView1.Rows[n].Cells[2].Text.ToString().Replace(",", ".") + "',";
                }

                SQL += "3";
              
                SQL += ")";



                SqlCommand cmd = new SqlCommand(SQL, con);

                SqlDataReader reader = cmd.ExecuteReader();

                con.Close();

            }


            //Decomp 37

            SqlConnection con2 = new SqlConnection(ConnectionString);
            con2.Open();

            string SQL1 = "";
            SQL1 = "INSERT INTO T_DECEMP0N";
            SQL1 += "(";
            SQL1 += "Tot_Ass , Tau_Ret , Tot_Ret , Exercice";
            SQL1 += ")";
            SQL1 += "VALUES ";
            SQL1 += "(";
            SQL1 += "'0',";
            SQL1 += "'0',";
            SQL1 += "'0',";

            SQL1 += "'" + 3 + "'";
            SQL1 += ")";


            SqlCommand cmd2 = new SqlCommand(SQL1, con2);

            SqlDataReader reader2 = cmd2.ExecuteReader();

            con2.Close();

            Refresh_list();

        }

        public string Get_ConnexionString()
        {

            string ch = "";
            ch = ConfigurationManager.ConnectionStrings["GDLConnectionString2"].ConnectionString.ToString();
            return ch;
        }

        private void Refresh_list()
        {
           // throw new NotImplementedException();
        }
        private class GridView1Row
        {
            internal int Index;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
        string file_name;
            


            string ch1;
            ch1 = "";

            string ConnectionString = Get_ConnexionString(); // chaine de connexion

            //-------------------Debut DECEMP00 -------------------------------------------

            string E000 = "000";
            string E001 = "";
            string E002 = "";
            string E003 = "";
            string E004 = "";
            string E005 = "";


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
                E002 = mySqDataReader1["CleMatriculeFiscal"].ToString();
                E003 = mySqDataReader1["CodeCategorie"].ToString();
                E004 = mySqDataReader1["NumEtablissement"].ToString();

            }

            myConnection1.Close();

            string file_name1 = "DECEMP_" + E005.Substring(0) + ".txt";
            //file_name1 = (open1.SelectedPath + "\\" + file_name1).ToString();

            System.IO.StreamWriter objWriter;
            objWriter = new System.IO.StreamWriter(file_name1);


            string D006 = ""; // code présence annexe 1
            if (ANXBEN01.Checked)
            {
                D006 = "1";
            }
            else
            {
                D006 = "0";
            }
            string D007 = "";  // code présence annexe 2
            if (ANXBEN02.Checked)
            {
                D007 = "1";
            }
            else
            {
                D007 = "0";
            }

            string D008 = "";  // code présence annexe 3
            if (ANXBEN03.Checked)
            {
                D008 = "1";
            }
            else
            {
                D008 = "0";
            }
            string D009 = "";  // code présence annexe 4
            if (ANXBEN04.Checked)
            {
                D009 = "1";
            }
            else
            {
                D009 = "0";
            }
            string D010 = "";  // code présence annexe 5
            if (ANXBEN05.Checked)
            {
                D010 = "1";
            }
            else
            {
                D010 = "0";
            }
            string D011 = "";  // code présence annexe 6
            if (ANXBEN06.Checked)
            {
                D011 = "1";
            }
            else
            {
                D011 = "0";
            }
            string D012 = "";  // code présence annexe 7
            if (ANXBEN07.Checked)
            {
                D012 = "1";
            }
            else
            {
                D012 = "0";
            }
            string D013 = "";
            D013 = D013.PadRight(12, ' ');

            ch1 = E000 + E001 + E002 + E003 + E004 + E005 + D006 + D007 + D008 + D009 + D010 + D011 + D012 + D013;
            objWriter.WriteLine(ch1);

            //-------------------Fin DECEMP00 -------------------------------------------

            int cmp; //compteur
            cmp = 0;




            String SQL;
            SQL = "SELECT Tot_Ass , Tau_Ret , Tot_Ret ";
            SQL += " FROM [dbo].[T_DECEMP0N]";
            SQL += " WHERE Exercice = " + 3;

            SqlConnection myConnection2;

            myConnection2 = new SqlConnection(ConnectionString);

            myConnection2.Open();

            myCommand = new SqlCommand(SQL, myConnection2);
            SqlDataReader mySqDataReader2 = myCommand.ExecuteReader();

            while (mySqDataReader2.Read())
            {

                cmp = cmp + 1;



                switch (cmp)
                {
                    case 1:
                        //-------------------Debut DECEMP01 -------------------------------------------
                        string D010_1 = "010";
                        string D011_1 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D012_1 = "00000";
                        string D013_1 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D010_1 + D011_1.PadLeft(15, '0') + D012_1 + D013_1.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP01 -------------------------------------------
                        break;

                    case 2:


                        //-------------------Debut DECEMP02 -------------------------------------------

                        string D020 = "170";
                        string D021 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D022 = "00000";
                        string D023 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D020 + D021.PadLeft(15, '0') + D022 + D023.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP02 -------------------------------------------


                        break;
                    case 3:

                        //-------------------Debut DECEMP03 -------------------------------------------

                        string D000 = "300";
                        string D001 = "";
                        D001 = D001.PadRight(20, ' ');

                        string D003 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D000 + D001 + D003.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP03 -------------------------------------------


                        break;

                    case 4:

                        //-------------------Debut DECEMP03 -------------------------------------------

                        string D030 = "021";
                        string D031 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D032 = "01500";
                        string D033 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D030 + D031.PadLeft(15, '0') + D032 + D033.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP03 -------------------------------------------


                        break;
                    case 5:

                        //-------------------Debut DECEMP04 -------------------------------------------

                        string D040 = "023";
                        string D041 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D042 = "01500";
                        string D043 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D040 + D041.PadLeft(15, '0') + D042 + D043.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP04 -------------------------------------------


                        break;

                    case 6:
                        //-------------------Debut DECEMP05 -------------------------------------------

                        string D050 = "025";
                        string D051 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D052 = "00250";
                        string D053 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D050 + D051.PadLeft(15, '0') + D052 + D053.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP05 -------------------------------------------


                        break;

                    case 7:

                        //-------------------Debut DECEMP06 -------------------------------------------

                        string D060 = "030";
                        string D061 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D062 = "00500";
                        string D063 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D060 + D061.PadLeft(15, '0') + D062 + D063.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP06 -------------------------------------------


                        break;

                    case 8:

                        //-------------------Debut DECEMP07 -------------------------------------------

                        string D070 = "180";
                        string D071 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D072 = "00250";
                        string D073 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D070 + D071.PadLeft(15, '0') + D072 + D073.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP07 -------------------------------------------


                        break;

                    case 9:


                        //-------------------Debut DECEMP08 -------------------------------------------

                        string D080 = "040";
                        string D081 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D082 = "00500";
                        string D083 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D080 + D081.PadLeft(15, '0') + D082 + D083.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP08 -------------------------------------------


                        break;

                    case 10:

                        //-------------------Debut DECEMP09 -------------------------------------------

                        string D090 = "260";
                        string D091 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D092 = "01500";
                        string D093 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D090 + D091.PadLeft(15, '0') + D092 + D093.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP09 -------------------------------------------

                        break;

                    case 11:

                        //-------------------Debut DECEMP010 -------------------------------------------

                        string D100 = "060";
                        string D101 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D102 = "02000";
                        string D103 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D100 + D101.PadLeft(15, '0') + D102 + D103.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP010 -------------------------------------------


                        break;

                    case 12:

                        //-------------------Debut DECEMP11 -------------------------------------------

                        string D110 = "071";
                        string D111 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D112 = "02000";
                        string D113 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D110 + D111.PadLeft(15, '0') + D112 + D113.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP11 -------------------------------------------


                        break;

                    case 13:

                        //-------------------Debut DECEMP12 -------------------------------------------

                        string D120 = "073";
                        string D121 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D122 = "02000";
                        string D123 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D120 + D121.PadLeft(15, '0') + D122 + D123.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP12 -------------------------------------------


                        break;

                    case 14:

                        //-------------------Debut DECEMP13 -------------------------------------------

                        string D130 = "080";
                        string D131 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D132 = "01500";
                        string D133 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D130 + D131.PadLeft(15, '0') + D132 + D133.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP13 -------------------------------------------


                        break;

                    case 15:
                        //-------------------Debut DECEMP14 -------------------------------------------

                        string D140 = "241";
                        string D141 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D142 = "01000";
                        string D143 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D140 + D141.PadLeft(15, '0') + D142 + D143.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP14 -------------------------------------------


                        break;

                    case 16:


                        //-------------------Debut DECEMP15 -------------------------------------------

                        string D150 = "242";
                        string D151 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D152 = "01000";
                        string D153 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D150 + D151.PadLeft(15, '0') + D152 + D153.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP15 -------------------------------------------


                        break;

                    case 17:

                        //-------------------Debut DECEMP16 -------------------------------------------

                        string D160 = "091";
                        string D161 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D162 = "02000";
                        string D163 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D160 + D161.PadLeft(15, '0') + D162 + D163.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP16 -------------------------------------------


                        break;

                    case 18:

                        //-------------------Debut DECEMP17 -------------------------------------------

                        string D170 = "093";
                        string D171 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D172 = "02000";
                        string D173 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D170 + D171.PadLeft(15, '0') + D172 + D173.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP17 -------------------------------------------



                        break;

                    case 19:

                        //-------------------Debut DECEMP18 -------------------------------------------

                        string D180 = "100";
                        string D181 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D182 = "01500";
                        string D183 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D180 + D181.PadLeft(15, '0') + D182 + D183.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP18 -------------------------------------------


                        break;

                    case 20:


                        //-------------------Debut DECEMP19 -------------------------------------------
                        string D190 = "110";
                        string D191 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D192 = "01000";
                        string D193 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D190 + D191.PadLeft(15, '0') + D192 + D193.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP19 -------------------------------------------


                        break;

                    case 21:

                        //-------------------Debut DECEMP20 -------------------------------------------

                        string D200 = "121";
                        string D201 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D202 = "00250";
                        string D203 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D200 + D201.PadLeft(15, '0') + D202 + D203.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP20 -------------------------------------------


                        break;

                    case 22:


                        //-------------------Debut DECEMP21 -------------------------------------------

                        string D210 = "122";
                        string D211 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D212 = "00250";
                        string D213 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D210 + D211.PadLeft(15, '0') + D212 + D213.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP21 -------------------------------------------



                        break;

                    case 23:

                        //-------------------Debut DECEMP22 -------------------------------------------

                        string D220 = "123";
                        string D221 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D222 = "01500";
                        string D223 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D220 + D221.PadLeft(15, '0') + D222 + D223.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP22 -------------------------------------------


                        break;

                    case 24:

                        //-------------------Debut DECEMP23 -------------------------------------------

                        string D230 = "131";
                        string D231 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D232 = "00050";
                        string D233 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D230 + D231.PadLeft(15, '0') + D232 + D233.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP23 -------------------------------------------


                        break;

                    case 25:

                        //-------------------Debut DECEMP24 -------------------------------------------

                        string D240 = "132";
                        string D241 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D242 = "00150";
                        string D243 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D240 + D241.PadLeft(15, '0') + D242 + D243.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP24 -------------------------------------------


                        break;

                    case 26:


                        //-------------------Debut DECEMP25 -------------------------------------------
                        string D250 = "140";
                        string D251 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D252 = "02500";
                        string D253 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D250 + D251.PadLeft(15, '0') + D252 + D253.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP25 -------------------------------------------


                        break;

                    case 27:

                        //-------------------Debut DECEMP26 -------------------------------------------

                        string D260 = "150";
                        string D261 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D262 = "10000";
                        string D263 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D260 + D261.PadLeft(15, '0') + D262 + D263.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP26 -------------------------------------------


                        break;

                    case 28:


                        //-------------------Debut DECEMP27 -------------------------------------------

                        string D270 = "160";
                        string D271 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D272 = "00000";
                        string D273 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D270 + D271.PadLeft(15, '0') + D272 + D273.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP27 -------------------------------------------


                        break;

                    case 29:

                        //-------------------Debut DECEMP28 -------------------------------------------
                        string D280 = "270";
                        string D281 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D282 = "00000";
                        string D283 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D280 + D281.PadLeft(15, '0') + D282 + D283.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP28 -------------------------------------------


                        break;

                    case 30:


                        //-------------------Debut DECEMP29 -------------------------------------------
                        string D290 = "200";
                        string D291 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D292 = "00100";
                        string D293 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D290 + D291.PadLeft(15, '0') + D292 + D293.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP29 -------------------------------------------


                        break;

                    case 31:

                        //-------------------Debut DECEMP30 -------------------------------------------
                        string D300 = "191";
                        string D301 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D302 = "01000";
                        string D303 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D300 + D301.PadLeft(15, '0') + D302 + D303.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP30 -------------------------------------------


                        break;

                    case 32:


                        //-------------------Debut DECEMP31 -------------------------------------------
                        string D310 = "192";
                        string D311 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D312 = "02500";
                        string D313 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D310 + D311.PadLeft(15, '0') + D312 + D313.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP31 -------------------------------------------


                        break;

                    case 33:

                        //-------------------Debut DECEMP32 -------------------------------------------
                        string D320 = "051";
                        string D321 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D322 = "01500";
                        string D323 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D320 + D321.PadLeft(15, '0') + D322 + D323.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP32 -------------------------------------------


                        break;

                    case 34:

                        //-------------------Debut DECEMP33 -------------------------------------------
                        string D330 = "220";
                        string D331 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D332 = "02500";
                        string D333 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D330 + D331.PadLeft(15, '0') + D332 + D333.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP33 -------------------------------------------


                        break;

                    case 35:

                        //-------------------Debut DECEMP34 -------------------------------------------
                        string D340 = "250";
                        string D341 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D342 = "00150";
                        string D343 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D340 + D341.PadLeft(15, '0') + D342 + D343.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP34 -------------------------------------------


                        break;

                    case 36:
                        //-------------------Debut DECEMP35 -------------------------------------------
                        string D350 = "280";
                        string D351 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D352 = "02500";
                        string D353 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D350 + D351.PadLeft(15, '0') + D352 + D353.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP35 -------------------------------------------

                        break;

                    case 37:

                        //-------------------Debut DECEMP36 -------------------------------------------
                        string D360 = "290";
                        string D361 = mySqDataReader2["Tot_Ass"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");
                        string D362 = "00300";
                        string D363 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D360 + D361.PadLeft(15, '0') + D362 + D363.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP36 -------------------------------------------


                        break;

                    case 38:

                        //-------------------Debut DECEMP37 -------------------------------------------
                        string D370 = "999";
                        string D371 = "";
                        D371 = D371.PadRight(20, ' ');

                        string D372 = mySqDataReader2["Tot_Ret"].ToString().Replace(",", "").Replace(".", "").Replace(" ", "");

                        ch1 = D370 + D371 + D372.PadLeft(15, '0');
                        objWriter.WriteLine(ch1);

                        //-------------------Fin DECEMP37 -------------------------------------------


                        break;
                }



            }
            objWriter.Close();
            objWriter.Dispose();

            //MessageBox.Show("Fichier exporté avec succès : " + open1.SelectedPath + "\\" + file_name1, "Operation effecutée", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
