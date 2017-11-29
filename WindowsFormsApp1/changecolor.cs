using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class changecolor : Form
    {
        private form1 parent;
        public int colorRed
        {
            set
            {
                trackBar1.Value = value;
            }
            get
            {
                return trackBar1.Value;
            }
        }

        public int colorGreen
        {
            set
            {
                trackBar2.Value = value;
            }
            get
            {
                return trackBar2.Value;
            }
        }

        public int colorBlue
        {
            set
            {
                trackBar3.Value = value;
            }
            get
            {
                return trackBar3.Value;
            }
        }

        public changecolor(form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            parent.ChangeColor(colorRed, colorGreen, colorBlue);
            this.Close();
        }

        
    }
}
