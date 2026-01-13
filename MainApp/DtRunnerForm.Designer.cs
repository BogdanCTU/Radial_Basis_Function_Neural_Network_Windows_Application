
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace WinForm_RFBN_APP
{
    partial class DtRunnerForm
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTrainFile = new MaterialSkin.Controls.MaterialLabel();
            this.txtTrainFile = new MaterialSkin.Controls.MaterialTextBox();
            this.lblValFile = new MaterialSkin.Controls.MaterialLabel();
            this.txtValFile = new MaterialSkin.Controls.MaterialTextBox();
            this.lblSchema = new MaterialSkin.Controls.MaterialLabel();
            this.txtSchema = new MaterialSkin.Controls.MaterialTextBox();
            this.lblTotalSamples = new MaterialSkin.Controls.MaterialLabel();
            this.txtTotalSamples = new MaterialSkin.Controls.MaterialTextBox();
            this.lblMinSamplesSplit = new MaterialSkin.Controls.MaterialLabel();
            this.txtMinSamplesSplit = new MaterialSkin.Controls.MaterialTextBox();
            this.lblMinSamplesLeaf = new MaterialSkin.Controls.MaterialLabel();
            this.txtMinSamplesLeaf = new MaterialSkin.Controls.MaterialTextBox();
            this.lblMaxDepth = new MaterialSkin.Controls.MaterialLabel();
            this.txtMaxDepth = new MaterialSkin.Controls.MaterialTextBox();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnTrain = new MaterialSkin.Controls.MaterialButton();
            this.btnOptimize = new MaterialSkin.Controls.MaterialButton();
            this.btnPredict = new MaterialSkin.Controls.MaterialButton();
            this.btnBack = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            
            // Layout constants
            int margin = 20;
            int startY = 80;
            int col1X = 20;
            int col2X = 350;
            int controlWidth = 300;
            int btnY = startY + 490;
            int btnWidth = 140;

            // 
            // lblTrainFile
            // 
            this.lblTrainFile.AutoSize = true;
            this.lblTrainFile.Depth = 0;
            this.lblTrainFile.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTrainFile.Location = new System.Drawing.Point(col1X, startY);
            this.lblTrainFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTrainFile.Name = "lblTrainFile";
            this.lblTrainFile.Size = new System.Drawing.Size(92, 19);
            this.lblTrainFile.TabIndex = 0;
            this.lblTrainFile.Text = "Training File:";
            // 
            // txtTrainFile
            // 
            this.txtTrainFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTrainFile.Depth = 0;
            this.txtTrainFile.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTrainFile.Hint = "Path relative to project root";
            this.txtTrainFile.LeadingIcon = null;
            this.txtTrainFile.Location = new System.Drawing.Point(col1X, startY + 25);
            this.txtTrainFile.MaxLength = 50;
            this.txtTrainFile.MouseState = MaterialSkin.MouseState.OUT;
            this.txtTrainFile.Multiline = false;
            this.txtTrainFile.Name = "txtTrainFile";
            this.txtTrainFile.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtTrainFile.TabIndex = 1;
            this.txtTrainFile.Text = "DataSet\\train_split.csv";
            this.txtTrainFile.TrailingIcon = null;
            // 
            // lblValFile
            // 
            this.lblValFile.AutoSize = true;
            this.lblValFile.Depth = 0;
            this.lblValFile.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblValFile.Location = new System.Drawing.Point(col1X, startY + 80);
            this.lblValFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblValFile.Name = "lblValFile";
            this.lblValFile.Size = new System.Drawing.Size(107, 19);
            this.lblValFile.TabIndex = 2;
            this.lblValFile.Text = "Validation File:";
            // 
            // txtValFile
            // 
            this.txtValFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValFile.Depth = 0;
            this.txtValFile.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtValFile.Hint = "Path relative to project root";
            this.txtValFile.LeadingIcon = null;
            this.txtValFile.Location = new System.Drawing.Point(col1X, startY + 105);
            this.txtValFile.MaxLength = 50;
            this.txtValFile.MouseState = MaterialSkin.MouseState.OUT;
            this.txtValFile.Multiline = false;
            this.txtValFile.Name = "txtValFile";
            this.txtValFile.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtValFile.TabIndex = 3;
            this.txtValFile.Text = "DataSet\\val_split.csv";
            this.txtValFile.TrailingIcon = null;
            // 
            // lblSchema
            // 
            this.lblSchema.AutoSize = true;
            this.lblSchema.Depth = 0;
            this.lblSchema.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSchema.Location = new System.Drawing.Point(col1X, startY + 160);
            this.lblSchema.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(209, 19);
            this.lblSchema.TabIndex = 4;
            this.lblSchema.Text = "Schema (Feature;...;Target):";
            // 
            // txtSchema
            // 
            this.txtSchema.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSchema.Depth = 0;
            this.txtSchema.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSchema.LeadingIcon = null;
            this.txtSchema.Location = new System.Drawing.Point(col1X, startY + 185);
            this.txtSchema.MaxLength = 500;
            this.txtSchema.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSchema.Multiline = false;
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtSchema.TabIndex = 5;
            this.txtSchema.Text = "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;final_score";
            this.txtSchema.TrailingIcon = null;
            // 
            // lblTotalSamples
            // 
            this.lblTotalSamples.AutoSize = true;
            this.lblTotalSamples.Depth = 0;
            this.lblTotalSamples.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTotalSamples.Location = new System.Drawing.Point(col1X, startY + 240);
            this.lblTotalSamples.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTotalSamples.Name = "lblTotalSamples";
            this.lblTotalSamples.Size = new System.Drawing.Size(176, 19);
            this.lblTotalSamples.TabIndex = 6;
            this.lblTotalSamples.Text = "Total Samples (0 = All):";
            // 
            // txtTotalSamples
            // 
            this.txtTotalSamples.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalSamples.Depth = 0;
            this.txtTotalSamples.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTotalSamples.LeadingIcon = null;
            this.txtTotalSamples.Location = new System.Drawing.Point(col1X, startY + 265);
            this.txtTotalSamples.MaxLength = 50;
            this.txtTotalSamples.MouseState = MaterialSkin.MouseState.OUT;
            this.txtTotalSamples.Multiline = false;
            this.txtTotalSamples.Name = "txtTotalSamples";
            this.txtTotalSamples.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtTotalSamples.TabIndex = 7;
            this.txtTotalSamples.Text = "0";
            this.txtTotalSamples.TrailingIcon = null;
            // 
            // lblMinSamplesSplit
            // 
            this.lblMinSamplesSplit.AutoSize = true;
            this.lblMinSamplesSplit.Depth = 0;
            this.lblMinSamplesSplit.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMinSamplesSplit.Location = new System.Drawing.Point(col1X, startY + 320);
            this.lblMinSamplesSplit.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMinSamplesSplit.Name = "lblMinSamplesSplit";
            this.lblMinSamplesSplit.Size = new System.Drawing.Size(139, 19);
            this.lblMinSamplesSplit.TabIndex = 8;
            this.lblMinSamplesSplit.Text = "Min Samples Split:";
            // 
            // txtMinSamplesSplit
            // 
            this.txtMinSamplesSplit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinSamplesSplit.Depth = 0;
            this.txtMinSamplesSplit.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMinSamplesSplit.LeadingIcon = null;
            this.txtMinSamplesSplit.Location = new System.Drawing.Point(col1X, startY + 345);
            this.txtMinSamplesSplit.MaxLength = 50;
            this.txtMinSamplesSplit.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMinSamplesSplit.Multiline = false;
            this.txtMinSamplesSplit.Name = "txtMinSamplesSplit";
            this.txtMinSamplesSplit.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtMinSamplesSplit.TabIndex = 9;
            this.txtMinSamplesSplit.Text = "20";
            this.txtMinSamplesSplit.TrailingIcon = null;
            // 
            // lblMinSamplesLeaf
            // 
            this.lblMinSamplesLeaf.AutoSize = true;
            this.lblMinSamplesLeaf.Depth = 0;
            this.lblMinSamplesLeaf.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMinSamplesLeaf.Location = new System.Drawing.Point(col1X, startY + 400);
            this.lblMinSamplesLeaf.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMinSamplesLeaf.Name = "lblMinSamplesLeaf";
            this.lblMinSamplesLeaf.Size = new System.Drawing.Size(136, 19);
            this.lblMinSamplesLeaf.TabIndex = 10;
            this.lblMinSamplesLeaf.Text = "Min Samples Leaf:";
            // 
            // txtMinSamplesLeaf
            // 
            this.txtMinSamplesLeaf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinSamplesLeaf.Depth = 0;
            this.txtMinSamplesLeaf.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMinSamplesLeaf.LeadingIcon = null;
            this.txtMinSamplesLeaf.Location = new System.Drawing.Point(col1X, startY + 425);
            this.txtMinSamplesLeaf.MaxLength = 50;
            this.txtMinSamplesLeaf.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMinSamplesLeaf.Multiline = false;
            this.txtMinSamplesLeaf.Name = "txtMinSamplesLeaf";
            this.txtMinSamplesLeaf.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtMinSamplesLeaf.TabIndex = 11;
            this.txtMinSamplesLeaf.Text = "1";
            this.txtMinSamplesLeaf.TrailingIcon = null;
            // 
            // lblMaxDepth
            // 
            this.lblMaxDepth.AutoSize = true;
            this.lblMaxDepth.Depth = 0;
            this.lblMaxDepth.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblMaxDepth.Location = new System.Drawing.Point(col1X, startY + 480);
            this.lblMaxDepth.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMaxDepth.Name = "lblMaxDepth";
            this.lblMaxDepth.Size = new System.Drawing.Size(84, 19);
            this.lblMaxDepth.TabIndex = 12;
            this.lblMaxDepth.Text = "Max Depth:";
            // 
            // txtMaxDepth
            // 
            this.txtMaxDepth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaxDepth.Depth = 0;
            this.txtMaxDepth.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMaxDepth.LeadingIcon = null;
            this.txtMaxDepth.Location = new System.Drawing.Point(col1X, startY + 505);
            this.txtMaxDepth.MaxLength = 50;
            this.txtMaxDepth.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMaxDepth.Multiline = false;
            this.txtMaxDepth.Name = "txtMaxDepth";
            this.txtMaxDepth.Size = new System.Drawing.Size(controlWidth, 50);
            this.txtMaxDepth.TabIndex = 13;
            this.txtMaxDepth.Text = "10";
            this.txtMaxDepth.TrailingIcon = null;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.White;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.TabIndex = 14;
            this.txtOutput.Text = "";
            // 
            // btnTrain
            // 
            this.btnTrain.AutoSize = false;
            this.btnTrain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTrain.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnTrain.Depth = 0;
            this.btnTrain.HighEmphasis = true;
            this.btnTrain.Icon = null;
            this.btnTrain.Location = new System.Drawing.Point(col2X, btnY);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnTrain.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnTrain.Size = new System.Drawing.Size(btnWidth, 36);
            this.btnTrain.TabIndex = 15;
            this.btnTrain.Text = "Train Model";
            this.btnTrain.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnTrain.UseAccentColor = false;
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.BtnTrain_Click);
            // 
            // btnOptimize
            // 
            this.btnOptimize.AutoSize = false;
            this.btnOptimize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOptimize.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnOptimize.Depth = 0;
            this.btnOptimize.HighEmphasis = true;
            this.btnOptimize.Icon = null;
            this.btnOptimize.Location = new System.Drawing.Point(col2X + 150, btnY);
            this.btnOptimize.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnOptimize.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnOptimize.Size = new System.Drawing.Size(btnWidth + 20, 36);
            this.btnOptimize.TabIndex = 16;
            this.btnOptimize.Text = "Optimize (Grid)";
            this.btnOptimize.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnOptimize.UseAccentColor = true;
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.BtnOptimize_Click);
            // 
            // btnPredict
            // 
            this.btnPredict.AutoSize = false;
            this.btnPredict.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPredict.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPredict.Depth = 0;
            this.btnPredict.HighEmphasis = true;
            this.btnPredict.Icon = null;
            this.btnPredict.Location = new System.Drawing.Point(col2X + 330, btnY);
            this.btnPredict.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPredict.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPredict.Size = new System.Drawing.Size(btnWidth + 20, 36);
            this.btnPredict.TabIndex = 17;
            this.btnPredict.Text = "Predict (Manual)";
            this.btnPredict.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnPredict.UseAccentColor = false;
            this.btnPredict.UseVisualStyleBackColor = true;
            this.btnPredict.Click += new System.EventHandler(this.BtnPredict_Click);
            // 
            // btnBack
            // 
            this.btnBack.AutoSize = false;
            this.btnBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBack.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBack.Depth = 0;
            this.btnBack.HighEmphasis = true;
            this.btnBack.Icon = null;
            this.btnBack.Name = "btnBack";
            this.btnBack.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBack.Size = new System.Drawing.Size(80, 36);
            this.btnBack.TabIndex = 18;
            this.btnBack.Text = "Back";
            this.btnBack.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnBack.UseAccentColor = false;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // DtRunnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 800);
            this.Sizable = true;
            
            // Anchors for Inputs (Default Top-Left is fine, explicit is better)
            this.lblTrainFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTrainFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblValFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMinSamplesSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMinSamplesSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMinSamplesLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMinSamplesLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMaxDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));

            // Output Box: Expand to fill right and bottom
            this.txtOutput.Location = new System.Drawing.Point(col2X, startY);
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // Adjust initial size to fill new larger form
            this.txtOutput.Size = new System.Drawing.Size(700, 600); 

            // Buttons: Anchor Bottom-Left (except Back)
            // Adjust initial Y position to be lower
            int newBtnY = 700;
            this.btnTrain.Location = new System.Drawing.Point(col2X, newBtnY);
            this.btnTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            
            this.btnOptimize.Location = new System.Drawing.Point(col2X + 150, newBtnY);
            this.btnOptimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            
            this.btnPredict.Location = new System.Drawing.Point(col2X + 330, newBtnY);
            this.btnPredict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            
            this.btnBack.Location = new System.Drawing.Point(col2X + 510, newBtnY);
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))); // Keep with group

            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnPredict);
            this.Controls.Add(this.btnOptimize);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtMaxDepth);
            this.Controls.Add(this.lblMaxDepth);
            this.Controls.Add(this.txtMinSamplesLeaf);
            this.Controls.Add(this.lblMinSamplesLeaf);
            this.Controls.Add(this.txtMinSamplesSplit);
            this.Controls.Add(this.lblMinSamplesSplit);
            this.Controls.Add(this.txtTotalSamples);
            this.Controls.Add(this.lblTotalSamples);
            this.Controls.Add(this.txtSchema);
            this.Controls.Add(this.lblSchema);
            this.Controls.Add(this.txtValFile);
            this.Controls.Add(this.lblValFile);
            this.Controls.Add(this.txtTrainFile);
            this.Controls.Add(this.lblTrainFile);
            this.Name = "DtRunnerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decision Tree Regression Runner";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel lblTrainFile;
        private MaterialSkin.Controls.MaterialTextBox txtTrainFile;
        private MaterialSkin.Controls.MaterialLabel lblValFile;
        private MaterialSkin.Controls.MaterialTextBox txtValFile;
        private MaterialSkin.Controls.MaterialLabel lblSchema;
        private MaterialSkin.Controls.MaterialTextBox txtSchema;
        private MaterialSkin.Controls.MaterialLabel lblTotalSamples;
        private MaterialSkin.Controls.MaterialTextBox txtTotalSamples;
        private MaterialSkin.Controls.MaterialLabel lblMinSamplesSplit;
        private MaterialSkin.Controls.MaterialTextBox txtMinSamplesSplit;
        private MaterialSkin.Controls.MaterialLabel lblMinSamplesLeaf;
        private MaterialSkin.Controls.MaterialTextBox txtMinSamplesLeaf;
        private MaterialSkin.Controls.MaterialLabel lblMaxDepth;
        private MaterialSkin.Controls.MaterialTextBox txtMaxDepth;
        private System.Windows.Forms.RichTextBox txtOutput;
        private MaterialSkin.Controls.MaterialButton btnTrain;
        private MaterialSkin.Controls.MaterialButton btnOptimize;
        private MaterialSkin.Controls.MaterialButton btnPredict;
        private MaterialSkin.Controls.MaterialButton btnBack;
    }
}
