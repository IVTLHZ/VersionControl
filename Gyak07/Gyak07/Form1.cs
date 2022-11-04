using Gyak07.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gyak07
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        //kifejtett prop
        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            //példány feltöltése konstruktorból
            Factory = new BallFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
