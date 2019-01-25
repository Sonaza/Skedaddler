using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skedaddler
{
	public partial class Skedaddler : Form
	{
		public Skedaddler()
		{
			InitializeComponent();
		}

		SoundPlayer soundPlayer;

		private void Skedaddler_Load(object sender, EventArgs e)
		{
			this.Icon = Properties.Resources.skedaddlerIcon;

			Timer timer = new Timer();
			timer.Interval = 500;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();

			arrivalTimeBox.Text = DateTime.Now.ToString(@"H\:mm", CultureInfo.InvariantCulture);
			flexMinutesBox.Text = "0:00";
			breakMinutesBox.Text = "0:00";

			soundPlayer = new SoundPlayer();
			soundPlayer.Stream = Properties.Resources.alarm;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			updateTimeRemaining();
			updateAlarm();
		}

		public bool parseDateTime(String timeString, out DateTime result)
		{
			if (timeString.Length == 0)
			{
				result = DateTime.MinValue;
				return false;
			}

			string[] formats = { @"H\:mm", @"H\.mm" };
			if (DateTime.TryParseExact(timeString, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result))
			{
				return true;
			}
			else
			{
				result = DateTime.MinValue;
				return false;
			}
		}
		
		public bool parseTimeSpan(String timeString, out TimeSpan result)
		{
			if (timeString.Length == 0)
			{
				result = TimeSpan.Zero;
				return false;
			}

			bool isNegative = false;
			if(timeString[0] == '-')
			{
				isNegative = true;
				timeString = timeString.Substring(1);
			}

			try
			{
				result = new TimeSpan(
					int.Parse(timeString.Split(':')[0]),
					int.Parse(timeString.Split(':')[1]),
					0);

				if (isNegative)
					result = -result;
				return true;
			}
			catch
			{
				result = TimeSpan.Zero;
				return false;
			}
		}

		public void updateTimeRemaining()
		{
			DateTime arrivalTime;
			if (!parseDateTime(arrivalTimeBox.Text, out arrivalTime))
			{
				leaveTimeLabel.Text = "-";
				timeUntilLabel.Text = "-";
				return;
			}

			TimeSpan workDayLength = TimeSpan.FromHours(8);

			TimeSpan flexTime = TimeSpan.Zero;
			if (parseTimeSpan(flexMinutesBox.Text, out flexTime))
			{
				workDayLength -= flexTime;
			}
			
			TimeSpan breakTime = TimeSpan.Zero;
			if (parseTimeSpan(breakMinutesBox.Text, out breakTime))
			{
				workDayLength += breakTime;
			}

			DateTime leaveTime = arrivalTime + workDayLength;
			leaveTimeLabel.Text = leaveTime.ToString(@"HH\:mm", CultureInfo.InvariantCulture);

			TimeSpan timeRemaining = leaveTime - DateTime.Now;
			if(timeRemaining < TimeSpan.Zero)
			{
				timeUntilLabel.Text = "Freeedooom!";
			}
			else
			{
				timeUntilLabel.Text = String.Format("{0}h {1}m {2}s", (int)timeRemaining.TotalHours, timeRemaining.Minutes, timeRemaining.Seconds);
			}
		}

		private void arrivalTimeBox_TextChanged(object sender, EventArgs e)
		{
			updateTimeRemaining();
		}

		private void flexMinutesBox_TextChanged(object sender, EventArgs e)
		{
			updateTimeRemaining();
		}

		private void breakMinutesBox_TextChanged(object sender, EventArgs e)
		{
			updateTimeRemaining();
		}

		bool alarmIsSet = false;
		bool alarmActive = false;
		DateTime alarmTime;

		private void updateAlarmTime()
		{
			if (alarmTimeBox.Text.Length == 0 || !parseDateTime(alarmTimeBox.Text, out alarmTime))
			{
				soundPlayer.Stop();
				alarmIsSet = false;
				alarmActive = false;
				return;
			}

			TimeSpan timeRemaining = alarmTime - DateTime.Now;
			if (timeRemaining > TimeSpan.Zero)
			{
				soundPlayer.Stop();
				alarmIsSet = true;
				alarmActive = false;
				alarmLabel.ForeColor = System.Drawing.Color.ForestGreen;
			}
		}

		int alarmCounter = 0;

		private void updateAlarm()
		{
			TimeSpan timeRemaining = alarmTime - DateTime.Now;
			if (alarmIsSet && !alarmActive && timeRemaining <= TimeSpan.Zero)
			{
				alarmActive = true;
				soundPlayer.PlayLooping();
			}

			if (alarmActive)
			{
				alarmCounter++;
				if (alarmCounter % 2 == 0)
				{
					alarmLabel.ForeColor = System.Drawing.Color.White;
					this.BackColor = Color.FromArgb(40, 40, 40);
				}
				else
				{
					alarmLabel.ForeColor = System.Drawing.Color.OrangeRed;
					this.BackColor = Color.FromArgb(20, 20, 20);
				}

				if (timeRemaining <= TimeSpan.FromSeconds(-15.0))
				{
					soundPlayer.Stop();
				}
			}
		}

		private void clearAlarm()
		{
			soundPlayer.Stop();
			alarmTimeBox.Text = "";
			alarmIsSet = false;
			alarmActive = false;
			alarmLabel.ForeColor = System.Drawing.Color.White;
			this.BackColor = Color.FromArgb(20, 20, 20);
		}

		private void alarmTimeBox_TextChanged(object sender, EventArgs e)
		{
			updateAlarmTime();
		}

		private void alarmTimeBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				alarmLabel.Focus();
			}

			if (e.KeyCode == Keys.Up)
			{
				adjustDateTime((TextBox)sender, 1, DateTime.Now, null);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustDateTime((TextBox)sender, -1, DateTime.Now, null);
				e.SuppressKeyPress = true;
			}
		}

		private void arrivalTimeBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustDateTime((TextBox)sender, isShiftDown() ? 10 : 1, null, null);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustDateTime((TextBox)sender, isShiftDown() ? -10 : -1, null, null);
				e.SuppressKeyPress = true;
			}
		}

		private bool isShiftDown()
		{
			return (Control.ModifierKeys & Keys.Shift) != 0;
		}

		private void flexMinutesBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustTimeSpan((TextBox)sender, 1);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustTimeSpan((TextBox)sender, -1);
				e.SuppressKeyPress = true;
			}
		}

		private void breakMinutesBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustTimeSpan((TextBox)sender, 1);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustTimeSpan((TextBox)sender, -1);
				e.SuppressKeyPress = true;
			}
		}

		private void adjustDateTime(TextBox theTextbox, int value, DateTime? lowLimit, DateTime? highLimit)
		{
			DateTime time;
			if (theTextbox.Text.Length == 0 || !parseDateTime(theTextbox.Text, out time))
			{
				time = DateTime.Now + TimeSpan.FromMinutes(5);
			}
			else
			{
				TimeSpan deltaTime = TimeSpan.FromMinutes(value);

				if (lowLimit != null && value < 0 && time + deltaTime <= lowLimit)
					return;

				if (highLimit != null && value > 0 && time + deltaTime >= highLimit)
					return;

				time += deltaTime;
			}
			

			theTextbox.Text = String.Format("{0:H\\:mm}", time);
			updateAlarmTime();
		}

		private void adjustTimeSpan(TextBox theTextbox, int value)
		{
			TimeSpan time;
			if (theTextbox.Text.Length == 0 || !parseTimeSpan(theTextbox.Text, out time))
				return;
			
			time += TimeSpan.FromMinutes(value);
			
			theTextbox.Text = (time < TimeSpan.Zero ? "-" : "") + String.Format("{0:h\\:mm}", time);

			updateAlarmTime();
		}

		private void clearAlarmButton_Click(object sender, EventArgs e)
		{
			clearAlarm();
		}
	}
}
