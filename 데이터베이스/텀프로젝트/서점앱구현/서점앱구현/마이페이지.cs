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
    public partial class 마이페이지 : Form
    {
        메인화면 ma;
        public 마이페이지()
        {
            InitializeComponent();
        }

        public void refresh()
        {
            label1.Text = 로그인.login_name + " 님";
        }

        public 마이페이지(메인화면 mai)
        {
            InitializeComponent();
            ma = mai;
        }

        private void 마이페이지_Load(object sender, EventArgs e)
        {
            label1.Text = 로그인.login_name + " 님" ;
        }

        private void button5_Click(object sender, EventArgs e)//회원정보 수정
        {
            회원정보수정 change_user_info = new 회원정보수정(ma,this);
            change_user_info.Owner = this;
            change_user_info.Show();
        }

        private void button1_Click(object sender, EventArgs e)//결제카드
        {
            신용카드 card_info = new 신용카드();
            card_info.Owner = this;
            card_info.Show();
        }

        private void button2_Click(object sender, EventArgs e)//배송주소
        {
            배송주소 deliv_address = new 배송주소();
            deliv_address.Owner = this;
            deliv_address.Show();
        }
    }
}
