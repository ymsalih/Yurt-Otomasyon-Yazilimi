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
    public partial class FrmGider : Form
    {
        public FrmGider()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // giderlerin yazıldığı formdaki bilgileri giderler tablosuna kaydetme için gerekli kod 
                // tabloya form ekranından girilen verileri eklemek için kullanılan kod mantığı şu şekilde 

                SqlCommand komut = new SqlCommand("insert into Giderler (Elektrik,Su,Dogalgaz,internet,Gıda,Personel,Diger) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtElektrik.Text); // , ile atama yapılıyor zaten 
                komut.Parameters.AddWithValue("@p2", TxtSu.Text);
                komut.Parameters.AddWithValue("@p3", TxtDagalgaz.Text);
                komut.Parameters.AddWithValue("@p4", TxtInternet.Text);
                komut.Parameters.AddWithValue("@p5", TxtGıda.Text);
                komut.Parameters.AddWithValue("@p6", TxtPersonel.Text);
                komut.Parameters.AddWithValue("@p7", TxtDiger.Text); // formda yazılan değeri burdaki parametre aracılığıyla taboya kaydediyoruz 
                komut.ExecuteNonQuery(); // yapılan değişiklikleri kaydetmek için 
                bgl.baglanti().Close();
                MessageBox.Show("Girilen Bilgiler Kaydedildi");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,tekrar deneyiniz ");
            }
            this.Close();

        }

        private void FrmGider_Load(object sender, EventArgs e)
        {
            
        }
    }
}
