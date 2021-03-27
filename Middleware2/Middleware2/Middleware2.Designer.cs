namespace Middleware2
{
    partial class Middleware2
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
            this.sendButton = new System.Windows.Forms.Button();
            this.sentBox = new System.Windows.Forms.ListBox();
            this.receivedBox = new System.Windows.Forms.ListBox();
            this.readyBox = new System.Windows.Forms.ListBox();
            this.sentLabel = new System.Windows.Forms.Label();
            this.receivedLabel = new System.Windows.Forms.Label();
            this.readyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendButton.Location = new System.Drawing.Point(36, 30);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(163, 52);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // sentBox
            // 
            this.sentBox.FormattingEnabled = true;
            this.sentBox.ItemHeight = 16;
            this.sentBox.Location = new System.Drawing.Point(31, 147);
            this.sentBox.Name = "sentBox";
            this.sentBox.Size = new System.Drawing.Size(207, 260);
            this.sentBox.TabIndex = 1;
            // 
            // receivedBox
            // 
            this.receivedBox.FormattingEnabled = true;
            this.receivedBox.ItemHeight = 16;
            this.receivedBox.Location = new System.Drawing.Point(261, 147);
            this.receivedBox.Name = "receivedBox";
            this.receivedBox.Size = new System.Drawing.Size(275, 260);
            this.receivedBox.TabIndex = 2;
            // 
            // readyBox
            // 
            this.readyBox.FormattingEnabled = true;
            this.readyBox.ItemHeight = 16;
            this.readyBox.Location = new System.Drawing.Point(561, 147);
            this.readyBox.Name = "readyBox";
            this.readyBox.Size = new System.Drawing.Size(212, 260);
            this.readyBox.TabIndex = 3;
            // 
            // sentLabel
            // 
            this.sentLabel.AutoSize = true;
            this.sentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sentLabel.Location = new System.Drawing.Point(89, 111);
            this.sentLabel.Name = "sentLabel";
            this.sentLabel.Size = new System.Drawing.Size(61, 26);
            this.sentLabel.TabIndex = 4;
            this.sentLabel.Text = "Sent";
            // 
            // receivedLabel
            // 
            this.receivedLabel.AutoSize = true;
            this.receivedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.receivedLabel.Location = new System.Drawing.Point(333, 111);
            this.receivedLabel.Name = "receivedLabel";
            this.receivedLabel.Size = new System.Drawing.Size(111, 26);
            this.receivedLabel.TabIndex = 5;
            this.receivedLabel.Text = "Received";
            // 
            // readyLabel
            // 
            this.readyLabel.AutoSize = true;
            this.readyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readyLabel.Location = new System.Drawing.Point(625, 111);
            this.readyLabel.Name = "readyLabel";
            this.readyLabel.Size = new System.Drawing.Size(80, 26);
            this.readyLabel.TabIndex = 6;
            this.readyLabel.Text = "Ready";
            this.readyLabel.Click += new System.EventHandler(this.Label3_Click);
            // 
            // Middleware2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.readyLabel);
            this.Controls.Add(this.receivedLabel);
            this.Controls.Add(this.sentLabel);
            this.Controls.Add(this.readyBox);
            this.Controls.Add(this.receivedBox);
            this.Controls.Add(this.sentBox);
            this.Controls.Add(this.sendButton);
            this.Name = "Middleware2";
            this.Text = "Middleware 2";
            this.Load += new System.EventHandler(this.Middleware1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ListBox sentBox;
        private System.Windows.Forms.ListBox receivedBox;
        private System.Windows.Forms.ListBox readyBox;
        private System.Windows.Forms.Label sentLabel;
        private System.Windows.Forms.Label receivedLabel;
        private System.Windows.Forms.Label readyLabel;
    }
}

