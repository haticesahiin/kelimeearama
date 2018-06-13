using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muhendislikprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[] dizi = new int[203];
        string[] kelimeler = new String[203];//istenilen formata uygun olması icin
        private void button1_Click(object sender, EventArgs e)
        { 
            //dosya secme
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Metin dosyaları|*.txt|" + "Bütün dosyalar|*.*";
            openFileDialog1.Title = "Açılacak dosyayı seçiniz";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya_adi;
                dosya_adi = openFileDialog1.FileName;
                System.IO.TextReader dosya = System.IO.File.OpenText(dosya_adi);
                string x;
                x = dosya.ReadLine();
                //satır satır okuma islemi
                while (x != null)
                {
                    x = x.ToLower();// kelimeyi kucuk harfe cevirdim
                    char[] ayir = x.ToCharArray();//kelimeyi harflere ayirdim
                    int toplam = 0;
                    //agirlik hesabi 
                    for (int i = 0; i < ayir.Length; i++)
                    {
                        toplam += (int)ayir[i] * (i + 1)* (i + 1);//harfin ascıı karsiligi ile indisinin karesini carptim
                    }
                    int agirlik = toplam;
                    toplam = toplam % 203;
                    if (dizi[toplam] <= 0)//o indisdeki deger bos mu diye baktim.Bos ise;
                    {
                        dizi[toplam] = agirlik;
                        kelimeler[toplam] = x;
                    }
                    else//bos degil ise ;
                    {
                        int i = 0;
                        int toplam1 = toplam;
                        while (dizi[toplam1] > 0)
                        {
                            toplam1 = ((i * i) + toplam) % 203;
                            i++;
                        }
                        dizi[toplam1] = agirlik;
                        kelimeler[toplam1] = x;
                    }


                    x = dosya.ReadLine();
                }
                dosya.Close();

                int j = 0;
                //listboxa yazdirdim
                foreach (var item in dizi)
                {
                    listBox1.Items.Add(j + "  -  " + item + "  -  " + kelimeler[j]);
                    j++;

                }
            }


        }
        Boolean kelimeAra(String kelime)
        {
            kelime = kelime.ToLower();//kucuk harflere donusturdum
            char[] bol = kelime.ToCharArray();//harflere boldum
            int agirlik = 0;
            for (int i = 0; i < bol.Length; i++)//agirlik hesabi 
            {
                agirlik += (int)bol[i] * (i + 1) * (i + 1);//harfin ascıı karsiligi ile indisinin karesini carptim

            }
            int index = agirlik % 203;
            if (dizi[index] == agirlik)//indexteki agirlik onceki agirligimize esit diye baktim 
            {
                return true;

            }
            else
            {

                for (int i = 0; i < 9; i++)//kareleri toplami 203 e en yakin olan sayi 8 dir.O yuzden 9 a kadar dondurduk.
                {
                    index += i * i;
                    index = index % 203;
                    if (dizi[index] == agirlik)
                    {
                        return true;
                    }
                }
                return false;
            }



        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                if (kelimeAra(textBox1.Text))
                {
                    MessageBox.Show("Girdiğiniz " + textBox1.Text.ToLower() + " kelimesi metin dosyasında bulunmaktadır ");

                }
                else
                {
                    //kelime bulunamadiysa 1 harf eksik olan var mi diye baktim
                    char[] harf = textBox1.Text.ToLower().ToCharArray();
                    String bulunan = "";
                    for (int i = 0; i < harf.Length; i++)
                    {
                        String birlestir = "";
                        for (int j = 0; j < harf.Length; j++)
                        {
                            if (i != j)
                            {
                                birlestir += harf[j];
                            }
                        }
                        if (kelimeAra(birlestir))
                        {
                            bulunan += birlestir + " , ";
                        }
                    }


                    //yanindakiyle yer degitirilmis hali varmi diye baktim
                    for (int i = 0; i < harf.Length - 1; i++)//ikiserli ikiserli degisacegi icin (harf.Length - 1) dedim
                    {
                        String yerdegistir = "";
                        for (int j = 0; j < i; j++)
                        {
                            yerdegistir += harf[j];

                        }
                        yerdegistir += harf[i + 1];
                        yerdegistir += harf[i];
                        for (int k = i + 2; k < harf.Length; k++)
                        {
                            yerdegistir += harf[k];
                        }
                        if (kelimeAra(yerdegistir))
                        {
                            bulunan += yerdegistir + " , ";
                        }
                    }
                    //sonucu verdim
                    if (bulunan != "")
                    {
                        MessageBox.Show("Girdiğiniz " + textBox1.Text.ToLower() + " kelimesi metin dosyasında " + bulunan + " olarak bulunmuştur");
                    }
                    else
                    {
                        MessageBox.Show("Girdiğiniz " + textBox1.Text.ToLower() + " kelimesi bulunamadı");
                    }


                }


            }
        }
    }
}
