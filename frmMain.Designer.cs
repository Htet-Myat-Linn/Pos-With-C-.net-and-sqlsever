namespace POS
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProfitLoss = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.masterDataToolStripMenuItem,
            this.processToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogIn,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuLogIn
            // 
            this.mnuLogIn.Name = "mnuLogIn";
            this.mnuLogIn.Size = new System.Drawing.Size(104, 22);
            this.mnuLogIn.Text = "LogIn";
            this.mnuLogIn.Click += new System.EventHandler(this.mnuLogIn_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(104, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // masterDataToolStripMenuItem
            // 
            this.masterDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem,
            this.mnuSupplier,
            this.mnuUser});
            this.masterDataToolStripMenuItem.Name = "masterDataToolStripMenuItem";
            this.masterDataToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.masterDataToolStripMenuItem.Text = "Master Data";
            // 
            // mnuItem
            // 
            this.mnuItem.Name = "mnuItem";
            this.mnuItem.Size = new System.Drawing.Size(117, 22);
            this.mnuItem.Text = "Item";
            this.mnuItem.Click += new System.EventHandler(this.mnuItem_Click);
            // 
            // mnuSupplier
            // 
            this.mnuSupplier.Name = "mnuSupplier";
            this.mnuSupplier.Size = new System.Drawing.Size(117, 22);
            this.mnuSupplier.Text = "Supplier";
            this.mnuSupplier.Click += new System.EventHandler(this.mnuSupplier_Click);
            // 
            // mnuUser
            // 
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(117, 22);
            this.mnuUser.Text = "User";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPurchase,
            this.mnuSale,
            this.mnuProfitLoss});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // mnuPurchase
            // 
            this.mnuPurchase.Name = "mnuPurchase";
            this.mnuPurchase.Size = new System.Drawing.Size(154, 22);
            this.mnuPurchase.Text = "Purchase";
            this.mnuPurchase.Click += new System.EventHandler(this.mnuPurchase_Click);
            // 
            // mnuSale
            // 
            this.mnuSale.Name = "mnuSale";
            this.mnuSale.Size = new System.Drawing.Size(154, 22);
            this.mnuSale.Text = "Sale";
            this.mnuSale.Click += new System.EventHandler(this.mnuSale_Click);
            // 
            // mnuProfitLoss
            // 
            this.mnuProfitLoss.Name = "mnuProfitLoss";
            this.mnuProfitLoss.Size = new System.Drawing.Size(154, 22);
            this.mnuProfitLoss.Text = "Profit And Loss";
            this.mnuProfitLoss.Click += new System.EventHandler(this.mnuProfitLoss_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Emoji", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(387, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "\"POINT OF SALE SYSTEM\"";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(226)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(1041, 413);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Point of Sale System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem masterDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem mnuItem;
        public System.Windows.Forms.ToolStripMenuItem mnuSupplier;
        public System.Windows.Forms.ToolStripMenuItem mnuUser;
        public System.Windows.Forms.ToolStripMenuItem mnuPurchase;
        public System.Windows.Forms.ToolStripMenuItem mnuSale;
        public System.Windows.Forms.ToolStripMenuItem mnuProfitLoss;
        private System.Windows.Forms.ToolStripMenuItem mnuLogIn;
        private System.Windows.Forms.Label label1;
    }
}

