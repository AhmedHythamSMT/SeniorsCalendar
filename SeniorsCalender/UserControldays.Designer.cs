namespace SeniorsCalender
{
    partial class UserControldays
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControldays));
            this.lbdays = new System.Windows.Forms.Label();
            this.lbEvName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbEvID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbdays
            // 
            resources.ApplyResources(this.lbdays, "lbdays");
            this.lbdays.Name = "lbdays";
            // 
            // lbEvName
            // 
            resources.ApplyResources(this.lbEvName, "lbEvName");
            this.lbEvName.Name = "lbEvName";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbEvID
            // 
            resources.ApplyResources(this.lbEvID, "lbEvID");
            this.lbEvID.Name = "lbEvID";
            // 
            // UserControldays
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbEvID);
            this.Controls.Add(this.lbEvName);
            this.Controls.Add(this.lbdays);
            this.Name = "UserControldays";
            this.Load += new System.EventHandler(this.UserControldays_Load);
            this.Click += new System.EventHandler(this.UserControldays_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbdays;
        private System.Windows.Forms.Label lbEvName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbEvID;
    }
}
