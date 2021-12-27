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
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.DAL;

namespace TomaFoodRestaurant.Sequrity
{
    public partial class QueryExecuteForm : Form{
        public QueryExecuteForm()
        {
            InitializeComponent();
        }

        private void inputTextButton1_Click(object sender, EventArgs e)
        {

            try
            {
                var connection = new MySqlGatewayConnection();
                using (MySqlConnection con = connection.Connection)
                {
                    
                    string Query = richTextBox1.Text;
                    MySqlCommand command = new MySqlCommand(Query, con);
                    
                    int row = command.ExecuteNonQuery();
                    con.Close();
                    if (row > 0)
                    {
                        MessageBox.Show("Query is executed.");
                        richTextBox1.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
            
            
        }
    }
}
