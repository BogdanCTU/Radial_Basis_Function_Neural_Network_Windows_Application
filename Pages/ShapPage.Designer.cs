using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinForm_RFBN_APP
{
    partial class ShapPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            SaltTextBox = new MaterialSkin.Controls.MaterialTextBox();
            CsvPathTextBox = new MaterialSkin.Controls.MaterialTextBox();
            SchemaTextBox = new MaterialSkin.Controls.MaterialTextBox();
            ProteinBox = new MaterialSkin.Controls.MaterialTextBox();
            FatBox = new MaterialSkin.Controls.MaterialTextBox();
            CarbBox = new MaterialSkin.Controls.MaterialTextBox();
            KcalBox = new MaterialSkin.Controls.MaterialTextBox();
            FiberBox = new MaterialSkin.Controls.MaterialTextBox();
            SatFatBox = new MaterialSkin.Controls.MaterialTextBox();
            SugarBox = new MaterialSkin.Controls.MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            ExplainButton = new MaterialSkin.Controls.MaterialButton();
            ExplainButton = new MaterialSkin.Controls.MaterialButton();
            OutputGroupBox = new GroupBox();
            ShapChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            OutputGroupBox.SuspendLayout();
            ConfigGroupBox.SuspendLayout();
            ConfigLayout.SuspendLayout();
            ActionsGroupBox.SuspendLayout();
            ActionsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.ColumnCount = 1;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayout.Controls.Add(ConfigGroupBox, 0, 0);
            MainLayout.Controls.Add(ActionsGroupBox, 0, 1);
            MainLayout.Controls.Add(OutputGroupBox, 0, 2);
            MainLayout.Dock = DockStyle.Fill;
            MainLayout.Location = new Point(0, 0);
            MainLayout.Name = "MainLayout";
            MainLayout.RowCount = 3;
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 531F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.Size = new Size(800, 750);
            MainLayout.TabIndex = 0;
            // 
            // ConfigGroupBox
            // 
            ConfigGroupBox.Controls.Add(ConfigLayout);
            ConfigGroupBox.Dock = DockStyle.Fill;
            ConfigGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigGroupBox.Location = new Point(3, 3);
            ConfigGroupBox.Name = "ConfigGroupBox";
            ConfigGroupBox.Size = new Size(794, 525);
            ConfigGroupBox.TabIndex = 0;
            ConfigGroupBox.TabStop = false;
            ConfigGroupBox.Text = "SHAP Configuration & Inputs";
            // 
            // ConfigLayout
            // 
            ConfigLayout.ColumnCount = 1;
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            ConfigLayout.Controls.Add(SaltTextBox, 0, 9);
            ConfigLayout.Controls.Add(CsvPathTextBox, 1, 0);
            ConfigLayout.Controls.Add(SchemaTextBox, 1, 1);
            ConfigLayout.Controls.Add(ProteinBox, 1, 2);
            ConfigLayout.Controls.Add(FatBox, 1, 3);
            ConfigLayout.Controls.Add(CarbBox, 1, 4);
            ConfigLayout.Controls.Add(KcalBox, 1, 5);
            ConfigLayout.Controls.Add(FiberBox, 1, 6);
            ConfigLayout.Controls.Add(SatFatBox, 1, 7);
            ConfigLayout.Controls.Add(SugarBox, 1, 8);
            ConfigLayout.Dock = DockStyle.Fill;
            ConfigLayout.Location = new Point(3, 21);
            ConfigLayout.Name = "ConfigLayout";
            ConfigLayout.RowCount = 10;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ConfigLayout.Size = new Size(788, 501);
            ConfigLayout.TabIndex = 0;
            // 
            // SaltTextBox
            // 
            SaltTextBox.AnimateReadOnly = false;
            SaltTextBox.BorderStyle = BorderStyle.None;
            SaltTextBox.Depth = 0;
            SaltTextBox.Dock = DockStyle.Fill;
            SaltTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SaltTextBox.Hint = "Salt";
            SaltTextBox.LeadingIcon = null;
            SaltTextBox.Location = new Point(3, 453);
            SaltTextBox.MaxLength = 50;
            SaltTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SaltTextBox.Multiline = false;
            SaltTextBox.Name = "SaltTextBox";
            SaltTextBox.Size = new Size(782, 50);
            SaltTextBox.TabIndex = 9;
            SaltTextBox.Text = "0.02";
            SaltTextBox.TrailingIcon = null;
            // 
            // CsvPathTextBox
            // 
            CsvPathTextBox.AnimateReadOnly = false;
            CsvPathTextBox.BorderStyle = BorderStyle.None;
            CsvPathTextBox.Depth = 0;
            CsvPathTextBox.Dock = DockStyle.Fill;
            CsvPathTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CsvPathTextBox.Hint = "CSV Path";
            CsvPathTextBox.LeadingIcon = null;
            CsvPathTextBox.Location = new Point(3, 3);
            CsvPathTextBox.MaxLength = 500;
            CsvPathTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CsvPathTextBox.Multiline = false;
            CsvPathTextBox.Name = "CsvPathTextBox";
            CsvPathTextBox.Size = new Size(782, 50);
            CsvPathTextBox.TabIndex = 0;
            CsvPathTextBox.Text = "train.csv";
            CsvPathTextBox.TrailingIcon = null;
            // 
            // SchemaTextBox
            // 
            SchemaTextBox.AnimateReadOnly = false;
            SchemaTextBox.BorderStyle = BorderStyle.None;
            SchemaTextBox.Depth = 0;
            SchemaTextBox.Dock = DockStyle.Fill;
            SchemaTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            SchemaTextBox.Hint = "Schema";
            SchemaTextBox.LeadingIcon = null;
            SchemaTextBox.Location = new Point(3, 53);
            SchemaTextBox.MaxLength = 500;
            SchemaTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SchemaTextBox.Multiline = false;
            SchemaTextBox.Name = "SchemaTextBox";
            SchemaTextBox.Size = new Size(782, 50);
            SchemaTextBox.TabIndex = 1;
            SchemaTextBox.Text = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";
            SchemaTextBox.TrailingIcon = null;
            // 
            // ProteinBox
            // 
            ProteinBox.AnimateReadOnly = false;
            ProteinBox.BorderStyle = BorderStyle.None;
            ProteinBox.Depth = 0;
            ProteinBox.Dock = DockStyle.Fill;
            ProteinBox.Font = new Font("Microsoft Sans Serif", 12F);
            ProteinBox.Hint = "Protein";
            ProteinBox.LeadingIcon = null;
            ProteinBox.Location = new Point(3, 103);
            ProteinBox.MaxLength = 50;
            ProteinBox.MouseState = MaterialSkin.MouseState.OUT;
            ProteinBox.Multiline = false;
            ProteinBox.Name = "ProteinBox";
            ProteinBox.Size = new Size(782, 50);
            ProteinBox.TabIndex = 2;
            ProteinBox.Text = "7.41";
            ProteinBox.TrailingIcon = null;
            // 
            // FatBox
            // 
            FatBox.AnimateReadOnly = false;
            FatBox.BorderStyle = BorderStyle.None;
            FatBox.Depth = 0;
            FatBox.Dock = DockStyle.Fill;
            FatBox.Font = new Font("Microsoft Sans Serif", 12F);
            FatBox.Hint = "Total Fat";
            FatBox.LeadingIcon = null;
            FatBox.Location = new Point(3, 153);
            FatBox.MaxLength = 50;
            FatBox.MouseState = MaterialSkin.MouseState.OUT;
            FatBox.Multiline = false;
            FatBox.Name = "FatBox";
            FatBox.Size = new Size(782, 50);
            FatBox.TabIndex = 3;
            FatBox.Text = "25.93";
            FatBox.TrailingIcon = null;
            // 
            // CarbBox
            // 
            CarbBox.AnimateReadOnly = false;
            CarbBox.BorderStyle = BorderStyle.None;
            CarbBox.Depth = 0;
            CarbBox.Dock = DockStyle.Fill;
            CarbBox.Font = new Font("Microsoft Sans Serif", 12F);
            CarbBox.Hint = "Carbohydrate";
            CarbBox.LeadingIcon = null;
            CarbBox.Location = new Point(3, 203);
            CarbBox.MaxLength = 50;
            CarbBox.MouseState = MaterialSkin.MouseState.OUT;
            CarbBox.Multiline = false;
            CarbBox.Name = "CarbBox";
            CarbBox.Size = new Size(782, 50);
            CarbBox.TabIndex = 4;
            CarbBox.Text = "55.56";
            CarbBox.TrailingIcon = null;
            // 
            // KcalBox
            // 
            KcalBox.AnimateReadOnly = false;
            KcalBox.BorderStyle = BorderStyle.None;
            KcalBox.Depth = 0;
            KcalBox.Dock = DockStyle.Fill;
            KcalBox.Font = new Font("Microsoft Sans Serif", 12F);
            KcalBox.Hint = "Energy (Kcal)";
            KcalBox.LeadingIcon = null;
            KcalBox.Location = new Point(3, 253);
            KcalBox.MaxLength = 50;
            KcalBox.MouseState = MaterialSkin.MouseState.OUT;
            KcalBox.Multiline = false;
            KcalBox.Name = "KcalBox";
            KcalBox.Size = new Size(782, 50);
            KcalBox.TabIndex = 5;
            KcalBox.Text = "481";
            KcalBox.TrailingIcon = null;
            // 
            // FiberBox
            // 
            FiberBox.AnimateReadOnly = false;
            FiberBox.BorderStyle = BorderStyle.None;
            FiberBox.Depth = 0;
            FiberBox.Dock = DockStyle.Fill;
            FiberBox.Font = new Font("Microsoft Sans Serif", 12F);
            FiberBox.Hint = "Fiber";
            FiberBox.LeadingIcon = null;
            FiberBox.Location = new Point(3, 303);
            FiberBox.MaxLength = 50;
            FiberBox.MouseState = MaterialSkin.MouseState.OUT;
            FiberBox.Multiline = false;
            FiberBox.Name = "FiberBox";
            FiberBox.Size = new Size(782, 50);
            FiberBox.TabIndex = 6;
            FiberBox.Text = "3.7";
            FiberBox.TrailingIcon = null;
            // 
            // SatFatBox
            // 
            SatFatBox.AnimateReadOnly = false;
            SatFatBox.BorderStyle = BorderStyle.None;
            SatFatBox.Depth = 0;
            SatFatBox.Dock = DockStyle.Fill;
            SatFatBox.Font = new Font("Microsoft Sans Serif", 12F);
            SatFatBox.Hint = "Saturated Fat";
            SatFatBox.LeadingIcon = null;
            SatFatBox.Location = new Point(3, 353);
            SatFatBox.MaxLength = 50;
            SatFatBox.MouseState = MaterialSkin.MouseState.OUT;
            SatFatBox.Multiline = false;
            SatFatBox.Name = "SatFatBox";
            SatFatBox.Size = new Size(782, 50);
            SatFatBox.TabIndex = 7;
            SatFatBox.Text = "7.41";
            SatFatBox.TrailingIcon = null;
            // 
            // SugarBox
            // 
            SugarBox.AnimateReadOnly = false;
            SugarBox.BorderStyle = BorderStyle.None;
            SugarBox.Depth = 0;
            SugarBox.Dock = DockStyle.Fill;
            SugarBox.Font = new Font("Microsoft Sans Serif", 12F);
            SugarBox.Hint = "Sugar";
            SugarBox.LeadingIcon = null;
            SugarBox.Location = new Point(3, 403);
            SugarBox.MaxLength = 50;
            SugarBox.MouseState = MaterialSkin.MouseState.OUT;
            SugarBox.Multiline = false;
            SugarBox.Name = "SugarBox";
            SugarBox.Size = new Size(782, 50);
            SugarBox.TabIndex = 8;
            SugarBox.Text = "33.33";
            SugarBox.TrailingIcon = null;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Fill;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 534);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(794, 70);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(ExplainButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(788, 46);
            ActionsLayout.TabIndex = 0;
            // 
            // ExplainButton
            // 
            ExplainButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ExplainButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            ExplainButton.Depth = 0;
            ExplainButton.HighEmphasis = true;
            ExplainButton.Icon = null;
            ExplainButton.Location = new Point(4, 6);
            ExplainButton.Margin = new Padding(4, 6, 4, 6);
            ExplainButton.MouseState = MaterialSkin.MouseState.HOVER;
            ExplainButton.Name = "ExplainButton";
            ExplainButton.NoAccentTextColor = Color.Empty;
            ExplainButton.Size = new Size(169, 36);
            ExplainButton.TabIndex = 0;
            ExplainButton.Text = "Explain Prediction";
            ExplainButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            ExplainButton.UseAccentColor = false;
            ExplainButton.Click += ExplainButton_Click;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Controls.Add(ShapChart);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 610);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(794, 137);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "SHAP Explainability Chart";
            // 
            // ShapChart
            // 
            chartArea1.Name = "ChartArea1";
            ShapChart.ChartAreas.Add(chartArea1);
            ShapChart.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            ShapChart.Legends.Add(legend1);
            ShapChart.Location = new Point(3, 21);
            ShapChart.Name = "ShapChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "ShapValues";
            ShapChart.Series.Add(series1);
            ShapChart.Size = new Size(788, 160);
            ShapChart.TabIndex = 0;
            ShapChart.Text = "SHAP Values";
            // 
            // ShapPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "ShapPage";
            Size = new Size(800, 750);
            MainLayout.ResumeLayout(false);
            ConfigGroupBox.ResumeLayout(false);
            ConfigLayout.ResumeLayout(false);
            ActionsGroupBox.ResumeLayout(false);
            ActionsLayout.ResumeLayout(false);
            ActionsLayout.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel MainLayout;
        private GroupBox ConfigGroupBox;
        private TableLayoutPanel ConfigLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;

        private System.Windows.Forms.DataVisualization.Charting.Chart ShapChart;
        private MaterialSkin.Controls.MaterialButton ExplainButton;
        private MaterialSkin.Controls.MaterialTextBox CsvPathTextBox;
        private MaterialSkin.Controls.MaterialTextBox SchemaTextBox;
        private MaterialSkin.Controls.MaterialTextBox ProteinBox;
        private MaterialSkin.Controls.MaterialTextBox FatBox;
        private MaterialSkin.Controls.MaterialTextBox CarbBox;
        private MaterialSkin.Controls.MaterialTextBox KcalBox;
        private MaterialSkin.Controls.MaterialTextBox FiberBox;
        private MaterialSkin.Controls.MaterialTextBox SatFatBox;
        private MaterialSkin.Controls.MaterialTextBox SugarBox;
        private MaterialSkin.Controls.MaterialTextBox SaltTextBox;
    }
}