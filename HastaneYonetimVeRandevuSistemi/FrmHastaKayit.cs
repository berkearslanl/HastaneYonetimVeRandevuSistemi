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

namespace HastaneYonetimVeRandevuSistemi
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi(); // sqlbaglantisi sınıfından bir tane bgl adında nesne ürettik
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            SqlCommand kaydet = new SqlCommand("insert into Tbl_Hastalar(HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) " +
                "values(@ad,@soyad,@tc,@tel,@sifre,@cinsiyet) ",bgl.baglanti());
            kaydet.Parameters.AddWithValue("@ad",txtAd.Text);
            kaydet.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            kaydet.Parameters.AddWithValue("@tc",mskTc.Text);
            kaydet.Parameters.AddWithValue("@tel",mskTel.Text);
            kaydet.Parameters.AddWithValue("@sifre",txtSifre.Text);
            kaydet.Parameters.AddWithValue("@cinsiyet",cmbCinsiyet.Text);
            kaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz : "+txtSifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            txtAd.Clear();
            txtSifre.Clear();
            txtSoyad.Clear();
            mskTc.Clear();
            mskTel.Clear();
            cmbCinsiyet.SelectedIndex = -1;
        }

       
    }
}
