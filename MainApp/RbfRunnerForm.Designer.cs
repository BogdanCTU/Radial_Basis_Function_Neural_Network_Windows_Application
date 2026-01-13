using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForm_RFBN_APP
{
    public partial class RbfRunnerForm : MaterialSkin.Controls.MaterialForm
    {

        #region Fields --------------------------------------------------------------

        private UserControl currentPage;

        // PANELS
        private Panel NavigationPanel;
        private Panel ControlPanel;

        // BUTTONS
        private MaterialSkin.Controls.MaterialButton TrainingPageButton;
        private MaterialSkin.Controls.MaterialButton TestingPageButton;
        private MaterialSkin.Controls.MaterialButton ManualTestingPageButton;
        private MaterialSkin.Controls.MaterialButton CrossValidationPageButton;
        private MaterialSkin.Controls.MaterialButton ShapPageButton;
        private MaterialSkin.Controls.MaterialButton GlobalShapPageButton;
        private MaterialSkin.Controls.MaterialButton NetworkVisualisationPageButton;
        private MaterialSkin.Controls.MaterialButton BackPageButton;

        #endregion


        #region Windows Form Designer generated code --------------------------------

        ///<summary>
        ///  Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;

        ///<summary>
        ///  Clean up any resources being used.
        ///</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        ///<summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            NavigationPanel = new Panel();
            TestingPageButton = new MaterialSkin.Controls.MaterialButton();
            TrainingPageButton = new MaterialSkin.Controls.MaterialButton();
            ManualTestingPageButton = new MaterialSkin.Controls.MaterialButton();
            CrossValidationPageButton = new MaterialSkin.Controls.MaterialButton();
            ShapPageButton = new MaterialSkin.Controls.MaterialButton();
            GlobalShapPageButton = new MaterialSkin.Controls.MaterialButton();
            NetworkVisualisationPageButton = new MaterialSkin.Controls.MaterialButton();
            BackPageButton = new MaterialSkin.Controls.MaterialButton();
            ControlPanel = new Panel();
            NavigationPanel.SuspendLayout();
            SuspendLayout();
            // 
            // NavigationPanel
            // 
            NavigationPanel.Controls.Add(TestingPageButton);
            NavigationPanel.Controls.Add(TrainingPageButton);
            NavigationPanel.Controls.Add(ManualTestingPageButton);
            NavigationPanel.Controls.Add(CrossValidationPageButton);
            NavigationPanel.Controls.Add(ShapPageButton);
            NavigationPanel.Controls.Add(GlobalShapPageButton);
            NavigationPanel.Controls.Add(NetworkVisualisationPageButton);
            NavigationPanel.Controls.Add(BackPageButton);
            NavigationPanel.Dock = DockStyle.Top;
            NavigationPanel.Location = new Point(0, 0);
            NavigationPanel.Name = "NavigationPanel";
            NavigationPanel.Size = new Size(1100, 48);
            NavigationPanel.TabIndex = 0;
            // 
            // TestingPageButton
            // 
            TestingPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            TestingPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestingPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestingPageButton.Depth = 0;
            TestingPageButton.HighEmphasis = true;
            TestingPageButton.Icon = null;
            TestingPageButton.Location = new Point(144, 6);
            TestingPageButton.Margin = new Padding(4, 6, 4, 6);
            TestingPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            TestingPageButton.Name = "TestingPageButton";
            TestingPageButton.NoAccentTextColor = Color.Empty;
            TestingPageButton.Size = new Size(122, 36);
            TestingPageButton.TabIndex = 0;
            TestingPageButton.Text = "Testing Page";
            TestingPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TestingPageButton.UseAccentColor = false;
            TestingPageButton.UseVisualStyleBackColor = true;
            TestingPageButton.Click += TestingPageButton_Click;
            // 
            // TrainingPageButton
            // 
            TrainingPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TrainingPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TrainingPageButton.Depth = 0;
            TrainingPageButton.HighEmphasis = true;
            TrainingPageButton.Icon = null;
            TrainingPageButton.Location = new Point(6, 6);
            TrainingPageButton.Margin = new Padding(4, 6, 4, 6);
            TrainingPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            TrainingPageButton.Name = "TrainingPageButton";
            TrainingPageButton.NoAccentTextColor = Color.Empty;
            TrainingPageButton.Size = new Size(130, 36);
            TrainingPageButton.TabIndex = 1;
            TrainingPageButton.Text = "Training Page";
            TrainingPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TrainingPageButton.UseAccentColor = false;
            TrainingPageButton.UseVisualStyleBackColor = true;
            TrainingPageButton.Click += TrainingPageButton_Click;
            // 
            // ManualTestingPageButton
            // 
            ManualTestingPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ManualTestingPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ManualTestingPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            ManualTestingPageButton.Depth = 0;
            ManualTestingPageButton.HighEmphasis = true;
            ManualTestingPageButton.Icon = null;
            ManualTestingPageButton.Location = new Point(274, 6);
            ManualTestingPageButton.Margin = new Padding(4, 6, 4, 6);
            ManualTestingPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            ManualTestingPageButton.Name = "ManualTestingPageButton";
            ManualTestingPageButton.NoAccentTextColor = Color.Empty;
            ManualTestingPageButton.Size = new Size(145, 36);
            ManualTestingPageButton.TabIndex = 2;
            ManualTestingPageButton.Text = "Manual Testing\r\nPage";
            ManualTestingPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            ManualTestingPageButton.UseAccentColor = false;
            ManualTestingPageButton.UseVisualStyleBackColor = true;
            ManualTestingPageButton.Click += ManualTestingPageButton_Click;
            // 
            // CrossValidationPageButton
            // 
            CrossValidationPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CrossValidationPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            CrossValidationPageButton.Depth = 0;
            CrossValidationPageButton.HighEmphasis = true;
            CrossValidationPageButton.Icon = null;
            CrossValidationPageButton.Location = new Point(427, 6);
            CrossValidationPageButton.Margin = new Padding(4, 6, 4, 6);
            CrossValidationPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            CrossValidationPageButton.Name = "CrossValidationPageButton";
            CrossValidationPageButton.NoAccentTextColor = Color.Empty;
            CrossValidationPageButton.Size = new Size(156, 36);
            CrossValidationPageButton.TabIndex = 4;
            CrossValidationPageButton.Text = "Cross Validation";
            CrossValidationPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            CrossValidationPageButton.UseAccentColor = false;
            CrossValidationPageButton.UseVisualStyleBackColor = true;
            CrossValidationPageButton.Click += CrossValidationPageButton_Click;
            // 
            // ShapPageButton
            // 
            ShapPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ShapPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            ShapPageButton.Depth = 0;
            ShapPageButton.HighEmphasis = true;
            ShapPageButton.Icon = null;
            ShapPageButton.Location = new Point(591, 6);
            ShapPageButton.Margin = new Padding(4, 6, 4, 6);
            ShapPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            ShapPageButton.Name = "ShapPageButton";
            ShapPageButton.NoAccentTextColor = Color.Empty;
            ShapPageButton.Size = new Size(141, 36);
            ShapPageButton.TabIndex = 5;
            ShapPageButton.Text = "SHAP Explainer";
            ShapPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            ShapPageButton.UseAccentColor = false;
            ShapPageButton.UseVisualStyleBackColor = true;
            ShapPageButton.Click += ShapPageButton_Click;
            // 
            // GlobalShapPageButton
            // 
            GlobalShapPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GlobalShapPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            GlobalShapPageButton.Depth = 0;
            GlobalShapPageButton.HighEmphasis = true;
            GlobalShapPageButton.Icon = null;
            GlobalShapPageButton.Location = new Point(740, 6);
            GlobalShapPageButton.Margin = new Padding(4, 6, 4, 6);
            GlobalShapPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            GlobalShapPageButton.Name = "GlobalShapPageButton";
            GlobalShapPageButton.NoAccentTextColor = Color.Empty;
            GlobalShapPageButton.Size = new Size(119, 36);
            GlobalShapPageButton.TabIndex = 6;
            GlobalShapPageButton.Text = "Global SHAP";
            GlobalShapPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            GlobalShapPageButton.UseAccentColor = false;
            GlobalShapPageButton.UseVisualStyleBackColor = true;
            GlobalShapPageButton.Click += GlobalShapPageButton_Click;
            // 
            // NetworkVisualisationPageButton
            // 
            NetworkVisualisationPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            NetworkVisualisationPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            NetworkVisualisationPageButton.Depth = 0;
            NetworkVisualisationPageButton.HighEmphasis = true;
            NetworkVisualisationPageButton.Icon = null;
            NetworkVisualisationPageButton.Location = new Point(865, 6);
            NetworkVisualisationPageButton.Margin = new Padding(4, 6, 4, 6);
            NetworkVisualisationPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            NetworkVisualisationPageButton.Name = "NetworkVisualisationPageButton";
            NetworkVisualisationPageButton.NoAccentTextColor = Color.Empty;
            NetworkVisualisationPageButton.Size = new Size(119, 36);
            NetworkVisualisationPageButton.TabIndex = 7;
            NetworkVisualisationPageButton.Text = "Visualize";
            NetworkVisualisationPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            NetworkVisualisationPageButton.UseAccentColor = false;
            NetworkVisualisationPageButton.UseVisualStyleBackColor = true;
            NetworkVisualisationPageButton.Click += NetworkVisualisationPageButton_Click;
            // 
            // BackPageButton
            // 
            BackPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackPageButton.AutoSize = false;
            BackPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BackPageButton.Depth = 0;
            BackPageButton.HighEmphasis = true;
            BackPageButton.Icon = null;
            BackPageButton.Location = new Point(995, 2);
            BackPageButton.Margin = new Padding(4, 6, 4, 6);
            BackPageButton.MouseState = MaterialSkin.MouseState.HOVER;
            BackPageButton.Name = "BackPageButton";
            BackPageButton.NoAccentTextColor = Color.Empty;
            BackPageButton.Size = new Size(80, 36);
            BackPageButton.TabIndex = 18;
            BackPageButton.Text = "Back";
            BackPageButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            BackPageButton.UseAccentColor = false;
            BackPageButton.UseVisualStyleBackColor = true;
            BackPageButton.Click += BackPageButton_Click;
            // 
            // ControlPanel
            // 
            ControlPanel.Dock = DockStyle.Fill;
            ControlPanel.Location = new Point(0, 48);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Size = new Size(984, 713);
            ControlPanel.TabIndex = 1;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 800);
            Controls.Add(ControlPanel);
            Controls.Add(NavigationPanel);
            Name = "RbfRunnerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Machine Learning Demo V1";
            NavigationPanel.ResumeLayout(false);
            NavigationPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        #region Buttons -------------------------------------------------------------

        ///<summary>
        /// Handles the click event for the Training Page button.
        /// Loads the TrainingPage and updates button states.
        ///</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void TrainingPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new TrainingPage(), TrainingPageButton);
        }

        ///<summary>
        /// Handles the click event for the Testing Page button.
        /// Loads the TestingPage and updates button states.
        ///</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void TestingPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new TestingPage(), TestingPageButton);
        }

        ///<summary>
        /// Handles the click event for the Manual Testing Page button.
        /// Loads the ManualTestingPage and updates button states.
        ///</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ManualTestingPageButton_Click(object sender, EventArgs e)
        {
        // Corrected: Now passes ManualTestingPageButton instead of TestingPageButton
            LoadPageWithButtonControl(new ManualTestingPage(), ManualTestingPageButton);
        }

        ///<summary>
        /// Handles the click event for the Cross Validation Page button.
        ///</summary>
        private void CrossValidationPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new CrossValidationPage(), CrossValidationPageButton);
        }

        ///<summary>
        /// Handles the click event for the SHAP Page button.
        ///</summary>
        private void ShapPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new ShapPage(), ShapPageButton);
        }

        ///<summary>
        /// Handles the click event for the SHAP Page button.
        ///</summary>
        private void GlobalShapPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new GlobalShapPage(), GlobalShapPageButton);
        }

        ///<summary>
        /// Handles the click event for the Network Visualization Page button.
        ///</summary>
        private void NetworkVisualisationPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new NetworkVisualizationPage(), NetworkVisualisationPageButton);
        }

        private void BackPageButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainScreen().ShowDialog();
            this.Close();
        }
        #endregion

        #region Logics --------------------------------------------------------------

        ///<summary>
        /// Clears the ControlPanel and loads the specified UserControl into it.
        /// Sets the current page reference to the new page.
        ///</summary>
        /// <param name="page">The UserControl to load.</param>
        private void LoadPage(UserControl page)
        {
            ControlPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            ControlPanel.Controls.Add(page);
            currentPage = page;
        }

        ///<summary>
        /// Manages the navigation logic by updating the visual state of buttons and loading the requested page.
        /// Automatically enables all other navigation buttons and disables the clicked button.
        ///</summary>
        /// <param name="page">The UserControl to display.</param>
        /// <param name="clickedButton">The MaterialButton that triggered the navigation.</param>
        private void LoadPageWithButtonControl(UserControl page, MaterialSkin.Controls.MaterialButton clickedButton)
        {
            // Checks if the requested page type is already the current page to prevent unnecessary reloading
            if (currentPage != null && currentPage.GetType() == page.GetType())
                return;

            // Iterates through all controls in the NavigationPanel to reset their state.
            // This ensures that any new buttons added to the panel are automatically handled 
            // without modifying this logic.
            foreach (Control control in NavigationPanel.Controls)
            {
                if (control is MaterialSkin.Controls.MaterialButton button)
                {
                    button.Enabled = true;
                }
            }

            // Disables the specific button that was clicked to indicate the active page
            clickedButton.Enabled = false;

            // Proceeds to load the content of the page
            LoadPage(page);
        }

        #endregion

    }
}