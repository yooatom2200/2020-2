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
    public partial class 회원가입 : Form
    {
        bool is_checked_id = false;
        OleDbConnection conn;
        string connectionString;
        public 회원가입()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("아이디를 입력해주세요!", "입력오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원 where 회원번호='" + textBox1.Text + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                read.Read();
                MessageBox.Show("ID : " + read.GetValue(0).ToString() + "는(은) 사용중인 아이디 입니다.\n" +
                    "다른 아이디를 선택해 주세요!");
                is_checked_id = false;
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("사용 가능한 아이디 입니다."); //에러 메세지 
                is_checked_id = true;
            }
        }

        private void 회원가입_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";

            conn = new OleDbConnection(connectionString);
            conn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!is_checked_id)
            {
                MessageBox.Show("아이디 중복 체크를 해주세요!", "중복체크");
                return;
            }
            if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("비밀번호가 서로 다릅니다. 다시 확인해 주세요!", "비밀번호 오류");
                return;
            }
            if(string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox1.Text)||
                string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("모든 값을 입력해 주세요!", "입력 오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "INSERT INTO 회원 VALUES ('" + textBox1.Text + "','" +
                    textBox2.Text + "','" + textBox4.Text + "')";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("회원 ID : " + textBox1.Text +
                    "\n회원 비밀번호 : " + textBox2.Text +
                    "\n회원 이름 : " + textBox4.Text +
                    "\n회원가입을 축하합니다!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }
        }
    }
}
