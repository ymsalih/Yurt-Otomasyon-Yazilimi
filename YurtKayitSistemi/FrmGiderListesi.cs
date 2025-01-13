using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YurtKayitSistemi
{
    public partial class FrmGiderListesi : Form
    {
        public FrmGiderListesi()
        {
            InitializeComponent();
        }

        private void FrmGiderListesi_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet4.Giderler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.giderlerTableAdapter.Fill(this.yurtOtomasyonuDataSet4.Giderler);
            // burda veritabanındaki bilgileri tabloya çektiğimizi gösteren kod bu kodu aynı zamanda yapılan değişikliklerden sonra tekrar güncel halini göstermek için tekrar tekrar kullanıyoruz 
        }
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // böylece formlar arası işlem yapabiliyoruz yani bu formdaki bilgileri diğer formda(FrmGiderGüncelle) hazırladığımız formdaki text yerlerine yerleştiriyoruz 
            // bu da bizim tablodaki verileri diğer formda uzun uzun çekmek yerine daha kolay şekilde çekmemizi sağlıyor
            int secilen;
            FrmGiderGuncelle fr = new FrmGiderGuncelle(); // burda tıklanan bilgileri diğer forma atmak için geekli kod satırı 
            secilen = dataGridView1.SelectedCells[0].RowIndex; // burda tıkladığımız yerin satır ve hücrelerini tutuyor yani tıkladığımız yeri hafızaya atıyor 
            fr.id = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // burda tıkladığımız yerin secilen satırın 0.hücre indeksindeki değerin string şeklini fr.id at diyoruz bu da frmGiderGüncelle de oluşturduğumuz id olup diğer formda id yerini doldurmuş oluyoryuz 
            fr.elektrik = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // diğer formda da bu değişkenlere text kısmını atadığımız için otomatik olarak listedeki cverileri diğer forma doldurmuş oluyoruz 
            fr.su = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            fr.dogalgaz = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            fr.internet = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            fr.gıda = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            fr.personel = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            fr.diger = dataGridView1.Rows[secilen].Cells[7].Value.ToString(); // böylece diğer forma atama yapmış oluyoruz 
            fr.Show(); // bu kodun amacı da o sayfadaki verileri görüntülemek için yani gider tablosuna tıkladığımız zaman diğer formun açılması için 
            // yani sadece Fr.Show yazsakta boş bi form görünecek tıkladığımız zaman atamalar olmadan zaten CellClick herhangi bir hücreye tıklandığı zaman açılacak olan şeydir 
            
        }
    }
}
