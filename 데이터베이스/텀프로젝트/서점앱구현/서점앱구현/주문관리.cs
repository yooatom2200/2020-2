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
    public partial class 주문관리 : Form
    {
        OleDbConnection conn;
        string connectionString;
        public 주문관리()
        {
            InitializeComponent();
        }

        public void db_load()
        {
            dataGridView1.Rows.Clear();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.CommandText = "select * from 주문";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView1.ColumnCount = 11;
                //필드명 받아오는 반복문
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[dataGridView1.ColumnCount];

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        obj[i] = new object();
                        obj[i] = new object();

                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 10);
                            continue;
                        }
                        if (i == 5)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 7);
                            continue;
                        }
                        obj[i] = read.GetValue(i).ToString();
                    }

                    dataGridView1.Rows.Add(obj);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
        }

        public void db_down()
        {
            dataGridView2.Rows.Clear();
            if (dataGridView1.Rows.Count == 0)
                return;
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.CommandText = "SELECT 주문_선택.도서번호, 도서.도서명, 주문_선택.수량, 도서.판매가, 주문_선택.수량*도서.판매가 AS 총_가격 " +
                    "FROM 주문_선택, 도서 WHERE 주문_선택.도서번호 = 도서.도서번호 AND 주문_선택.주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView2.ColumnCount = 5;
                //필드명 받아오는 반복문
                for (int i = 0; i < 5; i++)
                {
                    dataGridView2.Columns[i].Name = read.GetName(i);
                }
                if (dataGridView1.Rows.Count == 0)
                    return;
                while (read.Read())
                {
                    object[] obj = new object[5];

                    for (int i = 0; i < 5; i++)
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
                return;
            }
        }

        private void 주문관리_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void 주문관리_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            db_load();
            db_down();
            if (dataGridView1.Rows.Count == 0)
            {
                label4.Text = "주문번호 : NOT FOUND";
                return;
            }
            dataGridView1_CellClick(null, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            label4.Text = "주문번호 : " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() +
                "   주문상태 : " + dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "발송" ||
                dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "판매완료")
            {
                button2.Enabled = false;
                button2.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            }
            else
            {
                button2.Enabled = true;
                button2.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 128);
            }
            db_down();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.CommandText = "UPDATE 주문 set 주문상태 = '발송' where 주문번호 = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            cmd.CommandText = "commit";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            db_load();
            dataGridView1_CellClick(null, null);
            MessageBox.Show("발송처리되었습니다.", "발송 처리");
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                    cmd.CommandText = "select * from 주문";
                else
                    cmd.CommandText = "select * from 주문 where 회원번호 = '" + textBox1.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView1.ColumnCount = 11;
                //필드명 받아오는 반복문
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }
                /*if (dataGridView1.Rows.Count == 0)
                    return;*/
                while (read.Read())
                {
                    object[] obj = new object[dataGridView1.ColumnCount];

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        obj[i] = new object();
                        obj[i] = new object();

                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 10);
                            continue;
                        }
                        if (i == 5)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 7);
                            continue;
                        }
                        obj[i] = read.GetValue(i).ToString();
                    }

                    dataGridView1.Rows.Add(obj);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            db_down();
            dataGridView1_CellClick(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                
                cmd.CommandText = "select * from 주문 where 주문상태 = '신청'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView1.ColumnCount = 11;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }
                while (read.Read())
                {
                    object[] obj = new object[dataGridView1.ColumnCount];

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        obj[i] = new object();
                        obj[i] = new object();

                        if (i == 1)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 10);
                            continue;
                        }
                        if (i == 5)
                        {
                            obj[i] = read.GetValue(i).ToString().Substring(0, 7);
                            continue;
                        }
                        obj[i] = read.GetValue(i).ToString();
                    }

                    dataGridView1.Rows.Add(obj);
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            if (dataGridView1.Rows.Count == 0)
                return;
            db_down();
            dataGridView1_CellClick(null, null);
        }
    }
}
