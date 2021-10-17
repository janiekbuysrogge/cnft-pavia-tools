using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sales_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CreateWorld();
        }

        private void CreateWorld()
        {
            // 3 rows of 4 columns
            TableLayoutPanel table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;

            table.ColumnCount = 223;
            table.RowCount = 222;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            // Pavia chart is four quadrants

            // from left to right: -115 to 108 -> 223 width
            // from top to down: 84 to -138 -> 222


            for (int y = 0; y < 223; y++)
            {
                for (int x = 0; x <= 222; x++)
                {
                    //Label lbl = new Label();
                    //lbl.Text = $"{x} {y}";
                    //table.Controls.Add(lbl, x, y);

                    Land land = new Land
                    {
                        X = x,
                        Y = y
                    };
                    LandControl lc = new LandControl(land);
                    table.Controls.Add(lc, x, y);
                }
            }

            this.Controls.Add(table);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
