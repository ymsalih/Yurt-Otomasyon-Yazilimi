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
    public partial class FrmGiderGuncelle : Form
    {
        public FrmGiderGuncelle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();

        public string elektrik, su, dogalgaz, gıda, personel, diger, internet, id;

        private void FrmGiderGuncelle_Load(object sender, EventArgs e)
        {
            TxtElektrik.Text = elektrik;
            TxtGıda.Text = gıda;
            TxtDiger.Text = diger;
            TxtInternet.Text = internet;
            TxtSu.Text = su;
            TxtDagalgaz.Text = dogalgaz;
            TxtPersonel.Text = personel;
            TxtGiderid.Text = id;
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // böylece gider listesine tıkladığımız zaman açılan formda giderleri güncelleyip kaydetmemiz için gerekli olan kodu yazmış olduk 
            // bu kod sayesinde gider güncellemesi yapılabilecek 
            try
            {
                // vermiş olduğumuz komutta giderler tablosundaki her bir veriyi bir parametreye atayıp bunlardaki değişikleri id ye göre bulup o gideri kaydedip her bir parametreye Form üzerinde yazdığımız giderleri atayıp tablo üzerine hepsini kaydetmiş oluyoruz 
                SqlCommand komut = new SqlCommand("update Giderler set Elektrik=@p1,Su=@p2,Dogalgaz=@p3,internet=@p4,Gıda=@p5,Personel=@p6,Diger=@p7 where Odemeid=@p8", bgl.baglanti());
                komut.Parameters.AddWithValue("@p8", TxtGiderid.Text); // burdaki gider id ye göre tüm değişiklikleri tabloya kaydedecek 
                komut.Parameters.AddWithValue("@p1", TxtElektrik.Text);
                komut.Parameters.AddWithValue("@p2", TxtSu.Text);
                komut.Parameters.AddWithValue("@p3", TxtDagalgaz.Text);
                komut.Parameters.AddWithValue("@p4", TxtInternet.Text);
                komut.Parameters.AddWithValue("@p5", TxtGıda.Text);
                komut.Parameters.AddWithValue("@p6", TxtPersonel.Text);
                komut.Parameters.AddWithValue("@p7", TxtDiger.Text);
                komut.ExecuteNonQuery(); // yapılan değğişiklikleri kaydetmek için kullanılır 
                bgl.baglanti().Close();
                MessageBox.Show("Gider Bilgileri Güncellendi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,tekrar deneyiniz. ");
            }
            this.Close();
        }
    }
}
