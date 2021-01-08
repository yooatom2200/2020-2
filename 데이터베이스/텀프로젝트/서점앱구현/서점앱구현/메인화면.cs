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
    public partial class 메인화면 : Form
    {
        public static DataGridViewRow[] dr;
        public static int dr_num;

        OleDbConnection conn;
        string connectionString;

        
        public void refresh()
        {
            pictureBox1_Click(null, null);
            label2.Text = 로그인.login_name + "님 환영합니다!";
        }
        public 메인화면()
        {
            InitializeComponent();
        }
        private void 메인화면_Load(object sender, EventArgs e)
        {
            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

            dataGridView1.Font = new Font("나눔고딕", 10, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 12, FontStyle.Bold);

            comboBox1.SelectedItem = 0;
            comboBox1.Text = comboBox1.Items[0].ToString();

            label2.Text = 로그인.login_name + "님 환영합니다!";
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            updatedb();

            if (dataGridView1.Rows.Count == 0)
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                return;
            }
            if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Length > 10)
                label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10) +
                    "\n" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(10, dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Length - 10);
            else
                label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            try
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
            }
        }
        private void updatedb()
        {
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 도서";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView1.ColumnCount = 4;
                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i).ToString(); // 오브젝트배열에 데이터 저장
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

        

        private void 메인화면_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            마이페이지 mp = new 마이페이지(this);
            mp.Owner = this;
            mp.Show();
        }

        private void button3_Click(object sender, EventArgs e)//검색
        {
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                if (comboBox1.SelectedIndex == 0)
                    cmd.CommandText = "select * from 도서 where upper(도서명) like '%" + textBox1.Text.ToUpper() + "%'";
                else if (comboBox1.SelectedIndex == 1)
                    cmd.CommandText = "select * from 도서 where upper(도서번호) like '%" + textBox1.Text.ToUpper() + "%'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView1.ColumnCount = 4;
                //필드명 받아오는 반복문
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i).ToString(); // 오브젝트배열에 데이터 저장
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

        private void pictureBox1_Click(object sender, EventArgs e)//로고 클릭시 검색 초기화
        {
            textBox1.Text = string.Empty;
            comboBox1.SelectedItem = 0;
            comboBox1.Text = comboBox1.Items[0].ToString();
            dataGridView1.Rows.Clear();
            updatedb();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)//장바구니
        {
            장바구니관리 kart_set = new 장바구니관리(this);
            kart_set.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dr_num = dataGridView1.SelectedRows.Count;
            dr = new DataGridViewRow[dr_num];
            for (int i = 0; i < dr_num; i++)
            {
                if(Int32.Parse(dataGridView1.SelectedRows[i].Cells[2].Value.ToString()) == 0)
                {
                    MessageBox.Show("재고가 없는 책은 장바구니에 넣으실 수 없으십니다.\n다시 선택해 주세요!", "재고확인");
                    return;
                }
                dr[i] = dataGridView1.SelectedRows[i];
            }
            장바구니담기 kart_set = new 장바구니담기();
            kart_set.Owner = this;
            kart_set.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dr_num = dataGridView1.SelectedRows.Count;
            dr = new DataGridViewRow[dr_num];
            for (int i = 0; i < dr_num; i++)
            {
                if (Int32.Parse(dataGridView1.SelectedRows[i].Cells[2].Value.ToString()) == 0)
                {
                    MessageBox.Show("재고가 없는 책은 바로 구매하실 수 없으십니다.\n다시 선택해 주세요!", "재고확인");
                    break;
                }
                dr[i] = dataGridView1.SelectedRows[i];
            }
            바로주문 now_order = new 바로주문(this);
            now_order.Owner = this;
            now_order.Show();
        }
        public void Refresh()
        {
            updatedb();
        }
        private void button2_Click(object sender, EventArgs e)//주문내역
        {
            주문내역 order_history = new 주문내역(this);
            order_history.Owner = this;
            order_history.Show();
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                return;
            }
            if (dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Length > 10)
                label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10) +
                    "\n" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(10, dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Length - 10);
            else
                label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            try
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
