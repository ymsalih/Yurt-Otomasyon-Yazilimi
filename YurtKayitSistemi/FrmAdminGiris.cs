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
    public partial class FrmAdminGiris : Form
    {
        public FrmAdminGiris()
        {
            InitializeComponent();
        }
        SqlBaglantim bgl = new SqlBaglantim();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            // veritabanı kullanarak yöneticinin giriş yapmasını sağlarız 
            // collate SQL_Latin1_General_CP1_CS_AS bu şekilde büyük küçük harf duyarlılığı sağlanıyor hem şifre hem de kullanıcı ada yaptık 
            SqlCommand komut = new SqlCommand("select * from Admin where YoneticiAd=@p1 collate SQL_Latin1_General_CP1_CS_AS and YoneticiSifre=@p2 collate SQL_Latin1_General_CP1_CS_AS", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if(oku.Read()) // eğer girilen şifre ve kullanıcı adı veritabanındaki ile aynı ise ekrana ana formu verecek 
            {
                FrmAnaForm fr = new FrmAnaForm();
                fr.Show(); // eğer doğru ise ana formu ver 
                this.Hide(); // giriş formunu ise gizle demek ana forma girdikten sonra 
              

            }else{
                MessageBox.Show("Hatalı kullanıcı adı ya da şifre.");
                TxtKullaniciAd.Clear(); // eğer hatalı giriş olup uyuşmazsa kullanıcı adı kısmını sil   
                TxtSifre.Clear(); // hatalı giriş sonrasında şifre kısmını sil 
                TxtKullaniciAd.Focus(); // tüm giriş yerlerini sildikten sonra imleci tekrar kullanıcının girmesi için kullanıcı adı kısmına getir 
            }
            bgl.baglanti().Close();

      
            
            
         
            //if(TxtKullaniciAd.Text=="salih" && TxtSifre.Text == "190772**") // bu veritabanı olmadan belirlediğimiz şifreye eşit ise ana forma girecek değilse hata verecek 
            //{
            //    FrmAnaForm fr = new FrmAnaForm(); manuel giriş olduğu için bunu kullanmayıp veri tabanlı giriş yapacaz 
            //    fr.Show(); // giriş yapığı zaman açılacak olan formu göstermek için yazdığımız kod 
            //    // UseSystemPasswordChar özelliğini true yaparak şifreyi yazdığında nokta şeklinde görünüp gizlemesi için bunu yaptık 
            //}
            //else
            //{
            //    MessageBox.Show("Hatalı Giriş Yaptınız ");
            //}
        }

        private void FrmAdminGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmŞifreSıfırlama fr = new FrmŞifreSıfırlama();
            fr.Show();
        }

        private void FrmAdminGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
