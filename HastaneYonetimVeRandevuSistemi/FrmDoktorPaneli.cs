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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        void clear()
        {
            txtAd.Clear();
            txtSifre.Clear();
            txtSoyad.Clear();
            cmbBrans.SelectedIndex = -1;
            mskTc.Clear();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Doktorlar", bgl.baglanti());
            DataTable table = new DataTable();
            adp.Fill(table);
            dataGridView1.DataSource = table;
            bgl.baglanti().Close();

            SqlCommand sec = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr = sec.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand ekle = new SqlCommand("insert into Tbl_Doktorlar(DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@ad,@soyad,@brans,@tc,@sifre)", bgl.baglanti());
            ekle.Parameters.AddWithValue("@ad",txtAd.Text);
            ekle.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            ekle.Parameters.AddWithValue("@brans",cmbBrans.Text);
            ekle.Parameters.AddWithValue("@tc",mskTc.Text);
            ekle.Parameters.AddWithValue("@sifre",txtSifre.Text);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ekleme işlemi başarıyla gerçekleştirildi!");
            clear();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@ad,DoktorSoyad=@soyad,DoktorBrans=@brans,DoktorSifre=@sifre where DoktorTC=@tc", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@ad", txtAd.Text);
            guncelle.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@brans", cmbBrans.Text);
            guncelle.Parameters.AddWithValue("@tc", mskTc.Text);
            guncelle.Parameters.AddWithValue("@sifre", txtSifre.Text);
            guncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirildi!");
            clear();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            sil.Parameters.AddWithValue("@p1",mskTc.Text);
            sil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Başarıyla silindi!");
            clear();
        }
    }
}
