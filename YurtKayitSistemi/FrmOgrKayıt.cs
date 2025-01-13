using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // böylece veritabanına ait tüm şeyleri kullanabiliriz 

namespace YurtKayitSistemi
{
    public partial class FrmOgrKayıt : Form
    {
        public FrmOgrKayıt()
        {
            InitializeComponent();
        }
        // SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-29179S9D\SQLEXPRESS;Initial Catalog=YurtOtomasyonu;Integrated Security=True ");  bu SqlBaglantım sınıfını oluşturmadan önce kullandığım koddu 
        // ilk VeriTabnı bağlantısında bağlantı yolu adresi gösteriliyor ordan aldık böylece bağlantı sağlanacaktır 

        SqlBaglantim bgl = new SqlBaglantim();

        private void FrmOgrKayıt_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet8.Odalar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.odalarTableAdapter.Fill(this.yurtOtomasyonuDataSet8.Odalar);
            // bölümler tablosundaki bölümleri ComboBox a çekmek için 

            // baglanti.Open(); // bağlantıyı sağlamış olduk bu SqlBaglantım sınıfını oluşturduğumuz zaman zaten bağlantıyı sağladığımız için buna gerek kalmadı 

            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler ", bgl.baglanti()); // bağlantı adresi aracılığıyla Bolumler tablosundan BolumAd kısmını çek diyoruz baglantıda veritabanı var çünkü 

            SqlDataReader oku = komut.ExecuteReader(); // böylece okuma işlemini gerçekleştiriyor

            while (oku.Read()) // bu okuma işlemi gerçekleştikçe devam edecek son bulduğunda duracak 
            {
                CmbBolum.Items.Add(oku[0].ToString()); // CmdBolum ComboBox kısmına okudan gelen 0.paramatereyi Items yani öğeler kısmına ekle demek 0.paramametrede sorgu içindeki alanım oluyor sadece bir alan olduğüu için ve programlamada indeks 0 dan başladığı için 0 yazdık eğer ki birden çok bölüm(BolumAd) olsaydı indeks değerlerine göre çağıracaktık
            }
            bgl.baglanti().Close(); // böylece veriTabanından bölümleri çekmiş olduk 


            // boş odaları  Listeleme komutları 

           

            SqlCommand komut2 = new SqlCommand("Select OdaNo From Odalar where OdaKapasite!=OdaAktif", bgl.baglanti()); // böylece odano su odaaktife eşit ise bu oda dolu analamına geliyor ve biz de bu komutla odano ile odaaktif aynı olmayanları getir diyoruz where aslında ara demek select ise bunları getir demek 

            SqlDataReader oku2 = komut2.ExecuteReader();

            while (oku2.Read()) // bu kommut çalıştığı müddetçe döngü devam edecek 
            {
                CmbOdaNo.Items.Add(oku2[0].ToString()); // tek bir şey yani sadece odaNo çektiğimiz için 0.parametre kullandık ek olarak başka birşey daha çekseydik bu sefeer farklı bir parametre daha kullanacaktık 
            }
            bgl.baglanti().Close();
            
            //bu komut ile sadece boş olan odaları getirmiş olduk

        }

        private void BtnKaydet_Click(object sender, EventArgs e) // butona tıklandığında kaydedilmesi için gerekli kod sütunu 
        {
            // öğrenci bilgilerinin kayıt edilme komutları 
            try
            {
             

                SqlCommand komutkaydet = new SqlCommand("insert into Ogrenci (OgrAd,OgrSoyad,OgrTC,OgrTelefon,OgrDogum,OgrBolum,OgrMail,OgrOdaNo,OgrVeliAdSoyad,OgrVeliTelefon,OgrVeliAdres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti()); // BU KOD ÖĞRENCİ TABLOSUNA KAYIT EDECEĞİMİZ ŞEYLERDİR @ bunlar ise parametre olup her biri bir özelliği tutumuş olacak ve komut olan ise @ işaretidir 

                komutkaydet.Parameters.AddWithValue("@p1", TxtOgrAd.Text); // parametrelerin geleceği yerleri belirliyoruz
                komutkaydet.Parameters.AddWithValue("@p2", TxtOgrSoyad.Text); // misal burda öğrenci soyad textbox ına yazılanı parametre 2 ye yani öğrenci soyad kısmına tabloda yaz diyoruz yani ekliyor 
                komutkaydet.Parameters.AddWithValue("@p3", MskTC.Text);
                komutkaydet.Parameters.AddWithValue("@p4", MskOgrTelefon.Text);
                komutkaydet.Parameters.AddWithValue("@p5", MskDogum.Text);
                komutkaydet.Parameters.AddWithValue("@p6", CmbBolum.Text);
                komutkaydet.Parameters.AddWithValue("@P7", TxtMail.Text);
                komutkaydet.Parameters.AddWithValue("@P8", CmbOdaNo.Text);
                komutkaydet.Parameters.AddWithValue("@p9", TxtVeliAdSoyad.Text);
                komutkaydet.Parameters.AddWithValue("@p10", MskVeliTelefon.Text);
                komutkaydet.Parameters.AddWithValue("@p11", RchAdres.Text);
                komutkaydet.ExecuteNonQuery(); // bu değişiklikleri kaydet sorgular üzerindeki değişiklikleri gerçekleştiriyor 

                bgl.baglanti().Close(); // bu kapatmak için kullanı şekli belli zaten sınıftan oluşturduğun nesne. sınıftaki bağlantı methodu.close şeklinde 

                MessageBox.Show("Kayıt Başaerılı Bir Şekilde Eklendi");





                // öğrenci id labele çekme ve label visible özelliğini false yaptık ki form çalıştığında gözükmeyip arka planda id çalışsın 

                SqlCommand komut = new SqlCommand("select Ogrid from Ogrenci", bgl.baglanti());

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    label12.Text = oku[0].ToString(); // bunun amacı kaydettiğimiz zaman o öğrencinin kaçıncı id olduğunu bulmak ve ona göre borç yazma işlemi 

                }
                bgl.baglanti().Close();

                // Öğrenci Borç Alanı Oluşturma her yeni öğrenci için tanımlanmış otomatik atanan borç 
                // burda id özelliğini label12 den çektik ki hangi öğrenciye ait borç olduğu bilinsin diye ve böylece özelliğini de gizli yaptık ki görünmesin ekranda 
                SqlCommand komutkaydet2 = new SqlCommand("insert into Borclar (Ogrid,OgrAd,OgrSoyad) values (@b1,@b2,@b3)", bgl.baglanti());
                komutkaydet2.Parameters.AddWithValue("@b1", label12.Text); // kaydetme sırasındaki öğrenci id label12 ye atadığımız için bunu da borçlar tablosuna aynı öğrenci id ile kaydedelim ki karışıklık olmasın 
                komutkaydet2.Parameters.AddWithValue("@b2", TxtOgrAd.Text);
                komutkaydet2.Parameters.AddWithValue("@b3",TxtOgrSoyad.Text);
                komutkaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();
            } 
            catch (Exception)
            {

                MessageBox.Show("Hata Lütfen Yeniden Deneyiniz "); // böylece bir hata durumunda ekrana bir mesaj verecektir 
            }
            // burda ogrenci tablosundaki her bir özelliğe kullanıcının girmiş olduğu değerleri atadık burda önemli olan parametreler ve her bir verinin eşleşmiş olup doğru parametreyi doğru yere atadığın 
            // her işlemden sonra veriTabanı bağlantısını açıp kapatıyoruz 
            // try catch kullanarak herhangi bir hata durumunda programın kapanması yerine uyarı verip devame etmesini sağladık 


            // ÖĞRENCİ ODA KONTEJANI ARTTIRMA (ÖĞRENCİNİN O ODAYA KAYDI YAPILDIĞINDA O KONTEJANIN AZALMASI İÇİN)
            SqlCommand komutoda = new SqlCommand("update Odalar set OdaAktif=OdaAktif+1 where OdaNo=@oda1", bgl.baglanti());
            komutoda.Parameters.AddWithValue("@oda1", CmbOdaNo.Text);
            komutoda.ExecuteNonQuery();
            bgl.baglanti().Close();
            // amaç bu öğrenci kaydederken seçtiğim odanın aktif öğrenci sayısını 1 arttırsın ki hangi oda boş hangisi dolu görelim 
            // böylece her öğrenci kaydettiğimiz zaman o odadaki aktif öğrenci sayısını 1 artttıracak böylece bir dahaki kayıtta dolan odaları getirmeyecek 
            // denediğin zaman veritabanında da net şekilde görünüyor yapılan değişiklik 
            this.odalarTableAdapter.Fill(this.yurtOtomasyonuDataSet8.Odalar);
            this.Close();
          
        }
    }
}
// veriTabanı dosya bağlantısı yolu bu bizim için çok önemli yoksa veriTabanından bişey çekemeyiz 
// Data Source=LAPTOP-29179S9D\SQLEXPRESS;Initial Catalog=YurtOtomasyonu;Integrated Security=True 
