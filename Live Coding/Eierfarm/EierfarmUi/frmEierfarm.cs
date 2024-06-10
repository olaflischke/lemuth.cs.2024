using EierfarmBl;

namespace EierfarmUi
{
    public partial class frmEierfarm : Form
    {
        public frmEierfarm()
        {
            InitializeComponent();
        }

        private void btnNeuesHuhn_Click(object sender, EventArgs e)
        {
            Huhn huhn = new Huhn();

            cbxTiere.Items.Add(huhn);
            cbxTiere.SelectedItem = huhn;
        }

        private void cbxTiere_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgdTier.SelectedObject = cbxTiere.SelectedItem;
        }

        private void btnFuettern_Click(object sender, EventArgs e)
        {
            Huhn huhn = cbxTiere.SelectedItem as Huhn; // Safe-Cast, null, wenn Cast fehlschlägt.
            if (huhn != null)
            {
                huhn.Fressen();
            }
        }

        private void btnEiLegen_Click(object sender, EventArgs e)
        {
            if (cbxTiere.SelectedItem is Huhn huhn)
            {
                huhn.EiLegen();
            }
        }
    }
}
