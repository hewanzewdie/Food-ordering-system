using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace LoungeManagementApplication
{
    public partial class frmAddlLounge : Form
    {  private bool _isLounge;
        private bool isAddbtn=true;
        public frmAddlLounge()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            

                if (text_loungeid.Text == "")
                {
                    warningmessage.Show("Please enter the Lounge id");
                    return;
                }
            if (txt_loungename.Text == "")
            {
                warningmessage.Show("Please enter the Lounge Name");
                return;
            }
            if (text_location.Text == "")
            {
                warningmessage.Show("Please enter the Lounge Location");
                return;
            }
            if (text_cheifusername.Text == "")
            {
                warningmessage.Show("Please enter the cheif_username Name");
                return;
            }
            if (text_password.Text == "")
            {
                warningmessage.Show("Please enter thepassword");
                return;
            }
            try
                {
                
	


                    MainClass.connection.Open();

                    using (SqlCommand command = new SqlCommand("Storelounge", MainClass.connection))
                    {
                     command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.AddWithValue("@loungeide", text_loungeid.Text);
                     command.Parameters.AddWithValue(" @loungename", txt_loungename.Text);
                     command.Parameters.AddWithValue(" @location", text_location.Text);
                     command.Parameters.AddWithValue(" @cheifusernamee",text_cheifusername.Text);
                     command.Parameters.AddWithValue(" @password", text_password.Text);

                    command.ExecuteNonQuery();
                        infomessage.Show("Add successfully");
                        this.Hide();
                    }
                    MainClass.connection.Close();
                    //frmAdmin.isAddbtn = false;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the initial directory to "My Documents"
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Filter to show only image files
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                // Show the dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = openFileDialog.FileName;

                    // Load the image into a PictureBox
                    guna2PictureBox1.Image = Image.FromFile(filePath);

                    // Optionally, display the file path
                    MessageBox.Show("Selected image: " + filePath);
                }
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmAddlLounge
            // 
            this.ClientSize = new System.Drawing.Size(372, 261);
            this.Name = "frmAddlLounge";
            this.ResumeLayout(false);

        }
    }
}
