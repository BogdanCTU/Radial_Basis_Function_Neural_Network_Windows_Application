namespace WinForm_RFBN_APP
{
    partial class ManualTestingPage
    {

        #region Component Designer generated code -----------------------------------

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonsPannel = new Panel();
            TestButton = new MaterialSkin.Controls.MaterialButton();
            RichTextBoxOutput = new RichTextBox();
            ButtonsPannel.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonsPannel
            // 
            ButtonsPannel.Controls.Add(TestButton);
            ButtonsPannel.Dock = DockStyle.Left;
            ButtonsPannel.Location = new Point(0, 0);
            ButtonsPannel.Name = "ButtonsPannel";
            ButtonsPannel.Size = new Size(100, 400);
            ButtonsPannel.TabIndex = 1;
            // 
            // TestButton
            // 
            TestButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestButton.Depth = 0;
            TestButton.HighEmphasis = true;
            TestButton.Icon = null;
            TestButton.Location = new Point(18, 6);
            TestButton.Margin = new Padding(4, 6, 4, 6);
            TestButton.MouseState = MaterialSkin.MouseState.HOVER;
            TestButton.Name = "TestButton";
            TestButton.NoAccentTextColor = Color.Empty;
            TestButton.Size = new Size(64, 36);
            TestButton.TabIndex = 0;
            TestButton.Text = "Test";
            TestButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TestButton.UseAccentColor = false;
            TestButton.UseVisualStyleBackColor = true;
            TestButton.Click += TestButton_Click;
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Location = new Point(100, 0);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(600, 400);
            RichTextBoxOutput.TabIndex = 3;
            RichTextBoxOutput.Text = "";
            // 
            // TestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(RichTextBoxOutput);
            Controls.Add(ButtonsPannel);
            Name = "TestingPage";
            Size = new Size(700, 400);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ButtonsPannel;
        private MaterialSkin.Controls.MaterialButton TestButton;
        private RichTextBox RichTextBoxOutput;
    }
}
