namespace Remote
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flashEmbed = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.flashEmbed)).BeginInit();
            this.SuspendLayout();
            // 
            // flashEmbed
            // 
            this.flashEmbed.Enabled = true;
            this.flashEmbed.Location = new System.Drawing.Point(13, 13);
            this.flashEmbed.Name = "flashEmbed";
            this.flashEmbed.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flashEmbed.OcxState")));
            this.flashEmbed.Size = new System.Drawing.Size(526, 31);
            this.flashEmbed.TabIndex = 0;
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(13, 51);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(526, 362);
            this.log.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 425);
            this.Controls.Add(this.log);
            this.Controls.Add(this.flashEmbed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "RemoteServer";
            ((System.ComponentModel.ISupportInitialize)(this.flashEmbed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash flashEmbed;
        private System.Windows.Forms.TextBox log;
    }
}

