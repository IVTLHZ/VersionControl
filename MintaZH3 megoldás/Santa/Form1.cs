using PackMaker;
using Santa.Entities;
using Santa.Enum;
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

namespace Santa
{
    public partial class Form1 : Form
    {
        BindingList<Child> children = new BindingList<Child>();
        SantaClausPack santaPack = new SantaClausPack();

        public Form1()
        {
            InitializeComponent();
            dgwChildren.DataSource = children;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var c = new Child()
            {
                Name = txtName.Text                
            };
            var behaviour = int.Parse(txtBehaviour.Text);

            if (!c.CheckBehaviour(behaviour))
            {
                MessageBox.Show("Helytelen viselkedési szint!");
                return;
            }

            c.Behaviour = (Behaviour)behaviour;
            c.Gifts = santaPack.GetGifts(behaviour);
            children.Add(c);

            var numberOfBadKids = (from x in children
                                   where (int)x.Behaviour <= 2
                                   select x).Count();            
            lblBadCounter.Text = numberOfBadKids.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                sw.WriteLine("Név;Ajándékok");
                foreach (var c in children)
                {
                    sw.Write(c.Name);
                    sw.Write(";");
                    foreach (var g in c.Gifts)
                    {
                        sw.Write(g.Name + " ");
                    }
                    sw.Write("\n");
                }
            }
        }
    }
}
