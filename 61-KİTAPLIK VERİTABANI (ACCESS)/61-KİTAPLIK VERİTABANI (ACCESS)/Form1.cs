using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _61_KİTAPLIK_VERİTABANI__ACCESS_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\msı\Documents\Visual Studio 2019\Projects\c# form\61-KİTAPLIK VERİTABANI (ACCESS)\Kitaplik.mdb");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Clear();
            txtad.Clear();
            txtsayfa.Clear();
            txtyazar.Clear();
            cmbtur.SelectedIndex = 4;
            lbldurum.Text = "2";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtyazar.Text);
            komut.Parameters.AddWithValue("@p3", cmbtur.Text);
            komut.Parameters.AddWithValue("@p4", txtsayfa.Text);
            komut.Parameters.AddWithValue("@p5", lbldurum.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KİTAP KAYDEDİLDİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void rdykullanilmis_CheckedChanged(object sender, EventArgs e)
        {
            if (rdykullanilmis.Checked == true)
            {
                lbldurum.Text = "0";
            }
        }

        private void rdysifir_CheckedChanged(object sender, EventArgs e)
        {
            if (rdysifir.Checked == true)
            {
                lbldurum.Text = "1";
            }
        }

        private void lbldurum_TextChanged(object sender, EventArgs e)
        {
            if (lbldurum.Text == "1")
            {
                rdysifir.Checked = true;
            }
            else
            {
                rdysifir.Checked = false;
            }
            if (lbldurum.Text == "0")
            {
                rdykullanilmis.Checked = true;
            }
            else
            {
                rdykullanilmis.Checked = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbtur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                rdysifir.Checked = true;
            }
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "False")
            {
                rdykullanilmis.Checked = true;
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("delete from Kitaplar where KitapID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KİTAP SİLİNDİ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update Kitaplar set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where KitapID=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtyazar.Text);
            komut.Parameters.AddWithValue("@p3", cmbtur.Text);
            komut.Parameters.AddWithValue("@p4", txtsayfa.Text);
            komut.Parameters.AddWithValue("@p5", lbldurum.Text);
            komut.Parameters.AddWithValue("@p6", txtid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KİTAP GÜNCELLENDİ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void txtara_TextChanged(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("select * from Kitaplar where KitapAd like '" + txtara.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
