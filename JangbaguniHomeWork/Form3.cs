using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JangbaguniHomeWork
{
    public partial class Form3 : Form
    {
        DataTable dtCart;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            InitCart();
            BookDB db = new BookDB();
            DataTable dt =  db.GetEveryData();
            int idx = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserControl1 ucon = new UserControl1();
                ucon.Name = $"conn{idx}";
                ucon.Location = new Point(3, (i * 200)+ 10);
                ucon.Size = new Size(566, 33);

                ucon.Bookid = Convert.ToInt32(dt.Rows[i]["bookid"].ToString());
                ucon.Bookname = dt.Rows[i]["bookname"].ToString();
                //  ucon.Bookprice = ;        가격은 스킵
                ucon.Bookimage = dt.Rows[i]["bookimage"].ToString();

                ucon.AddCart += Ucon_AddCart;
                panel1.Controls.Add(ucon);
                idx++;
            }

            db.Dispose();
        }

        private void Ucon_AddCart(object sender, AddCartEventArgs e)
        {
            DataRow[] drrows = dtCart.Select("bookid=" + e.Bookid);
            if (drrows.Length > 0)
            { //이미 장바구니에 있는 경우 
                drrows[0]["bookqty"] = Convert.ToInt32(drrows[0]["bookqty"]) + 1;
                
            }
            else
            //장바구니에 없는 경우
            {
                DataRow dr = dtCart.NewRow();
                dr["bookid"] = e.Bookid;
                dr["bookname"] = e.Bookname;
                dr["bookqty"] = 1;
                //  dr["bookprice"] = e.Bookprice;
                dtCart.Rows.Add(dr);

            }
            dtCart.AcceptChanges();
            dataGridView1.DataSource = dtCart;
        }

        private void InitCart()
        {
            dtCart = new DataTable();
            dtCart.Columns.Add(new DataColumn("bookid", typeof(int)));
            dtCart.Columns.Add(new DataColumn("bookname", typeof(string)));
            dtCart.Columns.Add(new DataColumn("bookqty", typeof(int)));
            dtCart.Columns.Add(new DataColumn("bookprice", typeof(int)));
        }
    }
}
