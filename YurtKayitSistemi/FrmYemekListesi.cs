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
    public partial class FrmYemekListesi : Form
    {
        public FrmYemekListesi()
        {
            InitializeComponent();
        }

        private void FrmYemekListesi_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'yurtOtomasyonuDataSet7.Yemek' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.yemekTableAdapter.Fill(this.yurtOtomasyonuDataSet7.Yemek);

        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmYemekDüzenle fr = new FrmYemekDüzenle();
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            fr.id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            fr.yemekad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            fr.ikinciyemekad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            fr.çorba = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            fr.tatlı = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            fr.yanürün = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            fr.Show();
        }
    }
}
