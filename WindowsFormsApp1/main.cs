using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class form1 : Form
    {
        public int red, green, blue;
        public List <Bitmap> changes;
        Bitmap CurrentBitmap;
        public changecolor form2;
        public static form1 _Form1;
        

        public form1()
        {
            changes= new List <Bitmap>();
            InitializeComponent();
            _Form1 = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter="Image File (*.bmp,*.jpg)|*.bmp;*.jpg";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                this.picOriginal.Image = new Bitmap(ofile.FileName);
                updateRichTextBox("Otwarto plik: " + ofile.FileName+ "\n");
            }
            progressBar.Minimum = 0;
            progressBar.Maximum = (CurrentBitmap.Width - 1)/2;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {   
            int index = changes.FindIndex(x=>x.Equals(CurrentBitmap));  
            btnNext.Enabled = true;
            CurrentBitmap = changes[index - 1];
            this.picOriginal.Image = CurrentBitmap;
            if (index == 1)
            {
                btnPrevious.Enabled = false;
                return;
            }
        }

        private void przetwarzanieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ustawKoloryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateRichTextBox("rozpoczęto zmianę koloru \n");
            form2 = new changecolor(this);
            form2.Show();
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "Image File (*.bmp,*.jpg)|*.bmp;*.jpg";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                this.picOriginal.Image = new Bitmap(ofile.FileName);
                updateRichTextBox("Otwarto plik: " + ofile.FileName + "\n");
            }
            CurrentBitmap = new Bitmap((Bitmap)this.picOriginal.Image);
            changes.Add(CurrentBitmap);
            progressBar.Minimum = 0;
            progressBar.Maximum = CurrentBitmap.Width - 1;
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfile = new SaveFileDialog();
            sfile.Filter = "zapisz jako (*.bmp)|*.bmp|jpeg (*.jpeg)|*.jpeg|png (*.png)|*.png"; // DO ROZWINIĘCIA!!!!
            if (DialogResult.OK == sfile.ShowDialog())
            {
                this.picOriginal.Image.Save(sfile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                updateRichTextBox("Zapisano plik: " + sfile.FileName+"\n");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int index = changes.FindIndex(x => x.Equals(CurrentBitmap));

            btnPrevious.Enabled = true;
            CurrentBitmap = changes[index + 1];
            this.picOriginal.Image = CurrentBitmap;
            if (index == changes.Count - 2)
            {
                btnNext.Enabled = false;
                return;
            }
        }

        private void historiaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Call Metods
        private void btnNegativ_Click(object sender, EventArgs e)
        {
            updateRichTextBox("Zastosowano funkcje negatyw \n");
            Thread thr = new Thread(ChangeNegativ);
            thr.Start();
            changeBtnEnable();
        }
        private void btnGray_Click(object sender, EventArgs e)
        {
            updateRichTextBox("Rozpoczęto zamiane koloru obrazu na czarno-biały \n");
            Thread thr = new Thread(ChangeGrayscale);
            thr.Start();
            changeBtnEnable();
        }

        private void btnThreshold_Click(object sender, EventArgs e)
        {
            updateRichTextBox("Rozpoczęto wykonanie funkcji progującej \n");
            Thread thr = new Thread(ChangeThreshold);
            thr.Start();
            changeBtnEnable();
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {       
            Thread thr = new Thread(() => ChangeColor(red, green, blue));
            thr.Start();
            changeBtnEnable();
        }
        
        private void btnBlur_Click(object sender, EventArgs e)
        {
            updateRichTextBox("Rozpoczęto wygładzanie obrazu \n");
            Thread thr = new Thread(ChangeBlur);
            thr.Start();
            changeBtnEnable();
        }
        #endregion

        #region metods processing
        public void ChangeColor(object red, object blue, object green)
        {
            CurrentBitmap = new Bitmap((Bitmap)this.picOriginal.Image);
            processing.ChangeColor(CurrentBitmap, red, green, blue);
            this.picOriginal.Image = CurrentBitmap;
            updateRichTextBox("Kolory obrazu zostały zmienione \n");
            changes.Add(CurrentBitmap);
        }
        public void ChangeThreshold()
        {
            CurrentBitmap = new Bitmap((Bitmap)this.picOriginal.Image);
            processing.ConvertToProg(CurrentBitmap);
            this.picOriginal.Image = CurrentBitmap;
            updateRichTextBox("Funkcja progująca została zastosowana \n");
            changes.Add(CurrentBitmap);
        }

        public void ChangeBlur()
        {
            CurrentBitmap = new Bitmap((Bitmap)this.picOriginal.Image);
            processing.Blur(CurrentBitmap);
            this.picOriginal.Image = CurrentBitmap;
            updateRichTextBox("Obraz został wygładzony \n");
            changes.Add(CurrentBitmap);
        }

        public void ChangeNegativ()
        {
            CurrentBitmap = new Bitmap((Bitmap)this.picOriginal.Image);
            processing.ConvertToNegative(CurrentBitmap);
            this.picOriginal.Image = CurrentBitmap;
            updateRichTextBox("Obraz został wyświetlony w negatywie \n");
            changes.Add(CurrentBitmap);
        }

        public void ChangeGrayscale()
        {
            CurrentBitmap = new Bitmap((Bitmap)picOriginal.Image);
            processing.ConvertToGray(CurrentBitmap);
            this.picOriginal.Image = CurrentBitmap;
            updateRichTextBox("Obraz zamieniono na czarno-biały \n");
            changes.Add(CurrentBitmap);
        }
        #endregion

        public void changeBtnEnable()
        {
            btnNext.Enabled = false;
            btnPrevious.Enabled = true;
        }

        public void updateRichTextBox(string info)
        {
            if (BoxInfo.InvokeRequired) BoxInfo.Invoke(new Action<string>(updateRichTextBox), info);
            else {
                BoxInfo.AppendText(info);
                BoxInfo.ScrollToCaret();
            }
        }

        public void updateProgressBarValue(int i)
        {
            if (progressBar.InvokeRequired) progressBar.Invoke(new Action<int>(updateProgressBarValue), i);
            else
            {
                progressBar.Increment(i);
                if (progressBar.Value == progressBar.Maximum) progressBar.Value = 0;
            }
        }

    }
}
