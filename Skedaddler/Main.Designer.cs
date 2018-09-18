using System.Windows.Forms;

namespace Skedaddler
{
	partial class Skedaddler
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
			this.label1 = new System.Windows.Forms.Label();
			this.arrivalTimeBox = new System.Windows.Forms.TextBox();
			this.flexMinutesBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.breakMinutesBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.leaveTimeLabel = new System.Windows.Forms.Label();
			this.timeUntilLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.alarmTimeBox = new System.Windows.Forms.TextBox();
			this.clearAlarmButton = new System.Windows.Forms.Button();
			this.alarmLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Arrived at work";
			// 
			// arrivalTimeBox
			// 
			this.arrivalTimeBox.Location = new System.Drawing.Point(12, 28);
			this.arrivalTimeBox.MaxLength = 5;
			this.arrivalTimeBox.Name = "arrivalTimeBox";
			this.arrivalTimeBox.Size = new System.Drawing.Size(100, 22);
			this.arrivalTimeBox.TabIndex = 1;
			this.arrivalTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.arrivalTimeBox.TextChanged += new System.EventHandler(this.arrivalTimeBox_TextChanged);
			// 
			// flexMinutesBox
			// 
			this.flexMinutesBox.Location = new System.Drawing.Point(118, 28);
			this.flexMinutesBox.MaxLength = 6;
			this.flexMinutesBox.Name = "flexMinutesBox";
			this.flexMinutesBox.Size = new System.Drawing.Size(100, 22);
			this.flexMinutesBox.TabIndex = 2;
			this.flexMinutesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.flexMinutesBox.TextChanged += new System.EventHandler(this.flexMinutesBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(131, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Flex minutes";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(251, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Breaks";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// breakMinutesBox
			// 
			this.breakMinutesBox.Location = new System.Drawing.Point(224, 28);
			this.breakMinutesBox.MaxLength = 5;
			this.breakMinutesBox.Name = "breakMinutesBox";
			this.breakMinutesBox.Size = new System.Drawing.Size(100, 22);
			this.breakMinutesBox.TabIndex = 3;
			this.breakMinutesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.breakMinutesBox.TextChanged += new System.EventHandler(this.breakMinutesBox_TextChanged);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label4.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(311, 15);
			this.label4.TabIndex = 6;
			this.label4.Text = "You can skedaddle at";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// leaveTimeLabel
			// 
			this.leaveTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.leaveTimeLabel.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.leaveTimeLabel.Location = new System.Drawing.Point(12, 79);
			this.leaveTimeLabel.Name = "leaveTimeLabel";
			this.leaveTimeLabel.Size = new System.Drawing.Size(312, 22);
			this.leaveTimeLabel.TabIndex = 7;
			this.leaveTimeLabel.Text = "13:37";
			this.leaveTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timeUntilLabel
			// 
			this.timeUntilLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.timeUntilLabel.BackColor = System.Drawing.Color.Transparent;
			this.timeUntilLabel.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeUntilLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.timeUntilLabel.Location = new System.Drawing.Point(12, 122);
			this.timeUntilLabel.Name = "timeUntilLabel";
			this.timeUntilLabel.Size = new System.Drawing.Size(312, 37);
			this.timeUntilLabel.TabIndex = 8;
			this.timeUntilLabel.Text = "2h 12m 5s";
			this.timeUntilLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label5.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(12, 107);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(312, 15);
			this.label5.TabIndex = 9;
			this.label5.Text = "Time remaining";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// alarmTimeBox
			// 
			this.alarmTimeBox.Location = new System.Drawing.Point(118, 172);
			this.alarmTimeBox.MaxLength = 6;
			this.alarmTimeBox.Name = "alarmTimeBox";
			this.alarmTimeBox.Size = new System.Drawing.Size(100, 22);
			this.alarmTimeBox.TabIndex = 10;
			this.alarmTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.alarmTimeBox.TextChanged += new System.EventHandler(this.alarmTimeBox_TextChanged);
			this.alarmTimeBox.KeyDown += new KeyEventHandler(this.alarmTimeBox_KeyDown);
			
			// 
			// clearAlarmButton
			// 
			this.clearAlarmButton.ForeColor = System.Drawing.Color.Black;
			this.clearAlarmButton.Location = new System.Drawing.Point(224, 171);
			this.clearAlarmButton.Name = "clearAlarmButton";
			this.clearAlarmButton.Size = new System.Drawing.Size(98, 23);
			this.clearAlarmButton.TabIndex = 11;
			this.clearAlarmButton.Text = "Clear Alarm";
			this.clearAlarmButton.UseVisualStyleBackColor = true;
			this.clearAlarmButton.Click += new System.EventHandler(this.clearAlarmButton_Click);
			// 
			// alarmLabel
			// 
			this.alarmLabel.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.alarmLabel.ForeColor = System.Drawing.Color.White;
			this.alarmLabel.Location = new System.Drawing.Point(12, 172);
			this.alarmLabel.Name = "alarmLabel";
			this.alarmLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.alarmLabel.Size = new System.Drawing.Size(100, 22);
			this.alarmLabel.TabIndex = 12;
			this.alarmLabel.Text = "Alarm:";
			this.alarmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Skedaddler
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.ClientSize = new System.Drawing.Size(335, 206);
			this.Controls.Add(this.alarmLabel);
			this.Controls.Add(this.clearAlarmButton);
			this.Controls.Add(this.alarmTimeBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.timeUntilLabel);
			this.Controls.Add(this.leaveTimeLabel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.breakMinutesBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.flexMinutesBox);
			this.Controls.Add(this.arrivalTimeBox);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.Window;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Skedaddler";
			this.Text = "Skedaddler";
			this.Load += new System.EventHandler(this.Skedaddler_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox arrivalTimeBox;
		private System.Windows.Forms.TextBox flexMinutesBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox breakMinutesBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label leaveTimeLabel;
		private System.Windows.Forms.Label timeUntilLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox alarmTimeBox;
		private System.Windows.Forms.Button clearAlarmButton;
		private System.Windows.Forms.Label alarmLabel;
	}
}

