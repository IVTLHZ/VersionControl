using Gyak03IVTLHZ.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak03IVTLHZ
{

    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();


            //resourceból érték állítása
            label1.Text = Resource1.FullName;
            //csv -label2.Text = Resource1.FirstName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User() {
                //csv - LastName = textBox1.Text,
                //csv - FirstName = textBox2.Text
                FullName = textBox1.Text,
            };
            users.Add(user);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);

                foreach (var user in users)
                {
                    sw.Write($"{user.ID};{user.FullName}");
                    
                    
                }  
                sw.Close();
            }
        }
    }
}
