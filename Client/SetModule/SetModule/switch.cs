﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetModule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            ShowMassage show_massage = new ShowMassage(this);
            this.Hide();
            show_massage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetSth set_sth = new SetSth(this);
            this.Hide();
            set_sth.Show();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);//关闭所有线程
        }
    }
}
