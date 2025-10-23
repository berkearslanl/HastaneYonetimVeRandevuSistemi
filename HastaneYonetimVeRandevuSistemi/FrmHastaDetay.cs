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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }


        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc; // diğer formdan almak için
        void gecmisRandevular()//randevu geçmişi verileri
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC=" + tc, bgl.baglanti());
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void aktifRandevular()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans= '" + cmbBrans.Text + "'" + " and RandevuDoktor='" + cmbDoktor.Text + "' and RandevuDurum=0 ", bgl.baglanti());
            adp.Fill(table);
            dataGridView2.DataSource = table;
        }
        void temizle()//textboxları temizlemek için
        {
            txtid.Clear();
            cmbBrans.SelectedIndex = -1;
            cmbDoktor.SelectedIndex = -1;
            rchSikayet.Clear();
        }
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                lblAdSoyad.Text = rd[0] + " "+ rd[1];
            }
            bgl.baglanti().Close();


            //Randevu Geçmişi

            gecmisRandevular();

            //Branş Combobox'u doldurma

            SqlCommand brans = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader reader = brans.ExecuteReader();
            while (reader.Read())
            {
                cmbBrans.Items.Add(reader[0]);
            }
            bgl.baglanti().Close();
        }

        //branş seçildiğinde o branştaki doktorları combobox'ta göster
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();//tüm seçilenleri üst üste yazdığı için her seçimden önce içi temizlenir

            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
            
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            aktifRandevular();
        }

        private void lnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDüzenle frm = new FrmBilgiDüzenle();
            frm.TCno = lblTc.Text;
            frm.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=@p1, HastaTc=@p2,HastaSikayet=@p3 where Randevuid=@p4", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",true);
            komut.Parameters.AddWithValue("@p2",lblTc.Text);
            komut.Parameters.AddWithValue("@p3",rchSikayet.Text);
            komut.Parameters.AddWithValue("@p4",txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu başarıyla oluşturuldu!");
            gecmisRandevular();
            aktifRandevular();
            temizle();
        }
    }
}
