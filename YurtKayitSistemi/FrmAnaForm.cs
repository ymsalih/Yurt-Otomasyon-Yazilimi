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
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
           
        }
        
        // bu kod sayfasında ise yönetici girdiği zaman ana menüye gelecek ve o ana menü üzerinde menustrip üzerinde oluşturduğumuz yerlere tıklandığında gerekli formu açması için gerekli kodları yazıyoruz 
        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet1.Ogrenci' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ogrenciTableAdapter.Fill(this.yurtOtomasyonuDataSet1.Ogrenci);
            timer1.Start(); // form yüklendiği zaman zamanlayıcı çalışsın 

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString(); // LABEL1 e uzun tarih kısmını çek 
            label2.Text = DateTime.Now.ToLongTimeString(); // label2 e uzun saat kısmını çek 
        }

        private void hesapMakinesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe"); 
            // bu kod satırı bilgisayardaki exe uygulamalarını çalıştırmak için kullanılıyor 
            // böylece erişim kolaylığı kısmından hesap makinasına tıkladığımız zaman bilgisayarda yüklü olan hesap makinasını açacaktır
            // tanımladığım sistem işlemini başlat gibi bir şey bu kod 
 }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MsPaint.exe");
            // böylece erişim kolaylığı kısmındaki painte tıkladığımız zaman paint açılacak 
            // bu kod bilgisayardaki exe uygulamalarını çalıştırmamıza yardımcı olur 

        }

        private void öğrenciEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ana Form üzerinde öğrenci ekle kısmına tıklandığı zaman öğrenci kayıt formunun açılması için bu şekilde yazdık 
            FrmOgrKayıt fr = new FrmOgrKayıt();
            fr.Show(); // istediğimiz formdan oluşturduğumuz nesne aracılığyla show deyip öğrenci ekle kısmına tıklanmdığı zaman öğrenci kayıt formu açılmasını sağladık 
        }

        private void öğrenciListessiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOgrListe fr = new FrmOgrListe();
            fr.Show(); // ana sayfadan öğrenci listesine tıkladığı zaman öğrenci listesinin gösterildiği formu açacak 
        }

        private void öğrenciDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOgrListe fr = new FrmOgrListe();
            fr.Show(); // mantık belli 
        }

        private void bölümEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBolumler fr = new FrmBolumler();
            fr.Show();
        }

        private void bölümDüzenlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBolumler fr = new FrmBolumler();
            fr.Show();
        }

        private void ödemeAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOdemeler fr = new FrmOdemeler();
            fr.Show();
        }

        private void giderEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGider fr = new FrmGider();
            fr.Show();
        }

        private void giderListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiderListesi fr = new FrmGiderListesi();
            fr.Show(); // gider listesine tıkladığın zaman açılan forma tıklandığında onu da gider güncellemeye bağladığımızdan gider güncellemeyi de orda yapabiliyoruz tekrar gerek yok 
        }

        private void giderGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGiderGuncelle fr = new FrmGiderGuncelle();
            fr.Show(); // bunu da ek olarak ekledim olur da anlaşılmaz diye 
        }

        private void gelirİstatistikleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGelirİstatistik fr = new FrmGelirİstatistik();
            fr.Show();
        }

        private void şifreİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYoneticiDuzenle fr = new FrmYoneticiDuzenle();
            fr.Show();
        }

        private void personelDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPersonel fr = new FrmPersonel();
            fr.Show();
        }

        private void notEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNotEkle fr = new FrmNotEkle();
            fr.Show();
        }

        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu program Yurt Otomasyon Sistemi olup Muhammed Salih Atiç tarafından 29 Aralık 2024 de tamamlanmıştır. ","Öğrenci Yurt Otomasyonu",MessageBoxButtons.OK,MessageBoxIcon.Information); // ınformation tarafı üst panelde verdiğin bilgidir ok butonu da ekledik daha iyi görünsün diye 
        }

        //private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    Application.Exit(); // uygulamadan çıkış için 
        //}

        private void yemekListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYemekListesi fr = new FrmYemekListesi();
            fr.Show();
        }

        private void menüEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYemekEkle fr = new FrmYemekEkle();
            fr.Show();
        }

        private void yemekGüncellemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYemekListesi fr = new FrmYemekListesi();
            fr.Show();
        }

        private void mailGöndermeEkranıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMailGönder fr = new FrmMailGönder();
            fr.Show();
        }

        private void FrmAnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // sistem admingiriş formundan başlatıldığı için anaform üzerinden kapatma butonuna tıkladığın zaman uygulama kapanmıyordu kapanması için ekledik 
        }

        private void OturumuKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAdminGiris fr = new FrmAdminGiris();
            fr.Show();
        }
    }
}
