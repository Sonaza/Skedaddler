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
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Text;

namespace Skedaddler
{
	public partial class Skedaddler : Form
	{
		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
			IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

		private PrivateFontCollection fonts = new PrivateFontCollection();

		SoundPlayer soundPlayer;

		public Skedaddler()
		{
			InitializeComponent();

			byte[] fontData = Properties.Resources.OpenSans;
			IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
			System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
			uint dummy = 0;
			fonts.AddMemoryFont(fontPtr, Properties.Resources.OpenSans.Length);
			AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.OpenSans.Length, IntPtr.Zero, ref dummy);
			System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

			this.label4.Font = new Font(fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.leaveTimeLabel.Font = new Font(fonts.Families[0], 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.timeUntilLabel.Font = new Font(fonts.Families[0], 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.label5.Font = new Font(fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.alarmLabel.Font = new Font(fonts.Families[0], 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.Font = new Font(fonts.Families[0], 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		}

		private void Skedaddler_Load(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.WindowLocation.X == 0 || Properties.Settings.Default.WindowLocation.Y == 0)
			{
				Properties.Settings.Default.Upgrade();

				CenterToScreen();
				Properties.Settings.Default.WindowLocation = this.Location;
			}
			else
			{
				this.WindowState = Properties.Settings.Default.WindowState;

				// we don't want a minimized window at startup
				if (this.WindowState == FormWindowState.Minimized)
					this.WindowState = FormWindowState.Normal;

				this.Location = Properties.Settings.Default.WindowLocation;
			}

			this.Icon = Properties.Resources.skedaddlerIcon;

			Timer timer = new Timer();
			timer.Interval = 500;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();

			if (!reloadValues())
			{
				arrivalTimeBox.Text = DateTime.Now.ToString(@"H\:mm", CultureInfo.InvariantCulture);
				flexMinutesBox.Text = "0:00";
				breakMinutesBox.Text = "0:00";
			}
			saveCurrentValues();

			soundPlayer = new SoundPlayer();
			soundPlayer.Stream = Properties.Resources.alarm;	
		}

		private void Skedaddler_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.WindowState = this.WindowState;
			if (this.WindowState == FormWindowState.Normal)
			{
				Properties.Settings.Default.WindowLocation = this.Location;
			}
			else
			{
				Properties.Settings.Default.WindowLocation = this.RestoreBounds.Location;
			}

			Properties.Settings.Default.Save();
		}

		private void saveCurrentValues()
		{
			System.Console.WriteLine("Saving data:");
			System.Console.WriteLine("  arrivalTimeBox " + arrivalTimeBox.Text);
			System.Console.WriteLine("  flexMinutesBox " + flexMinutesBox.Text);
			System.Console.WriteLine("  breakMinutesBox " + breakMinutesBox.Text);

			Properties.Settings.Default.LastStateUpdate = DateTime.Today;
			Properties.Settings.Default.LastArrivalTime = arrivalTimeBox.Text;
			Properties.Settings.Default.LastFlexMinutes = flexMinutesBox.Text;
			Properties.Settings.Default.LastBreakMinutes = breakMinutesBox.Text;
			Properties.Settings.Default.Save();
		}

		private bool reloadValues()
		{

			try
			{
				if (!Properties.Settings.Default.LastStateUpdate.Equals(DateTime.Today))
					return false;
			}
			catch
			{
				// Any exception would tell things are wrong or uninitialized
				return false;
			}

			System.Console.WriteLine("Loading data:");
			System.Console.WriteLine("  LastArrivalTime " + Properties.Settings.Default.LastArrivalTime);
			System.Console.WriteLine("  LastFlexMinutes " + Properties.Settings.Default.LastFlexMinutes);
			System.Console.WriteLine("  LastBreakMinutes " + Properties.Settings.Default.LastBreakMinutes);

			// Copy values over since changing the fields will trigger immediate re-save and overwriting values
			string lastArrivalTime = Properties.Settings.Default.LastArrivalTime;
			string lastFlexMinutes = Properties.Settings.Default.LastFlexMinutes;
			string lastBreakMinutes = Properties.Settings.Default.LastBreakMinutes;

			DateTime dt;
			if (parseDateTime(lastArrivalTime, out dt))
				arrivalTimeBox.Text = lastArrivalTime;
			else
				arrivalTimeBox.Text = DateTime.Now.ToString(@"H\:mm", CultureInfo.InvariantCulture);

			TimeSpan ts;
			if (parseTimeSpan(lastFlexMinutes, out ts))
				flexMinutesBox.Text = lastFlexMinutes;
			else
				flexMinutesBox.Text = "0:00";

			if (parseTimeSpan(lastBreakMinutes, out ts))
				breakMinutesBox.Text = lastBreakMinutes;
			else
				breakMinutesBox.Text = "0:00";

			return true;
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

			Regex timeSpanRegex = new Regex(@"(-?)([012]?[0-9]):([0-5][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

			MatchCollection matches = timeSpanRegex.Matches(timeString);

			if (matches.Count != 1)
			{
				result = TimeSpan.Zero;
				return false;
			}

			Match match = matches[0];
			GroupCollection groups = match.Groups;

			try
			{
				result = new TimeSpan(
					int.Parse(groups[2].Value),
					int.Parse(groups[3].Value),
					0
				);

				if (groups[1].Value.Equals("-"))
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
			saveCurrentValues();
		}

		private void flexMinutesBox_TextChanged(object sender, EventArgs e)
		{
			updateTimeRemaining();
			saveCurrentValues();
		}

		private void breakMinutesBox_TextChanged(object sender, EventArgs e)
		{
			updateTimeRemaining();
			saveCurrentValues();
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
			else if(e.KeyCode == Keys.Enter)
			{
				convertToDatetime((TextBox)sender);
				alarmLabel.Focus();
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.Delete)
			{
				clearAlarm();
				updateAlarmTime();
				e.SuppressKeyPress = true;
			}
		}
		private void alarmTimeBox_LostFocus(object sender, EventArgs e)
		{
			convertToDatetime((TextBox)sender);
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
				flexMinutesBox.Text = "0:00";
				e.SuppressKeyPress = true;
			}
		}
		private void flexMinutesBox_LostFocus(object sender, EventArgs e)
		{
			convertToTimeSpan((TextBox)sender, true);
		}

		private void breakMinutesBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				adjustTimeSpan((TextBox)sender, 1, false);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				adjustTimeSpan((TextBox)sender, -1, false);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				convertToTimeSpan((TextBox)sender, false);
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Z || e.KeyCode == Keys.Delete)
			{
				breakMinutesBox.Text = "0:00";
				e.SuppressKeyPress = true;
			}
		}

		private void breakMinutesBox_LostFocus(object sender, EventArgs e)
		{
			convertToTimeSpan((TextBox)sender, false);
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

		private void convertToDatetime(TextBox theTextbox)
		{
			DateTime time = DateTime.Now;
			TimeSpan offset = TimeSpan.Zero;

			// If the datetime is already valid there's no need to do anything more
			if (parseDateTime(theTextbox.Text, out time))
				return;

			if (theTextbox.Text.Length > 0)
			{
				int minutes = 0;
				if (Int32.TryParse(theTextbox.Text, out minutes))
					offset = TimeSpan.FromMinutes(minutes);
			}
			
			if (offset <= TimeSpan.Zero)
			{
				clearAlarm();
				updateAlarmTime();
				return;
			}

			time = DateTime.Now + offset;
			
			theTextbox.Text = String.Format("{0:H\\:mm}", time);
			updateAlarmTime();
		}

		private void adjustTimeSpan(TextBox theTextbox, int value, bool canBeNegative)
		{
			TimeSpan time;
			if (theTextbox.Text.Length == 0 || !parseTimeSpan(theTextbox.Text, out time))
			{
				theTextbox.Text = "0:00";
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
			if (parseTimeSpan(theTextbox.Text, out time))
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

		private void clearAlarmButton_Click(object sender, EventArgs e)
		{
			clearAlarm();
		}
	}
}
