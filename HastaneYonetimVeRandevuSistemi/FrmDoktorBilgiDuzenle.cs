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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        void temizle()
        {
            txtAd.Clear();
            txtSifre.Clear();
            txtSoyad.Clear();
            cmbBrans.SelectedIndex = -1;
            mskTc.Clear();
        }
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTc.Text = tc;//doktor tc'sini aldık
            //verileri çekme
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTc=@tc",bgl.baglanti());
            komut.Parameters.AddWithValue("@tc",mskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }
        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            //güncelleme komutu
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@ad,DoktorSoyad=@soyad,DoktorBrans=@brans,DoktorSifre=@sifre where DoktorTc=@tc", bgl.baglanti());
            komut.Parameters.AddWithValue("@ad",txtAd.Text);
            komut.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            komut.Parameters.AddWithValue("@brans",cmbBrans.Text);
            komut.Parameters.AddWithValue("@sifre",txtSifre.Text);
            komut.Parameters.AddWithValue("@tc",mskTc.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Veriler başarıyla güncellendi!");
            temizle();
        }

        
    }
}
