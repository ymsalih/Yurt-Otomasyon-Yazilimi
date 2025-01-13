using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YurtKayitSistemi
{
    public partial class FrmOgrDuzenle : Form
    {
        public FrmOgrDuzenle()
        {
            InitializeComponent();
        }
        public string id, ad, soyad, TC, telefon, dogum, bölüm, mail, odano, veliadsoyad, velitelefon, veliadres; // başka formlarda kullanılsın diye 

        private void button1_Click(object sender, EventArgs e)
        {
            //Öğrenci Silme İşlemi 
            try
            {
                SqlCommand komutsil = new SqlCommand("delete from Ogrenci where Ogrid=@k1", bgl.baglanti());
                komutsil.Parameters.AddWithValue("@k1", TxtOgrid.Text);
                komutsil.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Silindi. ");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,öğrenci silinemedi.");
            }

            // Odanın aktif öğrenci sayısını azaltma böylece oda kontejanı artmış oluyor 
            SqlCommand komutoda = new SqlCommand("update Odalar set OdaAktif=OdaAktif-1 where OdaNo=@oda", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda", CmbOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
            // böylece öğrenci kaydı silindiğinde öğrencinin kaldığı odada 1 kişilik koontejan açılacak yani odada kalan kişi sayısı 1 azalacak 

            SqlCommand komutborcsil = new SqlCommand("delete from Borclar where Ogrİd=@b1 ",bgl.baglanti());
            komutborcsil.Parameters.AddWithValue("@b1", TxtOgrid.Text);
            komutborcsil.ExecuteNonQuery();
            bgl.baglanti().Close();
        }


        SqlBaglantim bgl = new SqlBaglantim();
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // bu kod satırı sayesinde öğrenci bilgilerinde yapılan değişiklikleri güncelleyebiliriz 
                SqlCommand komut = new SqlCommand("update Ogrenci set OgrAd=@p2,OgrSoyad=@p3,OgrTC=@p4,OgrTelefon=@p5,OgrDogum=@p6,OgrBolum=@p7,OgrMail=@p8,OgrOdaNo=@p9,OgrVeliAdSoyad=@p10,OgrVeliTelefon=@p11,OgrVeliAdres=@p12 where Ogrid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtOgrid.Text); // benim TxtOgrİd girdiğim değeri parametre1(@p1) aracılığıyla o da Ogridyi tuttuğu için onun yerini güncelleyip yeni atadığım değeri kaydediyor 
                komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
                komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
                komut.Parameters.AddWithValue("@p4", MskTC.Text);
                komut.Parameters.AddWithValue("@p5", MskOgrTelefon.Text);
                komut.Parameters.AddWithValue("@p6", MskDogum.Text);
                komut.Parameters.AddWithValue("@p7", CmbBolum.Text);
                komut.Parameters.AddWithValue("@p8", TxtMail.Text);
                komut.Parameters.AddWithValue("@p9", CmbOdaNo.Text);
                komut.Parameters.AddWithValue("@p10", TxtVeliAdSoyad.Text);
                komut.Parameters.AddWithValue("@p11", MskVeliTelefon.Text);
                komut.Parameters.AddWithValue("@p12", RchAdres.Text);
                komut.ExecuteNonQuery(); // yapılan değişiklikleri kaydediyor 
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Bilgileri Güncellendi. ");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,Kontrol Edip Tekrar Deneyiniz!");
            }
        }

        private void FrmOgrDuzenle_Load(object sender, EventArgs e)
        {
            // burdaki amaç FrmOgrListe formundaki her bir öğrenciye tıklandığında öğrencinin bilgilerini görmek için yeni açılacak olan FrmOgrDuzenle formunda bilgileri yerine yerleştirmek amaç bu 
            TxtOgrid.Text = id; // burda da OgrListe formunda atadığımız bu string değişkenleri de burda yerine yazmış gibi oluyoruz 
            TxtOgrAd.Text = ad; // tüm bu ad soyad tc atamalarını falan fdiğer FrmOgrListe.cs de yapmış oluyoruz çünkü ordaki bilgiler hazır zaten onları atamış oluyoruz 
            TxtOgrSoyad.Text = soyad;
            MskTC.Text = TC;
            MskOgrTelefon.Text = telefon;
            MskDogum.Text = dogum;
            CmbBolum.Text = bölüm;
            TxtMail.Text = mail;
            CmbOdaNo.Text = odano;
            TxtVeliAdSoyad.Text = veliadsoyad;
            MskVeliTelefon.Text = velitelefon;
            RchAdres.Text = veliadres;
            // böylece FrmOgrListe.cs de basılan satırın değerlerini okuyup her birini ait olduğu özelliğe atadıktan sonra buradaki form üzerine yerleştirip bir öğrencinin nilgilerini doldurmuş oluyoruz 

        }
    }
}
