using System;
using System.Windows.Forms;

namespace Dead_Matter_Server_Manager
{
    public partial class RestartSchedule : Form
    {
        public RestartSchedule()
        {
            InitializeComponent();
            foreach(DateTime time in ServerManager.restartSchedules)
            {
                restartTimeDGV.Rows.Add(time.ToString("HH:mm"));
            }
        }

        private void restartTimeDGV_CurrentCellChanged(object sender, EventArgs e)
        {
            ServerManager.restartSchedules.Clear();
            foreach(DataGridViewRow dataGridViewRow in restartTimeDGV.Rows)
            {
                if(dataGridViewRow.Cells[0].Value != null)
                {
                    DateTime temp;
                    if(DateTime.TryParse(dataGridViewRow.Cells[0].Value.ToString(),out temp))
                    {
                        ServerManager.restartSchedules.Add(Convert.ToDateTime(dataGridViewRow.Cells[0].Value));
                    }
                    else
                    {
                        MessageBox.Show("Invalid Time Format in Row " + dataGridViewRow.Index + 1 + Environment.NewLine + "Correct format should be hh:mm and in 24h", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                }
                
            }
            ServerManager.serverManager.SaveData();
        }
    }
}
