using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

namespace WindowsFormsApp1
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm

    {
        public Form1()
        {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, 
                                                     Primary.BlueGrey500, Accent.LightBlue200,
                                                     TextShade.WHITE);
        }

        private void Edit (bool value)
        {
            txtFullName.Enabled = value;
            txtxPhoneNumber.Enabled = value;

        }

     
        private void Form1_Load(object sender, EventArgs e)
        {
            Edit(false);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Edit(true);
                myAppData.PhoneList.AddPhoneListRow(myAppData.PhoneList.NewPhoneListRow());
                phoneListBindingSource.MoveLast();
                txtxPhoneNumber.Focus();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myAppData.PhoneList.RejectChanges();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
            Edit(true);
            txtxPhoneNumber.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Edit(false);
            phoneListBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Edit(false);
                phoneListBindingSource.EndEdit();
                phoneListTableAdapter.Update(myAppData.PhoneList);
                dataGridView1.Refresh();
                txtxPhoneNumber.Focus();
                MessageBox.Show("Data saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myAppData.PhoneList.RejectChanges();
            }
        }

        //private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if(e.KeyChar == (char)13)
        //    {
        //        if (MessageBox.Show("are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            phoneListBindingSource.RemoveCurrent();

        //    }
        //}

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                    phoneListBindingSource.Filter = string.Format("PhoneNumber ='{0}'OR FullName LIKE '*{1}*'", txtxPhoneNumber.Text, txtFullName.Text);
                else
                    phoneListBindingSource.Filter = string.Empty;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {

                if (MessageBox.Show("are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    phoneListBindingSource.RemoveCurrent();

            }

        }
    }
}
