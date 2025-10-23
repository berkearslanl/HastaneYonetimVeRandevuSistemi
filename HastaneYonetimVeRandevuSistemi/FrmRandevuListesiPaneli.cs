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
    public partial class FrmRandevuListesiPaneli : Form
    {
        public FrmRandevuListesiPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRandevuListesiPaneli_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Tbl_Randevular", bgl.baglanti());
            DataTable table = new DataTable();
            adp.Fill(table);
            dataGridView1.DataSource = table;
        }

    }
}
