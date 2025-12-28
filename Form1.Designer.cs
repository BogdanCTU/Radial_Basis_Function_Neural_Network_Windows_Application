namespace WinForm_RFBN_APP
{
    partial class Form1
    {

        #region Windows Form Designer generated code ----------------------------------------

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NavigationPanel = new Panel();
            ControlPanel = new Panel();
            TestingPageButton = new MaterialSkin.Controls.MaterialButton();
            TrainingPageButton = new MaterialSkin.Controls.MaterialButton();
            NavigationPanel.SuspendLayout();
            SuspendLayout();
            // 
            // NavigationPanel
            // 
            NavigationPanel.Controls.Add(TrainingPageButton);
            NavigationPanel.Controls.Add(TestingPageButton);
            NavigationPanel.Dock = DockStyle.Left;
            NavigationPanel.Location = new Point(0, 0);
            NavigationPanel.Name = "NavigationPanel";
            NavigationPanel.Size = new Size(154, 450);
            NavigationPanel.TabIndex = 0;
            // 
            // ControlPanel
            // 
            ControlPanel.Dock = DockStyle.Fill;
            ControlPanel.Location = new Point(154, 0);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Size = new Size(646, 450);
            ControlPanel.TabIndex = 1;
            // 
            // TestingPageButton
            // 
            TestingPageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TestingPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestingPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestingPageButton.Depth = 0;
            TestingPageButton.HighEmphasis = true;
            TestingPageButton.Icon = null;
            TestingPageButton.Location = new Point(13, 63);
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
            // 
            // TrainingPageButton
            // 
            TrainingPageButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TrainingPageButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TrainingPageButton.Depth = 0;
            TrainingPageButton.HighEmphasis = true;
            TrainingPageButton.Icon = null;
            TrainingPageButton.Location = new Point(13, 15);
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
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ControlPanel);
            Controls.Add(NavigationPanel);
            Name = "Form1";
            Text = "Form1";
            NavigationPanel.ResumeLayout(false);
            NavigationPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        #region Fields --------------------------------------------------------------

        private UserControl currentPage;

        // PANNELS
        private Panel NavigationPanel;
        private Panel ControlPanel;

        // BUTTONS
        private MaterialSkin.Controls.MaterialButton TrainingPageButton;
        private MaterialSkin.Controls.MaterialButton TestingPageButton;

        #endregion


        #region Buttons -------------------------------------------------------------

        private void TrainingPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new TrainingPage(), TrainingPageButton);
        }

        private void TestingPageButton_Click(object sender, EventArgs e)
        {
            LoadPageWithButtonControl(new TestingPage(), TestingPageButton);
        }

        #endregion


        #region Logics --------------------------------------------------------------

        private void LoadPage(UserControl page)
        {
            ControlPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            ControlPanel.Controls.Add(page);
            currentPage = page;
        }

        private void LoadPageWithButtonControl(UserControl page, MaterialSkin.Controls.MaterialButton clickedButton)
        {
            // If the same page is already loaded, do nothing
            if (currentPage != null && currentPage.GetType() == page.GetType())
                return;

            // Enable all buttons first
            TrainingPageButton.Enabled = true;
            TestingPageButton.Enabled = true;

            // Disable the clicked button
            clickedButton.Enabled = false;

            // Load the page
            LoadPage(page);
        }

        #endregion

    }
}
