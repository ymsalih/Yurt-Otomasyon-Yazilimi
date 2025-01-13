using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace YurtKayitSistemi
{
    // bu şekilde sqp bağlantı sınıfı oluşturmuş olduk çünkü her seferinde bağlantı adresi için kopyalama yapmak yerine bu sınıftan bir nesne üretip o nesneyi kullanıp bağlantıyı sağlamış oluruz 
  public class SqlBaglantim
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-29179S9D\SQLEXPRESS;Initial Catalog=YurtOtomasyonu;Integrated Security=True ");
            if (baglan.State==ConnectionState.Closed) // eğer bağlantı kapalı ise aç 
            {
                baglan.Open();
            }
           
            return baglan;
            // bu sınıf herkese açık olsun ki diğer sınıflardan da bağlanabileyim 
        }
    }
}
