using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace script_runner
{
    public partial class MainWindow : Form
    {
        // Variables
        private String dataEntryHelp = "Copy/Paste SSH info here seperated by a delimeter....\nIE)\nssh - p < PORT > < USER >@< IP >,< SSH_PASSWORD >\n\nNOTE: only accepts 2 columns.";

        public MainWindow()
        {
            InitializeComponent();
            dataEntry.Text = dataEntryHelp;
            dataEntry.ForeColor = Color.Gray;
            dataEntry.GotFocus += new EventHandler(dataEntry_GotFocus);
            dataEntry.LostFocus += new EventHandler(dataEntry_LostFocus);
            dataView.ColumnCount = 2;
            dataView.Columns[0].Name = "SSH Login";
            dataView.Columns[1].Name = "SSH Password";
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Console.Beep();
        }

        private void dataEntry_TextChanged(object sender, EventArgs e)
        {
            
            dataView.Rows.Clear();
            dataView.Refresh();
            String[] lines = this.dataEntry.Text.Split(new String[] { "\r\n", "\n" }, StringSplitOptions.None);
            String delimeter = getDelimeter(lines);
            for (int x = 0; x < lines.Length; x++)
            {
                String[] line = lines[x].Split(new String[] { delimeter }, StringSplitOptions.None);
                System.Console.WriteLine("Length: " + line.Length);
                this.dataView.Rows.Add(lines[x].Split(new String[] { delimeter }, StringSplitOptions.None));
            }
        }

        void dataEntry_LostFocus(object sender, EventArgs e)
        {
            if (dataEntry.Text.Equals(string.Empty))
            {
                dataEntry.Text = dataEntryHelp;
                dataEntry.ForeColor = Color.Gray;
            }
        }

        void dataEntry_GotFocus(object sender, EventArgs e)
        {
            if (dataEntry.Text.Equals(dataEntryHelp))
            {
                dataEntry.Text = string.Empty;
                dataEntry.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// This function takes in a csv in a string[] format, and determines the delimeter
        /// </summary>
        /// <param name="csvRow"></param>
        /// <returns></returns>
        public String getDelimeter(string[] csvRows)
        {
            // get counts of different possible seperators and compare.
            int num_tabs = 0;
            int num_commas = 0;
            int num_pipes = 0;
            int num_semicolons = 0;

            for (int i = 0; i < 1000 && i < csvRows.Length; i++)
            {
                num_commas += csvRows[i].Count(f => f == ',');
                num_tabs += csvRows[i].Count(f => f == '\t');
                num_pipes += csvRows[i].Count(f => f == '|');
                num_semicolons += csvRows[i].Count(f => f == ';');
            }

            if (num_commas > num_tabs && num_commas > num_pipes && num_commas > num_semicolons)
            {
                return ",";
            }
            else if (num_tabs > num_commas && num_tabs > num_pipes && num_tabs > num_semicolons)
            {
                return "\t";
            }
            else if (num_pipes > num_commas && num_pipes > num_tabs && num_pipes > num_semicolons)
            {
                return "|";
            }
            else
            {
                return ";";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
