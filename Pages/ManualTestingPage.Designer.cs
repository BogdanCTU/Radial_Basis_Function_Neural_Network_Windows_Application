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
            TestCustomButton = new MaterialSkin.Controls.MaterialButton();
            TestFullButton = new MaterialSkin.Controls.MaterialButton();
            RichTextBoxOutput = new RichTextBox();
            panel1 = new Panel();
            KiloCaloriesTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            FiberTextBox = new MaterialSkin.Controls.MaterialTextBox();
            SugarTextBox = new MaterialSkin.Controls.MaterialTextBox();
            CarbohydratesTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            SaturatedFatTextBox = new MaterialSkin.Controls.MaterialTextBox();
            TotalFatTextBox = new MaterialSkin.Controls.MaterialTextBox();
            ProteinTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            FullInputTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            ButtonsPannel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ButtonsPannel
            // 
            ButtonsPannel.Controls.Add(TestCustomButton);
            ButtonsPannel.Controls.Add(TestFullButton);
            ButtonsPannel.Dock = DockStyle.Left;
            ButtonsPannel.Location = new Point(0, 0);
            ButtonsPannel.Name = "ButtonsPannel";
            ButtonsPannel.Size = new Size(100, 400);
            ButtonsPannel.TabIndex = 1;
            // 
            // TestCustomButton
            // 
            TestCustomButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestCustomButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestCustomButton.Depth = 0;
            TestCustomButton.HighEmphasis = true;
            TestCustomButton.Icon = null;
            TestCustomButton.Location = new Point(9, 81);
            TestCustomButton.Margin = new Padding(4, 6, 4, 6);
            TestCustomButton.MouseState = MaterialSkin.MouseState.HOVER;
            TestCustomButton.Name = "TestCustomButton";
            TestCustomButton.NoAccentTextColor = Color.Empty;
            TestCustomButton.Size = new Size(81, 36);
            TestCustomButton.TabIndex = 1;
            TestCustomButton.Text = "Test\r\nCustom";
            TestCustomButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TestCustomButton.UseAccentColor = false;
            TestCustomButton.UseVisualStyleBackColor = true;
            TestCustomButton.Click += TestCustomButton_Click;
            // 
            // TestFullButton
            // 
            TestFullButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestFullButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestFullButton.Depth = 0;
            TestFullButton.HighEmphasis = true;
            TestFullButton.Icon = null;
            TestFullButton.Location = new Point(17, 6);
            TestFullButton.Margin = new Padding(4, 6, 4, 6);
            TestFullButton.MouseState = MaterialSkin.MouseState.HOVER;
            TestFullButton.Name = "TestFullButton";
            TestFullButton.NoAccentTextColor = Color.Empty;
            TestFullButton.Size = new Size(64, 36);
            TestFullButton.TabIndex = 0;
            TestFullButton.Text = "Test\r\nFull";
            TestFullButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            TestFullButton.UseAccentColor = false;
            TestFullButton.UseVisualStyleBackColor = true;
            TestFullButton.Click += TestFullButton_Click;
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Bottom;
            RichTextBoxOutput.Location = new Point(100, 338);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(600, 62);
            RichTextBoxOutput.TabIndex = 3;
            RichTextBoxOutput.Text = "";
            // 
            // panel1
            // 
            panel1.Controls.Add(KiloCaloriesTextBox);
            panel1.Controls.Add(materialLabel9);
            panel1.Controls.Add(materialLabel6);
            panel1.Controls.Add(materialLabel7);
            panel1.Controls.Add(FiberTextBox);
            panel1.Controls.Add(SugarTextBox);
            panel1.Controls.Add(CarbohydratesTextBox);
            panel1.Controls.Add(materialLabel8);
            panel1.Controls.Add(materialLabel5);
            panel1.Controls.Add(materialLabel4);
            panel1.Controls.Add(SaturatedFatTextBox);
            panel1.Controls.Add(TotalFatTextBox);
            panel1.Controls.Add(ProteinTextBox);
            panel1.Controls.Add(materialLabel3);
            panel1.Controls.Add(materialLabel2);
            panel1.Controls.Add(FullInputTextBox);
            panel1.Controls.Add(materialLabel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(100, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 338);
            panel1.TabIndex = 4;
            // 
            // KiloCaloriesTextBox
            // 
            KiloCaloriesTextBox.AnimateReadOnly = false;
            KiloCaloriesTextBox.BorderStyle = BorderStyle.None;
            KiloCaloriesTextBox.Depth = 0;
            KiloCaloriesTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            KiloCaloriesTextBox.LeadingIcon = null;
            KiloCaloriesTextBox.Location = new Point(0, 282);
            KiloCaloriesTextBox.MaxLength = 50;
            KiloCaloriesTextBox.MouseState = MaterialSkin.MouseState.OUT;
            KiloCaloriesTextBox.Multiline = false;
            KiloCaloriesTextBox.Name = "KiloCaloriesTextBox";
            KiloCaloriesTextBox.Size = new Size(195, 50);
            KiloCaloriesTextBox.TabIndex = 16;
            KiloCaloriesTextBox.Text = "-0.37";
            KiloCaloriesTextBox.TrailingIcon = null;
            // 
            // materialLabel9
            // 
            materialLabel9.AutoSize = true;
            materialLabel9.Depth = 0;
            materialLabel9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel9.Location = new Point(55, 260);
            materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel9.Name = "materialLabel9";
            materialLabel9.Size = new Size(89, 19);
            materialLabel9.TabIndex = 15;
            materialLabel9.Text = "Kilo Calories";
            // 
            // materialLabel6
            // 
            materialLabel6.AutoSize = true;
            materialLabel6.Depth = 0;
            materialLabel6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel6.Location = new Point(487, 182);
            materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel6.Name = "materialLabel6";
            materialLabel6.Size = new Size(36, 19);
            materialLabel6.TabIndex = 14;
            materialLabel6.Text = "Fiber";
            // 
            // materialLabel7
            // 
            materialLabel7.AutoSize = true;
            materialLabel7.Depth = 0;
            materialLabel7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel7.Location = new Point(276, 182);
            materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel7.Name = "materialLabel7";
            materialLabel7.Size = new Size(43, 19);
            materialLabel7.TabIndex = 13;
            materialLabel7.Text = "Sugar";
            // 
            // FiberTextBox
            // 
            FiberTextBox.AnimateReadOnly = false;
            FiberTextBox.BorderStyle = BorderStyle.None;
            FiberTextBox.Depth = 0;
            FiberTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            FiberTextBox.LeadingIcon = null;
            FiberTextBox.Location = new Point(401, 204);
            FiberTextBox.MaxLength = 50;
            FiberTextBox.MouseState = MaterialSkin.MouseState.OUT;
            FiberTextBox.Multiline = false;
            FiberTextBox.Name = "FiberTextBox";
            FiberTextBox.Size = new Size(195, 50);
            FiberTextBox.TabIndex = 12;
            FiberTextBox.Text = "-0.75";
            FiberTextBox.TrailingIcon = null;
            // 
            // SugarTextBox
            // 
            SugarTextBox.AnimateReadOnly = false;
            SugarTextBox.BorderStyle = BorderStyle.None;
            SugarTextBox.Depth = 0;
            SugarTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SugarTextBox.LeadingIcon = null;
            SugarTextBox.Location = new Point(200, 204);
            SugarTextBox.MaxLength = 50;
            SugarTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SugarTextBox.Multiline = false;
            SugarTextBox.Name = "SugarTextBox";
            SugarTextBox.Size = new Size(195, 50);
            SugarTextBox.TabIndex = 11;
            SugarTextBox.Text = "-0.87";
            SugarTextBox.TrailingIcon = null;
            // 
            // CarbohydratesTextBox
            // 
            CarbohydratesTextBox.AnimateReadOnly = false;
            CarbohydratesTextBox.BorderStyle = BorderStyle.None;
            CarbohydratesTextBox.Depth = 0;
            CarbohydratesTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CarbohydratesTextBox.LeadingIcon = null;
            CarbohydratesTextBox.Location = new Point(0, 204);
            CarbohydratesTextBox.MaxLength = 50;
            CarbohydratesTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CarbohydratesTextBox.Multiline = false;
            CarbohydratesTextBox.Name = "CarbohydratesTextBox";
            CarbohydratesTextBox.Size = new Size(195, 50);
            CarbohydratesTextBox.TabIndex = 10;
            CarbohydratesTextBox.Text = "1.50";
            CarbohydratesTextBox.TrailingIcon = null;
            // 
            // materialLabel8
            // 
            materialLabel8.AutoSize = true;
            materialLabel8.Depth = 0;
            materialLabel8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel8.Location = new Point(47, 182);
            materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel8.Name = "materialLabel8";
            materialLabel8.Size = new Size(104, 19);
            materialLabel8.TabIndex = 9;
            materialLabel8.Text = "Carbohydrates";
            // 
            // materialLabel5
            // 
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel5.Location = new Point(456, 105);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(97, 19);
            materialLabel5.TabIndex = 8;
            materialLabel5.Text = "Saturated Fat";
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel4.Location = new Point(266, 105);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(65, 19);
            materialLabel4.TabIndex = 7;
            materialLabel4.Text = "Total Fat";
            // 
            // SaturatedFatTextBox
            // 
            SaturatedFatTextBox.AnimateReadOnly = false;
            SaturatedFatTextBox.BorderStyle = BorderStyle.None;
            SaturatedFatTextBox.Depth = 0;
            SaturatedFatTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SaturatedFatTextBox.LeadingIcon = null;
            SaturatedFatTextBox.Location = new Point(401, 127);
            SaturatedFatTextBox.MaxLength = 50;
            SaturatedFatTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SaturatedFatTextBox.Multiline = false;
            SaturatedFatTextBox.Name = "SaturatedFatTextBox";
            SaturatedFatTextBox.Size = new Size(195, 50);
            SaturatedFatTextBox.TabIndex = 6;
            SaturatedFatTextBox.Text = "-0.81";
            SaturatedFatTextBox.TrailingIcon = null;
            // 
            // TotalFatTextBox
            // 
            TotalFatTextBox.AnimateReadOnly = false;
            TotalFatTextBox.BorderStyle = BorderStyle.None;
            TotalFatTextBox.Depth = 0;
            TotalFatTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            TotalFatTextBox.LeadingIcon = null;
            TotalFatTextBox.Location = new Point(200, 127);
            TotalFatTextBox.MaxLength = 50;
            TotalFatTextBox.MouseState = MaterialSkin.MouseState.OUT;
            TotalFatTextBox.Multiline = false;
            TotalFatTextBox.Name = "TotalFatTextBox";
            TotalFatTextBox.Size = new Size(195, 50);
            TotalFatTextBox.TabIndex = 5;
            TotalFatTextBox.Text = "-1.02";
            TotalFatTextBox.TrailingIcon = null;
            // 
            // ProteinTextBox
            // 
            ProteinTextBox.AnimateReadOnly = false;
            ProteinTextBox.BorderStyle = BorderStyle.None;
            ProteinTextBox.Depth = 0;
            ProteinTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            ProteinTextBox.LeadingIcon = null;
            ProteinTextBox.Location = new Point(0, 127);
            ProteinTextBox.MaxLength = 50;
            ProteinTextBox.MouseState = MaterialSkin.MouseState.OUT;
            ProteinTextBox.Multiline = false;
            ProteinTextBox.Name = "ProteinTextBox";
            ProteinTextBox.Size = new Size(195, 50);
            ProteinTextBox.TabIndex = 4;
            ProteinTextBox.Text = "-0.44";
            ProteinTextBox.TrailingIcon = null;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(73, 105);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(51, 19);
            materialLabel3.TabIndex = 3;
            materialLabel3.Text = "Protein";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(6, 81);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(100, 19);
            materialLabel2.TabIndex = 2;
            materialLabel2.Text = "Custom input:";
            // 
            // FullInputTextBox
            // 
            FullInputTextBox.AnimateReadOnly = false;
            FullInputTextBox.BorderStyle = BorderStyle.None;
            FullInputTextBox.Depth = 0;
            FullInputTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            FullInputTextBox.LeadingIcon = null;
            FullInputTextBox.Location = new Point(6, 28);
            FullInputTextBox.MaxLength = 50;
            FullInputTextBox.MouseState = MaterialSkin.MouseState.OUT;
            FullInputTextBox.Multiline = false;
            FullInputTextBox.Name = "FullInputTextBox";
            FullInputTextBox.Size = new Size(594, 50);
            FullInputTextBox.TabIndex = 1;
            FullInputTextBox.Text = "-0.44;-1.02;1.50;-0.37;-0.75;-0.81;-0.87";
            FullInputTextBox.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(6, 6);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(71, 19);
            materialLabel1.TabIndex = 0;
            materialLabel1.Text = "Full input:";
            // 
            // ManualTestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(RichTextBoxOutput);
            Controls.Add(ButtonsPannel);
            Name = "ManualTestingPage";
            Size = new Size(700, 400);
            ButtonsPannel.ResumeLayout(false);
            ButtonsPannel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel ButtonsPannel;
        private MaterialSkin.Controls.MaterialButton TestFullButton;
        private RichTextBox RichTextBoxOutput;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox FullInputTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox ProteinTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialTextBox SaturatedFatTextBox;
        private MaterialSkin.Controls.MaterialTextBox TotalFatTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialTextBox FiberTextBox;
        private MaterialSkin.Controls.MaterialTextBox SugarTextBox;
        private MaterialSkin.Controls.MaterialTextBox CarbohydratesTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialTextBox KiloCaloriesTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialButton TestCustomButton;
    }
}
