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
    public partial class 장바구니관리 : Form
    {
        메인화면 ma;
        public static DataGridViewRow[] dr2;
        public static int dr_num2;
        public static int kart_num;

        OleDbConnection conn;
        string connectionString;
        public 장바구니관리()
        {
            InitializeComponent();
        }
        public 장바구니관리(메인화면 mai)
        {
            InitializeComponent();
            ma = mai;
        }
        public void refresh()
        {
            updatedb_right();
        }
        private void updatedb_left()
        {
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "select * from 장바구니 where 회원번호='" + 로그인.login_id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView1.ColumnCount = 1;
                dataGridView1.Columns[0].Name = read.GetName(0);

                while (read.Read())
                {
                    object obj = new object();
                    obj = new object();
                    obj = read.GetValue(0).ToString();

                    dataGridView1.Rows.Add(obj);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void updatedb_right()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            dataGridView2.Rows.Clear();
            
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT 장바구니_담기.도서번호, 도서.도서명, 장바구니_담기.수량 " +
                    "FROM 장바구니_담기 INNER JOIN 도서 ON 장바구니_담기.도서번호 = 도서.도서번호 " +
                    "WHERE 장바구니_담기.바구니번호=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView2.ColumnCount = 3;

                for (int i = 0; i < 3; i++)
                {
                    dataGridView2.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[3];

                    for (int i = 0; i < 3; i++)
                    {
                        obj[i] = new object();
                        obj[i] = read.GetValue(i).ToString();
                    }

                    dataGridView2.Rows.Add(obj);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void change_book()
        {
            comboBox1.Items.Clear();
            if (dataGridView2.Rows.Count == 0)
            {
                label2.Text = "책을 선택해 주세요";
                comboBox1.Enabled = false;
                return;
            }
            comboBox1.Enabled = true;
            label2.Text = dataGridView2.SelectedCells[1].Value.ToString();
            int change_max;
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "SELECT 재고량 FROM 도서 WHERE 도서번호 = '" + dataGridView2.SelectedCells[0].Value.ToString() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            OleDbDataReader read = cmd.ExecuteReader();
            read.Read();
            change_max = Int32.Parse(read.GetValue(0).ToString());
            if (change_max == 0)
            {
                comboBox1.Enabled = false;
                return;
            }
            for (int i = 0; i < change_max; i++)
            {
                comboBox1.Items.Add((i + 1).ToString() + "권");
            }
            comboBox1.Text = dataGridView2.SelectedCells[2].Value.ToString();
        }
        private void 장바구니관리_Load(object sender, EventArgs e)
        {
            label5.Text = 로그인.login_name + " 고객님";
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            updatedb_left();
            updatedb_right();
            change_book();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("장바구니에 담은 품목이 존재하지 않습니다.", "삭제 오류");
                return;
            }

            DataGridViewRow dr = dataGridView2.SelectedRows[0];
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "DELETE FROM 장바구니_담기 WHERE 도서번호=" + dr.Cells[0].Value.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb_right();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            change_book();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updatedb_right();
            change_book();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("장바구니에 담은 품목이 존재하지 않습니다.", "삭제 오류");
                return;
            }

            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("수량이 적절하지 않습니다.", "수량 오류");
                return;
            }

            DataGridViewRow dr = dataGridView2.SelectedRows[0];
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "UPDATE 장바구니_담기 SET 수량=" + (comboBox1.SelectedIndex + 1).ToString() + " WHERE 도서번호='" +
                    dr.Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "commit";//커밋으로 적용
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                updatedb_right();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kart_num = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            dr_num2 = dataGridView2.Rows.Count;
            dr2 = new DataGridViewRow[dr_num2];
            for (int i = 0; i < dr_num2; i++)
            {
                dr2[i] = dataGridView2.Rows[i];
            }
            장바구니주문 kart_order = new 장바구니주문(ma, this);
            kart_order.Show();
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
