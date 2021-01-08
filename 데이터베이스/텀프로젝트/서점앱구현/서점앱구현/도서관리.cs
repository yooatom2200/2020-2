using System;
using System.IO;
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
    public partial class 도서관리 : Form
    {
        OleDbConnection conn;
        string connectionString;
        public 도서관리()
        {
            InitializeComponent();
        }
        public void db_load()
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
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = read.GetName(i);
                }

                while (read.Read())
                {
                    object[] obj = new object[dataGridView1.ColumnCount]; // 필드수만큼 오브젝트 배열

                    for (int i = 0; i < dataGridView1.ColumnCount; i++) // 필드 수만큼 반복
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
                return;
            }
            if (dataGridView1.Rows.Count == 0)
                return;
            dataGridView1_CellClick(null, null);
        }

        private void 도서관리_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            db_load();
            try
            {
                using (FileStream stream = new FileStream(@"..\..\Resources\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg",
                    FileMode.Open, FileAccess.Read))
                {
                    pictureBox2.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
                label8.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg";
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                label8.Text = "Not Found";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            try
            {
                using (FileStream stream = new FileStream(@"..\..\Resources\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg",
                    FileMode.Open, FileAccess.Read))
                {
                    pictureBox2.Image = Image.FromStream(stream);
                    stream.Dispose();
                }
                label8.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + ".jpg";
            }
            catch
            {
                pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                label8.Text = "Not Found";
            }
        }

        private void 도서관리_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("이미지 선택 전 이미지파일 이름을 해당 도서의 도서코드와 일치시켜주세요!");
            openFileDialog1.Title = "이미지 선택";
            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = "Image Files(*.jpg)|*.jpg;";
            openFileDialog1.ShowDialog();
            label8.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            using (FileStream stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
            {
                pictureBox2.Image = Image.FromStream(stream);
                stream.Dispose();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(pictureBox2.Image);
                img.Save(@"..\..\Resources\" + label8.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                MessageBox.Show("이미지가 리소스폴더에 저장되었습니다.");
                label8.Text += ".jpg";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("이미지를 정말 삭제하시겠습니까?","삭제여부",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.IO.File.Delete(@"..\..\Resources\" + label8.Text);
                    MessageBox.Show("이미지가 삭제되었습니다.");
                    label8.Text = "Not Found";
                    pictureBox2.Image = Image.FromFile(@"..\..\Resources\notfound.png");
                }
                else
                {
                    MessageBox.Show("취소하셨습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.CommandText = "INSERT INTO 도서 VALUES ('" + textBox4.Text + "','" + textBox5.Text + "', " + textBox6.Text +
                            ", " + textBox7.Text + ")";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            cmd.CommandText = "commit";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            db_load();
            MessageBox.Show("책 데이터가 추가되었습니다.", "추가 완료");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.CommandText = "UPDATE 도서 set 도서명 = '" + textBox5.Text + "', 재고량 = " + textBox6.Text +
                        ", 판매가 = " + textBox7.Text + " where 도서번호 = '" + textBox4.Text + "'";
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
            MessageBox.Show("책 데이터가 수정되었습니다.", "수정 완료");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if(MessageBox.Show("해당 책을 정말로 삭제하시겠습니까?","삭제 여부",MessageBoxButtons.YesNo) == DialogResult.No)
            {
                MessageBox.Show("취소하셨습니다.");
                return;
            }
            
            try
            {
                cmd.CommandText = "DELETE FROM 도서 WHERE 도서번호 = '" + textBox4.Text + "'";
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
            MessageBox.Show("책 데이터가 삭제되었습니다.", "삭제 완료");
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
