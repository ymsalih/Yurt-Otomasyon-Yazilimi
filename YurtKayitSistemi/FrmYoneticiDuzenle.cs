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
    public partial class FrmYoneticiDuzenle : Form
    {
        public FrmYoneticiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet9.Admin' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.adminTableAdapter1.Fill(this.yurtOtomasyonuDataSet9.Admin);
           

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try {
                SqlCommand komut = new SqlCommand("insert into Admin (YoneticiAd,YoneticiSifre,KullanıcıEposta) values (@p1,@p2,@p3)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p3", txtEposta.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Kullanıcı Kaydedildi.");
                this.adminTableAdapter1.Fill(this.yurtOtomasyonuDataSet9.Admin);
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,Kullanıcı kaydedilemedi ");
            }
    }

     

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("delete from Admin where Yoneticiid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtYöneticiid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Silme İşlemi Gerçekleşti.");
                this.adminTableAdapter1.Fill(this.yurtOtomasyonuDataSet9.Admin); // bu aslında yenileme işlemi gibi son yapılan değişikliklerden sonra en güncel halini göstermek için kullanıyoruz 
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,silme işlemi gerçekleşmedi.");
            }

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Admin set YoneticiAd=@p1,YoneticiSifre=@p2,KullanıcıEposta=@p4 where Yoneticiid=@p3", bgl.baglanti());
                komut.Parameters.AddWithValue("@p3", TxtYöneticiid.Text);
                komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p4", txtEposta.Text);
                komut.ExecuteNonQuery(); // yapılan değişiklikleri kaydetmek için 
                bgl.baglanti().Close();
                MessageBox.Show("Kullanıcı Bilgileri Güncellendi. ");
                this.adminTableAdapter1.Fill(this.yurtOtomasyonuDataSet9.Admin); // en son halini getirsin diye böylece yönetici düzenleme işlemi de bitiyor 
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,kullanıcı bilgileri güncellenemedi.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // herhangi bir hücreye tıkladığım anda fromda gerekli yerleri otomatik o bilgilerle doldursun diye 
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            string ad, sifre, id,eposta;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            sifre = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            eposta = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            TxtYöneticiid.Text = id;
            TxtKullanıcıAd.Text = ad;
            TxtSifre.Text = sifre;
            txtEposta.Text = eposta;
        }
    }
}
