using System.Windows.Forms;
using System.Drawing;

namespace WinForm_RFBN_APP
{
    partial class NetworkVisualizationPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.LoadModelButton = new MaterialSkin.Controls.MaterialButton();
            this.ModelNameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.GraphPictureBox = new System.Windows.Forms.PictureBox();
            this.DetailsPanel = new System.Windows.Forms.Panel();
            this.DetailsLabel = new MaterialSkin.Controls.MaterialLabel();
            this.NeuronDetailsBox = new System.Windows.Forms.RichTextBox();

            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).BeginInit();
            this.DetailsPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.ModelNameTextBox);
            this.TopPanel.Controls.Add(this.LoadModelButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(900, 60);
            this.TopPanel.TabIndex = 0;

            // 
            // ModelNameTextBox
            // 
            this.ModelNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModelNameTextBox.Depth = 0;
            this.ModelNameTextBox.Font = new System.Drawing.Font("Roboto", 12F);
            this.ModelNameTextBox.Hint = "Model Name (e.g. FoodClassifier_V1)";
            this.ModelNameTextBox.Location = new System.Drawing.Point(190, 8);
            this.ModelNameTextBox.MaxLength = 50;
            this.ModelNameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.ModelNameTextBox.Name = "ModelNameTextBox";
            this.ModelNameTextBox.Size = new System.Drawing.Size(250, 50);
            this.ModelNameTextBox.TabIndex = 1;
            this.ModelNameTextBox.Text = "FoodClassifier_V1";
            this.ModelNameTextBox.UseTallSize = false;
            
            // 
            // LoadModelButton
            // 
            this.LoadModelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadModelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.LoadModelButton.Depth = 0;
            this.LoadModelButton.HighEmphasis = true;
            this.LoadModelButton.Icon = null;
            this.LoadModelButton.Location = new System.Drawing.Point(20, 12);
            this.LoadModelButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LoadModelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadModelButton.Name = "LoadModelButton";
            this.LoadModelButton.Size = new System.Drawing.Size(150, 36);
            this.LoadModelButton.TabIndex = 0;
            this.LoadModelButton.Text = "Load Trained Model";
            this.LoadModelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.LoadModelButton.UseAccentColor = false;
            this.LoadModelButton.UseVisualStyleBackColor = true;
            this.LoadModelButton.Click += new System.EventHandler(this.LoadModelButton_Click);

            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.Location = new System.Drawing.Point(0, 60);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.GraphPictureBox);
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.NeuronDetailsBox);
            this.MainContainer.Panel2.Controls.Add(this.DetailsLabel);
            this.MainContainer.Panel2.Controls.Add(this.DetailsPanel);
            this.MainContainer.Size = new System.Drawing.Size(900, 540);
            this.MainContainer.SplitterDistance = 650;
            this.MainContainer.TabIndex = 1;

            // 
            // GraphPictureBox
            // 
            this.GraphPictureBox.BackColor = System.Drawing.Color.White;
            this.GraphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GraphPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphPictureBox.Location = new System.Drawing.Point(0, 0);
            this.GraphPictureBox.Name = "GraphPictureBox";
            this.GraphPictureBox.Size = new System.Drawing.Size(650, 540);
            this.GraphPictureBox.TabIndex = 0;
            this.GraphPictureBox.TabStop = false;
            this.GraphPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPictureBox_Paint);
            this.GraphPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphPictureBox_MouseMove);
            this.GraphPictureBox.Resize += new System.EventHandler(this.GraphPictureBox_Resize);

            // 
            // DetailsPanel
            // 
            this.DetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.DetailsPanel.Name = "DetailsPanel";
            this.DetailsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.DetailsPanel.Size = new System.Drawing.Size(246, 540);
            this.DetailsPanel.TabIndex = 0;

            // 
            // DetailsLabel
            // 
            this.DetailsLabel.AutoSize = true;
            this.DetailsLabel.Depth = 0;
            this.DetailsLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DetailsLabel.Location = new System.Drawing.Point(10, 10);
            this.DetailsLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(109, 19);
            this.DetailsLabel.TabIndex = 0;
            this.DetailsLabel.Text = "Neuron Details";

            // 
            // NeuronDetailsBox
            // 
            this.NeuronDetailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NeuronDetailsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.NeuronDetailsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NeuronDetailsBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.NeuronDetailsBox.Location = new System.Drawing.Point(10, 40);
            this.NeuronDetailsBox.Name = "NeuronDetailsBox";
            this.NeuronDetailsBox.ReadOnly = true;
            this.NeuronDetailsBox.Size = new System.Drawing.Size(226, 490);
            this.NeuronDetailsBox.TabIndex = 1;
            this.NeuronDetailsBox.Text = "Hover over a hidden neuron to see details...";

            // 
            // NetworkVisualizationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.TopPanel);
            this.Name = "NetworkVisualizationPage";
            this.Size = new System.Drawing.Size(900, 600);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).EndInit();
            this.DetailsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private MaterialSkin.Controls.MaterialButton LoadModelButton;
        private MaterialSkin.Controls.MaterialTextBox ModelNameTextBox;
        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.PictureBox GraphPictureBox;
        private System.Windows.Forms.Panel DetailsPanel;
        private MaterialSkin.Controls.MaterialLabel DetailsLabel;
        private System.Windows.Forms.RichTextBox NeuronDetailsBox;
    }
}
