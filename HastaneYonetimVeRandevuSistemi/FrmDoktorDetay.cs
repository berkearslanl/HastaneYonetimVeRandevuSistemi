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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;//tc'yi çekme
            //Doktor ad soyad
            SqlCommand komut = new SqlCommand("select (DoktorAd+' '+DoktorSoyad) from Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Bu doktora ait randevuları getirme

            DataTable table = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Randevular where RandevuDoktor='"+lblAdSoyad.Text+"'",bgl.baglanti());
            adp.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.ShowDialog();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frm = new FrmDoktorBilgiDuzenle();
            frm.tc = lblTc.Text;
            frm.ShowDialog();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //hücreye tıklandığında seçilen kişinin şikayeti görünür
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();//cells kaçıncı hücreyi almak istediğimizi belirler
        }
    }
}
