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
    public partial class 판매이력조회 : Form
    {
        OleDbConnection conn;
        string connectionString;

        public 판매이력조회()
        {
            InitializeComponent();
        }

        private void 판매이력조회_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            db_load(string.Empty);
        }

        public void db_load(string id)
        {
            dataGridView1.Rows.Clear();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                if(string.IsNullOrEmpty(id))
                    cmd.CommandText = "select 주문.회원번호, 주문.주문번호, 주문.주문일자, 도서.도서번호, 도서.도서명, 주문_선택.수량, 도서.판매가, 주문_선택.수량*도서.판매가 AS 총_가격" +
                        " from 주문, 주문_선택, 도서 WHERE 주문_선택.도서번호 = 도서.도서번호 AND 주문.주문번호 = 주문_선택.주문번호";
                else
                    cmd.CommandText = "select 주문.회원번호, 주문.주문번호, 주문.주문일자, 도서.도서번호, 도서.도서명, 주문_선택.수량, 도서.판매가, 주문_선택.수량*도서.판매가 AS 총_가격" +
                        " from 주문, 주문_선택, 도서 WHERE 주문_선택.도서번호 = 도서.도서번호 AND 주문.주문번호 = 주문_선택.주문번호 AND 주문.회원번호 = '" + id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                OleDbDataReader read = cmd.ExecuteReader();
                dataGridView1.ColumnCount = 7;
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

        private void 판매이력조회_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                db_load(string.Empty);
            else
                db_load(textBox1.Text);
        }
    }
}
