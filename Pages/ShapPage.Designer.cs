
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

            this.InputPanel = new System.Windows.Forms.Panel();
            this.ShapChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ExplainButton = new MaterialSkin.Controls.MaterialButton();
            
            // Input Text Boxes
            this.ProteinBox = new MaterialSkin.Controls.MaterialTextBox();
            this.FatBox = new MaterialSkin.Controls.MaterialTextBox();
            this.CarbBox = new MaterialSkin.Controls.MaterialTextBox();
            this.KcalBox = new MaterialSkin.Controls.MaterialTextBox();
            this.FiberBox = new MaterialSkin.Controls.MaterialTextBox();
            this.SatFatBox = new MaterialSkin.Controls.MaterialTextBox();
            this.SugarBox = new MaterialSkin.Controls.MaterialTextBox();

            this.InputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShapChart)).BeginInit();
            this.SuspendLayout();

            // 
            // InputPanel
            // 
            this.InputPanel.Controls.Add(this.SugarBox);
            this.InputPanel.Controls.Add(this.SatFatBox);
            this.InputPanel.Controls.Add(this.FiberBox);
            this.InputPanel.Controls.Add(this.KcalBox);
            this.InputPanel.Controls.Add(this.CarbBox);
            this.InputPanel.Controls.Add(this.FatBox);
            this.InputPanel.Controls.Add(this.ProteinBox);
            this.InputPanel.Controls.Add(this.ExplainButton);
            this.InputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.InputPanel.Location = new System.Drawing.Point(0, 0);
            this.InputPanel.Name = "InputPanel";
            this.InputPanel.Size = new System.Drawing.Size(250, 600);
            this.InputPanel.TabIndex = 0;

            // 
            // TextBoxes Layout (Vertical Stack)
            // 
            int y = 20;

            this.ProteinBox.Hint = "Protein";
            this.ProteinBox.Location = new System.Drawing.Point(10, y);
            this.ProteinBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.FatBox.Hint = "Total Fat";
            this.FatBox.Location = new System.Drawing.Point(10, y);
            this.FatBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.CarbBox.Hint = "Carbohydrate";
            this.CarbBox.Location = new System.Drawing.Point(10, y);
            this.CarbBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.KcalBox.Hint = "Energy (Kcal)";
            this.KcalBox.Location = new System.Drawing.Point(10, y);
            this.KcalBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.FiberBox.Hint = "Fiber";
            this.FiberBox.Location = new System.Drawing.Point(10, y);
            this.FiberBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.SatFatBox.Hint = "Saturated Fat";
            this.SatFatBox.Location = new System.Drawing.Point(10, y);
            this.SatFatBox.Size = new System.Drawing.Size(220, 50);
            y += 60;
            this.SugarBox.Hint = "Sugar";
            this.SugarBox.Location = new System.Drawing.Point(10, y);
            this.SugarBox.Size = new System.Drawing.Size(220, 50);
            y += 70;

            // 
            // ExplainButton
            // 
            this.ExplainButton.Text = "Explain Prediction";
            this.ExplainButton.Location = new System.Drawing.Point(10, y);
            this.ExplainButton.Size = new System.Drawing.Size(220, 36);
            this.ExplainButton.Click += new System.EventHandler(this.ExplainButton_Click);

            // 
            // ShapChart
            // 
            chartArea1.Name = "ChartArea1";
            this.ShapChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Enabled = false;
            this.ShapChart.Legends.Add(legend1);
            this.ShapChart.Location = new System.Drawing.Point(260, 20);
            this.ShapChart.Name = "ShapChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = SeriesChartType.Bar; // Horizontal Bars
            series1.Name = "ShapValues";
            this.ShapChart.Series.Add(series1);
            this.ShapChart.Size = new System.Drawing.Size(500, 500);
            this.ShapChart.TabIndex = 1;
            this.ShapChart.Text = "SHAP Values";
            this.ShapChart.Dock = DockStyle.Fill;

            // 
            // ShapPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ShapChart);
            this.Controls.Add(this.InputPanel);
            this.Name = "ShapPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.InputPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShapChart)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel InputPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart ShapChart;
        private MaterialSkin.Controls.MaterialButton ExplainButton;
        private MaterialSkin.Controls.MaterialTextBox ProteinBox;
        private MaterialSkin.Controls.MaterialTextBox FatBox;
        private MaterialSkin.Controls.MaterialTextBox CarbBox;
        private MaterialSkin.Controls.MaterialTextBox KcalBox;
        private MaterialSkin.Controls.MaterialTextBox FiberBox;
        private MaterialSkin.Controls.MaterialTextBox SatFatBox;
        private MaterialSkin.Controls.MaterialTextBox SugarBox;
    }
}
