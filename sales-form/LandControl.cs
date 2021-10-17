using System.Windows.Forms;

namespace sales_form
{
    public partial class LandControl : UserControl
    {        
        public LandControl(Land land)
        {
            InitializeComponent();

            lblTitle.Text = $"{land.X} {land.Y}";
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void SetPrice(decimal price)
        {
            lblPrice.Text = price.ToString();
        }
    }
}
