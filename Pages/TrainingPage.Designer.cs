namespace WinForm_RFBN_APP
{
    partial class TrainingPage
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
            ViewerPannel = new Panel();
            TrainButton = new MaterialSkin.Controls.MaterialButton();
            RichTextBoxOutput = new RichTextBox();
            ButtonsPannel.SuspendLayout();
            ViewerPannel.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonsPannel
            // 
            ButtonsPannel.Controls.Add(TrainButton);
            ButtonsPannel.Dock = DockStyle.Left;
            ButtonsPannel.Location = new Point(0, 0);
            ButtonsPannel.Name = "ButtonsPannel";
            ButtonsPannel.Size = new Size(73, 444);
            ButtonsPannel.TabIndex = 0;
            // 
            // ViewerPannel
            // 
            ViewerPannel.Controls.Add(RichTextBoxOutput);
            ViewerPannel.Dock = DockStyle.Fill;
            ViewerPannel.Location = new Point(73, 0);
            ViewerPannel.Name = "ViewerPannel";
            ViewerPannel.Size = new Size(636, 444);
            ViewerPannel.TabIndex = 1;
            // 
            // TrainButton
            // 
            TrainButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TrainButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TrainButton.Depth = 0;
            TrainButton.HighEmphasis = true;
            TrainButton.Icon = null;
            TrainButton.Location = new Point(4, 6);
            TrainButton.Margin = new Padding(4, 6, 4, 6);
            TrainButton.MouseState = MaterialSkin.MouseState.HOVER;
            TrainButton.Name = "TrainButton";
            TrainButton.NoAccentTextColor = Color.Empty;
            TrainButton.Size = new Size(64, 36);
            TrainButton.TabIndex = 0;
            TrainButton.Text = "Train";
            TrainButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TrainButton.UseAccentColor = false;
            TrainButton.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Location = new Point(0, 0);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(636, 444);
            RichTextBoxOutput.TabIndex = 0;
            RichTextBoxOutput.Text = "";
            // 
            // TrainingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ViewerPannel);
            Controls.Add(ButtonsPannel);
            Name = "TrainingPage";
            Size = new Size(709, 444);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            ViewerPannel.ResumeLayout(false);
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
        private Panel ViewerPannel;
        private MaterialSkin.Controls.MaterialButton TrainButton;
        private RichTextBox RichTextBoxOutput;
    }
}
