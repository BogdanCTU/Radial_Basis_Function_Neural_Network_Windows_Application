
using System.Drawing;
using System.Windows.Forms;

namespace WinForm_RFBN_APP
{
    partial class CrossValidationPage
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
            this.ConfigPanel = new System.Windows.Forms.Panel();
            this.LearningRateTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.StepTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.EndNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.StartNeuronTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.KFoldsTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.RunGridSearchButton = new MaterialSkin.Controls.MaterialButton();
            this.ConfigLabel = new MaterialSkin.Controls.MaterialLabel();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ConfigPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfigPanel
            // 
            this.ConfigPanel.Controls.Add(this.LearningRateTextBox);
            this.ConfigPanel.Controls.Add(this.StepTextBox);
            this.ConfigPanel.Controls.Add(this.EndNeuronTextBox);
            this.ConfigPanel.Controls.Add(this.StartNeuronTextBox);
            this.ConfigPanel.Controls.Add(this.KFoldsTextBox);
            this.ConfigPanel.Controls.Add(this.RunGridSearchButton);
            this.ConfigPanel.Controls.Add(this.ConfigLabel);
            this.ConfigPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.ConfigPanel.Name = "ConfigPanel";
            this.ConfigPanel.Size = new System.Drawing.Size(800, 200);
            this.ConfigPanel.TabIndex = 0;
            // 
            // LearningRateTextBox
            // 
            this.LearningRateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LearningRateTextBox.Depth = 0;
            this.LearningRateTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LearningRateTextBox.Hint = "Learning Rate (e.g. 0.01)";
            this.LearningRateTextBox.LeadingIcon = null;
            this.LearningRateTextBox.Location = new System.Drawing.Point(450, 50);
            this.LearningRateTextBox.MaxLength = 50;
            this.LearningRateTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.LearningRateTextBox.Multiline = false;
            this.LearningRateTextBox.Name = "LearningRateTextBox";
            this.LearningRateTextBox.Size = new System.Drawing.Size(200, 50);
            this.LearningRateTextBox.TabIndex = 5;
            this.LearningRateTextBox.Text = "0.01";
            this.LearningRateTextBox.TrailingIcon = null;
            // 
            // StepTextBox
            // 
            this.StepTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StepTextBox.Depth = 0;
            this.StepTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.StepTextBox.Hint = "Step";
            this.StepTextBox.LeadingIcon = null;
            this.StepTextBox.Location = new System.Drawing.Point(340, 50);
            this.StepTextBox.MaxLength = 50;
            this.StepTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.StepTextBox.Multiline = false;
            this.StepTextBox.Name = "StepTextBox";
            this.StepTextBox.Size = new System.Drawing.Size(100, 50);
            this.StepTextBox.TabIndex = 4;
            this.StepTextBox.Text = "5";
            this.StepTextBox.TrailingIcon = null;
            // 
            // EndNeuronTextBox
            // 
            this.EndNeuronTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndNeuronTextBox.Depth = 0;
            this.EndNeuronTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.EndNeuronTextBox.Hint = "End Neurons";
            this.EndNeuronTextBox.LeadingIcon = null;
            this.EndNeuronTextBox.Location = new System.Drawing.Point(230, 50);
            this.EndNeuronTextBox.MaxLength = 50;
            this.EndNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.EndNeuronTextBox.Multiline = false;
            this.EndNeuronTextBox.Name = "EndNeuronTextBox";
            this.EndNeuronTextBox.Size = new System.Drawing.Size(100, 50);
            this.EndNeuronTextBox.TabIndex = 3;
            this.EndNeuronTextBox.Text = "50";
            this.EndNeuronTextBox.TrailingIcon = null;
            // 
            // StartNeuronTextBox
            // 
            this.StartNeuronTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartNeuronTextBox.Depth = 0;
            this.StartNeuronTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.StartNeuronTextBox.Hint = "Start Neurons";
            this.StartNeuronTextBox.LeadingIcon = null;
            this.StartNeuronTextBox.Location = new System.Drawing.Point(120, 50);
            this.StartNeuronTextBox.MaxLength = 50;
            this.StartNeuronTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.StartNeuronTextBox.Multiline = false;
            this.StartNeuronTextBox.Name = "StartNeuronTextBox";
            this.StartNeuronTextBox.Size = new System.Drawing.Size(100, 50);
            this.StartNeuronTextBox.TabIndex = 2;
            this.StartNeuronTextBox.Text = "5";
            this.StartNeuronTextBox.TrailingIcon = null;
            // 
            // KFoldsTextBox
            // 
            this.KFoldsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.KFoldsTextBox.Depth = 0;
            this.KFoldsTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.KFoldsTextBox.Hint = "K-Folds (e.g. 5)";
            this.KFoldsTextBox.LeadingIcon = null;
            this.KFoldsTextBox.Location = new System.Drawing.Point(10, 50);
            this.KFoldsTextBox.MaxLength = 50;
            this.KFoldsTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.KFoldsTextBox.Multiline = false;
            this.KFoldsTextBox.Name = "KFoldsTextBox";
            this.KFoldsTextBox.Size = new System.Drawing.Size(100, 50);
            this.KFoldsTextBox.TabIndex = 1;
            this.KFoldsTextBox.Text = "5";
            this.KFoldsTextBox.TrailingIcon = null;
            // 
            // RunGridSearchButton
            // 
            this.RunGridSearchButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RunGridSearchButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.RunGridSearchButton.Depth = 0;
            this.RunGridSearchButton.HighEmphasis = true;
            this.RunGridSearchButton.Icon = null;
            this.RunGridSearchButton.Location = new System.Drawing.Point(10, 110);
            this.RunGridSearchButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.RunGridSearchButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RunGridSearchButton.Name = "RunGridSearchButton";
            this.RunGridSearchButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.RunGridSearchButton.Size = new System.Drawing.Size(150, 36);
            this.RunGridSearchButton.TabIndex = 6;
            this.RunGridSearchButton.Text = "Run Grid Search";
            this.RunGridSearchButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.RunGridSearchButton.UseAccentColor = false;
            this.RunGridSearchButton.UseVisualStyleBackColor = true;
            this.RunGridSearchButton.Click += new System.EventHandler(this.RunGridSearchButton_Click);
            // 
            // ConfigLabel
            // 
            this.ConfigLabel.AutoSize = true;
            this.ConfigLabel.Depth = 0;
            this.ConfigLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ConfigLabel.Location = new System.Drawing.Point(10, 10);
            this.ConfigLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.ConfigLabel.Name = "ConfigLabel";
            this.ConfigLabel.Size = new System.Drawing.Size(161, 19);
            this.ConfigLabel.TabIndex = 0;
            this.ConfigLabel.Text = "Configuration";
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRichTextBox.Location = new System.Drawing.Point(0, 200);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.Size = new System.Drawing.Size(800, 400);
            this.LogRichTextBox.TabIndex = 1;
            this.LogRichTextBox.Text = "Grid Search Results will appear here...";
            // 
            // CrossValidationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LogRichTextBox);
            this.Controls.Add(this.ConfigPanel);
            this.Name = "CrossValidationPage";
            this.Size = new System.Drawing.Size(800, 600);
            this.ConfigPanel.ResumeLayout(false);
            this.ConfigPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel ConfigPanel;
        private MaterialSkin.Controls.MaterialTextBox KFoldsTextBox;
        private MaterialSkin.Controls.MaterialTextBox StartNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox EndNeuronTextBox;
        private MaterialSkin.Controls.MaterialTextBox StepTextBox;
        private MaterialSkin.Controls.MaterialTextBox LearningRateTextBox;
        private MaterialSkin.Controls.MaterialButton RunGridSearchButton;
        private MaterialSkin.Controls.MaterialLabel ConfigLabel;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
    }
}
