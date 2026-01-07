
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MaterialSkin.Controls;

namespace WinForm_RFBN_APP
{
    partial class GlobalShapPage
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
            MainLayout = new TableLayoutPanel();
            ConfigGroupBox = new GroupBox();
            ConfigLayout = new TableLayoutPanel();
            CsvPathTextBox = new MaterialTextBox();
            SchemaTextBox = new MaterialTextBox();
            ActionsGroupBox = new GroupBox();
            ActionsLayout = new FlowLayoutPanel();
            ExplainBatchButton = new MaterialButton();
            OutputGroupBox = new GroupBox();
            MainLayout.SuspendLayout();
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
            CsvPathTextBox.Text = "train_80k.csv";
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
            SchemaTextBox.Text = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";
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
            ActionsLayout.Controls.Add(ExplainBatchButton);
            ActionsLayout.Dock = DockStyle.Fill;
            ActionsLayout.Location = new Point(3, 21);
            ActionsLayout.Name = "ActionsLayout";
            ActionsLayout.Size = new Size(788, 40);
            ActionsLayout.TabIndex = 0;
            // 
            // ExplainBatchButton
            // 
            ExplainBatchButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ExplainBatchButton.Density = MaterialButton.MaterialButtonDensity.Default;
            ExplainBatchButton.Depth = 0;
            ExplainBatchButton.HighEmphasis = true;
            ExplainBatchButton.Icon = null;
            ExplainBatchButton.Location = new Point(4, 6);
            ExplainBatchButton.Margin = new Padding(4, 6, 4, 6);
            ExplainBatchButton.MouseState = MaterialSkin.MouseState.HOVER;
            ExplainBatchButton.Name = "ExplainBatchButton";
            ExplainBatchButton.NoAccentTextColor = Color.Empty;
            ExplainBatchButton.Size = new Size(137, 36);
            ExplainBatchButton.TabIndex = 0;
            ExplainBatchButton.Text = "Analyze Batch";
            ExplainBatchButton.Type = MaterialButton.MaterialButtonType.Contained;
            ExplainBatchButton.UseAccentColor = false;
            ExplainBatchButton.Click += ExplainBatchButton_Click;
            // 
            // OutputGroupBox
            // 
            OutputGroupBox.Dock = DockStyle.Fill;
            OutputGroupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            OutputGroupBox.Location = new Point(3, 233);
            OutputGroupBox.Name = "OutputGroupBox";
            OutputGroupBox.Size = new Size(794, 364);
            OutputGroupBox.TabIndex = 2;
            OutputGroupBox.TabStop = false;
            OutputGroupBox.Text = "Global Feature Importance (Mean |SHAP|)";
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
            ResumeLayout(false);
        }

        private TableLayoutPanel MainLayout;
        private GroupBox ConfigGroupBox;
        private TableLayoutPanel ConfigLayout;
        private GroupBox ActionsGroupBox;
        private FlowLayoutPanel ActionsLayout;
        private GroupBox OutputGroupBox;

        private MaterialTextBox CsvPathTextBox;
        private MaterialTextBox SchemaTextBox;
        private MaterialButton ExplainBatchButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart ShapChart;
    }
}
