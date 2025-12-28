namespace WinForm_RFBN_APP
{
    partial class TestingPage
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
            richTextBox1 = new RichTextBox();
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
            ButtonsPannel.Size = new Size(73, 548);
            ButtonsPannel.TabIndex = 1;
            // 
            // TestButton
            // 
            TestButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestButton.Depth = 0;
            TestButton.HighEmphasis = true;
            TestButton.Icon = null;
            TestButton.Location = new Point(4, 6);
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
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(210, 118);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(100, 96);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Location = new Point(73, 0);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(802, 548);
            RichTextBoxOutput.TabIndex = 3;
            RichTextBoxOutput.Text = "";
            // 
            // TestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(RichTextBoxOutput);
            Controls.Add(richTextBox1);
            Controls.Add(ButtonsPannel);
            Name = "TestingPage";
            Size = new Size(875, 548);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion







        #region Data Access ---------------------------------------------------------

        /// <summary>
        /// LoadCsv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Skip(1); // Skip header
            var inputs = new List<double[]>();
            var targets = new List<double>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                // Indexes 0-7 are inputs (8 features), Index 8 is Class
                double[] rowInput = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    rowInput[i] = double.Parse(parts[i]);
                }

                inputs.Add(rowInput);
                targets.Add(double.Parse(parts[8]));
            }

            return (inputs, targets);
        }

        #endregion

        private Panel ButtonsPannel;
        private MaterialSkin.Controls.MaterialButton TestButton;
        private RichTextBox richTextBox1;
        private RichTextBox RichTextBoxOutput;
    }
}
