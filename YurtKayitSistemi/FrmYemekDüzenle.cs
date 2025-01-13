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
    public partial class FrmYemekDüzenle : Form
    {
        public FrmYemekDüzenle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        public string id, yemekad, ikinciyemekad, çorba, tatlı, yanürün;

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Yemek set AnaYemek=@p1,İkinciAnaYemek=@p2,Çorba=@p3,Tatlı=@p4,YanÜrün=@p5 where Yemekid=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p6", TxtYemekİd.Text);
                komut.Parameters.AddWithValue("@p1", TxtAnaYemek.Text);
                komut.Parameters.AddWithValue("@p2", TxtİikinciAnaYemek.Text);
                komut.Parameters.AddWithValue("@p3", TxtÇorba.Text);
                komut.Parameters.AddWithValue("@p4", TxtTatlı.Text);
                komut.Parameters.AddWithValue("@p5", TxtYanÜrün.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yemek Güncellendi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,yemek güncellenemedi.");
            }
            this.Close();
        }

        private void FrmYemekDüzenle_Load(object sender, EventArgs e)
        {
            TxtYemekİd.Text = id;
            TxtAnaYemek.Text = yemekad;
            TxtİikinciAnaYemek.Text = ikinciyemekad;
            TxtÇorba.Text = çorba;
            TxtTatlı.Text = tatlı;
            TxtYanÜrün.Text = yanürün;
        }
    }
}
