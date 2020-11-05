using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JangbaguniHomeWork
{
    public partial class UserControl1 : UserControl
    {
        public delegate void AddCartEventHandler(object sender, AddCartEventArgs e);
        public event AddCartEventHandler AddCart;

        int bookid;
        public int Bookid { get { return bookid; } set{ bookid = value; } }
        public string Bookname { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string Bookimage { get { return pictureBox1.ImageLocation; } set { pictureBox1.ImageLocation = value; } }
       // public int Bookprice { get { return int.Parse(textBox4.Text.Replace("원", "")); } set { textBox4.Text = $"{value}원"; } }
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AddCart != null)        //등록된 이벤트 핸들러가 있으면 
            {
                AddCartEventArgs args = new AddCartEventArgs();
                args.Bookid = bookid;
                args.Bookname = Bookname;
              //  args.Bookprice = Bookprice;
                args.BookImage = Bookimage;

                AddCart(this, args);        //이벤트를 발생시킨다
            }
        }
    }

    public class AddCartEventArgs : EventArgs
    {
        int bookid, bookprice;
        string bookname, bookimage;
        public int Bookid { get { return bookid; } set { bookid = value; } }

        public string Bookname { get { return bookname; } set { bookname = value; } }

        //public int Bookprice { get { return bookprice; } set { bookprice = value; } }

        public string  BookImage { get { return bookimage; } set { bookimage = value; } }
    }
}
