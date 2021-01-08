using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace 서점앱구현
{
    public partial class 로그인 : Form
    {
        public static string login_id;
        public static string login_name;
        OleDbConnection conn;
        string connectionString;
        public 로그인()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";

            conn = new OleDbConnection(connectionString);
            conn.Open();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string search_id = textBox1.Text;
            string search_pw = textBox2.Text;
            if(string.IsNullOrEmpty(search_id) || string.IsNullOrEmpty(search_pw))
            {
                MessageBox.Show("아이디와 비밀번호를 입력해주세요!", "입력오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원 where 회원번호='" + search_id + "' and 비밀번호 = '" +
                    search_pw + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                read.Read();
                login_id = read.GetValue(0).ToString();
                login_name = read.GetValue(2).ToString();
                textBox2.Text = "";
                read.Close();
                if(login_id == "admin")//관리자 접속
                {
                    관리자메뉴 admin_menu = new 관리자메뉴();
                    admin_menu.Owner = this;
                    admin_menu.Show();
                    this.Hide();
                    return;
                }
                메인화면 main = new 메인화면();
                main.Owner = this;
                main.Show();
                this.Hide();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("아이디와 비밀번호를 확인해 주세요!","오류",MessageBoxButtons.OK,MessageBoxIcon.Error); //에러 메세지 
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            아이디찾기 find_id = new 아이디찾기();
            find_id.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            비밀번호찾기 find_pw = new 비밀번호찾기();
            find_pw.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            회원가입 Sign = new 회원가입();
            Sign.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                pictureBox3_Click(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox3_Click(sender, e);
            }
        }

        private void 로그인_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox3_Click(sender, e);
            }
        }
    }
}
