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
    public partial class 바로주문 : Form
    {
        메인화면 ma;

        OleDbConnection conn;
        string connectionString;
        bool state = true;

        int[] cost;
        int total_cost;
        int total_books;

        public 바로주문()
        {
            InitializeComponent();
        }

        public 바로주문(메인화면 mai)
        {
            InitializeComponent();
            ma = mai;
        }
        private void updatedb_book()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "도서코드";
            dataGridView1.Columns[1].Name = "도서명";
            dataGridView1.Columns[2].Name = "도서재고";
            dataGridView1.Columns[3].Name = "도서가격";
            dataGridView1.Columns[4].Name = "수량";
            cost = new int[메인화면.dr_num];
            for (int i = 0; i < 메인화면.dr_num; i++)
            {
                object[] tmp = new object[dataGridView1.ColumnCount];
                for (int j = 0; j < dataGridView1.ColumnCount-1; j++)
                {
                    tmp[j] = new object();
                    tmp[j] = 메인화면.dr[i].Cells[j].Value.ToString();
                }
                tmp[dataGridView1.ColumnCount - 1] = "0";
                dataGridView1.Rows.Add(tmp);
                cost[i] = 0;
            }
        }

        private void updatedb_card()
        {
            dataGridView2.Rows.Clear();
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원_신용카드 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView2.ColumnCount = 3;
                //필드명 받아오는 반복문
                for (int i = 1; i < 4; i++)
                {
                    dataGridView2.Columns[i - 1].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[3]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 3; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = new object();
                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i + 1).ToString().Substring(0, 7); // 오브젝트배열에 데이터 저장
                            continue;
                        }
                        obj[i] = read.GetValue(i + 1).ToString(); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                if (dataGridView2.Rows.Count == 0)
                {
                    state = false;
                    MessageBox.Show("등록된 카드가 없습니다.\n마이페이지에서 카드 등록 후 이용해 주세요!", "카드 누락");
                    this.Close();
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void updatedb_address()
        {
            dataGridView3.Rows.Clear();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 회원_주소록 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView3.ColumnCount = 4;
                //필드명 받아오는 반복문
                for (int i = 1; i < 5; i++)
                {
                    dataGridView3.Columns[i - 1].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[4]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 4; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i + 1).ToString(); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView3.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                if (dataGridView3.Rows.Count == 0)
                {
                    state = false;
                    MessageBox.Show("등록된 주소가 없습니다.\n마이페이지에서 주소 등록 후 이용해 주세요!", "주소 누락");
                    this.Close();
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void setCost()
        {
            total_cost = 0;
            total_books = 0;
            for (int i = 0; i < 메인화면.dr_num; i++)
            {
                cost[i] = Int32.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) *
                    Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                total_cost += cost[i];
                total_books += Int32.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            label4.Text = total_cost.ToString() + " 원";
            label6.Text = total_books.ToString() + " 권";
        }

        private void 바로주문_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();

            label10.Text = 로그인.login_name + " 고객님";
            updatedb_book();
            updatedb_card();
            updatedb_address();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            for (int i = 1; i <= Int32.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString()); i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
            setCost();
            if (state == true)
            {
                dataGridView3_CellClick(null, null);
                dataGridView2_CellClick(null, null);
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectedRows[0].Cells[4].Value = comboBox1.SelectedIndex + 1;
            setCost();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            for (int i = 1; i <= Int32.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString()); i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label20.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            label22.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label21.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            label23.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            label24.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                MessageBox.Show("주문내역확인 동의에 체크해 주세요!", "동의 확인");
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "0")
                {
                    MessageBox.Show("수량 선택이 안된 책이 존재합니다.\n확인후 다시 진행해주세요!", "수량 오류");
                    return;
                }
            }

            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "INSERT INTO 주문 VALUES(TO_CHAR(SYSDATE, 'YYYYMMDD') || TO_CHAR(SETNO.NEXTVAL, 'FM000'), SYSDATE, " +
                    total_cost.ToString() + ", '신청','" + label22.Text + "', TO_DATE('" + dataGridView2.SelectedRows[0].Cells[1].Value.ToString() +
                    "', 'YYYY-MM'), '" + label20.Text + "', '" + dataGridView3.SelectedRows[0].Cells[1].Value.ToString() +
                    "', '" + label23.Text + "', '" + label24.Text + "', '" + 로그인.login_id + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    cmd.CommandText = "INSERT INTO 주문_선택 VALUES(TO_CHAR(SYSDATE,'YYYYMMDD')" +
                        "||TO_CHAR((SELECT LAST_NUMBER FROM USER_SEQUENCES WHERE SEQUENCE_NAME = 'SETNO')" +
                        " - 1,'FM000'), '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "', " +
                        dataGridView1.Rows[i].Cells[4].Value.ToString() + ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    cmd.CommandText = "UPDATE 도서 SET 재고량=" + (Int32.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()) -
                    Int32.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString()
                        + "WHERE 도서번호='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "DELETE FROM 장바구니_담기 WHERE 바구니번호 = " + 장바구니관리.kart_num.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                MessageBox.Show("주문이 성공적으로 접수되었습니다!", "주문성공");
                주문성공 success = new 주문성공();
                success.Show();
                ma.refresh();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
