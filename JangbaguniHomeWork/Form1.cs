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
    public partial class Form1 : Form
    {
        
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BookDB db = new BookDB();
            dt = db.GetEveryData();
            dgvMember.DataSource = dt;
            


        }

        private void dgvMember_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtbooknum.Text = dt.Rows[e.RowIndex]["bookid"].ToString();
            txtbookname.Text = dt.Rows[e.RowIndex]["bookname"].ToString();

            pictureBox1.ImageLocation = dt.Rows[e.RowIndex]["bookimage"].ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //유효성체크
            if (lblbooknum.Text.Length < 1 || lblBookname.Text.Length < 1)
            {
                MessageBox.Show("이미지 추가를 할 제품을 선택하세요.");
                return;
            }

            int bookid = int.Parse(txtbooknum.Text);
            string bookname = txtbookname.Text;

            Form2 frm = new Form2(bookid, bookname);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
