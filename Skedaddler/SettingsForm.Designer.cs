
namespace Skedaddler
{
	partial class SettingsForm
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
			this.closeButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.autoAdjustBox = new System.Windows.Forms.TextBox();
			this.workDayLengthBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.autoUpdateArrival = new System.Windows.Forms.CheckBox();
			this.arrivalUpdateURL = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.resetOnResume = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// closeButton
			// 
			this.closeButton.ForeColor = System.Drawing.Color.Black;
			this.closeButton.Location = new System.Drawing.Point(223, 26);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(100, 24);
			this.closeButton.TabIndex = 6;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(136, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Auto Adjust";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// autoAdjustBox
			// 
			this.autoAdjustBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.autoAdjustBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.autoAdjustBox.ForeColor = System.Drawing.Color.White;
			this.autoAdjustBox.Location = new System.Drawing.Point(117, 29);
			this.autoAdjustBox.MaxLength = 6;
			this.autoAdjustBox.Name = "autoAdjustBox";
			this.autoAdjustBox.Size = new System.Drawing.Size(100, 20);
			this.autoAdjustBox.TabIndex = 2;
			this.autoAdjustBox.Text = "0:00";
			this.autoAdjustBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.autoAdjustBox.TextChanged += new System.EventHandler(this.autoAdjustBox_TextChanged);
			this.autoAdjustBox.Enter += new System.EventHandler(this.autoAdjustBox_Enter);
			this.autoAdjustBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.autoAdjustBox_KeyDown);
			this.autoAdjustBox.Leave += new System.EventHandler(this.autoAdjustBox_Leave);
			// 
			// workDayLengthBox
			// 
			this.workDayLengthBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.workDayLengthBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.workDayLengthBox.ForeColor = System.Drawing.Color.White;
			this.workDayLengthBox.Location = new System.Drawing.Point(11, 29);
			this.workDayLengthBox.MaxLength = 5;
			this.workDayLengthBox.Name = "workDayLengthBox";
			this.workDayLengthBox.Size = new System.Drawing.Size(100, 20);
			this.workDayLengthBox.TabIndex = 1;
			this.workDayLengthBox.Text = "8:00";
			this.workDayLengthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.workDayLengthBox.TextChanged += new System.EventHandler(this.workDayLengthBox_TextChanged);
			this.workDayLengthBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.workDayLengthBox_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Work Day Length";
			// 
			// autoUpdateArrival
			// 
			this.autoUpdateArrival.AutoSize = true;
			this.autoUpdateArrival.Location = new System.Drawing.Point(11, 58);
			this.autoUpdateArrival.Name = "autoUpdateArrival";
			this.autoUpdateArrival.Size = new System.Drawing.Size(191, 17);
			this.autoUpdateArrival.TabIndex = 3;
			this.autoUpdateArrival.Text = "Auto Update Arrival Time Next Day";
			this.autoUpdateArrival.UseVisualStyleBackColor = true;
			this.autoUpdateArrival.Enter += new System.EventHandler(this.autoUpdateArrival_Enter);
			this.autoUpdateArrival.Leave += new System.EventHandler(this.autoUpdateArrival_Leave);
			// 
			// arrivalUpdateURL
			// 
			this.arrivalUpdateURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.arrivalUpdateURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.arrivalUpdateURL.ForeColor = System.Drawing.Color.White;
			this.arrivalUpdateURL.Location = new System.Drawing.Point(12, 94);
			this.arrivalUpdateURL.MaxLength = 2000;
			this.arrivalUpdateURL.Name = "arrivalUpdateURL";
			this.arrivalUpdateURL.Size = new System.Drawing.Size(311, 20);
			this.arrivalUpdateURL.TabIndex = 5;
			this.arrivalUpdateURL.TextChanged += new System.EventHandler(this.arrivalWebAddress_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(179, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "URL to open on Auto Arrival update:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// resetOnResume
			// 
			this.resetOnResume.AutoSize = true;
			this.resetOnResume.Location = new System.Drawing.Point(212, 58);
			this.resetOnResume.Name = "resetOnResume";
			this.resetOnResume.Size = new System.Drawing.Size(111, 17);
			this.resetOnResume.TabIndex = 4;
			this.resetOnResume.Text = "Reset on Resume";
			this.resetOnResume.UseVisualStyleBackColor = true;
			this.resetOnResume.CheckedChanged += new System.EventHandler(this.resetOnResume_CheckedChanged);
			this.resetOnResume.Enter += new System.EventHandler(this.resetOnResume_Enter);
			this.resetOnResume.Leave += new System.EventHandler(this.resetOnResume_Leave);
			// 
			// SettingsForm
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.ClientSize = new System.Drawing.Size(334, 125);
			this.Controls.Add(this.resetOnResume);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.arrivalUpdateURL);
			this.Controls.Add(this.autoUpdateArrival);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.autoAdjustBox);
			this.Controls.Add(this.workDayLengthBox);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.SystemColors.Window;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Name = "SettingsForm";
			this.Text = "Skedaddler Settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox autoAdjustBox;
		private System.Windows.Forms.TextBox workDayLengthBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox autoUpdateArrival;
		private System.Windows.Forms.TextBox arrivalUpdateURL;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox resetOnResume;
	}
}