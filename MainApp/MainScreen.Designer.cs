
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace WinForm_RFBN_APP
{
    partial class MainScreen
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
            this.btnRbf = new MaterialSkin.Controls.MaterialButton();
            this.btnDt = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // btnRbf
            // 
            this.btnRbf.AutoSize = false;
            this.btnRbf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRbf.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRbf.Depth = 0;
            this.btnRbf.HighEmphasis = true;
            this.btnRbf.Icon = null;
            this.btnRbf.Location = new System.Drawing.Point(100, 90);
            this.btnRbf.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRbf.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRbf.Name = "btnRbf";
            this.btnRbf.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRbf.Size = new System.Drawing.Size(300, 60);
            this.btnRbf.TabIndex = 0;
            this.btnRbf.Text = "Classification Task (RBF Network)";
            this.btnRbf.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnRbf.UseAccentColor = false;
            this.btnRbf.UseVisualStyleBackColor = true;
            this.btnRbf.Click += new System.EventHandler(this.btnRbf_Click);
            // 
            // btnDt
            // 
            this.btnDt.AutoSize = false;
            this.btnDt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDt.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnDt.Depth = 0;
            this.btnDt.HighEmphasis = true;
            this.btnDt.Icon = null;
            this.btnDt.Location = new System.Drawing.Point(100, 170);
            this.btnDt.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDt.Name = "btnDt";
            this.btnDt.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnDt.Size = new System.Drawing.Size(300, 60);
            this.btnDt.TabIndex = 1;
            this.btnDt.Text = "Regression Task (Decision Tree)";
            this.btnDt.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnDt.UseAccentColor = true;
            this.btnDt.UseVisualStyleBackColor = true;
            this.btnDt.Click += new System.EventHandler(this.btnDt_Click);
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.btnDt);
            this.Controls.Add(this.btnRbf);
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ML Project - Task Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton btnRbf;
        private MaterialSkin.Controls.MaterialButton btnDt;
    }
}
