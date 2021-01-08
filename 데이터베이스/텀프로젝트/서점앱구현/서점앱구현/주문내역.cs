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
    public partial class 주문내역 : Form
    {
        OleDbConnection conn;
        string connectionString;
        bool is_history = true;
        private void updatedb_up()
        {
            dataGridView1.Rows.Clear();
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 주문 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView1.ColumnCount = 10;
                //필드명 받아오는 반복문
                for (int i = 0; i < 10; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[10]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 10; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 10); // 오브젝트배열에 데이터 저장
                            continue;
                        }
                        if (i == 5)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 7); // 오브젝트배열에 데이터 저장
                            continue;
                        }
                        obj[i] = read.GetValue(i).ToString(); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView1.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();
                if(dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("주문내역이 없습니다.", "주문내역");
                    is_history = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void updatedb_down()
        {
            dataGridView2.Rows.Clear();
            if (is_history == false)
                return;
            try
            {
                //conn.Open(); //데이터베이스 연결
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT 주문_선택.도서번호, 도서.도서명, 주문_선택.수량, 도서.판매가, 주문_선택.수량*도서.판매가 AS 총_가격 " +
                    "FROM 주문_선택, 도서 WHERE 주문_선택.도서번호 = 도서.도서번호 AND 주문_선택.주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView2.ColumnCount = 5;
                //필드명 받아오는 반복문
                for (int i = 0; i < 5; i++)
                {
                    dataGridView2.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[5]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 5; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i).ToString(); // 오브젝트배열에 데이터 저장
                    }

                    dataGridView2.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void check_on_off()
        {
            if (is_history == false)
                return;
            if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "신청" ||
                dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "판매완료")
            {
                button1.Enabled = false;
                button1.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            }
            else if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "발송")
            {
                button1.Enabled = true;
                button1.FlatAppearance.BorderColor = Color.FromArgb(255,128,128);
            }

            if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "발송" ||
                dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "판매완료")
            {
                button2.Enabled = false;
                button2.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            }
            else if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "신청")
            {
                button2.Enabled = true;
                button2.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            }
        }

        메인화면 ma;
        public 주문내역()
        {
            InitializeComponent();
        }
        public 주문내역(메인화면 mai)
        {
            ma = mai;
            InitializeComponent();
        }

        private void 주문내역_Load(object sender, EventArgs e)
        {
            label2.Text = 로그인.login_name + " 고객님";
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            button1.Enabled = false;
            button2.Enabled = false;
            updatedb_up();
            updatedb_down();
            check_on_off();

            if (dataGridView1.Rows.Count == 0)
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                return;
            }
            if (dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Length > 10)
                label10.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10) +
                    "\n" + dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Substring(10, dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Length - 10);
            else
                label10.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
            label12.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView2.Rows[0].Cells[4].Value.ToString();
            try
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updatedb_down();
            check_on_off();
            if (dataGridView1.Rows.Count == 0)
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                return;
            }
            if (dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Length > 10)
                label10.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10) +
                    "\n" + dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Substring(10, dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Length - 10);
            else
                label10.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
            label12.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView2.Rows[0].Cells[4].Value.ToString();
            try
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ".jpg");
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
            }
        }

        private void button1_Click(object sender, EventArgs e)//수취확인
        {
            if(MessageBox.Show("주문하신 물품은 잘 받으셨습니까?\n수취확인시 구매확정으로 간주됩니다","수취확인",MessageBoxButtons.YesNo) == DialogResult.No)
            {
                MessageBox.Show("취소하셨습니다.", "취소");
                return;
            }
            OleDbCommand cmd = new OleDbCommand();

            cmd.CommandText = "UPDATE 주문 SET 주문상태 = '판매완료' WHERE 주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "commit";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            MessageBox.Show("수취확인으로 처리되었습니다.\n이용해주셔서 감사합니다!", "수취 완료");
            updatedb_up();
            updatedb_down();
            check_on_off();
        }

        private void button2_Click(object sender, EventArgs e)//취소
        {
            List<string> book_code = new List<string>();
            List<int> book_amount = new List<int>();
            if(dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("선택하신 주문이 없습니다.", "주문취소 오류");
                return;
            }
            if (MessageBox.Show("주문 취소를 하시겠습니까?\n주문취소시 결제 및 배송이 취소됩니다.", "수취확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                MessageBox.Show("승인거부 하셨습니다.", "취소");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();

                cmd.CommandText = "select 도서번호, 수량 from 주문_선택 where 주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과

                while (read.Read())
                {
                    object[] obj = new object[2]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 2; i++) // 필드 수만큼 반복
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i).ToString(); // 오브젝트배열에 데이터 저장
                    }
                    book_code.Add(obj[0].ToString());
                    book_amount.Add(Int32.Parse(obj[1].ToString()));
                }
                read.Close();

                int tmp = book_code.Count;
                for (int i = 0; i < tmp; i++)
                {
                    cmd.CommandText = "UPDATE 도서 SET 재고량 = 재고량 + " + book_amount[i] +
                        " WHERE 도서번호 = '" + book_code[i] + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "DELETE FROM 주문 WHERE 주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("주문취소가 처리되었습니다.\n환불 정책은 결제회사 규제에 따라 진행됩니다.\n이용해주셔서 감사합니다!", "주문 취소 완료");
                updatedb_up();
                updatedb_down();
                check_on_off();
                ma.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                return;
            }
            if (dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString().Length > 10)
                label10.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString().Substring(0, 10) +
                    "\n" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString().Substring(10, dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString().Length - 10);
            else
                label10.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            label11.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            label12.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            label13.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            try
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ".jpg");
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

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
