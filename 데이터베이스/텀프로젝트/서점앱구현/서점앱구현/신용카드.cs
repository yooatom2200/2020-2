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
    public partial class 신용카드 : Form
    {
        OleDbConnection conn;
        string connectionString;
        private void updatedb()
        {
            dataGridView1.Rows.Clear();
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원_신용카드 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView1.ColumnCount = 3;
                //필드명 받아오는 반복문
                for (int i = 1; i < 4; i++)
                {
                    dataGridView1.Columns[i-1].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[3]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i + 1).ToString().Substring(0, 7);
                            continue;
                        }
                        obj[i] = read.GetValue(i+1).ToString(); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }
        public 신용카드()
        {
            InitializeComponent();
        }

        private void 카드추가_Load(object sender, EventArgs e)
        {
            label5.Text = 로그인.login_name + " 고객님";
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            updatedb();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dataGridView1.SelectedRows.Count > 0)
                dataGridView1_CellClick(null, null);
        }

        private void button4_Click(object sender, EventArgs e)//초기화
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox1.Text = string.Empty;
            updatedb();
        }

        private void button3_Click(object sender, EventArgs e)//수정
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("값들을 모두입력하셔야 합니다. 다시 확인해 주세요");
                return;
            }
            if(textBox1.Text.Length != 4 || textBox2.Text.Length != 4 || textBox3.Text.Length != 4 || textBox4.Text.Length != 4)
            {
                MessageBox.Show("신용카드 번호를 온전히 입력해 주세요!", "카드번호 오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "update 회원_신용카드 set 유효기간 = TO_DATE('" + comboBox2.Text + "-" + comboBox3.Text + "','YYYY-MM') , 카드종류 = '" +
                    comboBox1.Text + "' where 카드번호 = '" + textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)//삭제
        {
            if (string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox2.Text)|| string.IsNullOrEmpty(textBox3.Text)|| string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("삭제할 카드 번호를 입력해 주세요.");
                return;
            }
            if (textBox1.Text.Length != 4 || textBox2.Text.Length != 4 || textBox3.Text.Length != 4 || textBox4.Text.Length != 4)
            {
                MessageBox.Show("신용카드 번호를 온전히 입력해 주세요!", "카드번호 오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "delete from 회원_신용카드 where 카드번호='" + textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)//추가
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox3.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) ||
                string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("값들을 모두입력하셔야 합니다. 다시 확인해 주세요");
                return;
            }
            if (textBox1.Text.Length != 4 || textBox2.Text.Length != 4 || textBox3.Text.Length != 4 || textBox4.Text.Length != 4)
            {
                MessageBox.Show("신용카드 번호를 온전히 입력해 주세요!", "카드번호 오류");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "INSERT INTO 회원_신용카드 VALUES ('" + 로그인.login_id +
                    "','" + textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text +
                    "',TO_DATE('" + comboBox2.Text + "=" + comboBox3.Text + "','YYYY-MM'),'" +
                    comboBox1.Text + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Substring(0, 4);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Substring(5, 4);
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Substring(10, 4);
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Substring(15, 4);
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 4);
            comboBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(5, 2);
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
