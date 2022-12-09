
namespace Santa
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblBehaviour = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgwChildren = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBadCounter = new System.Windows.Forms.Label();
            this.txtBehaviour = new System.Windows.Forms.TextBox();
            this.lblBadCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgwChildren)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(77, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 20);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(27, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Név";
            // 
            // lblBehaviour
            // 
            this.lblBehaviour.AutoSize = true;
            this.lblBehaviour.Location = new System.Drawing.Point(12, 50);
            this.lblBehaviour.Name = "lblBehaviour";
            this.lblBehaviour.Size = new System.Drawing.Size(58, 13);
            this.lblBehaviour.TabIndex = 7;
            this.lblBehaviour.Text = "Viselkedés";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(37, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(142, 39);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Hozzáad";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgwChildren
            // 
            this.dgwChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwChildren.Location = new System.Drawing.Point(219, 12);
            this.dgwChildren.Name = "dgwChildren";
            this.dgwChildren.Size = new System.Drawing.Size(302, 252);
            this.dgwChildren.TabIndex = 9;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(416, 274);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 32);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblBadCounter
            // 
            this.lblBadCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBadCounter.AutoSize = true;
            this.lblBadCounter.Location = new System.Drawing.Point(306, 284);
            this.lblBadCounter.Name = "lblBadCounter";
            this.lblBadCounter.Size = new System.Drawing.Size(13, 13);
            this.lblBadCounter.TabIndex = 11;
            this.lblBadCounter.Text = "0";
            // 
            // txtBehaviour
            // 
            this.txtBehaviour.Location = new System.Drawing.Point(77, 47);
            this.txtBehaviour.Name = "txtBehaviour";
            this.txtBehaviour.Size = new System.Drawing.Size(121, 20);
            this.txtBehaviour.TabIndex = 13;
            this.txtBehaviour.Text = "3";
            // 
            // lblBadCount
            // 
            this.lblBadCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBadCount.AutoSize = true;
            this.lblBadCount.Location = new System.Drawing.Point(216, 284);
            this.lblBadCount.Name = "lblBadCount";
            this.lblBadCount.Size = new System.Drawing.Size(84, 13);
            this.lblBadCount.TabIndex = 14;
            this.lblBadCount.Text = "Rosszak száma:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 314);
            this.Controls.Add(this.lblBadCount);
            this.Controls.Add(this.txtBehaviour);
            this.Controls.Add(this.lblBadCounter);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dgwChildren);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblBehaviour);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgwChildren)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBehaviour;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgwChildren;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBadCounter;
        private System.Windows.Forms.TextBox txtBehaviour;
        private System.Windows.Forms.Label lblBadCount;
    }
}

