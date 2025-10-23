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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void clear()
        {
            txtid.Clear();
            txtBransAd.Clear();
        }
        void guncelle()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Branslar", bgl.baglanti());
            DataTable table = new DataTable();
            adp.Fill(table);
            dataGridView1.DataSource = table;
        }
            

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {

            guncelle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtBransAd.Text!="")
            {
                SqlCommand ekle = new SqlCommand("insert into Tbl_Branslar(BransAd) values(@bad)", bgl.baglanti());
                ekle.Parameters.AddWithValue("@bad", txtBransAd.Text);
                ekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Branş Başarıyla Eklendi!", "Bilgi");
                guncelle();
                clear();
            }
            else
            {
                MessageBox.Show("Lütfen Eklenecek Branş Adını Giriniz!","Hata");
            }
            

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from Tbl_Branslar where BransId=@p1",bgl.baglanti());
            sil.Parameters.AddWithValue("@p1", txtid.Text);
            sil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş başarıyla silindi");
            clear();
            guncelle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncel = new SqlCommand("update Tbl_Branslar set BransAd=@ad where BransId=@tc", bgl.baglanti());
            guncel.Parameters.AddWithValue("@tc", txtid.Text);
            guncel.Parameters.AddWithValue("@ad", txtBransAd.Text);
            guncel.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş başarıyla güncellendi");
            guncelle();
            clear();
        }
    }
}
