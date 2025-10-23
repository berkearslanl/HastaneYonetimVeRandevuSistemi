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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand giris = new SqlCommand("select * from Tbl_Doktorlar where DoktorTc=@p1 and DoktorSifre=@p2",bgl.baglanti());
            giris.Parameters.AddWithValue("@p1", mskTc.Text);
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = giris.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.tc = mskTc.Text;
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı tc veya şifre!");
            }
            bgl.baglanti().Close();
        }
    }
}
