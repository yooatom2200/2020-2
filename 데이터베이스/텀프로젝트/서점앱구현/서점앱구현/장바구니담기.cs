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
    public partial class 장바구니담기 : Form
    {
        OleDbConnection conn;
        string connectionString;
        public 장바구니담기()
        {
            InitializeComponent();
        }
        private void updatedb_up()
        {
            dataGridView3.Rows.Clear();
            dataGridView3.ColumnCount = 4;
            dataGridView3.Columns[0].Name = "도서코드";
            dataGridView3.Columns[1].Name = "도서명";
            dataGridView3.Columns[2].Name = "도서재고";
            dataGridView3.Columns[3].Name = "수량";
            for (int i = 0; i < 메인화면.dr_num; i++)
            {
                object[] tmp = new object[dataGridView3.ColumnCount];
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    tmp[j] = new object();
                    tmp[j] = 메인화면.dr[i].Cells[j].Value.ToString();
                }
                tmp[dataGridView3.ColumnCount - 1] = "선택";
                dataGridView3.Rows.Add(tmp);
            }
        }

        private void updatedb_left()
        {
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 장바구니 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView1.ColumnCount = 1;
                dataGridView1.Columns[0].Name = read.GetName(0);

                while (read.Read())
                {
                    object obj = new object(); // 필드수만큼 오브젝트 배열
                    obj = new object();
                    obj = read.GetValue(0).ToString(); // 오브젝트배열에 데이터 저장

                    dataGridView1.Rows.Add(obj); //데이터그리드뷰에 오브젝트 배열 추가
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //에러 메세지 
            }
        }

        private void updatedb_right()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            dataGridView2.Rows.Clear();
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT 장바구니_담기.도서번호, 도서.도서명, 장바구니_담기.수량 " +
                    "FROM 장바구니_담기 INNER JOIN 도서 ON 장바구니_담기.도서번호 = 도서.도서번호 " +
                    "WHERE 장바구니_담기.바구니번호=" + dr.Cells[0].Value.ToString();
                cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                dataGridView2.ColumnCount = 3;
                //필드명 받아오는 반복문
                for (int i = 0; i < 3; i++)
                {
                    dataGridView2.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[3]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < 3; i++) // 필드 수만큼 반복
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

        private void 장바구니_Load(object sender, EventArgs e)
        {
            label5.Text = 로그인.login_name + " 고객님";

            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();

            updatedb_left();
            updatedb_right();
            updatedb_up();
            comboBox1.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            for(int i = 1; i <= Int32.Parse(dataGridView3.SelectedRows[0].Cells[2].Value.ToString()); i++)
            {
                comboBox1.Items.Add(i.ToString());
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

        private void button1_Click(object sender, EventArgs e)//장바구니생성
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "INSERT INTO 장바구니 VALUES (KART_NUM.NEXTVAL, TO_CHAR(SYSDATE, 'YYYYMMDD'), '" + 로그인.login_id + "')";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb_left();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)//장바구니삭제
        {
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("장바구니가 존재하지 않습니다.", "삭제 오류");
                return;
            }
            if (MessageBox.Show("정말 장바구니를 삭제하시겠습니까?\n삭제하시면 해당 장바구니의 내용은 모두 없어집니다.", "삭제", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "DELETE FROM 장바구니 WHERE 바구니번호=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "commit";//커밋으로 적용
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    updatedb_left();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//품목담기
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("장바구니가 존재하지 않습니다.", "담기 오류");
                return;
            }

            try
            {
                for(int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataGridView3.Rows[i].Cells[3].Value.ToString()) || dataGridView3.Rows[i].Cells[3].Value.ToString() == "선택" ||
                        Int32.Parse(dataGridView3.Rows[i].Cells[3].Value.ToString()) == 0)
                    {
                        MessageBox.Show("수량 선택이 안된 책이 존재합니다.\n확인후 다시 진행해주세요!", "수량 오류");
                        return;
                    }
                }
                OleDbCommand cmd = new OleDbCommand();
                for(int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    cmd.CommandText = "INSERT INTO 장바구니_담기 VALUES (" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() +
                    ",'" + dataGridView3.Rows[i].Cells[0].Value.ToString() + "'," + dataGridView3.Rows[i].Cells[3].Value.ToString() + ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb_right();
                MessageBox.Show("물품들이 정상적으로 장바구니에 담겼습니다.", "담기완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show("장바구니에 이미 담긴 책이 있습니다.\n해당 책을 제거하고 다시 진행해 주세요!","중복");
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updatedb_right();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            for (int i = 1; i <= Int32.Parse(dataGridView3.SelectedRows[0].Cells[2].Value.ToString()); i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView3.SelectedRows[0].Cells[3].Value = comboBox1.SelectedIndex + 1;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
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
