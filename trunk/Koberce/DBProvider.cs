using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace Koberce
{
    public class DBProvider
    {
        private SQLiteDataAdapter DB;
        private string DbName = @".\arena.db";
        
        public static string[] TableNames = new string[] { "arena", "sold", "SK", "fromSK" , "inventory"};

        public DBProvider(string dbName)
        {
            DbName = dbName;
        }

        public SQLiteConnection GetConnection()
        {
            try
            {
                var sql_con = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\" + DbName + "; Version=3;");

                return sql_con;
            }
            catch (Exception)
            {
                MessageBox.Show(null, "Error while creating DB connection!", "GetConnection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public void SoldItem(string code, string sellDate, string sellPrice)
        {
            var command = string.Format("update {0} set quantity = 0 where code in (\"{1}\")", DBProvider.TableNames[0], code);
            ExecuteNonQuery(command);
            command = string.Format("insert or replace into {0} (code,selldate,sellprice) values (\"{1}\",\"{2}\",\"{3}\")", DBProvider.TableNames[1], code, sellDate, sellPrice);
            ExecuteNonQuery(command);
        }

        public void SKItem(string code)
        {
            var command = string.Format("update {0} set quantity = 1 where code in (\"{1}\")", DBProvider.TableNames[0], code);
            ExecuteNonQuery(command);
            command = string.Format("insert or replace into {0} (code) values (\"{1}\")", DBProvider.TableNames[2], code);
            ExecuteNonQuery(command);
        }

        public void FromSKItem(string code)
        {
            var command = string.Format("update {0} set quantity = 0 where code in (\"{1}\")", DBProvider.TableNames[0], code);
            ExecuteNonQuery(command);
            command = string.Format("insert or replace into {0} (code) values (\"{1}\")", DBProvider.TableNames[3], code);
            ExecuteNonQuery(command);
        }

        public void InventoryItem(string code)
        {
            var command = string.Format("insert or replace into {0} (code) values (\"{1}\")", DBProvider.TableNames[4], code);
            ExecuteNonQuery(command);
        }

        public void ExecuteNonQuery(string txtQuery)
        {
            var sql_con = GetConnection();
            sql_con.Open();
            var sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public DataSet ExecuteQuery(string query)
        {
            DataSet DS = new DataSet();

            var sql_con = GetConnection();
            sql_con.Open();
            var sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(query, sql_con);
            DS.Reset();
            DB.Fill(DS);
            sql_con.Close();

            return DS;
        }

        public DataSet ExecuteQuery(string tableName, string where, string order)
        {
            DataSet DS = new DataSet();

            var sql_con = GetConnection();
            sql_con.Open();
            var sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from " + tableName + " A " + where + " " + order;
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            sql_con.Close();

            return DS;
        }

        public int LoadMaxCode()
        {
            var maxCode = ExecuteQuery("last", "", "");
            var max = maxCode.Tables[0].Rows[0].ItemArray.ElementAt(0).ToString();

            int iMax = -1;
            try
            {
                int.TryParse(max, out iMax);
            }
            catch (Exception)
            {
            }
         
            return iMax;
        }

        public void Add(string supplier_nb, string code, string name, string land, string supplier, int length, int width, string ekNetto, string quantity, string vkNetto, string date, string paid, string mvDate, string invoice, string col, string material, string comment)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" INSERT INTO ");
            sb.AppendFormat(TableNames[0]);
            sb.AppendFormat(" (SUPPLIER_NR,CODE,ITEMTITLE,COUNTRY,SUPPLIER,LENGTH,WIDTH,EK_NETTO,QUANTITY,VK_NETTO,DATE,PAID,MVDATE,INVOICE,COLOR,MATERIAL,COMMENT) ");
            sb.AppendFormat(" VALUES( ");
            sb.AppendFormat("'{0}',", supplier_nb);
            sb.AppendFormat("'{0}',", code);
            sb.AppendFormat("'{0}',", name);
            sb.AppendFormat("'{0}',", land);
            sb.AppendFormat("'{0}',", supplier);
            sb.AppendFormat("{0},", length);
            sb.AppendFormat("{0},", width);
            sb.AppendFormat("'{0}',", ekNetto);
            sb.AppendFormat("'{0}',", quantity);
            sb.AppendFormat("'{0}',", vkNetto);
            sb.AppendFormat("'{0}',", date);
            sb.AppendFormat("'{0}',", paid);
            sb.AppendFormat("'{0}',", mvDate);
            sb.AppendFormat("'{0}',", invoice);
            sb.AppendFormat("'{0}',", col);
            sb.AppendFormat("'{0}',", material);
            sb.AppendFormat("'{0}' ", comment);
            sb.AppendFormat(" );");
            string txtSQLQuery = sb.ToString();
            try
            {
                this.ExecuteNonQuery(txtSQLQuery);

                ExecuteNonQuery("update last set lastCode = " + code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Update(string supplier_nb, string code, string name, string land, string supplier, int length, int width, string ekNetto, string quantity, string vkNetto, string date, string paid, string mvDate, string invoice, string col, string material, string comment)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" UPDATE ");
            sb.AppendFormat(TableNames[0]);
            sb.AppendFormat(" SET SUPPLIER_NR=\"{0}\",ITEMTITLE=\"{1}\",COUNTRY=\"{2}\",SUPPLIER=\"{3}\",LENGTH=\"{4}\",WIDTH=\"{5}\",EK_NETTO=\"{6}\",QUANTITY=\"{7}\",VK_NETTO=\"{8}\",DATE=\"{9}\",PAID=\"{10}\",MVDATE=\"{11}\",INVOICE=\"{12}\",COLOR=\"{13}\",MATERIAL=\"{14}\",COMMENT=\"{15}\" ",
                supplier_nb,
                name,
                land,
                supplier,
                length,
                width,
                ekNetto,
                quantity,
                vkNetto,
                date,
                paid,
                mvDate,
                invoice,
                col,
                material,
                comment
                );
            sb.AppendFormat(" WHERE CODE = \"{0}\"", code);

            string txtSQLQuery = sb.ToString();
            try
            {
                this.ExecuteNonQuery(txtSQLQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void UpdateSold(string code, string sellDate, string sellPrice)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" UPDATE ");
            sb.AppendFormat(TableNames[1]);
            sb.AppendFormat(" SET SELLDATE=\"{0}\",SELLPRICE=\"{1}\"",
                sellDate,
                sellPrice
                );
            sb.AppendFormat(" WHERE CODE = \"{0}\"", code);

            string txtSQLQuery = sb.ToString();
            try
            {
                this.ExecuteNonQuery(txtSQLQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Delete(string tableName, string[] codes)
        {
            string inCodes = string.Join(",", codes);
            string txtSQLQuery = "delete from " + tableName + " where code in ( " + inCodes + " ) ";
            try
            {
                this.ExecuteNonQuery(txtSQLQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public DataItem GetItem(string code)
        {
            var row = ExecuteQuery(TableNames[0], " where code = " + code, "").Tables[0].Rows[0];
            DataItem ret = new DataItem();

            ret.SupplierNr = row["supplier_nr"].ToString();
            ret.GlobalNumber = row["code"].ToString();
            ret.Name = row["itemtitle"].ToString();
            ret.Country = row["country"].ToString();
            ret.Supplier = row["supplier"].ToString();
            ret.Length = row["length"].ToString();
            ret.Width = row["width"].ToString();
            ret.EkNetto = row["ek_netto"].ToString();
            ret.Quantity = row["quantity"].ToString();
            ret.VkNetto = row["vk_netto"].ToString();
            ret.Date = row["date"].ToString();
            ret.Paid = row["paid"].ToString();
            ret.MvDate = row["mvdate"].ToString();
            ret.Invoice = row["invoice"].ToString();
            ret.Color = row["color"].ToString();
            ret.Material = row["material"].ToString();
            ret.Comment = row["comment"].ToString();

            return ret;
        }

        public bool ExistsItem(string code, string tableName)
        {
            var ds = ExecuteQuery(tableName, " where code = " + code, "");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                return true;

            return false;
        }

        internal void InsertSoldItem(string code, string sellDate, string sellPrice)
        {
            var command = string.Format("insert or replace into {0} (code,selldate,sellprice) values (\"{1}\",\"{2}\",\"{3}\")", DBProvider.TableNames[1], code, sellDate, sellPrice);
            ExecuteNonQuery(command);
        }
    }
}
