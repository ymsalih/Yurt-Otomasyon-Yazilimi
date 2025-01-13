using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // SQL veritabanı işlemleri için
using System.Net; // Ağ işlemleri için
using System.Net.Mail; // E-posta gönderme işlemleri için

namespace YurtKayitSistemi
{
    public partial class FrmŞifreSıfırlama : Form
    {
        public FrmŞifreSıfırlama()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
           
        }

        private void btnOtpGonder_Click(object sender, EventArgs e)
        {
            StartCountDown();
            string kullaniciAdi = txtKullaniciAdi.Text;
            string kullaniciEmail = txtEmail.Text;

            // Kullanıcı adı veya e-posta adresi boş mu?
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(kullaniciEmail))
            {
                
                MessageBox.Show("Lütfen kullanıcı adınızı ve e-posta adresinizi girin.");
                return; // işlemi durdurur
            }

            // Kullanıcının e-posta adresini veritabanından alın eğer ki E-Posta ve kullanıcı ad eşleşirse o zaman mail olarak kodu gönderir
            SqlConnection baglanti = new SqlBaglantim().baglanti();
            SqlCommand komut = new SqlCommand("SELECT KullanıcıEposta FROM Admin WHERE YoneticiAd = @kullanici", baglanti);
            komut.Parameters.AddWithValue("@kullanici", kullaniciAdi); // Kullanıcı adını sorguya ekler
            SqlDataReader dr = komut.ExecuteReader(); // sorguyu çalıştırır ve sonuçları okur 

            if (dr.Read()) // Eğer kullanıcı bulunduysa
            {
                string veriTabaniEmail = dr["KullanıcıEposta"].ToString();
                dr.Close();

                if (kullaniciEmail == veriTabaniEmail) // veritabanından alınan email ile kullanıcının girdiği uyuşuyor mu 
                {
                    // Rastgele 6 haneli OTP kodu üret
                    Random rnd = new Random();
                    int otpKodu = rnd.Next(100000, 999999);
                    DateTime otpSuresi = DateTime.Now.AddMinutes(5); // OTP kodunun geçerlilik süresini 5 dakika yapar

                    // OTP kodunu veritabanına kaydet
                    SqlCommand updateKomut = new SqlCommand("UPDATE Admin SET OTP_Kodu = @otp, OTP_Suresi = @saat WHERE YoneticiAd = @kullanici", baglanti);
                    updateKomut.Parameters.AddWithValue("@otp", otpKodu); // OTP kodunu ekler 
                    updateKomut.Parameters.AddWithValue("@saat", otpSuresi);
                    updateKomut.Parameters.AddWithValue("@kullanici", kullaniciAdi);
                    updateKomut.ExecuteNonQuery(); // sorguyu çalıştırır

                    // OTP kodunu e-posta ile gönder
                    OtpEpostaGonder(kullaniciEmail, otpKodu);
                }
                else
                {
                    MessageBox.Show("E-posta adresi uyuşmuyor.");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı bulunamadı!");
            }

            baglanti.Close();
        }

      

        private void OtpEpostaGonder(string kullaniciEmail, int otpKod)
        {
            try
            {
                MailMessage mesaj = new MailMessage(); // Yeni bir e-posta mesajı oluşturur
                mesaj.From = new MailAddress("salihatic@gmail.com"); // Gönderen e-posta adresini belirler
                mesaj.To.Add(kullaniciEmail); // Alıcı e-posta adresini ekler
                mesaj.Subject = "Şifre Sıfırlama OTP Kodu"; // E-posta konusunu belirler
                mesaj.Body = "Şifre sıfırlama kodunuz: " + otpKod; // E-posta içeriğini belirler

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // SMTP istemcisi oluşturur
                smtp.Credentials = new NetworkCredential("examples@gmail.com", "abcd gdfs hdgs sbbs"); // SMTP kimlik doğrulama bilgilerini ekler
                smtp.EnableSsl = true; // SSL kullanımı etkinleştirir
                smtp.Send(mesaj); // E-postayı gönderir

                MessageBox.Show("OTP kodu e-posta adresinize gönderildi.E-Posta'nızı kontrol ediniz.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderme hatası: " + ex.Message);
            }
        }

        private void btnSifreyiGuncelle_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string girilenOtp = txtOtp.Text;
            string yeniSifre = txtYeniSifre.Text;

            // Girilen OTP veya yeni şifre boş mu kontrol eder
            if (string.IsNullOrEmpty(girilenOtp) || string.IsNullOrEmpty(yeniSifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            SqlConnection baglanti = new SqlBaglantim().baglanti();
            // OTP kodunu ve süresini veri tabanından almak için SQL sorgusu hazırlar
            SqlCommand komut = new SqlCommand("SELECT OTP_Kodu, OTP_Suresi FROM Admin WHERE YoneticiAd = @kullanici", baglanti);
            komut.Parameters.AddWithValue("@kullanici", kullaniciAdi);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read()) // Eğer kullanıcı bulunduysa
            {
                string otpKod = dr["OTP_Kodu"].ToString(); // Veri tabanındaki OTP kodunu alır
                DateTime otpSuresi = Convert.ToDateTime(dr["OTP_Suresi"]); // Veri tabanındaki OTP süresini alır

                if (DateTime.Now > otpSuresi) // OTP süresi dolmuş mu kontrol eder
                {
                    MessageBox.Show("OTP süresi doldu! Lütfen yeni bir kod isteyin.");
                    return;
                }

                if (girilenOtp == otpKod) // Girilen OTP kodu doğru mu kontrol eder
                {
                    dr.Close();
                    // Yeni şifreyi güncelle
                    SqlCommand sifreGuncelle = new SqlCommand("UPDATE Admin SET YoneticiSifre = @sifre, OTP_Kodu = NULL, OTP_Suresi = NULL WHERE YoneticiAd = @kullanici", baglanti);
                    sifreGuncelle.Parameters.AddWithValue("@sifre", yeniSifre);
                    sifreGuncelle.Parameters.AddWithValue("@kullanici", kullaniciAdi);
                    sifreGuncelle.ExecuteNonQuery();

                    MessageBox.Show("Şifreniz başarıyla değiştirildi.");
                }
                else
                {
                    MessageBox.Show("Girilen OTP kodu yanlış!");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!");
            }

            baglanti.Close();
        }

        
       
        // Geri sayım süresini tutmak için iki değişken tanımlanıyor 
        private int countdownMinutes = 5; // Başlangıç olarak 5 dakika 

        private int countdownSeconds = 0; // Başlangıç olarak 0 saniye
        private void FrmŞifreSıfırlama_Load(object sender, EventArgs e)
        {
         
            label2.Text = countdownMinutes.ToString("D2") + ":" + countdownSeconds.ToString("D2");             
            timer1.Interval = 1000; // 1 dakika çünkü milisaniye orjinali
            timer1.Enabled = false;
        }
        
        // Timer'ın Tick olayı tetiklendiğinde çalışan metod
        private void timer1_Tick(object sender, EventArgs e)
        {
            countdownSeconds--;
            // Geri sayım süresi 0'dan büyükse 
            if (countdownSeconds< 0)
            {
                countdownSeconds = 59;
                countdownMinutes--;
            }
            if (countdownMinutes<0)
            {
                timer1.Stop();
                label2.Visible = false;
                MessageBox.Show("Sayaç tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            label2.Text = countdownMinutes.ToString("D2") + ":" + countdownSeconds.ToString("D2");
        }
        // sayaç başlatma methodu
        private void StartCountDown()
        {
           
            countdownMinutes = 5;  // Sayaç süresi 5 dakika olarak yeniden ayarlanıyor 

            countdownSeconds = 0;  // Saniyeyi sıfırlıyoruz 
            // D2, sayıyı en az iki basamaklı bir hale getiren bir format belirticisidir. 05:00 olmasını sağlayan D2 formatıdır 
            label2.Text = countdownMinutes.ToString("D2") + ":" + countdownSeconds.ToString("D2"); // Sayaç süresi etiket üzerinde görüntüleniyor 

            label2.Visible = true; // Sayaç etiketi görünür yapılıyor

            timer1.Enabled = true; // Timer etkinleştiriliyor

            timer1.Start();  // Timer başlatılıyor 

        }
    }
}
        
    
