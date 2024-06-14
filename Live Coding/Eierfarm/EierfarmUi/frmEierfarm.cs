
namespace EierfarmUi;

using EierfarmBl;
using System.Xml.Serialization;

public partial class frmEierfarm : Form
{
    public frmEierfarm()
    {
        InitializeComponent();

    }

    private Huhn MachHuhn()
    {
        Huhn huhn = null;

        if ((DateTime.Now.Minute % 2) == 0)
        {
            huhn = new Huhn();
            return huhn;

        }

        return huhn;
    }


    private void btnNeuesHuhn_Click(object sender, EventArgs e)
    {
        Huhn huhn = new Huhn();

        cbxTiere.Items.Add(huhn);
        cbxTiere.SelectedItem = huhn;
    }

    private void btnNeueGans_Click(object sender, EventArgs e)
    {
        Gans gans = new Gans();

        cbxTiere.Items.Add(gans);
        cbxTiere.SelectedItem = gans;
    }


    private void cbxTiere_SelectedIndexChanged(object sender, EventArgs e)
    {
        pgdTier.SelectedObject = cbxTiere.SelectedItem;
    }

    private void btnFuettern_Click(object sender, EventArgs e)
    {
        Gefluegel? tier = cbxTiere.SelectedItem as Gefluegel; // Safe-Cast, null, wenn Cast fehlschlägt.
        if (tier != null)
        {
            tier.Fressen();
            pgdTier.SelectedObject = tier;
        }
    }

    private void btnEiLegen_Click(object sender, EventArgs e)
    {
        if (cbxTiere.SelectedItem is IEiLeger tier)
        {
            tier.EiLegen();
            pgdTier.SelectedObject = tier;
        }
    }

    private void btnSpeichern_Click(object sender, EventArgs e)
    {
        if (cbxTiere.SelectedItem is IEiLeger tier)
        {
            // Benutzer nach Speicherort fragen
            SaveFileDialog saveFileDialog = new SaveFileDialog()
                                                                {
                                                                    RestoreDirectory = true,
                                                                    Filter = "Hühner|*.hn|Gänse|*.gs|Alles|*.*",
                                                                    FilterIndex = 1
                                                                };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Tier dort speichern
                using StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                XmlSerializer serializer = new XmlSerializer(tier.GetType());
                serializer.Serialize(writer, tier);

                MessageBox.Show(text: "Tier gespeichert.",
                    caption: "Tier speichern",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
        }
    }
}
