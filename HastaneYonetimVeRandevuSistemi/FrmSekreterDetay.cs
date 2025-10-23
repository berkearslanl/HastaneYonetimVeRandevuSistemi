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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        void clear() 
        {
            rchDuyuru.Clear();
            cmbBrans.SelectedIndex = -1;
            cmbDoktor.SelectedIndex = -1;
            txtid.Clear();
            mskSaat.Clear();
            mskTarih.Clear();
            mskTc.Clear();  
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            //Ad Soyad
            SqlCommand komut1 = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTc = @p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları datagrid'e çekme
            DataTable table = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("select BransAd from Tbl_Branslar ", bgl.baglanti());
            adp.Fill(table);
            dataGridView1.DataSource = table;

            //Doktorları datagrid'e çekme
            DataTable table2 = new DataTable();
            SqlDataAdapter adp2 = new SqlDataAdapter("select (DoktorAd+' '+DoktorSoyad) as 'Doktor',DoktorBrans as 'Doktor Branş' from Tbl_Doktorlar", bgl.baglanti());
            adp2.Fill(table2); 
            dataGridView2.DataSource = table2;

            //Branşları combobox'a çekme
            SqlCommand sec = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr = sec.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();

        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frm = new FrmBransPaneli();
            frm.ShowDialog();

        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesiPaneli frm = new FrmRandevuListesiPaneli();
            frm.ShowDialog();

        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.ShowDialog();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydet = new SqlCommand("insert into Tbl_Randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@r1,@r2,@r3,@r4)", bgl.baglanti());
            kaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            kaydet.Parameters.AddWithValue("@r2",mskSaat.Text);
            kaydet.Parameters.AddWithValue("@r3",cmbBrans.Text);
            kaydet.Parameters.AddWithValue("@r4",cmbDoktor.Text);
            kaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu başasrıyla oluşturuldu!");
            clear();
        }
        
        //Doktorları combobox'a çekme
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            
            SqlCommand komut = new SqlCommand("select (DoktorAd+' '+DoktorSoyad) from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0].ToString());
            }
        }

        private void btnDuyuruolusutr_Click(object sender, EventArgs e)
        {
            SqlCommand ekle = new SqlCommand("insert into Tbl_Duyurular (duyuru) values(@p1)",bgl.baglanti());
            ekle.Parameters.AddWithValue("@p1", rchDuyuru.Text);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru ekleme işlemi başarılı!");
            clear();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }
    }
}
