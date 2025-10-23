using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//bağlantı için kütüphane

namespace HastaneYonetimVeRandevuSistemi
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-5Q1ARH5E;Initial Catalog=HastaneProje;Integrated Security=True;");
            baglan.Open();
            return baglan;
        }
    }
}
