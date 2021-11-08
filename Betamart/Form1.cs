using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Betamart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database2DataSet.Betamart' table. You can move, or remove it, as needed.
            this.betamartTableAdapter.Fill(this.database2DataSet.Betamart);
            betamartBindingSource.DataSource = this.database2DataSet.Betamart;

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.betamartTableAdapter.Fill(this.database2DataSet.Betamart);
                    betamartBindingSource.DataSource = this.database2DataSet.Betamart;
                    //dataGridView.DataSource = betamartBindingSource;
                }
                    
                else
                {
                    var query = from o in this.database2DataSet.Betamart
                                where o.NamaBarang.Contains(txtSearch.Text) || o.JenisBarang.Contains(txtSearch.Text)
                                select o;

                    betamartBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betamartBindingSource.RemoveCurrent();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNamaBarang.Focus();
                this.database2DataSet.Betamart.AddBetamartRow(this.database2DataSet.Betamart.NewBetamartRow());
                betamartBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betamartBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNamaBarang.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                betamartBindingSource.EndEdit();
                betamartTableAdapter.Update(this.database2DataSet.Betamart);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betamartBindingSource.ResetBindings(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            betamartBindingSource.ResetBindings(false);
        }
    }
}
