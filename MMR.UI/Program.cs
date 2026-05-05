using MMR.UI.Forms;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace MMR.UI;

[SupportedOSPlatform("windows")]
static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetColorMode(SystemColorMode.Classic); // TODO: Get dark mode to not look like shit in winforms
        Application.Run(new MainForm());
    }
}
