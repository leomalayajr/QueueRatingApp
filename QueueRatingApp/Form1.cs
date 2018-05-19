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

namespace QueueRatingApp
{
    public partial class Form1 : Form
    {
        private String connection_string = System.Configuration.ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            giveRating(1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            giveRating(2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            giveRating(3);
        }
        private void giveRating(int rate)
        {
            SqlConnection con = new SqlConnection(connection_string);
            string query = "update Rating_Office set Score = @param_score, isGiven = 1 where Customer_Queue_Number = @param_cqn and isGiven = 0";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@param_score", rate);
            cmd.Parameters.AddWithValue("@param_cqn", textBox1.Text);
            con.Open();
            int b = cmd.ExecuteNonQuery();
            textBox1.Clear();
            if (b == 0)
                MessageBox.Show("The Queue Number you entered is not queued on this office or you already gave your rating for the last transaction.", "Error");
            else
                MessageBox.Show("Thank you for the evaluation. This will help us know how to serve you better.", "Evaluation submitted"); 
            con.Close();
        }
    }
}
