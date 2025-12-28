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
            CleanButton = new MaterialSkin.Controls.MaterialButton();
            TrainButton = new MaterialSkin.Controls.MaterialButton();
            RichTextBoxOutput = new RichTextBox();
            panel1 = new Panel();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            LearningRateTextBox = new MaterialSkin.Controls.MaterialTextBox();
            HiddenNeuronsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            EpochsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            ButtonsPannel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonsPannel
            // 
            ButtonsPannel.Controls.Add(CleanButton);
            ButtonsPannel.Controls.Add(TrainButton);
            ButtonsPannel.Dock = DockStyle.Left;
            ButtonsPannel.Location = new Point(0, 0);
            ButtonsPannel.Name = "ButtonsPannel";
            ButtonsPannel.Size = new Size(100, 400);
            ButtonsPannel.TabIndex = 0;
            // 
            // CleanButton
            // 
            CleanButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CleanButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            CleanButton.Depth = 0;
            CleanButton.HighEmphasis = true;
            CleanButton.Icon = null;
            CleanButton.Location = new Point(17, 54);
            CleanButton.Margin = new Padding(4, 6, 4, 6);
            CleanButton.MouseState = MaterialSkin.MouseState.HOVER;
            CleanButton.Name = "CleanButton";
            CleanButton.NoAccentTextColor = Color.Empty;
            CleanButton.Size = new Size(68, 36);
            CleanButton.TabIndex = 1;
            CleanButton.Text = "Clean";
            CleanButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            CleanButton.UseAccentColor = false;
            CleanButton.UseVisualStyleBackColor = true;
            CleanButton.Click += CleanButton_Click;
            // 
            // TrainButton
            // 
            TrainButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TrainButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TrainButton.Depth = 0;
            TrainButton.HighEmphasis = true;
            TrainButton.Icon = null;
            TrainButton.Location = new Point(17, 6);
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
            TrainButton.Click += TrainButton_Click;
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Bottom;
            RichTextBoxOutput.Location = new Point(100, 62);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(600, 338);
            RichTextBoxOutput.TabIndex = 1;
            RichTextBoxOutput.Text = "";
            // 
            // panel1
            // 
            panel1.Controls.Add(materialLabel3);
            panel1.Controls.Add(materialLabel2);
            panel1.Controls.Add(materialLabel1);
            panel1.Controls.Add(LearningRateTextBox);
            panel1.Controls.Add(HiddenNeuronsTextBox);
            panel1.Controls.Add(EpochsTextBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(100, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 62);
            panel1.TabIndex = 2;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(405, 23);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(103, 19);
            materialLabel3.TabIndex = 5;
            materialLabel3.Text = "Learning Rate:";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(166, 23);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(118, 19);
            materialLabel2.TabIndex = 4;
            materialLabel2.Text = "Hidden Neurons:";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(6, 23);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(57, 19);
            materialLabel1.TabIndex = 3;
            materialLabel1.Text = "Epochs:";
            // 
            // LearningRateTextBox
            // 
            LearningRateTextBox.AnimateReadOnly = false;
            LearningRateTextBox.BorderStyle = BorderStyle.None;
            LearningRateTextBox.Depth = 0;
            LearningRateTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            LearningRateTextBox.LeadingIcon = null;
            LearningRateTextBox.Location = new Point(514, 6);
            LearningRateTextBox.MaxLength = 50;
            LearningRateTextBox.MouseState = MaterialSkin.MouseState.OUT;
            LearningRateTextBox.Multiline = false;
            LearningRateTextBox.Name = "LearningRateTextBox";
            LearningRateTextBox.Size = new Size(99, 50);
            LearningRateTextBox.TabIndex = 2;
            LearningRateTextBox.Text = "0.01";
            LearningRateTextBox.TrailingIcon = null;
            // 
            // HiddenNeuronsTextBox
            // 
            HiddenNeuronsTextBox.AnimateReadOnly = false;
            HiddenNeuronsTextBox.BorderStyle = BorderStyle.None;
            HiddenNeuronsTextBox.Depth = 0;
            HiddenNeuronsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            HiddenNeuronsTextBox.LeadingIcon = null;
            HiddenNeuronsTextBox.Location = new Point(290, 3);
            HiddenNeuronsTextBox.MaxLength = 50;
            HiddenNeuronsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            HiddenNeuronsTextBox.Multiline = false;
            HiddenNeuronsTextBox.Name = "HiddenNeuronsTextBox";
            HiddenNeuronsTextBox.Size = new Size(109, 50);
            HiddenNeuronsTextBox.TabIndex = 1;
            HiddenNeuronsTextBox.Text = "25";
            HiddenNeuronsTextBox.TrailingIcon = null;
            // 
            // EpochsTextBox
            // 
            EpochsTextBox.AnimateReadOnly = false;
            EpochsTextBox.BorderStyle = BorderStyle.None;
            EpochsTextBox.Depth = 0;
            EpochsTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            EpochsTextBox.LeadingIcon = null;
            EpochsTextBox.Location = new Point(73, 4);
            EpochsTextBox.MaxLength = 50;
            EpochsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            EpochsTextBox.Multiline = false;
            EpochsTextBox.Name = "EpochsTextBox";
            EpochsTextBox.Size = new Size(87, 50);
            EpochsTextBox.TabIndex = 0;
            EpochsTextBox.Text = "100";
            EpochsTextBox.TrailingIcon = null;
            // 
            // TrainingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(RichTextBoxOutput);
            Controls.Add(ButtonsPannel);
            Name = "TrainingPage";
            Size = new Size(700, 400);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ButtonsPannel;
        private MaterialSkin.Controls.MaterialButton TrainButton;
        private RichTextBox RichTextBoxOutput;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialTextBox EpochsTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox LearningRateTextBox;
        private MaterialSkin.Controls.MaterialTextBox HiddenNeuronsTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialButton CleanButton;
    }
}
