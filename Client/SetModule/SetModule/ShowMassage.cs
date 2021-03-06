﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SetModule
{
    public partial class ShowMassage : Form
    {
        private bool state = false;//是否开始监控标志位
        private Form temp;
        private bool showsth = true;//显示框显示的状态
        private string ModuleState ;

        public ShowMassage(Form sw)
        {
            InitializeComponent();
            temp = sw;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            this.Close();
            temp.Show();
        }

        string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
        MySqlConnection mycon;
        MySqlCommand mycmd;
        MySqlDataAdapter da;
        DataTable dt;

        private void ShowMassage_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            MySqlConnection mycon = new MySqlConnection(connstr);
            try
            {
                mycon.Open();
                mycmd = new MySqlCommand("SELECT * FROM `message`", mycon);
                da = new MySqlDataAdapter(mycmd);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch
            {
            }
            finally
            {
                //mycon.Close();
            }
            ThreadStart ts = new ThreadStart(Method);
            Thread t = new Thread(ts);
            t.Start();


            //查询是否处于报警状态
            MySqlConnection mycon2 = new MySqlConnection(connstr);
            MySqlCommand com = new MySqlCommand("SELECT * FROM `ModuleState`", mycon2);
            mycon2.Open();
            MySqlDataReader reader = com.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows) 
                    {
                        // Console.WriteLine("模块号:" + reader.GetInt32(0) + "状态:" + reader.GetString(1));
                        ModuleState = reader.GetString(1);
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("查询失败了！");
            }
            finally
            {
                reader.Close();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        void Method()
        {
            //更新线程

             while (true)
             {
                 if (state)
                 {
                    dt = new DataTable();
                    da.Fill(dt);
                    //Console.Write(11);

                    //更新模块报警状态////////////////////////////////////////////////////////////////////
                    MySqlConnection mycon2 = new MySqlConnection(connstr);
                    MySqlCommand com = new MySqlCommand("SELECT * FROM `ModuleState`", mycon2);
                    mycon2.Open();
                    MySqlDataReader reader = com.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                // Console.WriteLine("模块号:" + reader.GetInt32(0) + "状态:" + reader.GetString(1));
                                ModuleState = reader.GetString(1);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("查询失败了！");
                    }
                    finally
                    {
                        reader.Close();
                    }
                    ///////////////////////////////////////////////////////////////////////////////////

                }

                System.Threading.Thread.Sleep(900);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时器事件
            if (state)
            {
                try
                {
                    //mycon.Open();
                    //dt = new DataTable();
                    //da.Fill(dt);
                    dataGridView1.DataSource = dt;//更新UI

                    if (ModuleState == "OFF")
                    {
                        label3.Text = "无报警";
                    }
                    else
                    {
                        label3.Text = "有报警";
                    }
                }
                catch
                {
                }
                finally
                {
                    //mycon.Close();
                }
            }
        }

        private void textBox_bed_KeyPress(object sender, KeyPressEventArgs e)
        {
            //限制输入只能为整数
            char result = e.KeyChar;
            if (char.IsDigit(result) || result == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ShowMassage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(mycon !=null)
            if(mycon.State == ConnectionState.Open)
            mycon.Close();//关闭数据库
            temp.Show();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            state = true;
            label_state.Text = "开始监控";
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            state = false;
            label_state.Text = "停止监控";
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            MySqlConnection mycon = new MySqlConnection(connstr);
            try
            {
                mycon.Open();
                MySqlCommand mycmd_;
                if (showsth)
                {
                     mycmd_ = new MySqlCommand("truncate table `message`", mycon);
                }        
                else
                {
                     mycmd_ = new MySqlCommand("truncate table `warning`", mycon);
                }

                MySqlDataAdapter da_ = new MySqlDataAdapter(mycmd_);
                DataTable dt_ = new DataTable();
                da_.Fill(dt_);
            }
            catch
            {
            }
            finally
            {
                //mycon.Close();
            }
        }

        private void button_showall_Click(object sender, EventArgs e)
        {
            if (showsth)
            {
                button_showall.Text = "报警显示";
                showsth = false;

                MySqlConnection mycon = new MySqlConnection(connstr);
                try
                {
                    mycon.Open();
                    mycmd = new MySqlCommand("SELECT * FROM `warning`", mycon);
                    da = new MySqlDataAdapter(mycmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch
                {
                }

            }
            else
            {
                button_showall.Text = "离床显示";
                showsth = true;

                MySqlConnection mycon = new MySqlConnection(connstr);
                try
                {
                    mycon.Open();
                    mycmd = new MySqlCommand("SELECT * FROM `message`", mycon);
                    da = new MySqlDataAdapter(mycmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch
                {
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
