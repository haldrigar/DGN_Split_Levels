namespace DGN_Split_Levels
{
    partial class FormMain
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonRozdziel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(63, 36);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(341, 20);
            this.textBoxFileName.TabIndex = 0;
            // 
            // buttonRozdziel
            // 
            this.buttonRozdziel.Location = new System.Drawing.Point(316, 63);
            this.buttonRozdziel.Name = "buttonRozdziel";
            this.buttonRozdziel.Size = new System.Drawing.Size(75, 23);
            this.buttonRozdziel.TabIndex = 1;
            this.buttonRozdziel.Text = "buttonRozdziel";
            this.buttonRozdziel.UseVisualStyleBackColor = true;
            this.buttonRozdziel.Click += new System.EventHandler(this.ButtonRozdziel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 283);
            this.Controls.Add(this.buttonRozdziel);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonRozdziel;
    }
}

