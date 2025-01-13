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
using System.Net;
using System.Net.Mail;

namespace YurtKayitSistemi
{
    public partial class FrmMailGönder : Form
    {
       
        public FrmMailGönder()
        {
            InitializeComponent();
        }

        // Veritabanından öğrenci e-posta adreslerini çeker
        private List<string> GetStudentEmails()
        {
            List<string> emailList = new List<string>(); // E-posta adreslerini saklamak için liste

            SqlBaglantim bgl = new SqlBaglantim(); // SqlBaglantim sınıfından nesne oluşturduk
            using (SqlConnection conn = bgl.baglanti()) // Veritabanı bağlantısını aldık
            {
                if (conn.State == ConnectionState.Open) // eğer bağlantı açık ise 
                {
                    conn.Close(); // Önce bağlantıyı kapat
                }
                conn.Open(); // Sonra tekrar aç

                string query = "SELECT OgrMail FROM Ogrenci"; // Öğrenci e-postalarını çeken SQL sorgusu

                using (SqlCommand cmd = new SqlCommand(query, conn)) // SQL komutunu çalıştırıyoruz
                {
                    using (SqlDataReader reader = cmd.ExecuteReader()) // Verileri okuyoruz
                    {
                        while (reader.Read()) // Tüm satırları oku
                        {
                            emailList.Add(reader["OgrMail"].ToString()); // E-posta adresini listeye ekle
                        }
                    }
                }
            }
            return emailList; // Listeyi döndür
        }

        // Toplu e-posta gönderme işlemini yapan metot
        private void SendBulkEmails()
        {
            List<string> emails = GetStudentEmails(); // Öğrenci e-postalarını al

            string senderEmail = txtSenderEmail.Text.Trim(); // Kullanıcının girdiği e-posta adresi trim burada boşluklar var ise onları kaldırıp düzenliyor 
            string senderPassword = txtSenderPassword.Text.Trim(); // Kullanıcının girdiği uygulama şifresi
            string subject = txtSubject.Text.Trim(); // Kullanıcının belirlediği e-posta konusu
            string body = txtMessage.Text.Trim(); // Kullanıcının belirlediği mesaj içeriği

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword) ||
                string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Gmail SMTP ayarları
                {
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword); // Kullanıcı giriş bilgileri
                    smtp.EnableSsl = true; // SSL güvenlik önlemi etkinleştiriliyor

                    foreach (string email in emails) // Listedeki her öğrenci e-postasına
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(senderEmail); // Gönderen e-posta
                            mail.To.Add(email); // Alıcı e-posta
                            mail.Subject = subject; // Konu
                            mail.Body = body; // Mesaj içeriği
                            mail.IsBodyHtml = true; // HTML formatını etkinleştiriyoruz (İsteğe bağlı)

                            smtp.Send(mail); // E-postayı gönder
                        }
                    }
                }
                MessageBox.Show("E-postalar başarıyla gönderildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMessage.Clear();
                txtSenderEmail.Clear();
                txtSenderPassword.Clear();
                txtSubject.Clear();
                txtSenderEmail.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gönder butonuna basıldığında toplu e-posta gönderme işlemi başlatılır
        private void btnSendEmails_Click(object sender, EventArgs e)
        {
            SendBulkEmails();
            

        }
        private string defaultMessage = " Yüksek Öğrenim Kredi ve Yurtlar Kurumu tarafından gönderilmiştir.\n\n Duyuru :\n  ";
        
        private void FrmMailGönder_Load(object sender, EventArgs e)
        {

            richTextBox1.ReadOnly = true;
            txtMessage.Text = defaultMessage; // Hazır mesajı ekle
            txtMessage.SelectionStart = txtMessage.Text.Length; // İmleci en sona al
            txtMessage.KeyPress += TxtMessage_KeyPress; // Kullanıcının silmesini engelle
            txtMessage.TextChanged += TxtMessage_TextChanged; // Kullanıcının içeriği değiştirmesini kontrol et

        }
        // Kullanıcının hazır mesajı silmesini engelleyen kod
        private void TxtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer kullanıcı hazır mesajın başına gelip değiştirmeye çalışıyorsa engelle
            if (txtMessage.SelectionStart < defaultMessage.Length)
            {
                e.Handled = true; // İşlemi iptal et
                txtMessage.SelectionStart = txtMessage.Text.Length; // İmleci en sona al
            }
        }

        // Eğer kullanıcı bir şekilde hazır mesajı silerse geri ekle
        private void TxtMessage_TextChanged(object sender, EventArgs e)
        {
            if (!txtMessage.Text.StartsWith(defaultMessage)) // Eğer hazır mesaj silinirse
            {
                int cursorPosition = txtMessage.SelectionStart; // Mevcut imleç pozisyonunu kaydet
                txtMessage.Text = defaultMessage; // Tekrar ekle
                txtMessage.SelectionStart = cursorPosition < defaultMessage.Length ? defaultMessage.Length : cursorPosition; // İmleci koru
            }
        }
    }
}

