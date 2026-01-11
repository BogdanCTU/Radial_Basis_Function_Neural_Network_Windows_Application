using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MaterialSkin.Controls;

namespace WinForm_RFBN_APP
{
    partial class GlobalShapPage
    {
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

        #region Component Designer generated code

        ///<summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            // Chart specific initializers
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();

            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            CsvPathTextBox = new MaterialTextBox();
            SchemaTextBox = new MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            ExplainMeanSHAPButton = new MaterialButton();
            ExplainSHAPButton = new MaterialButton();
            OutputGroupBox = new GroupBox();
            // 1. Instantiate the Chart
            ShapChart = new Chart();

            MainLayout.SuspendLayout();
            ConfigGroupBox.SuspendLayout();
            ConfigLayout.SuspendLayout();
            ActionsGroupBox.SuspendLayout();
            ActionsLayout.SuspendLayout();
            OutputGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(ShapChart)).BeginInit();
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
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.Size = new Size(800, 600);
            MainLayout.TabIndex = 0;
            // 
            // ConfigGroupBox
            // 
            ConfigGroupBox.Controls.Add(ConfigLayout);
            ConfigGroupBox.Dock = DockStyle.Fill;
            ConfigGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigGroupBox.Location = new Point(3, 3);
            ConfigGroupBox.Name = "ConfigGroupBox";
            ConfigGroupBox.Size = new Size(794, 154);
            ConfigGroupBox.TabIndex = 0;
            ConfigGroupBox.TabStop = false;
            ConfigGroupBox.Text = "Batch Data Configuration";
            // 
            // ConfigLayout
            // 
            ConfigLayout.ColumnCount = 2;
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            ConfigLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ConfigLayout.Controls.Add(CsvPathTextBox, 1, 0);
            ConfigLayout.Controls.Add(SchemaTextBox, 1, 1);
            ConfigLayout.Dock = DockStyle.Fill;
            ConfigLayout.Location = new Point(3, 21);
            ConfigLayout.Name = "ConfigLayout";
            ConfigLayout.RowCount = 2;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ConfigLayout.Size = new Size(788, 130);
            ConfigLayout.TabIndex = 0;
            // 
            // CsvPathTextBox
            // 
            CsvPathTextBox.AnimateReadOnly = false;
            CsvPathTextBox.BorderStyle = BorderStyle.None;
            CsvPathTextBox.Depth = 0;
            CsvPathTextBox.Dock = DockStyle.Fill;
            CsvPathTextBox.Font = new Font("Microsoft Sans Serif", 12F);
            CsvPathTextBox.Hint = "CSV File Path (Input Data for Batch Analysis)";
            CsvPathTextBox.LeadingIcon = null;
            CsvPathTextBox.Location = new Point(153, 3);
            CsvPathTextBox.MaxLength = 500;
            CsvPathTextBox.MouseState = MaterialSkin.MouseState.OUT;
            CsvPathTextBox.Multiline = false;
            CsvPathTextBox.Name = "CsvPathTextBox";
            CsvPathTextBox.Size = new Size(632, 50);
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
            SchemaTextBox.Location = new Point(153, 68);
            SchemaTextBox.MaxLength = 500;
            SchemaTextBox.MouseState = MaterialSkin.MouseState.OUT;
            SchemaTextBox.Multiline = false;
            SchemaTextBox.Name = "SchemaTextBox";
            SchemaTextBox.Size = new Size(632, 50);
            SchemaTextBox.TabIndex = 1;
            SchemaTextBox.Text = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";
            SchemaTextBox.TrailingIcon = null;
            // 
            // ActionsGroupBox
            // 
            ActionsGroupBox.Controls.Add(ActionsLayout);
            ActionsGroupBox.Dock = DockStyle.Fill;
            ActionsGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ActionsGroupBox.Location = new Point(3, 163);
            ActionsGroupBox.Name = "ActionsGroupBox";
            ActionsGroupBox.Size = new Size(794, 64);
            ActionsGroupBox.TabIndex = 1;
            ActionsGroupBox.TabStop = false;
            ActionsGroupBox.Text = "Actions";
            // 
            // ActionsLayout
            // 
            ActionsLayout.Controls.Add(ExplainMeanSHAPButton);
            ActionsLayout.Controls.Add(ExplainSHAPButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(788, 40);
            ActionsLayout.TabIndex = 0;
            // 
            // ExplainMeanSHAPButton
            // 
            ExplainMeanSHAPButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ExplainMeanSHAPButton.Density = MaterialButton.MaterialButtonDensity.Default;
            ExplainMeanSHAPButton.Depth = 0;
            ExplainMeanSHAPButton.HighEmphasis = true;
            ExplainMeanSHAPButton.Icon = null;
            ExplainMeanSHAPButton.Location = new Point(4, 6);
            ExplainMeanSHAPButton.Margin = new Padding(4, 6, 4, 6);
            ExplainMeanSHAPButton.MouseState = MaterialSkin.MouseState.HOVER;
            ExplainMeanSHAPButton.Name = "ExplainMeanSHAPButton";
            ExplainMeanSHAPButton.NoAccentTextColor = Color.Empty;
            ExplainMeanSHAPButton.Size = new Size(105, 36);
            ExplainMeanSHAPButton.TabIndex = 0;
            ExplainMeanSHAPButton.Text = "Mean SHAP";
            ExplainMeanSHAPButton.Type = MaterialButton.MaterialButtonType.Contained;
            ExplainMeanSHAPButton.UseAccentColor = false;
            ExplainMeanSHAPButton.Click += ExplainMeanSHAPButton_Click;
            // 
            // ExplainSHAPButton
            // 
            ExplainSHAPButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ExplainSHAPButton.Density = MaterialButton.MaterialButtonDensity.Default;
            ExplainSHAPButton.Depth = 0;
            ExplainSHAPButton.HighEmphasis = true;
            ExplainSHAPButton.Icon = null;
            ExplainSHAPButton.Location = new Point(117, 6);
            ExplainSHAPButton.Margin = new Padding(4, 6, 4, 6);
            ExplainSHAPButton.MouseState = MaterialSkin.MouseState.HOVER;
            ExplainSHAPButton.Name = "ExplainSHAPButton";
            ExplainSHAPButton.NoAccentTextColor = Color.Empty;
            ExplainSHAPButton.Size = new Size(143, 36);
            ExplainSHAPButton.TabIndex = 1;
            ExplainSHAPButton.Text = "Beeswarm SHAP";
            ExplainSHAPButton.Type = MaterialButton.MaterialButtonType.Contained;
            ExplainSHAPButton.UseAccentColor = false;
            ExplainSHAPButton.Click += ExplainSHAPButton_Click;
            // 
            // OutputGroupBox
            // 
            // 2. Add the Chart to the GroupBox
            OutputGroupBox.Controls.Add(ShapChart);
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 233);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(794, 364);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
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

            // 3. Configure Series Name exactly as used in logic ("ShapValues")
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "ShapValues";
            ShapChart.Series.Add(series1);

            ShapChart.Size = new Size(788, 340);
            ShapChart.TabIndex = 0;
            ShapChart.Text = "Global Importance Chart";
            OutputGroupBox.Text = "Global Feature Importance SHAP";
            // 
            // GlobalShapPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainLayout);
            Name = "GlobalShapPage";
            Size = new Size(800, 600);
            MainLayout.ResumeLayout(false);
            ConfigGroupBox.ResumeLayout(false);
            ConfigLayout.ResumeLayout(false);
            ActionsGroupBox.ResumeLayout(false);
            ActionsLayout.ResumeLayout(false);
            ActionsLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(ShapChart)).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private GroupBox ConfigGroupBox;
        private TableLayoutPanel ConfigLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;

        private MaterialTextBox CsvPathTextBox;
        private MaterialTextBox SchemaTextBox;
        private MaterialButton ExplainMeanSHAPButton;
        private Chart ShapChart; // Declared but was missing initialization
        private MaterialButton ExplainSHAPButton;
    }
}