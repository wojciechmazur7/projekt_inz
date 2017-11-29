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
    public partial class History : Form
    {
        public List<Bitmap> Collection;
        #region ctor
        public History(List<Bitmap> collection)
        {          
            InitializeComponent();
            this.Collection = collection;
            for (int i = 1; i >= collection.Count; i++)
            {
           
            }
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
