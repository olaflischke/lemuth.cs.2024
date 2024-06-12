namespace EierfarmUi
{
    partial class frmEierfarm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNeuesHuhn = new Button();
            this.cbxTiere = new ComboBox();
            this.pgdTier = new PropertyGrid();
            this.btnEiLegen = new Button();
            this.btnFuettern = new Button();
            this.btnNeueGans = new Button();
            this.SuspendLayout();
            // 
            // btnNeuesHuhn
            // 
            this.btnNeuesHuhn.Location = new Point(206, 12);
            this.btnNeuesHuhn.Name = "btnNeuesHuhn";
            this.btnNeuesHuhn.Size = new Size(103, 23);
            this.btnNeuesHuhn.TabIndex = 0;
            this.btnNeuesHuhn.Text = "Neues Huhn";
            this.btnNeuesHuhn.UseVisualStyleBackColor = true;
            this.btnNeuesHuhn.Click += this.btnNeuesHuhn_Click;
            // 
            // cbxTiere
            // 
            this.cbxTiere.FormattingEnabled = true;
            this.cbxTiere.Location = new Point(70, 13);
            this.cbxTiere.Name = "cbxTiere";
            this.cbxTiere.Size = new Size(130, 23);
            this.cbxTiere.TabIndex = 1;
            this.cbxTiere.SelectedIndexChanged += this.cbxTiere_SelectedIndexChanged;
            // 
            // pgdTier
            // 
            this.pgdTier.Location = new Point(70, 42);
            this.pgdTier.Name = "pgdTier";
            this.pgdTier.Size = new Size(130, 190);
            this.pgdTier.TabIndex = 2;
            // 
            // btnEiLegen
            // 
            this.btnEiLegen.Location = new Point(206, 168);
            this.btnEiLegen.Name = "btnEiLegen";
            this.btnEiLegen.Size = new Size(103, 23);
            this.btnEiLegen.TabIndex = 3;
            this.btnEiLegen.Text = "Ei legen";
            this.btnEiLegen.UseVisualStyleBackColor = true;
            this.btnEiLegen.Click += this.btnEiLegen_Click;
            // 
            // btnFuettern
            // 
            this.btnFuettern.Location = new Point(206, 139);
            this.btnFuettern.Name = "btnFuettern";
            this.btnFuettern.Size = new Size(103, 23);
            this.btnFuettern.TabIndex = 4;
            this.btnFuettern.Text = "Füttern";
            this.btnFuettern.UseVisualStyleBackColor = true;
            this.btnFuettern.Click += this.btnFuettern_Click;
            // 
            // btnNeueGans
            // 
            this.btnNeueGans.Location = new Point(206, 41);
            this.btnNeueGans.Name = "btnNeueGans";
            this.btnNeueGans.Size = new Size(103, 23);
            this.btnNeueGans.TabIndex = 5;
            this.btnNeueGans.Text = "Neue Gans";
            this.btnNeueGans.UseVisualStyleBackColor = true;
            this.btnNeueGans.Click += this.btnNeueGans_Click;
            // 
            // frmEierfarm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(321, 263);
            this.Controls.Add(this.btnNeueGans);
            this.Controls.Add(this.btnFuettern);
            this.Controls.Add(this.btnEiLegen);
            this.Controls.Add(this.pgdTier);
            this.Controls.Add(this.cbxTiere);
            this.Controls.Add(this.btnNeuesHuhn);
            this.Name = "frmEierfarm";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        #endregion

        private Button btnNeuesHuhn;
        private ComboBox cbxTiere;
        private PropertyGrid pgdTier;
        private Button btnEiLegen;
        private Button btnFuettern;
        private Button btnNeueGans;
    }
}
