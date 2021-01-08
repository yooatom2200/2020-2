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
    public partial class 회원정보수정 : Form
    {
        메인화면 ma;
        마이페이지 pa;
        OleDbConnection conn;
        string connectionString;
        public 회원정보수정()
        {
            InitializeComponent();
        }

        public 회원정보수정(메인화면 mai, 마이페이지 pag)
        {
            InitializeComponent();
            ma = mai;
            pa = pag;
        }

        private void 회원정보수정_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "select * from 회원 where 회원번호='" + 로그인.login_id + "'";
            cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
            cmd.Connection = conn;

            OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
            read.Read();
            textBox1.Text = read.GetValue(0).ToString();
            textBox2.Text = read.GetValue(1).ToString();
            textBox3.Text = read.GetValue(2).ToString();
            read.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)
                || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("모든칸이 입력되지 않았습니다.\n확인해 주세요!", "입력 오류");
                return;
            }
            if(textBox2.Text != textBox4.Text)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.\n확인해 주세요!", "비밀번호 오류");
                return;
            }
            try
            {
                로그인.login_name = textBox3.Text;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "update 회원 set 비밀번호='" + textBox2.Text + "', 성명='" + textBox3.Text + "'" +
                    " where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("회원정보 수정이 완료되었습니다.");
                ma.refresh();
                pa.refresh();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말로 계정을 삭제하실겁니까?\n삭제된 이후 정보는 모두 사라지며 프로그램은 종료됩니다.","YesOrNo",MessageBoxButtons.YesNo)
                == DialogResult.No)
            {
                MessageBox.Show("취소하셨습니다.");
                return;
            }
            else
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "delete from 회원 where 회원번호='" + 로그인.login_id + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("회원탈퇴가 완료되었습니다.\n다음에 더 좋은 모습으로 뵙겠습니다.");
                    Application.ExitThread();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
