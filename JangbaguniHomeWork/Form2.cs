using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JangbaguniHomeWork
{
    public partial class Form2 : Form
    {
        int bookid;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(int bookid, string bookname)
        {
            InitializeComponent();
            this.bookid = bookid;
            textBox1.Text = bookid.ToString();
            textBox2.Text = bookname;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.jpg;*.gid;*.jpeg;*.png;*.bmp)|*.jpg;*.gid;*.jpeg;*.png;*.bmp)";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dlg.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Tag = dlg.FileName;
            }

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string sPath = $"bookImage/{bookid}/";
                string localfile = pictureBox1.Tag.ToString();
                string sExt = localfile.Substring(localfile.LastIndexOf("."));
                string newfilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + sExt;
                string destfilename = Path.Combine(sPath, newfilename);

                DirectoryInfo dir = new DirectoryInfo(sPath);

                if (!dir.Exists)
                {

                    dir.Create();
                }

                File.Copy(localfile, destfilename, true);

                BookDB db = new BookDB();

                bool result = db.UpdateBookImage(bookid, textBox2.Text, destfilename);

                if (result)
                {
                    MessageBox.Show("이미지 추가가 완료되었습니다.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("이미지 저장에 실패하였습니다. 다시 시도하여 주십시오.");
                }

            }
            catch (Exception)
            { }
        }
    }
}
