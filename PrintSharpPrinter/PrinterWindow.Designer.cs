namespace PrintSharpPrinter
{
    partial class PrinterWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // inputLog
            // 
            this.inputLog.Location = new System.Drawing.Point(12, 55);
            this.inputLog.Name = "inputLog";
            this.inputLog.Size = new System.Drawing.Size(252, 147);
            this.inputLog.TabIndex = 2;
            this.inputLog.Text = "";
            this.inputLog.TextChanged += new System.EventHandler(this.inputLog_TextChanged);
            // 
            // Printer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.inputLog);
            this.Name = "Printer";
            this.Text = "Printer";
            this.Load += new System.EventHandler(this.Printer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputLog;
    }
}

