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
    public partial class FrmBolumler : Form
    {
        public FrmBolumler()
        {
            InitializeComponent();
        }
       
        // SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-29179S9D\SQLEXPRESS;Initial Catalog=YurtOtomasyonu;Integrated Security=True "); 
        // ilk VeriTabnı bağlantısında bağlantı yolu adresi gösteriliyor ordan aldık böylece bağlantı sağlanacaktır 
        // bunu yorum satırına aldım çünkü her seferinde bu şekilde bağlantı sağlamak uğraştırdığı için ek olarak bağlantı için sınıf oluşturup o sınıfın nesnesini kullanıp bağlantıyı sağlayabilirim

        SqlBaglantim bgl = new SqlBaglantim(); // bu şekilde oluşturduğumuz sqlBaglantım sınıfından nesne türeterek nesneyi kullanarak ve burdan oluşturuduğumuz baglanti methodunu kullanarak bağlantıyı sağlayabiliğyoruz 

        private void FrmBolumler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet.Bolumler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.bolumlerTableAdapter.Fill(this.yurtOtomasyonuDataSet.Bolumler); // bu komut bize verileri getiriyor yani bize bölümler tablosundaki bolumAd kısmındaki verileri çekiyor dataGrid ile 
            // datagrid sihirbazını tanımladığımız zaman kendisi otomatik olarak arkaplanda oluşturmuş oluyor 
            // dataGridView bize veritabanında oluşturduğumuz tabloları çekmek için kullanılıyor tabi öncesinde veritabanı bağlantısını bölümler tablosu iğle yapmış oluyorsun 
            // bu işleme zaten data sihirbazı da deniyor 
        }

        private void PcbolumEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // bu şekilde bolumler tablosuna kullanıcının tablo dışında girmiş olduğu bölümü ekleyebiliyoruz 
              
                SqlCommand komut1 = new SqlCommand("insert into Bolumler (BolumAd) values (@p1)", bgl.baglanti()); // insert into eklemek için kullanıyoruz 
                komut1.Parameters.AddWithValue("@p1", TxtBolumAd.Text); // yani kullanıcının giriş olduğu bölüm adını parametre 1 ile BolumAd tablosuna ekle demektir 
                komut1.ExecuteNonQuery(); // değişiklikleri kaydedecek 
                bgl.baglanti().Close(); // bağlantıyı kapatmak için 
                MessageBox.Show("bölüm Eklendi ");
                this.bolumlerTableAdapter.Fill(this.yurtOtomasyonuDataSet.Bolumler); // bu komut bize verileri getirdiği için bu şekilde refresh gibi oluyor yani eklediğimiz bölümde dahil tüm verileri çekiyor kısaca tabloda eklenmiş halini görüyoruz 
                // bölümİd kendisi eklenmiş oluyor zaten sırasıyla artıyor.
                // bölüm ekleme kodu hep birbiri ile bağlantılıdır birbirilerinin devamı gibi 
            }
            catch
            {
                MessageBox.Show("Hata Oluştu Yeniden Deneyin ");
            }
        }

        private void PcbBolumSil_Click(object sender, EventArgs e) // seçilen bölümün silinmesi için geçerli kod satırı 
        {
            try
            {
              
                SqlCommand komut2 = new SqlCommand("delete from Bolumler where Bolumid=@p1", bgl.baglanti()); // aranan ya da özel olarak istediğin şeyi where anahtarı ile arıyoruz 
                komut2.Parameters.AddWithValue("@p1", TxtBolumid.Text); // buradaki atamalara dikkat et ama yanlış atama yaparsan olmaz 
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Silme İşlemi Gerçekleşti ");
                this.bolumlerTableAdapter.Fill(this.yurtOtomasyonuDataSet.Bolumler); // silme işlemi gerçekleştikten sonra da tekrar yenilensin diye çünkü bu kod satırı bize verileri çekmemize yarıyordu 
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,İşlem Gerçekleşmedi ");
            }
        }

        int secilen; // tıkladığım zaman hafızaya aldığı satır değeri ya da hücre değeri 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // CellClick hücreye tıklandığı zaman ne olsun 
            // yani hazır çekilen bölümlere tılandığında hem bölüm ismi hem de id  yukarıdaki textboxlara yazılsın diye bu olayı kullandık.
            // cell hücre demek Rows demek ise satır demek 

            string id, bolumad;
            secilen = dataGridView1.SelectedCells[0].RowIndex; // RowIndex satırın indeksi demek
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // seçilen satırdaki hücrelerin 0. değeri  
            bolumad = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // özelliğini string biçiminde boş olan txt bölümüne yaz 
            // burda verilen parametrelerden 0.hücre aslında Bolumid 1.hücre ise burda BolumAd oluyor.
            // Rows ise seçilen satır indeksi demek eğer ki satırdaki id numarası 12 olsa bile satır indeksi 10 ise burda inkesi 10 olarak alır 12 almaz.
            // selectedCells parametre olarak 0. indeks vermemizin nedeni tek bir satır seçeceğimiz için eğer birden çok farklı tıklanma olsaydı başka bir parametre daha verecektik.

            TxtBolumid.Text = id;
            TxtBolumAd.Text = bolumad;

            // mantık bu değişkenleri belirliyoruz ki onlar üzerinden atama yapabilmemiz için 
            // daha sonra bu değişkenlerden seçilen satırı atamak için seçtiğimiz satır veya hücre olsun onu oluşturduğumuz seçilen değişkenine atyoruz daha sonra bunun üzerinden işlem yapacaz
            // seçilen satırı veya hücreyi belirledikten sonra boş olan kısımlar için oluşturduğumuz değişkenlere atama yapıyoruz 
            // burda id değişkenine seçilen satırdaki 0.hücre değerini yani bu da id kısmı oluyor bu şd kısmını string olarak ata diyoruz 
            // burda bolumad değişkenine ise seçilen satırdaki denk gelen 1.hücredeki değeri string olarak ata demek yani öylece yazıyoruz 
            // daha sonra bu değişenleri txtlere verdiğimiz isimlere eşitledikten yani atama yaptıktan sonrea boşlukları doldurmuş oluyoruz 
            // bu işlem bize okunabilirlik açısından kolaylık sağlıyor 


            // bu şekilde tıkladığımız herhangi bir bölüm satırında hem bölüm id id kısmına hem de bölümadı bölümad kısmına yazılmış oluyor.
            // bölümid enabled özelliğini false yaptık bu şekilde dışarıdan değiştirilmesine yani müdahele edilmesine izin vermedik.
            // enabled yaptığımız için dışarıdan da kullanıcınınn kafasına göre id numarası vermesine izin vermiyoruz id zaten 1 arta arta gidiyor.
        }

        private void PcbBolumDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
              
                SqlCommand komut2 = new SqlCommand("update Bolumler set BolumAd=@p1 where Bolumid=@p2", bgl.baglanti()); // önemli olan bu komut işte 
                komut2.Parameters.AddWithValue("@p2", TxtBolumid.Text);
                komut2.Parameters.AddWithValue("@p1", TxtBolumAd.Text);
                komut2.ExecuteNonQuery(); // yapılan değişiklikleri kaydediyor 
                bgl.baglanti().Close();
                MessageBox.Show("Güncelleme Gerçekleşti ");
                this.bolumlerTableAdapter.Fill(this.yurtOtomasyonuDataSet.Bolumler); // bu da son yapılan değişikliklerin en güncel hali için maksat tablodada gözüksün 
            }
            catch (Exception)
            {
                MessageBox.Show("Hata, Güncellleme Yapılamadı ");
            }

            // böylece bölüm düzenleme güncelleştirme işlemlerini yapmış oluyoruz.
            // tooltip arka planda çalışıp mouse ile öğe üzerine gelindiğinde ekrana gelen mesaj gibi bişey bunu ekleyerek üzerlerine gelindiğinde ne yapılacağı mesajını verir. 
            // tooltip eklendiği gibi hepsine o özellik gelir. 

        }

    }
}
