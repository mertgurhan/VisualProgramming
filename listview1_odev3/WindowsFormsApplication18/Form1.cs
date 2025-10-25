 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;//arraylist
using System.IO;

namespace WindowsFormsApplication18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       ArrayList resim = new ArrayList();
          string resimadi = "resim.jpg";
          int i, j,k,a;//i=toplan kayıt, j=aktif/seçili kayıt, k=hedef kayıt
       
        private void byaz()
        {
            //ilerigeri
            b1.Text = "ilk"; b2.Text = "geri";
            b3.Text = "sonraki"; b4.Text = "son";
            //tasi
            b5.Text = "enust tasi"; b6.Text = "birust tasi";
            b7.Text = "biralt tasi"; b8.Text = "enalt tasi";
            //yerdegistirme
            b12.Text = "enust yerdegistir"; b13.Text = "birust yerdegistir";
            b14.Text = "biralt yerdegistir"; b15.Text = "enalt yerdegistir";
            //diger
            b9.Text = "ekle"; b10.Text = "arayaekle";b11.Text = "degistir";
            b16.Text = "Ara";  b17.Text = "Sil"; b18.Text = "kaydet";
            b19.Text = "Resim Seç"; b20.Text = "KAPAT";
        }
        private void lwolustur()
        {

      
            
           
         LV1.View = View.Details;
            LV1.GridLines = true;
            LV1.CheckBoxes = false;
            LV1.Dock = DockStyle.None;
            LV1.LabelEdit = false;
            LV1.MultiSelect = false;
            LV1.AllowColumnReorder = false;
            LV1.HideSelection = false;
            LV1.FullRowSelect = true;
            LV1.Items.Clear();
            LV1.Columns.Add("No", 50, HorizontalAlignment.Center);
            LV1.Columns.Add("Ad-Soyad", 100, HorizontalAlignment.Center);
            LV1.Columns.Add("Ortalama", 70, HorizontalAlignment.Center);
            LV1.Columns.Add("Resim", 100, HorizontalAlignment.Center);
         

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lwolustur();
            byaz();
            dosyaoku();
            i = LV1.Items.Count - 1;
            j = i;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            yazilerigeri();
   
        }
        private void dosyaoku()//
        {
            StreamReader sr = new StreamReader("sinav2.txt");
            while (!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                string[] kayit = satir.Split(';');
                string resimyolu = kayit[3];
                ListViewItem item = new ListViewItem(kayit);
                LV1.Items.Add(item);
                resim.Add(resimyolu);

            }
            sr.Close();
        }
        private void dosyakayit()
        {
            int s;
            StreamWriter sw = new StreamWriter("sinav2.txt");
            for (s = 0; s < LV1.Items.Count; s++)
            {
                sw.Write(LV1.Items[s].SubItems[0].Text + ";");
                sw.Write(LV1.Items[s].SubItems[1].Text + ";");
                sw.Write(LV1.Items[s].SubItems[2].Text + ";");
                sw.WriteLine(LV1.Items[s].SubItems[3].Text + ";");

            }
            string ort = LV1.Items[j].SubItems[2].Text;

            sw.Close();
        
        }
        private void b18_Click(object sender, EventArgs e)//kayıt
        {
            //en son değişiklikler (ekle,sil vs..) yapıldıktan sonra dosyaya kayıt
            dosyakayit();
        }
        private void yazilerigeri()//yazma,secme,aktif/pasif
        {
            foreach (ListViewItem item in LV1.Items)
            {
                string deger = item.SubItems[2].Text;
                int ortalama = Convert.ToInt32(deger);
                if (ortalama < 50)
                {
                    item.ForeColor = Color.Red;
                }
                else
                {
                    item.ForeColor = Color.Black;
                }

            }
            if (i > -1)
            {
                pictureBox1.ImageLocation = resim[j].ToString();
                label1.Text = (j + 1) + "/" + (i + 1);
                label2.Text = LV1.Items[j].SubItems[0].Text;
                label3.Text = pictureBox1.ImageLocation;
                LV1.Items[j].Selected = true;
                b2.Enabled = true;

            }
            else
            {
                pictureBox1.ImageLocation = "";
                label1.Text = "";
                label2.Text = "";
                b2.Enabled = false;
            }
            TB1.Focus();

            //3.aktif/pasif           
            if (j == 0 || j == -1)
            {
                b1.Enabled = false; b2.Enabled = false;
                b5.Enabled = false; b6.Enabled = false;
                b12.Enabled = false; b13.Enabled = false;
            }
            else
            {
                b1.Enabled = true; b2.Enabled = true;
                b5.Enabled = true; b6.Enabled = true;
                b12.Enabled = true; b13.Enabled = true;
            }
            if (j == i)
            {
                b3.Enabled = false; b4.Enabled = false;
                b7.Enabled = false; b8.Enabled = false;
                b14.Enabled = false; b15.Enabled = false;
            }
            else
            {
                b3.Enabled = true; b4.Enabled = true;
                b7.Enabled = true; b8.Enabled = true;
                b14.Enabled = true; b15.Enabled = true;
            }
        }
        private void ekle()//ekle, arayaekle
        {
            LV1.Items.Add(TB1.Text);
            LV1.Items[j].SubItems.Add(TB2.Text);
            LV1.Items[j].SubItems.Add(TB3.Text);
            LV1.Items.Insert(j, resimadi);
        }
        private void b9_Click(object sender, EventArgs e)//ekle
        {
            i++;
            j = i;
            ekle();
            yazilerigeri();
          //  resim.Insert(j, resimadi);
            //en sona ekleyecek
            //eklenen kayıt secili olacak
            //sıralama önemli (i,j,fonk. yazma sırası!!)
        }
        private void b10_Click(object sender, EventArgs e)//arayaekle
        {
            i++;
            j = i;
            ekle();
            yazilerigeri();

        
        }
        private void degistir()
        {
            LV1.SelectedItems[0].Text = TB1.Text;
            LV1.SelectedItems[0].SubItems[1].Text = TB2.Text;
            LV1.SelectedItems[0].SubItems[2].Text = TB3.Text;

        }
        private void b11_Click(object sender, EventArgs e)//degistir
        {
            degistir();
        }
        private void LV_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kullanma
        }
        private void LV_Click(object sender, EventArgs e)
        {
            j = LV1.FocusedItem.Index;
            yazilerigeri();
           
        }
    
        //***************ilerigeri*************************************
        private void button1_Click(object sender, EventArgs e)//ilk
        {
            j = 0;
            yazilerigeri();
          
        }
        private void button2_Click(object sender, EventArgs e)//geri
        {
            j--;
            yazilerigeri();
           
        }
        private void button3_Click(object sender, EventArgs e)//ileri
        {
            j++;
            yazilerigeri();
        }
        private void button4_Click(object sender, EventArgs e)//son
        {
            j = i;
            yazilerigeri();
        }
        //*******************tasima*************************************
        private void b5_Click(object sender, EventArgs e)//enust j=0
        {
            k = 0; tasi(); yazilerigeri();
        }
        private void b6_Click(object sender, EventArgs e)//birust//yukari j--
        {
            k = j - 1; tasi(); yazilerigeri();
        }
        private void b7_Click(object sender, EventArgs e)//biralt//asagi j++
        {
            k = j + 1; tasi(); yazilerigeri();
           
        }
        private void b8_Click(object sender, EventArgs e)//enalt
        {
            k = i; tasi(); yazilerigeri();
        }
        //*************************************************************
        private void tasi()//y1
        {
            string a1, a2, a3, a4;
            a1 = LV1.Items[j].Text;
            a2 = LV1.Items[j].SubItems[1].Text;
            a3 = LV1.Items[j].SubItems[2].Text;
            a4 = LV1.Items[j].SubItems[3].Text;
            LV1.Items.RemoveAt(j);
            string[] bilgiler = { a1, a2, a3, a4 };
            LV1.Items.Insert(k, new ListViewItem(bilgiler));
        }
        //*******************yerdegistirme*****************
        private void b12_Click(object sender, EventArgs e)//enust yerdegistir
        {
            k = 0;
            yerdegistir();
            yazilerigeri();
        }
        private void b13_Click(object sender, EventArgs e)//birust yerdegistir
        {
            k = j - 1;
            yerdegistir();
            yazilerigeri();
         
        }
        private void b14_Click(object sender, EventArgs e)//biralt yerdegistir
        {
            k = j + 1;
            yerdegistir();
            yazilerigeri();

        }
        private void b15_Click(object sender, EventArgs e)//enalt yerdegistir
        {
            k = i;
            yerdegistir();
            yazilerigeri();
        }
        private void yerdegistir()
        {
            string a1 = LV1.Items[j].Text;
            string a2 = LV1.Items[j].SubItems[1].Text;
            string a3 = LV1.Items[j].SubItems[2].Text;
            string a4 = LV1.Items[j].SubItems[3].Text;

            LV1.Items[k].Text = a1;
            LV1.Items[k].SubItems[1].Text = a2;
            LV1.Items[k].SubItems[2].Text = a3;
            LV1.Items[k].SubItems[3].Text = a4;

        }
        //*************************************************************
        private void b16_Click(object sender, EventArgs e)//ara
        {
        for(int s = 0; s < LV1.Items.Count - 1; s++)
            {
                if (LV1.Items[s].SubItems[0].Text == TB1.Text)
                {
                    j = s;
                    yazilerigeri();
                    MessageBox.Show("bulundu");
                    return;
                }
            }
            TB1.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void b17_Click(object sender, EventArgs e)//sil
        {
            if (LV1.SelectedItems.Count > 0)
            {
                LV1.SelectedItems[0].Remove();
                i--; j = i;
                yazilerigeri();
            }

            }
		 private void b19_Click(object sender, EventArgs e)//Resim Seç
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası | *jpg;*.png";
            dosya.ShowDialog();
            resimadi = System.IO.Path.GetFileName(dosya.FileName);
            pictureBox1.ImageLocation = resimadi.ToString();
        }
        private void b20_Click(object sender, EventArgs e)//kapat
        {
            this.Close();
        }
    }
}
