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
    public partial class 관리자페이지 : Form
    {
        OleDbConnection conn;
        string connectionString;
        public 관리자페이지()
        {
            InitializeComponent();
        }
        
        public void db_load()
        {
            dataGridView1.Rows.Clear();
            if (radioButton1.Checked)//회원
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
            else if (radioButton2.Checked)//회원 주소록
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원_주소록";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 5;
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
            }
            else if (radioButton3.Checked)//회원 신용카드
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원_신용카드";
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
                            if(i == 2)
                            {
                                obj[i] = read.GetValue(i).ToString().Substring(0,7);
                                continue;
                            }
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
            }
            else if (radioButton4.Checked)//도서
            {
                label3.Text = "도서번호 검색 : ";
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
            }
            else if (radioButton5.Checked)//주문 선택
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 주문_선택";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
            else if (radioButton6.Checked)//주문
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 주문";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 11;
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
            }
            else if (radioButton7.Checked)//장바구니
            {
                label3.Text = "아이디 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 장바구니";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
                            if(i == 1)
                            {
                                obj[i] = read.GetValue(i).ToString().Substring(0,10);
                                continue;
                            }
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
            }
            else if (radioButton8.Checked)//장바구니담기
            {
                label3.Text = "바구니번호 검색 : ";
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 장바구니_담기";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "회원번호 :";
            label5.Text = "비밀번호 :";
            label6.Text = "성명 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = false;
            textBox7.Visible = false;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "회원번호 :";
            label5.Text = "배송지 :";
            label6.Text = "우편번호 :";
            label7.Text = "기본주소 :";
            label8.Text = "상세주소 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = true;
            textBox7.Visible = true;
            label8.Visible = true;
            textBox8.Visible = true;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "회원번호 :";
            label5.Text = "카드번호 :";
            label6.Text = "유효기간 :";
            label7.Text = "카드종류 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = true;
            textBox7.Visible = true;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "도서번호 :";
            label5.Text = "도서명 :";
            label6.Text = "재고량 :";
            label7.Text = "판매가 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = true;
            textBox7.Visible = true;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "주문번호 :";
            label5.Text = "도서번호 :";
            label6.Text = "수량 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = false;
            textBox7.Visible = false;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "주문번호 :";
            label5.Text = "주문일자 :";
            label6.Text = "주문총액 :";
            label7.Text = "주문상태 :";
            label8.Text = "신용카드 카드번호 :";
            label9.Text = "신용카드 유효기간 :";
            label10.Text = "신용카드 카드종류 :";
            label11.Text = "배송지 우편번호 :";
            label12.Text = "배송지 기본주소 :";
            label13.Text = "배송지 상세주소 :";
            label14.Text = "회원번호 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = true;
            textBox7.Visible = true;
            label8.Visible = true;
            textBox8.Visible = true;
            label9.Visible = true;
            textBox9.Visible = true;
            label10.Visible = true;
            textBox10.Visible = true;
            label11.Visible = true;
            textBox11.Visible = true;
            label12.Visible = true;
            textBox12.Visible = true;
            label13.Visible = true;
            textBox13.Visible = true;
            label14.Visible = true;
            textBox14.Visible = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "바구니번호 :";
            label5.Text = "생성일자 :";
            label6.Text = "회원번호 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = false;
            textBox7.Visible = false;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            db_load();
            dataGridView1_CellClick(null, null);
            label4.Text = "바구니번호 :";
            label5.Text = "도서번호 :";
            label6.Text = "수량 :";
            label4.Visible = true;
            textBox4.Visible = true;
            label5.Visible = true;
            textBox5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            label7.Visible = false;
            textBox7.Visible = false;
            label8.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            label11.Visible = false;
            textBox11.Visible = false;
            label12.Visible = false;
            textBox12.Visible = false;
            label13.Visible = false;
            textBox13.Visible = false;
            label14.Visible = false;
            textBox14.Visible = false;
        }

        private void 관리자페이지_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void 관리자페이지_Load(object sender, EventArgs e)
        {
            connectionString = "Provider=MSDAORA;Password=Hr970924;User ID=HR20164091";
            conn = new OleDbConnection(connectionString);
            conn.Open();
            db_load();
            radioButton1_CheckedChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)//검색
        {
            dataGridView1.Rows.Clear();
            if (radioButton1.Checked)//회원
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원 where 회원번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
                    //필드명 받아오는 반복문
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        dataGridView1.Columns[i].Name = read.GetName(i);
                        return;
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
                    MessageBox.Show("Error: " + ex.Message); //에러 메세지 .
                    return;
                }
            }
            else if (radioButton2.Checked)//회원 주소록
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원_주소록 where 회원번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 5;
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
            }
            else if (radioButton3.Checked)//회원 신용카드
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 회원_신용카드 where 회원번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 4;
                    //필드명 받아오는 반복문
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        dataGridView1.Columns[i].Name = read.GetName(i);
                        return;
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
            }
            else if (radioButton4.Checked)//도서
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 도서 where 도서번호 = '" + textBox1.Text + "'";
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
            }
            else if (radioButton5.Checked)//주문 선택
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 주문_선택 where 주문번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
            else if (radioButton6.Checked)//주문
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 주문 where 회원번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 11;
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
            }
            else if (radioButton7.Checked)//장바구니
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 장바구니 where 회원번호='" + textBox1.Text + "'";
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
            else if (radioButton8.Checked)//장바구니담기
            {
                try
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "select * from 장바구니_담기 where 바구니번호 = " + textBox1.Text;
                    cmd.CommandType = CommandType.Text; //검색명령을 쿼리 형태로
                    cmd.Connection = conn;

                    OleDbDataReader read = cmd.ExecuteReader(); //select * from emp 결과
                    dataGridView1.ColumnCount = 3;
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
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            if (radioButton1.Checked)//회원
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            else if (radioButton2.Checked)//회원 주소록
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            else if (radioButton3.Checked)//회원 신용카드
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Substring(0, 7);
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
            else if (radioButton4.Checked)//도서
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
            else if (radioButton5.Checked)//주문 선택
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            else if (radioButton6.Checked)//주문
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10);
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Substring(0, 7);
                textBox10.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox11.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox12.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                textBox13.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                textBox14.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();

            }
            else if (radioButton7.Checked)//장바구니
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Substring(0, 10);
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            else if (radioButton8.Checked)//장바구니담기
            {
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)//입력버튼
        {
            OleDbCommand cmd = new OleDbCommand();
            if(MessageBox.Show("DB를 변경하는 작업입니다. 신중하게 진행해 주세요.\n생성작업을 진행하시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            try
            {
                if (radioButton1.Checked)//회원
                {
                    cmd.CommandText = "INSERT INTO 회원 VALUES ('" + textBox4.Text + "','" + textBox5.Text + "', '" + textBox6.Text + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton2.Checked)//회원 주소록
                {
                    cmd.CommandText = "INSERT INTO 회원_주소록 VALUES ('" + textBox4.Text + "','" + textBox5.Text + "', '" + textBox6.Text +
                        "', '" + textBox7.Text + "', '" + textBox8.Text + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton3.Checked)//회원 신용카드
                {
                    cmd.CommandText = "INSERT INTO 회원_신용카드 VALUES ('" + textBox4.Text +
                    "','" + textBox5.Text + "',TO_DATE('" + textBox6.Text + "','YYYY-MM-DD'),'" +
                    textBox7.Text + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton4.Checked)//도서
                {
                    cmd.CommandText = "INSERT INTO 도서 VALUES ('" + textBox4.Text + "','" + textBox5.Text + "', " + textBox6.Text +
                        ", " + textBox7.Text + ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton5.Checked)//주문 선택
                {
                    cmd.CommandText = "INSERT INTO 주문_선택 VALUES ('" + textBox4.Text + "','" + textBox5.Text + "', " + textBox6.Text +
                        ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton6.Checked)//주문
                {
                    cmd.CommandText = "INSERT INTO 주문 VALUES('"+ textBox4.Text + "',TO_DATE('" + textBox5.Text + "', 'YYYY-MM-DD')," + textBox6.Text + 
                        ",'" + textBox7.Text + "', '" + textBox8.Text + "', TO_DATE('" + textBox9.Text + "', 'YYYY-MM-DD'), '" + textBox10.Text + "', '" +
                        textBox11.Text + "', '" + textBox12.Text + "', '" + textBox13.Text + "', '" + textBox14.Text + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                }
                else if (radioButton7.Checked)//장바구니
                {
                    cmd.CommandText = "INSERT INTO 장바구니 VALUES(" + textBox4.Text + ",TO_DATE('" + textBox5.Text + "','YYYY-MM-DD'), '" + textBox6.Text + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton8.Checked)//장바구니담기
                {
                    cmd.CommandText = "INSERT INTO 장바구니_담기 VALUES(" + textBox4.Text + ",'" + textBox5.Text + "'," + textBox6.Text + ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            db_load();
            MessageBox.Show("데이터가 추가되었습니다.", "추가 완료");
        }

        private void button3_Click(object sender, EventArgs e)//수정
        {
            OleDbCommand cmd = new OleDbCommand();
            if (MessageBox.Show("DB를 변경하는 작업입니다. 신중하게 진행해 주세요.\n수정작업을 진행하시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            try
            {
                if (radioButton1.Checked)//회원
                {
                    cmd.CommandText = "UPDATE 회원 SET 비밀번호 = '" + textBox5.Text + "', 성명 = '" + textBox6.Text +
                        "' WHERE 회원번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton2.Checked)//회원 주소록
                {
                    cmd.CommandText = "UPDATE 회원_주소록 SET 우편번호 = '" + textBox6.Text + "', 기본주소 = '" + textBox7.Text +
                        "', 상세주소 = '" + textBox8.Text + "' WHERE 회원번호 = '" + textBox4.Text + "' AND 배송지 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton3.Checked)//회원 신용카드
                {
                    cmd.CommandText = "UPDATE 회원_신용카드 SET 유효기간 = TO_DATE('" + textBox6.Text + "','YYYY-MM'), 카드종류 = '" + 
                        textBox7.Text + "' WHERE 회원번호 = '" + textBox4.Text + "' AND 카드번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton4.Checked)//도서
                {
                    cmd.CommandText = "UPDATE 도서 set 도서명 = '" + textBox5.Text + "', 재고량 = " + textBox6.Text + 
                        ", 판매가 = " + textBox7.Text + " where 도서번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton5.Checked)//주문 선택
                {
                    cmd.CommandText = "UPDATE 주문_선택 SET 수량 = " + textBox6.Text + " WHERE 주문번호 = '" + textBox4.Text +
                        "' AND 도서번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton6.Checked)//주문
                {
                    cmd.CommandText = "UPDATE 주문 SET 주문일자 = TO_DATE('" + textBox5.Text + "','YYYY-MM-DD'), 주문총액 = " + textBox6.Text +
                        ", 주문상태 = '" + textBox7.Text + "', 신용카드_카드번호 = '" + textBox8.Text + "', 신용카드_유효기간 = TO_DATE('" + textBox9.Text +
                        "','YYYY-MM'), 신용카드_카드종류 = '" + textBox10.Text + "', 배송지_우편번호 = '" + textBox11.Text + "', 배송지_기본주소 = '" +
                        textBox12.Text + "', 배송지_상세주소 = '" + textBox13.Text + "', 회원번호 = '" + textBox14.Text + "' WHERE 주문번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                }
                else if (radioButton7.Checked)//장바구니
                {
                    cmd.CommandText = "UPDATE 장바구니 SET 생성일자 = TO_DATE('" + textBox5.Text + "','YYYY-MM-DD'), 회원번호 = '" +
                        textBox6.Text + "' WHERE 바구니번호 = " + textBox4.Text;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton8.Checked)//장바구니담기
                {
                    cmd.CommandText = "UPDATE 장바구니_담기 SET 수량 = " + textBox6.Text + " WHERE 바구니번호 = " + textBox4.Text +
                        " AND 도서번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            db_load();
            MessageBox.Show("데이터가 수정되었습니다.", "추가 완료");
        }

        private void button4_Click(object sender, EventArgs e)//삭제
        {
            OleDbCommand cmd = new OleDbCommand();
            if (MessageBox.Show("DB를 변경하는 작업입니다. 신중하게 진행해 주세요.\n삭제작업을 진행하시겠습니까?\n" +
                "삭제시 하위 FK 사용 데이터까지 모두 삭제됩니다.", "경고", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            try
            {
                if (radioButton1.Checked)//회원
                {
                    cmd.CommandText = "DELETE FROM 회원 WHERE 회원번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton2.Checked)//회원 주소록
                {
                    cmd.CommandText = "DELETE FROM 회원_주소록 WHERE 회원번호 = '" + textBox4.Text + "' AND 배송지 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton3.Checked)//회원 신용카드
                {
                    cmd.CommandText = "DELETE FROM 회원_신용카드 WHERE 회원번호 = '" + textBox4.Text + "' AND 카드번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton4.Checked)//도서
                {
                    cmd.CommandText = "DELETE FROM 도서 WHERE 도서번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton5.Checked)//주문 선택
                {
                    cmd.CommandText = "DELETE FROM 주문_선택 WHERE 주문번호 = '" + textBox4.Text + "' AND 도서번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton6.Checked)//주문
                {
                    cmd.CommandText = "DELETE FROM 주문 WHERE 주문번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();

                }
                else if (radioButton7.Checked)//장바구니
                {
                    cmd.CommandText = "DELETE FROM 장바구니 WHERE 바구니번호 = '" + textBox4.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton8.Checked)//장바구니담기
                {
                    cmd.CommandText = "DELETE FROM 장바구니_담기 WHERE 바구니번호 = '" + textBox4.Text + "' AND 도서번호 = '" + textBox5.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            db_load();
            MessageBox.Show("데이터가 삭제되었습니다.", "추가 완료");
        }

        private void label15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("회원 : PK = 회원번호\n회원_주소록 : PK = 회원번호, 배송지 FK = 회원번호\n" +
                "회원_신용카드 : PK = 회원번호, 카드번호 FK = 회원번호\n도서 : PK = 도서번호\n" +
                "주문_선택 : PK = 주문번호, 도서번호 FK = 주문번호, 도서번호\n주문 : PK = 주문번호 FK = 회원번호\n" +
                "장바구니 : PK = 바구니번호 FK = 회원번호\n장바구니_담기 : PK = 바구니번호, 도서번호 FK = 바구니번호, 도서번호", "PK, FK 알림");
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                return;
        }
    }
}
