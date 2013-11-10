using System.Windows.Forms;
using Alteration.Details;

namespace Alteration.Forms.Dialog
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            lblVersion.Text = "v " + AlterationDetails.Version;
        }
    }
}