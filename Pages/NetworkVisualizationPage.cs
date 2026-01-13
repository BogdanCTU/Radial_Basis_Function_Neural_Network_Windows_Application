using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Source;
using Source.Data;

namespace WinForm_RFBN_APP
{
    public partial class NetworkVisualizationPage : UserControl
    {
        private FoodClassifier _currentClassifier;
        private RbfNetwork _network; // Access internal network for visualization
        
        // Visualization Parameters
        private const int NODE_RADIUS = 15;
        private const int LAYER_PADDING = 50; // Padding from left/right/top/bottom

        private List<PointF> _hiddenNodePositions = new List<PointF>();

        public NetworkVisualizationPage()
        {
            InitializeComponent();
        }

        private void LoadModelButton_Click(object sender, EventArgs e)
        {
            string modelName = ModelNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(modelName))
            {
                MessageBox.Show("Please enter a model name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // We need to access the internal RbfNetwork. 
                // FoodClassifier wraps it, but looking at FoodClassifier.cs, it has private _network.
                // However, for visualization, we can either:
                // 1. Make RbfNetwork public in FoodClassifier (Refactor) -> Easiest
                // 2. Or Reflection -> Fragile
                // 3. Or LoadClassifier loads it, but returns FoodClassifier.
                
                // Let's check FoodClassifier code again...
                // It has no public accessor for _network. 
                // BUT, ModelRepository.LoadClassifier returns a FoodClassifier.
                // AND ModelRepository.cs constructs FoodClassifier passing the RbfNetwork.
                // I'll take a liberty here: Since I am "Antigravity", I should probably follow the architecture.
                // But wait, the user provided the codebase reference which implies I can modify if needed, OR 
                // I can implement a specific DTO for visualization.
                // Actually, reflection is quick and dirty for this specific visualization task without changing core library unless necessary.
                // Let's use Reflection to get the private _network field from FoodClassifier to avoid changing the shared library if possible?
                // No, it's better to update FoodClassifier to expose the Network properties for inspection/visualization. 
                // It is a valid use case for a "White Box" model.
                
                // FOR NOW, I will assume I can modify FoodClassifier later.
                // WAIT, I am supposed to modify files. I will update FoodClassifier.cs to add a public getter.
                
                _currentClassifier = ModelRepository.LoadClassifier(modelName);

                if (_currentClassifier == null)
                {
                    MessageBox.Show($"Model '{modelName}' not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Temporary Reflection to get the private network (to avoid multi-file edit right now, but I should probably fix it properly).
                // Actually, let's just do it properly. I will add a step to modify FoodClassifier.cs.
                // For this file content, I'll write the code effectively assuming a property 'Network' exists or using reflection.
                // Reflection is safer for now to avoid breaking other things if I make a mistake.
                
                var field = typeof(FoodClassifier).GetField("_network", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                _network = (RbfNetwork)field.GetValue(_currentClassifier);

                GraphPictureBox.Invalidate(); // Trigger redraw
                NeuronDetailsBox.Text = "Model Loaded successfully.\nHover over nodes to see details.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading model: {ex.Message}", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GraphPictureBox_Resize(object sender, EventArgs e)
        {
            GraphPictureBox.Invalidate();
        }

        private void GraphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_network == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = GraphPictureBox.Width;
            int height = GraphPictureBox.Height;

            int inputCount = _network.InputCount;
            int hiddenCount = _network.HiddenCount;
            int outputCount = 1; // RBF usually 1 output

            // --- Calculate Layer X Positions ---
            float xInput = width * 0.1f;
            float xHidden = width * 0.5f;
            float xOutput = width * 0.9f;

            _hiddenNodePositions.Clear();

            // --- Draw Connections (Input -> Hidden) ---
            // Just thin gray lines. Since there are many, we keep them subtle.
            using (Pen pen = new Pen(Color.FromArgb(50, Color.Gray), 1))
            {
                // We don't draw ALL N*M lines if N is huge, but here input is ~8, hidden ~25. 8*25 = 200 lines. Fine.
                for (int i = 0; i < inputCount; i++)
                {
                    PointF p1 = GetNeuronPosition(i, inputCount, xInput, height);
                    for (int j = 0; j < hiddenCount; j++)
                    {
                        PointF p2 = GetNeuronPosition(j, hiddenCount, xHidden, height);
                        g.DrawLine(pen, p1, p2);
                    }
                }
            }

            // --- Draw Connections (Hidden -> Output) ---
            // Color coded by Weight
            for (int j = 0; j < hiddenCount; j++)
            {
                PointF p1 = GetNeuronPosition(j, hiddenCount, xHidden, height);
                _hiddenNodePositions.Add(p1); // Store for hover detection

                PointF p2 = GetNeuronPosition(0, outputCount, xOutput, height); // Single output

                double weight = _network.Weights[j];
                Color lineColor = weight >= 0 ? Color.Green : Color.Red;
                float thickness = (float)(Math.Abs(weight) * 2.0); 
                // Clamp thickness
                if (thickness < 1) thickness = 1;
                if (thickness > 5) thickness = 5;

                using (Pen pen = new Pen(lineColor, thickness))
                {
                    g.DrawLine(pen, p1, p2);
                }
            }

            // --- Draw Nodes (Input) ---
            using (Brush brush = new SolidBrush(Color.LightBlue))
            using (Pen outline = new Pen(Color.Blue))
            {
                for (int i = 0; i < inputCount; i++)
                {
                    PointF p = GetNeuronPosition(i, inputCount, xInput, height);
                    g.FillEllipse(brush, p.X - NODE_RADIUS, p.Y - NODE_RADIUS, NODE_RADIUS * 2, NODE_RADIUS * 2);
                    g.DrawEllipse(outline, p.X - NODE_RADIUS, p.Y - NODE_RADIUS, NODE_RADIUS * 2, NODE_RADIUS * 2);
                    
                    // Draw Label
                    // If schema exists, use it. Else generic.
                    string label = $"Input {i+1}";
                    if (_currentClassifier != null && !string.IsNullOrEmpty(_currentClassifier.InputSchema))
                    {
                        var parts = _currentClassifier.InputSchema.Split(';');
                        if (i < parts.Length) label = parts[i];
                    }
                    
                    // Draw Label Text left aligned
                    g.DrawString(label, SystemFonts.DefaultFont, Brushes.Black, p.X - NODE_RADIUS - 80, p.Y - 6);
                }
            }

            // --- Draw Nodes (Hidden) ---
             using (Brush brush = new SolidBrush(Color.Orange))
            using (Pen outline = new Pen(Color.DarkOrange))
            {
                for (int j = 0; j < hiddenCount; j++)
                {
                    PointF p = GetNeuronPosition(j, hiddenCount, xHidden, height);
                    g.FillEllipse(brush, p.X - NODE_RADIUS, p.Y - NODE_RADIUS, NODE_RADIUS * 2, NODE_RADIUS * 2);
                    g.DrawEllipse(outline, p.X - NODE_RADIUS, p.Y - NODE_RADIUS, NODE_RADIUS * 2, NODE_RADIUS * 2);

                    // Show Sigma inside or near? 
                    // Nodes might be small. Let's just keep it clean, hover for details.
                    // Or minimal text if space allows.
                    // g.DrawString($"σ={_network.Sigmas[j]:F1}", SystemFonts.CaptionFont, Brushes.Black, p.X - 10, p.Y - 5);
                }
            }

            // --- Draw Nodes (Output) ---
             using (Brush brush = new SolidBrush(Color.LightGreen))
            using (Pen outline = new Pen(Color.Green))
            {
                PointF p = GetNeuronPosition(0, outputCount, xOutput, height);
                g.FillEllipse(brush, p.X - NODE_RADIUS * 1.5f, p.Y - NODE_RADIUS * 1.5f, NODE_RADIUS * 3, NODE_RADIUS * 3);
                g.DrawEllipse(outline, p.X - NODE_RADIUS * 1.5f, p.Y - NODE_RADIUS * 1.5f, NODE_RADIUS * 3, NODE_RADIUS * 3);
                g.DrawString("Output", SystemFonts.DefaultFont, Brushes.Black, p.X + 25, p.Y - 6);
                g.DrawString($"Bias: {_network.Bias:F3}", SystemFonts.CaptionFont, Brushes.Black, p.X + 25, p.Y + 10);
            }
        }

        private PointF GetNeuronPosition(int index, int totalCount, float x, float totalHeight)
        {
            float availableHeight = totalHeight - (2 * LAYER_PADDING);
            if (totalCount == 1)
            {
                return new PointF(x, totalHeight / 2);
            }
            else
            {
                float step = availableHeight / (totalCount - 1);
                float y = LAYER_PADDING + (index * step);
                return new PointF(x, y);
            }
        }

        private void GraphPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_network == null || _hiddenNodePositions.Count == 0) return;

            // Check if mouse is near any hidden node
            for (int i = 0; i < _hiddenNodePositions.Count; i++)
            {
                 PointF p = _hiddenNodePositions[i];
                 if (Distance(e.Location, p) <= NODE_RADIUS + 2)
                 {
                     ShowNeuronDetails(i);
                     return;
                 }
            }
        }

        private float Distance(Point p1, PointF p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private void ShowNeuronDetails(int hiddenIndex)
        {
            if (_network == null) return;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"--- Hidden Neuron #{hiddenIndex + 1} ---");
            sb.AppendLine();
            sb.AppendLine($"Sigma (Spread): {_network.Sigmas[hiddenIndex]:F4}");
            sb.AppendLine($"Output Weight:  {_network.Weights[hiddenIndex]:F4}");
            sb.AppendLine();
            sb.AppendLine("Centroid Vector:");
            
            // Get schema names if possible
            string[] featureNames = null;
            if (_currentClassifier != null && !string.IsNullOrEmpty(_currentClassifier.InputSchema))
            {
                featureNames = _currentClassifier.InputSchema.Split(';');
            }

            double[] centroid = _network.Centroids[hiddenIndex];
            for (int k = 0; k < centroid.Length; k++)
            {
                string name = (featureNames != null && k < featureNames.Length) ? featureNames[k] : $"Feature {k}";
                sb.AppendLine($"  {name}: {centroid[k]:F3}");
            }

            NeuronDetailsBox.Text = sb.ToString();
        }
    }
}
