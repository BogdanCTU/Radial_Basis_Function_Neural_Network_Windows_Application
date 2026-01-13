using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace WinForm_RFBN_APP
{
    public partial class RbfRunnerForm : MaterialForm
    {
        public RbfRunnerForm()
        {
            InitializeComponent();
            
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            // Unified Design: Match SelectionForm (Blue)
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700,
                Primary.Blue200, Accent.LightBlue200,
                TextShade.WHITE
            );
        }
    }
}
