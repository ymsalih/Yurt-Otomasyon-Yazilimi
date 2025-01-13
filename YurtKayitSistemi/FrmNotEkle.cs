using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace YurtKayitSistemi
{
    public partial class FrmNotEkle : Form
    {
        public FrmNotEkle()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "Kayıt Yeri Seçin"; // bu kayıt ekranı açıldığı zaman üst sekmede görnünen yazı 
                saveFileDialog1.Filter = "Metin Dosyası | *.txt"; // burda da metn dosyalarında kısıtlama kullandık sadece txt belgelerini göster dedik Metin Dosyası da kayıt türü kısmında gösterilen kısımdır
                                                                  //  saveFileDialog1.InitialDirectory = "‪C:\\Notlar"; bu kodun amacı normalde kayıt ekranı açıldığı zaman ilk açılacak olan belgeyi açılı halde göstermek için kullanılıyor ama olmadı diye normal seçip kaydediliyor 
                saveFileDialog1.ShowDialog(); // kayıt ekranını aç nereye kaydedileceği ile ilgili yeri 
                StreamWriter kaydet = new StreamWriter(saveFileDialog1.FileName);// dosyada verilen isimde kaydedilsin diye 

                kaydet.WriteLine(richTextBox1.Text);  //yazdığım notu txt olarak o klasöre kaydetmesi yani yazması için 
                kaydet.Close(); // sonrasında kaydetmeyi kapatıp ekrana mesaj vermek için 
                MessageBox.Show("Kayıt Yapıldı.");
            }
            catch (Exception)
            {
                MessageBox.Show("vazgeçildi. PROGRAM DEVAM EDİYOR");
            }

        }

        private void FrmNotEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
