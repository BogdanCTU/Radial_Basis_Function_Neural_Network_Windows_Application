namespace WinForm_RFBN_APP
{
    partial class ManualTestingPage
    {

        #region Component Designer generated code -----------------------------------

        ///<summary> 
        /// Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;

        ///<summary> 
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            MainLayout = new TableLayoutPanel();
            InputGroupBox = new GroupBox();
            InputLayout = new TableLayoutPanel();
            KiloCaloriesTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            FullInputTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            ProteinTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            TotalFatTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            SaturatedFatTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            CarbohydratesTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            SugarTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            FiberTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            SaltTextBox = new MaterialSkin.Controls.MaterialTextBox();
            materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            TestFullButton = new MaterialSkin.Controls.MaterialButton();
            TestCustomButton = new MaterialSkin.Controls.MaterialButton();
            OutputGroupBox = new GroupBox();
            RichTextBoxOutput = new RichTextBox();
            MainLayout.SuspendLayout();
            InputGroupBox.SuspendLayout();
            InputLayout.SuspendLayout();
            ActionsGroupBox.SuspendLayout();
            ActionsLayout.SuspendLayout();
            OutputGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.ColumnCount = 1;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayout.Controls.Add(InputGroupBox, 0, 0);
            MainLayout.Controls.Add(ActionsGroupBox, 0, 1);
            MainLayout.Controls.Add(OutputGroupBox, 0, 2);
            MainLayout.Dock = DockStyle.Fill;
            MainLayout.Location = new Point(0, 0);
            MainLayout.Name = "MainLayout";
            MainLayout.RowCount = 3;
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 340F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.Size = new Size(700, 600);
            MainLayout.TabIndex = 0;
            // 
            // InputGroupBox
            // 
            InputGroupBox.Controls.Add(InputLayout);
            InputGroupBox.Dock = DockStyle.Fill;
            InputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            InputGroupBox.Location = new Point(3, 3);
            InputGroupBox.Name = "InputGroupBox";
            InputGroupBox.Size = new Size(694, 334);
            InputGroupBox.TabIndex = 0;
            InputGroupBox.TabStop = false;
            InputGroupBox.Text = "Manual Input";
            // 
            // InputLayout
            // 
            InputLayout.ColumnCount = 4;
            InputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            InputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            InputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InputLayout.Controls.Add(KiloCaloriesTextBox, 3, 5);
            InputLayout.Controls.Add(materialLabel1, 0, 0);
            InputLayout.Controls.Add(FullInputTextBox, 1, 0);
            InputLayout.Controls.Add(materialLabel2, 0, 1);
            InputLayout.Controls.Add(ProteinTextBox, 1, 2);
            InputLayout.Controls.Add(materialLabel4, 2, 2);
            InputLayout.Controls.Add(TotalFatTextBox, 3, 2);
            InputLayout.Controls.Add(materialLabel5, 0, 3);
            InputLayout.Controls.Add(SaturatedFatTextBox, 1, 3);
            InputLayout.Controls.Add(materialLabel8, 2, 3);
            InputLayout.Controls.Add(CarbohydratesTextBox, 3, 3);
            InputLayout.Controls.Add(materialLabel7, 0, 4);
            InputLayout.Controls.Add(SugarTextBox, 1, 4);
            InputLayout.Controls.Add(materialLabel6, 2, 4);
            InputLayout.Controls.Add(FiberTextBox, 3, 4);
            InputLayout.Controls.Add(materialLabel9, 0, 5);
            InputLayout.Controls.Add(SaltTextBox, 1, 5);
            InputLayout.Controls.Add(materialLabel10, 2, 5);
            InputLayout.Controls.Add(materialLabel3, 0, 2);
            InputLayout.Dock = DockStyle.Fill;
            InputLayout.Location = new Point(3, 21);
            InputLayout.Name = "InputLayout";
            InputLayout.RowCount = 6;
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            InputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            InputLayout.Size = new Size(688, 310);
            InputLayout.TabIndex = 0;
            // 
            // KiloCaloriesTextBox
            // 
            KiloCaloriesTextBox.AnimateReadOnly = false;
            KiloCaloriesTextBox.BorderStyle = BorderStyle.None;
            KiloCaloriesTextBox.Depth = 0;
            KiloCaloriesTextBox.Dock = DockStyle.Fill;
            KiloCaloriesTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            KiloCaloriesTextBox.LeadingIcon = null;
            KiloCaloriesTextBox.Location = new Point(467, 253);
            KiloCaloriesTextBox.MaxLength = 50;
            KiloCaloriesTextBox.MouseState = MaterialSkin.MouseState.OUT;
            KiloCaloriesTextBox.Multiline = false;
            KiloCaloriesTextBox.Name = "KiloCaloriesTextBox";
            KiloCaloriesTextBox.Size = new Size(218, 50);
            KiloCaloriesTextBox.TabIndex = 18;
            KiloCaloriesTextBox.Text = "113.0";
            KiloCaloriesTextBox.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            materialLabel1.Anchor = AnchorStyles.Left;
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(3, 18);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(71, 19);
            materialLabel1.TabIndex = 0;
            materialLabel1.Text = "Full input:";
            // 
            // FullInputTextBox
            // 
            FullInputTextBox.AnimateReadOnly = false;
            FullInputTextBox.BorderStyle = BorderStyle.None;
            InputLayout.SetColumnSpan(FullInputTextBox, 3);
            FullInputTextBox.Depth = 0;
            FullInputTextBox.Dock = DockStyle.Fill;
            FullInputTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            FullInputTextBox.LeadingIcon = null;
            FullInputTextBox.Location = new Point(123, 3);
            FullInputTextBox.MaxLength = 50;
            FullInputTextBox.MouseState = MaterialSkin.MouseState.OUT;
            FullInputTextBox.Multiline = false;
            FullInputTextBox.Name = "FullInputTextBox";
            FullInputTextBox.Size = new Size(562, 50);
            FullInputTextBox.TabIndex = 1;
            FullInputTextBox.Text = "117.0;4.76;15.75;3.66;4.03;0.73;3.3;0.5225";
            FullInputTextBox.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            materialLabel2.Anchor = AnchorStyles.Left;
            materialLabel2.AutoSize = true;
            InputLayout.SetColumnSpan(materialLabel2, 4);
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(3, 60);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(100, 19);
            materialLabel2.TabIndex = 2;
            materialLabel2.Text = "Custom input:";
            // 
            // ProteinTextBox
            // 
            ProteinTextBox.AnimateReadOnly = false;
            ProteinTextBox.BorderStyle = BorderStyle.None;
            ProteinTextBox.Depth = 0;
            ProteinTextBox.Dock = DockStyle.Fill;
            ProteinTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            ProteinTextBox.LeadingIcon = null;
            ProteinTextBox.Location = new Point(123, 88);
            ProteinTextBox.MaxLength = 50;
            ProteinTextBox.MouseState = MaterialSkin.MouseState.OUT;
            ProteinTextBox.Multiline = false;
            ProteinTextBox.Name = "ProteinTextBox";
            ProteinTextBox.Size = new Size(218, 50);
            ProteinTextBox.TabIndex = 4;
            ProteinTextBox.Text = "1.61";
            ProteinTextBox.TrailingIcon = null;
            // 
            // materialLabel4
            // 
            materialLabel4.Anchor = AnchorStyles.Left;
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel4.Location = new Point(347, 103);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(65, 19);
            materialLabel4.TabIndex = 7;
            materialLabel4.Text = "Total Fat";
            // 
            // TotalFatTextBox
            // 
            TotalFatTextBox.AnimateReadOnly = false;
            TotalFatTextBox.BorderStyle = BorderStyle.None;
            TotalFatTextBox.Depth = 0;
            TotalFatTextBox.Dock = DockStyle.Fill;
            TotalFatTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            TotalFatTextBox.LeadingIcon = null;
            TotalFatTextBox.Location = new Point(467, 88);
            TotalFatTextBox.MaxLength = 50;
            TotalFatTextBox.MouseState = MaterialSkin.MouseState.OUT;
            TotalFatTextBox.Multiline = false;
            TotalFatTextBox.Name = "TotalFatTextBox";
            TotalFatTextBox.Size = new Size(218, 50);
            TotalFatTextBox.TabIndex = 5;
            TotalFatTextBox.Text = "11.29";
            TotalFatTextBox.TrailingIcon = null;
            // 
            // materialLabel5
            // 
            materialLabel5.Anchor = AnchorStyles.Left;
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel5.Location = new Point(3, 158);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(97, 19);
            materialLabel5.TabIndex = 8;
            materialLabel5.Text = "Saturated Fat";
            // 
            // SaturatedFatTextBox
            // 
            SaturatedFatTextBox.AnimateReadOnly = false;
            SaturatedFatTextBox.BorderStyle = BorderStyle.None;
            SaturatedFatTextBox.Depth = 0;
            SaturatedFatTextBox.Dock = DockStyle.Fill;
            SaturatedFatTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SaturatedFatTextBox.LeadingIcon = null;
            SaturatedFatTextBox.Location = new Point(123, 143);
            SaturatedFatTextBox.MaxLength = 50;
            SaturatedFatTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SaturatedFatTextBox.Multiline = false;
            SaturatedFatTextBox.Name = "SaturatedFatTextBox";
            SaturatedFatTextBox.Size = new Size(218, 50);
            SaturatedFatTextBox.TabIndex = 6;
            SaturatedFatTextBox.Text = "4.03";
            SaturatedFatTextBox.TrailingIcon = null;
            // 
            // materialLabel8
            // 
            materialLabel8.Anchor = AnchorStyles.Left;
            materialLabel8.AutoSize = true;
            materialLabel8.Depth = 0;
            materialLabel8.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel8.Location = new Point(347, 158);
            materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel8.Name = "materialLabel8";
            materialLabel8.Size = new Size(104, 19);
            materialLabel8.TabIndex = 9;
            materialLabel8.Text = "Carbohydrates";
            // 
            // CarbohydratesTextBox
            // 
            CarbohydratesTextBox.AnimateReadOnly = false;
            CarbohydratesTextBox.BorderStyle = BorderStyle.None;
            CarbohydratesTextBox.Depth = 0;
            CarbohydratesTextBox.Dock = DockStyle.Fill;
            CarbohydratesTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CarbohydratesTextBox.LeadingIcon = null;
            CarbohydratesTextBox.Location = new Point(467, 143);
            CarbohydratesTextBox.MaxLength = 50;
            CarbohydratesTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CarbohydratesTextBox.Multiline = false;
            CarbohydratesTextBox.Name = "CarbohydratesTextBox";
            CarbohydratesTextBox.Size = new Size(218, 50);
            CarbohydratesTextBox.TabIndex = 10;
            CarbohydratesTextBox.Text = "4.84";
            CarbohydratesTextBox.TrailingIcon = null;
            // 
            // materialLabel7
            // 
            materialLabel7.Anchor = AnchorStyles.Left;
            materialLabel7.AutoSize = true;
            materialLabel7.Depth = 0;
            materialLabel7.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel7.Location = new Point(3, 213);
            materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel7.Name = "materialLabel7";
            materialLabel7.Size = new Size(43, 19);
            materialLabel7.TabIndex = 13;
            materialLabel7.Text = "Sugar";
            // 
            // SugarTextBox
            // 
            SugarTextBox.AnimateReadOnly = false;
            SugarTextBox.BorderStyle = BorderStyle.None;
            SugarTextBox.Depth = 0;
            SugarTextBox.Dock = DockStyle.Fill;
            SugarTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SugarTextBox.LeadingIcon = null;
            SugarTextBox.Location = new Point(123, 198);
            SugarTextBox.MaxLength = 50;
            SugarTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SugarTextBox.Multiline = false;
            SugarTextBox.Name = "SugarTextBox";
            SugarTextBox.Size = new Size(218, 50);
            SugarTextBox.TabIndex = 11;
            SugarTextBox.Text = "1.61";
            SugarTextBox.TrailingIcon = null;
            // 
            // materialLabel6
            // 
            materialLabel6.Anchor = AnchorStyles.Left;
            materialLabel6.AutoSize = true;
            materialLabel6.Depth = 0;
            materialLabel6.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel6.Location = new Point(347, 213);
            materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel6.Name = "materialLabel6";
            materialLabel6.Size = new Size(36, 19);
            materialLabel6.TabIndex = 14;
            materialLabel6.Text = "Fiber";
            // 
            // FiberTextBox
            // 
            FiberTextBox.AnimateReadOnly = false;
            FiberTextBox.BorderStyle = BorderStyle.None;
            FiberTextBox.Depth = 0;
            FiberTextBox.Dock = DockStyle.Fill;
            FiberTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            FiberTextBox.LeadingIcon = null;
            FiberTextBox.Location = new Point(467, 198);
            FiberTextBox.MaxLength = 50;
            FiberTextBox.MouseState = MaterialSkin.MouseState.OUT;
            FiberTextBox.Multiline = false;
            FiberTextBox.Name = "FiberTextBox";
            FiberTextBox.Size = new Size(218, 50);
            FiberTextBox.TabIndex = 12;
            FiberTextBox.Text = "0.0";
            FiberTextBox.TrailingIcon = null;
            // 
            // materialLabel9
            // 
            materialLabel9.Anchor = AnchorStyles.Left;
            materialLabel9.AutoSize = true;
            materialLabel9.Depth = 0;
            materialLabel9.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel9.Location = new Point(3, 270);
            materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel9.Name = "materialLabel9";
            materialLabel9.Size = new Size(29, 19);
            materialLabel9.TabIndex = 15;
            materialLabel9.Text = "Salt";
            // 
            // SaltTextBox
            // 
            SaltTextBox.AnimateReadOnly = false;
            SaltTextBox.BorderStyle = BorderStyle.None;
            SaltTextBox.Depth = 0;
            SaltTextBox.Dock = DockStyle.Fill;
            SaltTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SaltTextBox.LeadingIcon = null;
            SaltTextBox.Location = new Point(123, 253);
            SaltTextBox.MaxLength = 50;
            SaltTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SaltTextBox.Multiline = false;
            SaltTextBox.Name = "SaltTextBox";
            SaltTextBox.Size = new Size(218, 50);
            SaltTextBox.TabIndex = 16;
            SaltTextBox.Text = "1.6925";
            SaltTextBox.TrailingIcon = null;
            // 
            // materialLabel10
            // 
            materialLabel10.Anchor = AnchorStyles.Left;
            materialLabel10.AutoSize = true;
            materialLabel10.Depth = 0;
            materialLabel10.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel10.Location = new Point(347, 270);
            materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel10.Name = "materialLabel10";
            materialLabel10.Size = new Size(89, 19);
            materialLabel10.TabIndex = 17;
            materialLabel10.Text = "Kilo Calories";
            // 
            // materialLabel3
            // 
            materialLabel3.Anchor = AnchorStyles.Left;
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(3, 103);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(51, 19);
            materialLabel3.TabIndex = 3;
            materialLabel3.Text = "Protein";
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Fill;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 343);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(694, 64);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(TestFullButton);
            ActionsLayout.Controls.Add(TestCustomButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(688, 40);
            ActionsLayout.TabIndex = 0;
            // 
            // TestFullButton
            // 
            TestFullButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestFullButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestFullButton.Depth = 0;
            TestFullButton.HighEmphasis = true;
            TestFullButton.Icon = null;
            TestFullButton.Location = new Point(4, 6);
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
            // TestCustomButton
            // 
            TestCustomButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TestCustomButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            TestCustomButton.Depth = 0;
            TestCustomButton.HighEmphasis = true;
            TestCustomButton.Icon = null;
            TestCustomButton.Location = new Point(76, 6);
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
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(RichTextBoxOutput);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 413);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(694, 184);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Logs";
            // 
            // RichTextBoxOutput
            // 
            RichTextBoxOutput.Dock = DockStyle.Fill;
            RichTextBoxOutput.Font = new Font("Segoe UI", 9F);
            RichTextBoxOutput.Location = new Point(3, 21);
            RichTextBoxOutput.Name = "RichTextBoxOutput";
            RichTextBoxOutput.Size = new Size(688, 160);
            RichTextBoxOutput.TabIndex = 1;
            RichTextBoxOutput.Text = "";
            // 
            // ManualTestingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "ManualTestingPage";
            Size = new Size(700, 600);
            MainLayout.ResumeLayout(false);
            InputGroupBox.ResumeLayout(false);
            InputLayout.ResumeLayout(false);
            InputLayout.PerformLayout();
            ActionsGroupBox.ResumeLayout(false);
            ActionsLayout.ResumeLayout(false);
            ActionsLayout.PerformLayout();
            OutputGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private GroupBox InputGroupBox;
        private TableLayoutPanel InputLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;

        private MaterialSkin.Controls.MaterialButton TestFullButton;
        private MaterialSkin.Controls.MaterialButton TestCustomButton;
        private RichTextBox RichTextBoxOutput;
        
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
        private MaterialSkin.Controls.MaterialTextBox SaltTextBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialLabel materialLabel10;
        private MaterialSkin.Controls.MaterialTextBox KiloCaloriesTextBox;
    }
}
