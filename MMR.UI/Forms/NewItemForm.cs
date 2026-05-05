using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace MMR.UI.Forms;

[SupportedOSPlatform("windows")]
public partial class NewItemForm : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ReturnValue { get; set; }

    public NewItemForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.ReturnValue = newItemText.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
