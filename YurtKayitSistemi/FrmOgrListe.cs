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
    public partial class FrmOgrListe : Form
    {
        public FrmOgrListe()
        {
            InitializeComponent();
        }

        private void FrmOgrListe_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet3.Ogrenci' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ogrenciTableAdapter.Fill(this.yurtOtomasyonuDataSet3.Ogrenci);
            // DataGridView'e BindingSource bağlanıyor
            // Arama Yapmak için gerekli kod satırı 
            bs.DataSource = yurtOtomasyonuDataSet3.Ogrenci; 
            dataGridView1.DataSource = bs;
        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // bu kod herhangi bir hücreye tıklandığında yapılacak olan olaylar için 
        {
            // bu dataGridWieve yüklenilen her bir veriyi tıklandığı satıra göre çekmek için ve çekip başka bir eyre aktarmak için 
            secilen = dataGridView1.SelectedCells[0].RowIndex; // yani ben hangi hücreyi seçmişsem o seilen hücreyi hafızaya alsın bunu yazdıktan sonra diğer mantık belli zaten 
            FrmOgrDuzenle fr = new FrmOgrDuzenle(); // ogr duzenle formundan bir nesne oluşturduk 
            fr.id = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); //  bu sefer seçtiğimizi başka bir forma göndermemiz lazım 
            fr.ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // burdaki amaç bahsettiğimiz konumdaki veriyi fr.ad kısmına atmak böylece bilgiyi oraya aktarıyoruz ve kullanabiliyoruz 
            fr.soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            fr.TC = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            fr.telefon = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            fr.dogum = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            fr.bölüm = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            fr.mail = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            fr.odano = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            fr.veliadsoyad = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            fr.velitelefon = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            fr.veliadres = dataGridView1.Rows[secilen].Cells[11].Value.ToString(); // burda her bir değeri diğer formdaki public yaptığımız değişkenlere atıyoruz ki diğer formda da bu verileri yerleştirebilelim 
            fr.Show(); // bu yeni formu göster demek sınıftan oluşturduğumuz nesne ile işlem yaparız her zaman 
                       // burda yaptığımız olay FrmOgrListe formunda geen öğrencilerden seçtiğin satırdan yani seçtiğin öğrenciye tıkladığımızda o öğrencinin bilgilerini güncellemek için OgrDuzenle formu açılıp üzerinde işlem yapılabilir 
                       // show göster demek zaten 
        }

        SqlBaglantim bgl = new SqlBaglantim();
        BindingSource bs = new BindingSource(); // datagridview bağlayarak veritabanı tablosu içine erişmek için kulanıyoruz 
        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            // Arama metni ile DataGridView'i filtreleyelim
            string aramaMetni = TxtArama.Text.ToLower(); // Küçük harfe çevirerek büyük/küçük harf farkını kaldırıyoruz
            bs.Filter = $" CONVERT (Ogrid, 'System.String') LIKE '%{aramaMetni}%' OR OgrAd LIKE '%{aramaMetni}%' OR OgrSoyad LIKE '%{aramaMetni}%' OR OgrTC LIKE '%{aramaMetni}%' OR OgrBolum LIKE '%{aramaMetni}%' OR OgrMail LIKE '%{aramaMetni}%' OR OgrOdaNo LIKE '%{aramaMetni}%' OR OgrVeliAdres LIKE '%{aramaMetni}%' OR OgrVeliAdSoyad LIKE '%{aramaMetni}%'";

        }
    }
}
