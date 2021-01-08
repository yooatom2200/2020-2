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
    public partial class 비밀번호찾기 : Form
    {
        OleDbConnection conn;
        string connectionString;
        public 비밀번호찾기()
        {
            InitializeComponent();
        }
        private void 비밀번호찾기_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";

            conn = new OleDbConnection(connectionString);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string search_id = textBox1.Text;
            string search_name = textBox2.Text;
            if (string.IsNullOrEmpty(search_id) || string.IsNullOrEmpty(search_name))
            {
                MessageBox.Show("아이디와 이름을 입력해주세요!", "입력오류");
                return;
            }
            if (search_id == "admin")
            {
                MessageBox.Show("관리자 계정은 확인 불가합니다.");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원 where 회원번호='" + search_id + "' and 성명 = '" +
                    search_name + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                read.Read();
                MessageBox.Show("비밀번호는 " + read.GetValue(1).ToString() + "입니다.");
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("아이디와 이름을 확인해 주세요!"); //에러 메세지 
            }
        }
    }
}
