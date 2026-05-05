using System.Runtime.Versioning;
using System.Windows.Forms;

namespace MMR.UI.Forms;

[SupportedOSPlatform("windows")]
public partial class AboutForm : Form
{
    public AboutForm()
    {
        InitializeComponent();
    }

    private void discordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("https://discord.gg/8qbreUM");
    }
}
