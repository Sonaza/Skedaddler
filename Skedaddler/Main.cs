using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
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

		private void Skedaddler_Load(object sender, EventArgs e)
		{
			Timer timer = new Timer();
			timer.Interval = 500;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();

			arrivalTimeBox.Text = DateTime.Now.ToString(@"H\:mm", CultureInfo.InvariantCulture);
			flexMinutesBox.Text = "0:00";
			breakMinutesBox.Text = "0:00";
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			updateTimeRemaining();
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
// 				timeUntilLabel.Text = timeRemaining.ToString("h'h 'm'm 's's'", CultureInfo.InvariantCulture);
// 				timeUntilLabel.Text = timeRemaining.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
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
	}
}
