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
    public partial class FrmOdemeler : Form
    {
        public FrmOdemeler()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim(); // kendi oluşturduğumuz SqlSınfı bağlantısı aracılığıyla bu şekilde basitleştirdik nesneyi kullanıp tüm bağlantıları sağlıyoruz 
        BindingSource bs = new BindingSource();
        private void FrmOdemeler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet2.Borclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.borclarTableAdapter.Fill(this.yurtOtomasyonuDataSet2.Borclar);
            bs.DataSource = yurtOtomasyonuDataSet2.Borclar;
            dataGridView1.DataSource = bs;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // bu kod sayesinde formda seçtiğimiz bi satır sütun veya tıklafığımız herhangi bir şey hazırladığımız olan textBoxlara doldurulması içindir böylece her şeyi orda düzenli görmüş olacaz 
            int secilen;
            string id, ad, soyad, kalan;
            secilen = dataGridView1.SelectedCells[0].RowIndex; // seçilen değeri hafızaya attık seçtğimiz satır falan daha sonra aşağıdaki kodda ise bunların her birini hücresine göre alacaz 
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // seçilen değerin 0.hücre indeksini id numarasına ata bu da tablodaki id sütununa denk geliyor
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // seçilen değerin 1.hücre indeksini ad stringine ata bu da seçilen 1.hücre demek tablodaki ad kısmına denk gelen sütun demek 
            soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // burdaki mantık yukarıdaki ile aynı 
            kalan = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            TxtAd.Text = ad; // burda ise ad değişkenine atadığımız değeri textBoxlara yazdırmak için böylece seçmiş olduğumuz öğrencinin tüm özelliklerini tek bir tabloda görebiliriz yerine göre 
            TxtSoyad.Text = soyad; // yaptığımız olay basit aslında seçtiğimiz yerleri boş olan yerlere doldurmak setiklerimizi getirmek aslında 
            TxtKalan.Text = kalan;
            TxtOgrid.Text = id;


        }

        private void BtnOdeme_Click(object sender, EventArgs e)
        {
            try
            {
                // ödenen tutarı kalan tutardan düşme işlemi için gerekli olan kod 
                int odenen, kalan, yeniborc;
                odenen = Convert.ToInt16(TxtOdenen.Text);
                kalan = Convert.ToInt16(TxtKalan.Text);
                yeniborc = kalan - odenen;
                TxtKalan.Text = yeniborc.ToString();
                if (yeniborc >= 0)
                {
                    // yeni tutarı veritabanına kaydetme yani borcu güncellştirme işlemi 
                    SqlCommand komut = new SqlCommand("update Borclar set OgrKalanBorc=@p1 where Ogrid=@p2", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p2", TxtOgrid.Text); // bu şekilde diyor ki TxtOgrid kısmında yazanı parametre p2 ile id hangisi ise o öğrenci ile ilgili işlem yapılacağını tanı ve tabloda atama yap 
                    komut.Parameters.AddWithValue("@p1", TxtKalan.Text); // daha sonra parametre 1 ile borc ödendikten sonra TxtKalan textinde yazan kalan borcu bu parametre ile tabloya OgrKalanBorc tarafına yaz 
                    komut.ExecuteNonQuery(); // yapılan değişiklikleri kaydedip yapılan son işlemi yani en güncekl borç durumunu kaydetmiş olduk 
                    bgl.baglanti().Close();
                    MessageBox.Show("Borç Ödendi "); //ödeme tamamlandığı zaman kaydetsin diye 
                    this.borclarTableAdapter.Fill(this.yurtOtomasyonuDataSet2.Borclar); // yenileme komutu olduğundan bu bize tablodaki verileri getiriyor bunub tekrar ekledikki yapılan değişiklikler hemen form ekranında olup görülebilsin 


                    // kasa tablosuna ekleme yapmak için gerekli kod 
                    // gider iststistikleri için yapılan ödemeleri ay bilgisi ile beraber kasa tablosuna eklemek için yazdığımız kod 
                    // form sisteminde girilen değerlerden ay ve ödenen miktar bizim için önemli olduğundan bu veri girişlerini kasa tablosuna aktardık 
                    SqlCommand komut2 = new SqlCommand("insert into Kasa (OdemeAy,OdemeMiktar) values (@k1,@k2)", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@k1", TxtOdenenAy.Text);
                    komut2.Parameters.AddWithValue("@k2", TxtOdenen.Text);
                    komut2.ExecuteNonQuery(); // yapılanları kaydeden komut 
                    bgl.baglanti().Close();

                }
                else if (odenen > yeniborc)
                {
                    TxtKalan.Text = kalan.ToString();
                    MessageBox.Show("kalan borç miktarindan fazla girdiniz tutarı yeniden giriniz.");
                    TxtOdenen.Clear();

                }
                else
                {
                    SqlCommand borcluöğrencisil = new SqlCommand("delete from Borclar where Ogrİd=@n1", bgl.baglanti());
                    borcluöğrencisil.Parameters.AddWithValue("@n1", TxtOgrid.Text);
                    borcluöğrencisil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    this.borclarTableAdapter.Fill(this.yurtOtomasyonuDataSet2.Borclar); // güncel halini getir 
                    // önceki öğrencinin bilgileri ile doldurulduğu içinde orayı sil diyorum 
                    TxtOgrid.Clear();
                    TxtAd.Clear();
                    TxtSoyad.Clear();
                    TxtOdenen.Clear();
                    TxtKalan.Clear();
                    TxtOdenenAy.Clear();
                    TxtOgrid.Focus();


                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hata,tekrar deneyiniz ");
            }
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            // Arama metni ile DataGridView'i filtreleyelim
            string aramaMetni = TxtArama.Text.ToLower(); // Küçük harfe çevirerek büyük/küçük harf farkını kaldırıyoruz
            bs.Filter = $"OgrAd LIKE '%{aramaMetni}%' OR OgrSoyad LIKE '%{aramaMetni}%' OR CONVERT (Ogrid, 'System.String') LIKE '%{aramaMetni}%' OR CONVERT(OgrKalanBorc, 'System.String') LIKE '%{aramaMetni}%' ";
        }
    }
}
