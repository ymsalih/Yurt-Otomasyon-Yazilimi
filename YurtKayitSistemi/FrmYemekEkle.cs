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
    public partial class FrmYemekEkle : Form
    {
        public FrmYemekEkle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();

        private void FrmYemekMenüsü_Load(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into Yemek (AnaYemek,İkinciAnaYemek,Çorba,Tatlı,YanÜrün) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAnaYemek.Text);
                komut.Parameters.AddWithValue("@p2", TxtİikinciAnaYemek.Text);
                komut.Parameters.AddWithValue("@p3", TxtÇorba.Text);
                komut.Parameters.AddWithValue("@p4", TxtTatlı.Text);
                komut.Parameters.AddWithValue("@p5", TxtYanÜrün.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yemek Menüsüne Kaydedildi. ");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,kaydedilemedi.");
            }
            this.Close();


        }
    }
}
