using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 서점앱구현
{
    public partial class 관리자메뉴 : Form
    {
        public 관리자메뉴()
        {
            InitializeComponent();
        }

        private void 관리자메뉴_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            관리자페이지 admin_page = new 관리자페이지();
            admin_page.Owner = this;
            admin_page.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            도서관리 setting_books = new 도서관리();
            setting_books.Owner = this;
            setting_books.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            주문관리 setting_order = new 주문관리();
            setting_order.Owner = this;
            setting_order.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            판매이력조회 search_history = new 판매이력조회();
            search_history.Owner = this;
            search_history.Show();
            this.Hide();
        }
    }
}
