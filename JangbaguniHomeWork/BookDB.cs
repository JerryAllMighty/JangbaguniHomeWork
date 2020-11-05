using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace JangbaguniHomeWork
{
    class BookDB : IDisposable
    {
        MySqlConnection conn;
            public BookDB()
        {
            string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            conn = new MySqlConnection(strConn);
            conn.Open();
        }


        public DataTable GetEveryData()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = @"select bookid, bookname, author, publisher, lendingstate, reservstuid, deleted, bookimage
                                from book";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateBookImage(int bookid, string bookname,string destfilename)
        {
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = @"update book set bookid = @bookid, bookname = @bookname, bookimage = @destfilename where bookid = @bookid";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.Add("@bookid", MySqlDbType.Int32);
                cmd.Parameters["@bookid"].Value = bookid;

                cmd.Parameters.Add("@bookname", MySqlDbType.VarChar);
                cmd.Parameters["@bookname"].Value = bookname;

                cmd.Parameters.Add("@destfilename", MySqlDbType.VarChar);
                cmd.Parameters["@destfilename"].Value = destfilename;


                if (cmd.ExecuteNonQuery() > 0)
                {
                    trans.Commit();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    return false;
                }
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }

       

        public void Dispose()
        {
            conn.Close();
        }
    }
}
