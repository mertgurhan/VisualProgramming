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
        int i, j,k,a;//i=toplan kayıt, j=aktif/seçili kayıt, k=hedef kayıt
        private void byaz()
        {
            //ilerigeri
            b1.Text = "ilk"; b2.Text = "geri";
            b3.Text = "sonraki"; b4.Text = "son";
            //tasi
            b5.Text = "enust tasi"; b6.Text = "birust tasi";
            b7.Text = "biralt tasi"; b8.Text = "enalt tasi";
            //diger
            b9.Text = "ekle"; b10.Text = "arayaekle";b11.Text = "degistir";
            b16.Text = "Ara";  b17.Text = "Sil"; b18.Text = "kaydet";
            b19.Text = "KAPAT";
            //b12,b13,b14,b15 başka amaç için sonra kullanılacaktır.
        }
        private void lwyukle()
        {
            LV.View = View.Details;
            LV.GridLines = true;
            LV.CheckBoxes = false;
            LV.Dock = DockStyle.None;
            LV.LabelEdit = false;
            LV.MultiSelect = false;
            LV.AllowColumnReorder = false;
            LV.HideSelection = false;
            LV.FullRowSelect = true;
            LV.Items.Clear();
            LV.Columns.Add("NO", 50, HorizontalAlignment.Center);
            LV.Columns.Add("İsim", 100, HorizontalAlignment.Center);
            LV.Columns.Add("Ortalama", 50, HorizontalAlignment.Center);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lwyukle();
            byaz();
            dosyaoku();
            i = LV.Items.Count - 1;
            j = i;
        }
        private void dosyaoku()//arraylist kullanmadan LV yukleme yapılabilir.
        {
            string satir;
            LV.Items.Clear();
            StreamReader sr = new StreamReader("ogrenci.txt");
            while ((satir = sr.ReadLine()) != null){
                string[] kayit = satir.Split(';');
                ListViewItem item = new ListViewItem(kayit);
                LV.Items.Add(item);

            }
            sr.Close();
            //kullanılacak dosya:>> \bin\Debug\ogrenci.txt
			//dosya \bin\Debug mevcuttur.
        }
        private void dosyakayit()
        {
            string dosya = "ogrenci.txt";
            string satir;
            if (File.Exists(dosya))
            {
                StreamReader sr = new StreamReader(dosya);
                while (!sr.EndOfStream)
                {
                    satir = sr.ReadLine();
                    LV.Items.Add(satir);
                    string[] kayit = satir.Split(';');
                    LV.Items.Add(kayit[0]);
                    LV.Items.Add(kayit[1]);
                    LV.Items.Add(kayit[2]);

                }
              

                sr.Close();
            }
         
            
        }
        private void b18_Click(object sender, EventArgs e)//kayıt
        {
            //en son değişiklikler (eekle,sil vs..) yapıldıktan sonra kayıt
            dosyakayit();
        }
        private void yazilerigeri()//yazma,secme,aktif/pasif
        {
            //1.label yazma   //2.seçme
            if (i > -1)
            {
                label1.Text = (j + 1) + " / " + (i + 1);
                label2.Text = LV.Items[j].SubItems[0].Text;
                LV.Items[j].Selected = true;
                b17.Enabled = true;
            }
            else
            {
                label1.Text = "";
                label2.Text = "";
                b17.Enabled = false;
            }
           
            //3.aktif/pasif           
            if (j==0 || j==-1)
            {
                b1.Enabled = false; b2.Enabled = false;
                b5.Enabled = false; b6.Enabled = false;
            }
            else
            {
                b1.Enabled = true; b2.Enabled = true;
                b5.Enabled = true; b6.Enabled = true;
            }
            if (j==i)
            {
                b3.Enabled = false; b4.Enabled = false;
                b7.Enabled = false; b8.Enabled = false;
            }
            else
            {
                b3.Enabled = true; b4.Enabled = true;
                b7.Enabled = true; b8.Enabled = true;
            }
        }
        private void ekle()//ekle, arayaekle
        {
            LV.Items.Add(TB1.Text);
            LV.Items[j].SubItems.Add(TB2.Text);
            LV.Items[j].SubItems.Add(TB3.Text);

            
        }
        private void b9_Click(object sender, EventArgs e)//ekle
        {
            i++;
            j = i;
            ekle();
            yazilerigeri();
            //en sona ekleyecek
            //eklenen kayıt secili olacak
            //sıralama önemli (i,j,fonk. yazma sırası!!)
        }
        private void b10_Click(object sender, EventArgs e)//arayaekle
        {
            i++;
            j = LV.SelectedIndices[0];
            ekle();
            yazilerigeri();
            //secilikaydın ustune ekleyecek
        }
        private void degistir()
        {
            LV.Items[j].Text = TB1.Text;
            LV.Items[j].SubItems[1].Text = TB2.Text;
            LV.Items[j].SubItems[2].Text = TB3.Text;

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
            j = LV.FocusedItem.Index;
            yazilerigeri();
        }
        //***************ilerigeri*************************************
        private void button1_Click(object sender, EventArgs e)//ilk
        {
            j = 0; yazilerigeri();
        }
        private void button2_Click(object sender, EventArgs e)//geri
        {
            j--; yazilerigeri();
        }
        private void button3_Click(object sender, EventArgs e)//ileri
        {
            j++; yazilerigeri();
        }
        private void button4_Click(object sender, EventArgs e)//son
        {
            j = i; yazilerigeri();
        }
        //*******************tasima*************************************
        private void b5_Click(object sender, EventArgs e)//enust j=0
        {
            tasi(); j = 0; yazilerigeri();
        }
        private void b6_Click(object sender, EventArgs e)//birust//yukari j--
        {
            tasi(); j--; yazilerigeri();
        }
        private void b7_Click(object sender, EventArgs e)//biralt//asagi j++
        {
            tasi(); j++; yazilerigeri();
        }
        private void b8_Click(object sender, EventArgs e)//enalt
        {
            tasi(); j = i; yazilerigeri();
        }
        //*************************************************************
        private void tasi()//y1
        {
            string a1, a2, a3, a4, a5;
            a1 = LV.Items[j].SubItems[0].Text;
            a2 = LV.Items[j].SubItems[1].Text;
            a3 = LV.Items[j].SubItems[2].Text;
            LV.Items.RemoveAt(j);
            string[] bilgiler = { a1, a2, a3};
            LV.Items.Insert(k, new ListViewItem(bilgiler));
        }
        private void tasi(int k)//y2
        {
        }
        //*************************************************************
        private void b16_Click(object sender, EventArgs e)//ara
        {
            string ara = TB1.Text;
            for (int s = 0; s <= LV.Items.Count - 1; s++)
            {
                if (LV.Items[s].SubItems[0].Text == ara)
                {
                    j = s;
                    yazilerigeri();
                    return;
                }
            }
            {
                MessageBox.Show("Yok");
            }
            TB1.Focus();
        }
        private void b17_Click(object sender, EventArgs e)//sil
        {
            //kayıt kalmayınca buton pasif olacak!!
            if (LV.SelectedItems.Count > 0)
            {
                LV.SelectedItems[0].Remove();
                i--; j = i;
                yazilerigeri();
            }
            else
            {
                MessageBox.Show("Önce silinecek elemanı seçiniz");
            }
        }
		 private void b19_Click(object sender, EventArgs e)//kapat
        {
            this.Close();
        }
    }
}
