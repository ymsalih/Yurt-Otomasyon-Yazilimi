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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void FrmPersonelcs_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet6.Personel' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Personel);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen;
            string Personelid, Personelad, Personelgörev;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            Personelid = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Personelad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Personelgörev = dataGridView1.Rows[secilen].Cells[2].Value.ToString();

            TxtPersonelid.Text = Personelid;
            TxtPersonelAd.Text = Personelad;
            TxtPersonelGorev.Text = Personelgörev;


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into Personel (PersonelAdSoyad,PersonelDepartman) values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtPersonelAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtPersonelGorev.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel Kaydedildi.");
                this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Personel);
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,personel kaydedilemedi.");
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("delete from Personel where Personelid=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtPersonelid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel Silme İşlemi Gerçekleşti. ");
                this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Personel);
            }
            catch (Exception)
            {
                MessageBox.Show("Hata,personel silme işlemi gerçekleşemedi.");
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("update Personel set PersonelAdSoyad=@p1,PersonelDepartman=@p2 where Personelid=@p3", bgl.baglanti());
                komut.Parameters.AddWithValue("@p3", TxtPersonelid.Text);
                komut.Parameters.AddWithValue("@p1", TxtPersonelAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtPersonelGorev.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Güncelleme İşlemi Gerçekleşti.");
                this.personelTableAdapter.Fill(this.yurtOtomasyonuDataSet6.Personel);
            }
            catch (Exception)
            {
                MessageBox.Show("Hata, güncelleme işlemi gerçekleşmedi. ");
            }

        }
    }
}
