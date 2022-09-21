using creation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace creation
{
    public partial class T_DECEMP0N : System.Web.UI.Page
    {
        GDLEntities db = new GDLEntities();
        private string Exercice = "3";
        private StreamWriter bjWriter;
        private string open;
        private object dataGridView1;
        private object label1;
        protected void Page_Load(object sender, EventArgs e)
        {
        //    string ConnectionString = Get_ConnexionString(); // chaine de connexion

        //    decimal DECEMP37; //Total général des retenues opérées.
        //    DECEMP37 = 0;


        //    DataTable table = new DataTable();

        //    table.Columns.Add("Code", typeof(String));
        //    table.Columns.Add("Libellé", typeof(String));
        //    table.Columns.Add("Total Assiette", typeof(String));
        //    table.Columns.Add("Taux", typeof(String));
        //    table.Columns.Add("Total Retenue", typeof(String));


        //    String SQL8;
        //    SQL8 = "SELECT *";
        //    SQL8 += " FROM [dbo].[T_Requettes]";


        //    SqlConnection myConnection8;
        //    SqlCommand myCommand8;

        //    myConnection8 = new SqlConnection(ConnectionString);

        //    myConnection8.Open();

        //    myCommand8 = new SqlCommand(SQL8, myConnection8);
        //    SqlDataReader mySqDataReader8 = myCommand8.ExecuteReader();

        //    while (mySqDataReader8.Read())
        //    {
        //        string Code = mySqDataReader8["Code"].ToString();
        //        string Libele = mySqDataReader8["Libele"].ToString();
        //        string RequeteAss = mySqDataReader8["RequeteAss"].ToString();
        //        RequeteAss = "SELECT  SUM(A119) AS A119 FROM   dbo.T_ANXBEN01 WHERE (A107 = 2)".ToString();
        //        string RequetRet = mySqDataReader8["RequetRet"].ToString();
        //        string Taux = mySqDataReader8["Taux"].ToString();

        //        decimal DECEMP_Ret = 0; //Retenue
        //        decimal DECEMP_Ass = 0; //Assiette

        //        if (RequeteAss != "")
        //        {
        //            DECEMP_Ass = Executer_SQL(RequeteAss);
        //        }

        //        if (RequetRet != "")
        //        {
        //            DECEMP_Ret = Executer_SQL(RequetRet);

        //            DECEMP37 += DECEMP_Ret;
        //        }

        //        table.Rows.Add(Code, Libele, DECEMP_Ass, Taux.ToString() + "%", DECEMP_Ret);


        //    }

        //    myConnection8.Close();


        //    GridView1.DataSource = table;

        //    //label1.Text = DECEMP37.ToString();

        //}

        //decimal Executer_SQL(string SQL)
        //{
        //    decimal Total = 0;

        //    if (SQL != "&nbsp;")
        //    {

        //        string ConnectionString = Get_ConnexionString(); // chaine de connexion

        //        SQL = SQL.Replace("*", ",");
        //        SQL += "" + Exercice;

        //        //  MessageBox.Show(SQL);

        //        SqlConnection myConnection9;
        //        SqlCommand myCommand9;

        //        myConnection9 = new SqlConnection(ConnectionString);

        //        myConnection9.Open();

        //        myCommand9 = new SqlCommand(SQL, myConnection9);
        //        SqlDataReader mySqDataReader9 = myCommand9.ExecuteReader();

        //        while (mySqDataReader9.Read())
        //        {
        //            Total = Convert.ToDecimal(mySqDataReader9["Total"].ToString());
        //        }

        //    }

        //    return Total;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = Get_ConnexionString(); // chaine de connexion

            //--------------Supprimer les enregistrement de l'exercice en cours---------------

            SqlConnection con1 = new SqlConnection(ConnectionString);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM [dbo].[T_DECEMP0N] WHERE Exercice = " + Exercice, con1);

            SqlDataReader reader1 = cmd1.ExecuteReader();

            con1.Close();

            //----------------Fin Suppression-----------------------------

            foreach (GridView1Row item in GridView1.Rows)
            {

                int n = item.Index;

                //------------Ajout les nouvelles données----------------------

                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();

                string SQL = "";
                SQL = "INSERT INTO T_DECEMP0N";
                SQL += "(";
                SQL += "Code,Libele,Tot_Ass , Tau_Ret , Tot_Ret , Exercice , idUser";
                SQL += ")";
                SQL += "VALUES ";
                SQL += "(";
                SQL += "'" + GridView1.Rows[n].Cells[0].Text.ToString() + "',";
                SQL += "'" + GridView1.Rows[n].Cells[1].Text.Replace("'", "‘") + "',";

                if (GridView1.Rows[n].Cells[2].Text.ToString() == "")
                {
                    SQL += "'0',";
                }
                else
                {
                    SQL += "'" + GridView1.Rows[n].Cells[2].Text.ToString().Replace(",", ".") + "',";
                }

                if (GridView1.Rows[n].Cells[3].Text.ToString().Replace("%", "").Trim() == "")
                {
                    SQL += "'0',";
                }
                else
                {
                    SQL += "'" + GridView1.Rows[n].Cells[3].Text.ToString().Replace(",", ".").Replace("%", "") + "',";
                }

                if (GridView1.Rows[n].Cells[4].Text.ToString() == "")
                {
                    SQL += "'0',";
                }
                else
                {
                    SQL += "'" + GridView1.Rows[n].Cells[4].Text.ToString().Replace(",", ".") + "',";
                }

                SQL += "'" + Exercice + "',";
                SQL += "'" + User + "'";
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
            SQL1 += "Tot_Ass , Tau_Ret , Tot_Ret , Exercice ,";
            SQL1 += ")";
            SQL1 += "VALUES ";
            SQL1 += "(";
            SQL1 += "'0',";
            SQL1 += "'0',";
            //SQL1 += "'" + label1.Text.Replace(",", ".") + "',";
            SQL1 += "'" + Exercice + "',";
            SQL1 += "'" + User + "'";
            SQL1 += ")";


            SqlCommand cmd2 = new SqlCommand(SQL1, con2);

            SqlDataReader reader2 = cmd2.ExecuteReader();

            con2.Close();

            Refresh_list();



        }

        private void Refresh_list()
        {
            throw new NotImplementedException();
        }

        public string Get_ConnexionString()
        {

            string ch = "";
            ch = ConfigurationManager.ConnectionStrings["GDLConnectionString"].ConnectionString.ToString();
            return ch;
        }

        private class DataGridViewRow
        {
        }

        private class GridView1Row
        {
            internal int Index;
        }
    }
 }
