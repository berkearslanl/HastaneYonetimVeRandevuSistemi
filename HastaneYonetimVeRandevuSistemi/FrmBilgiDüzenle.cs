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
    public partial class FrmBilgiDüzenle : Form
    {
        public FrmBilgiDüzenle()
        {
            InitializeComponent();
        }

        public string TCno;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBilgiDüzenle_Load(object sender, EventArgs e)
        {
            mskTc.Text = TCno;
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTc = @p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();//0 çekmedik çünkü 0'da id var
                txtSoyad.Text = dr[2].ToString();
                mskTel.Text = dr[4].ToString();//3ü çekmedik çünkü zaten diğer formdan gelmişti
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("Update Tbl_Hastalar Set HastaAd=@ad,HastaSoyad=@soyad,HastaTelefon=@tel,HastaSifre=@sifre,HastaCinsiyet=@cinsiyet where HastaTC=@tc",bgl.baglanti());
            guncelle.Parameters.AddWithValue("@ad",txtAd.Text);
            guncelle.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@tel",mskTel.Text);
            guncelle.Parameters.AddWithValue("@sifre",txtSifre.Text);
            guncelle.Parameters.AddWithValue("@cinsiyet",cmbCinsiyet.Text);
            guncelle.Parameters.AddWithValue("@tc", mskTc.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }
    }
}
