using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skedaddler
{
	public partial class SettingsForm : Form
	{
		private Skedaddler parent;

		public SettingsForm(Skedaddler parent)
		{
			InitializeComponent();

			this.parent = parent;
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			this.Icon = Properties.Resources.skedaddlerIcon;
			loadSettings();

			this.label1.Font = new Font(parent.fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Font = new Font(parent.fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Font = new Font(parent.fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		}

		private bool saveIgnore = false;

		private void loadSettings()
		{
			saveIgnore = true;
			
			workDayLengthBox.Text = Properties.Settings.Default.WorkDayLength;
			autoAdjustBox.Text = Properties.Settings.Default.AutoAdjust;

			autoUpdateArrival.Checked = Properties.Settings.Default.AutoUpdateArrivalTime;

			arrivalUpdateURL.Text = Properties.Settings.Default.ArrivalUpdateURL;

			resetOnResume.Checked = Properties.Settings.Default.PendingResetOnResume;

			saveIgnore = false;
		}

		private void saveSettings()
		{
			if (saveIgnore)
				return;

			Properties.Settings.Default.WorkDayLength = workDayLengthBox.Text;
			Properties.Settings.Default.AutoAdjust = autoAdjustBox.Text;

			Properties.Settings.Default.AutoUpdateArrivalTime = autoUpdateArrival.Checked;

			Properties.Settings.Default.ArrivalUpdateURL = arrivalUpdateURL.Text;

			Properties.Settings.Default.Save();

			parent.updateTimeRemaining();
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			saveSettings();
		}

		private void workDayLengthBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustTimeSpan((TextBox)sender, 1, false, "8:00");
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustTimeSpan((TextBox)sender, -1, false, "8:00");
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				convertToTimeSpan((TextBox)sender, false);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.Delete)
			{
				((TextBox)sender).Text = "8:00";
				e.SuppressKeyPress = true;
			}
		}

		private void workDayLengthBox_TextChanged(object sender, EventArgs e)
		{
			saveSettings();
		}

		private void autoAdjustBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustTimeSpan((TextBox)sender, 1, true);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustTimeSpan((TextBox)sender, -1, true);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				convertToTimeSpan((TextBox)sender, true);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.Delete)
			{
				((TextBox)sender).Text = "0:00";
				e.SuppressKeyPress = true;
			}
		}

		private void autoAdjustBox_TextChanged(object sender, EventArgs e)
		{
			saveSettings();

			if (autoAdjustTooltip != null)
				autoAdjustTooltip.Hide(this.autoAdjustBox);
		}

		private void adjustTimeSpan(TextBox theTextbox, int value, bool canBeNegative, string defaultValue = "0:00")
		{
			TimeSpan time;
			if (theTextbox.Text.Length == 0 || !Skedaddler.parseTimeSpan(theTextbox.Text, out time))
			{
				theTextbox.Text = defaultValue;
				return;
			}

			time += TimeSpan.FromMinutes(value);
			if (time < TimeSpan.Zero && canBeNegative == false)
				time = TimeSpan.Zero;

			theTextbox.Text = (time < TimeSpan.Zero ? "-" : "") + String.Format("{0:h\\:mm}", time);
		}

		private void convertToTimeSpan(TextBox theTextbox, bool canBeNegative)
		{
			TimeSpan time = TimeSpan.Zero;

			// If the timespan is already valid there's no need to do anything more
			if (Skedaddler.parseTimeSpan(theTextbox.Text, out time))
				return;

			if (theTextbox.Text.Length > 0)
			{
				int minutes = 0;
				if (Int32.TryParse(theTextbox.Text, out minutes))
					time = TimeSpan.FromMinutes(minutes);
			}

			if (time < TimeSpan.Zero && canBeNegative == false)
				time = TimeSpan.Zero;

			theTextbox.Text = (time < TimeSpan.Zero ? "-" : "") + String.Format("{0:h\\:mm}", time);
		}

		private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		ToolTip autoAdjustTooltip;

		private void autoAdjustBox_Enter(object sender, EventArgs e)
		{
			autoAdjustTooltip = new ToolTip();
			autoAdjustTooltip.Show("Time adjustment for arrival\ntime on refresh (startup/auto).", this.autoAdjustBox, 0, 22);
		}

		private void autoAdjustBox_Leave(object sender, EventArgs e)
		{
			if (autoAdjustTooltip != null)
				autoAdjustTooltip.Hide(this.autoAdjustBox);
		}

		ToolTip autoUpdateArrivalTooltip;

		private void autoUpdateArrival_Enter(object sender, EventArgs e)
		{
			autoUpdateArrivalTooltip = new ToolTip();
			autoUpdateArrivalTooltip.Show("Automatically refreshes the arrival time when\nthe program notices the day has changed.",
				this.autoUpdateArrival, 0, 17);
		}

		private void autoUpdateArrival_Leave(object sender, EventArgs e)
		{
			if (autoUpdateArrivalTooltip != null)
				autoUpdateArrivalTooltip.Hide(this.autoAdjustBox);
		}

		private void arrivalWebAddress_TextChanged(object sender, EventArgs e)
		{
			saveSettings();
		}

		private void resetOnResume_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.PendingResetOnResume = resetOnResume.Checked;
			Properties.Settings.Default.Save();

			parent.updateTimeRemaining();
		}
	}
}
