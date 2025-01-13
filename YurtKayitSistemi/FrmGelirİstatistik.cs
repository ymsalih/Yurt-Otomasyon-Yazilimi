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
    public partial class FrmGelirİstatistik : Form
    {
        public FrmGelirİstatistik()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();

        // yapmamız gereken şey kasa tablosundaki tüm miktarları toplamak 
        private void FrmGelirİstatistik_Load(object sender, EventArgs e)
        {
            // sum komutu ingilizce toplamdan geliyor bu komut sayesinde tablodaki OdemeMiktardaki verileri toplayabilecez
            SqlCommand komut = new SqlCommand("Select Sum(OdemeMiktar) from Kasa",bgl.baglanti()); // Sum komutu bize topklamı vermesi için kullandık 
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read()){
                lblPara.Text = oku[0].ToString() +" TL "; // 0.parametre burda sadece odememiktar kısmını çekeceğimiz için onu temsil ediyor bi başka tablo değişkenini daha çekmiş olsaydık o zaman daha sonraki parametreyi kullanacaktık
            }
            bgl.baglanti().Close();


           

            // Tekrarsız olacak şekilde Ayları Listeleme Hani aynı ayı tekrar Aylar listesinde listelemesin görünmesin diye 
            SqlCommand komut2 = new SqlCommand("Select distinct (OdemeAy) from Kasa ", bgl.baglanti()); // distinc mantığı tekrarlı olan şeyleri getirmesin diye anlamsız olur zaten 
            SqlDataReader oku2 = komut2.ExecuteReader(); // bu kod ile komut2 nin seçtiklerini oku demek 
            while (oku2.Read()) // okuma işlemi devam edene kadar döner 
            {
                CmbAy.Items.Add(oku2[0].ToString()); // CmbAy combobox kısmına tekrarsız olacak şekilde aylar okunana kadar devam edip combobox yerine string şeklinde ekle 
            }
            bgl.baglanti().Close();
            // böylece kasadaki ödeme yapılan ayları ComboBax text kısmına çekmiş olduk tekrarsız olması önemliydi çünkü aynı aylarda yapılan ödemeler olduğunda aynı ay ismi birden çok geceleği için saçma olacaktı 
            // burdaki parametrelerin her biri bir özellik için daha fazla özellik olsa o zaman diğer parametreleri de kullanmış olacaktık 




            //AYLIK KAZANÇ MANUEL
            //this.chart1.Series["Aylık"].Points.AddXY("Nisan",15); // BU ŞEKİLDE OLDUMU FORM EKRANINDA GELİR İSTATİSTİĞE TIKLADIĞIMIZ ZAMAN GRAFİKTE X EKSENİ AY KISMI OLUP Y EKSENİ DE RAKAMSAL KARŞILIĞI OLACAK ŞEKİLDE GELİR 
            //this.chart1.Series["Aylık"].Points.AddXY("Mayıs",22); //bu kod sayesinde veri grafiğine değer girişi yapabiliyoruz 
            //this.chart1.Series["Aylık"].Points.AddXY("Haziran",13);

            //Grafiklere veri tabanından veri çekme manuel olarak değil de veri tabanına göre grafik oluşturmak için 
            SqlCommand komut3 = new SqlCommand("select OdemeAy,sum(OdemeMiktar) from Kasa group by OdemeAy", bgl.baglanti());
            // bu kodun açıklaması kasadaki ödememiktarlarını ödenen aya göre gruplandırma yap ve o aydaki toplam ödeme mikatarını topla demek böylece gruplandırma yapıp grafikte yazabiliriz 
            SqlDataReader oku3 = komut3.ExecuteReader();
            while (oku3.Read())
            {
                this.chart1.Series["Aylık"].Points.AddXY(oku3[0],oku3[1]); // iki değer tuttuğu için iki parametre kullandık zaten satır veya sütuna gelmesi gereken parametreleri yukarıda manuel olarak yaptığımızda görmüştük 
            }
            bgl.baglanti().Close();
            //oku3[0]=OdemeAy kısmını tutuyor 
            //oku3[1]=OdemeMiktar kısmını tutuyor bu da 
            // böylece gelir istatistik ksımındaki grafiğiğe verileri eklemiş oluyoruz 

        }

        private void CmbAy_SelectedIndexChanged(object sender, EventArgs e) // seçilen değer değiştiği zaman ne olmasını istiyorsak bu anahtarı kullanıyoruz 
        {
            SqlCommand komut = new SqlCommand("select sum(OdemeMiktar) from Kasa where OdemeAy=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                lblAyKazanc.Text = oku[0].ToString(); // o ay ki yapılan kazancı yazmak için 
            }
            bgl.baglanti().Close();
            // bu şekilde seçilan aya göre kazancı listeleme komutunu yaptık o aydaki yapılan kazancı hesaplayıp veriyor 
            // böylece ay bazında işlem yapmış oluyoruz 
        }
    }
}
