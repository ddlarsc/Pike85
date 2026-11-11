using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Control;
using Model;
using Calendar = System.Web.UI.WebControls.Calendar;

namespace UserControls
{
    public partial class UcCalendar : UserControl
    {
        private List<ArrangementSetupObject> _setups;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Empty
        }

        protected void Calendar1DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected)
            {
                DecorateSelectedDate(e);
            }
        }

        protected void Calendar1PreRender(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                InitiateCal(Calendar1, DateTime.Now);
                InitiateCal(Calendar2, DateTime.Now.AddMonths(1));
                InitiateCal(Calendar3, DateTime.Now.AddMonths(2));
                FillControls();
            }
        }

        protected void Calendar2DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected)
            {
                DecorateSelectedDate(e);
            }
        }

        protected void Calendar3DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected)
            {
                DecorateSelectedDate(e);
            }
        }

        #region Private Methods

        private void FillControls()
        {
            var myConnection = Application["MySQLConnString"].ToString();
            var name = Context.User.Identity.Name;
            var ctrl = new ArrangementController();
            _setups = ctrl.GetSpecificParticipantForCalendar(myConnection, name);

            foreach (var setup in _setups)
            {
                if (!VerifyringDates(Calendar1, setup.Start, setup.End))
                {
                    if (!VerifyringDates(Calendar2, setup.Start, setup.End))
                    {
                        VerifyringDates(Calendar3, setup.Start, setup.End);
                    }
                }
            }
        }

        private static void InitiateCal(Calendar cal, DateTime offset)
        {
            cal.FirstDayOfWeek = FirstDayOfWeek.Monday;
            cal.OtherMonthDayStyle.Font.Bold = false;
            cal.OtherMonthDayStyle.ForeColor = Color.LightGray;
            cal.ShowDayHeader = false;
            cal.ShowNextPrevMonth = false;
            cal.SelectedDayStyle.BackColor = Color.White;
            cal.SelectedDayStyle.Font.Bold = false;
            cal.SelectedDayStyle.ForeColor = Color.Gray;
            cal.TitleStyle.BackColor = Color.White;
            cal.TitleStyle.BorderColor = Color.White;
            cal.TitleStyle.ForeColor = Color.Black;
            cal.TodaysDate = offset;
        }

        private static bool VerifyringDates(Calendar cal, DateTime startDate, DateTime endDate)
        {
            var offset = cal.TodaysDate.Month;
            var firstDay = DateTime.Parse(cal.TodaysDate.Year + "-" + offset + "-01");
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            if (offset < startDate.Month)
            {
                return false;
            }
            if (endDate.Month < offset)
            {
                return false;
            }
            if (startDate.Month == offset && offset == endDate.Month)
            {
                SelectingDates(cal, startDate, endDate);
                return true;
            }
            if (startDate.Month == offset && offset < endDate.Month)
            {
                SelectingDates(cal, startDate, lastDay);
                return false;
            }
            if (startDate.Month < offset && offset == endDate.Month)
            {
                SelectingDates(cal, firstDay, endDate);
                return false;
            }
            if (startDate.Month < offset && offset < endDate.Month)
            {
                SelectingDates(cal, firstDay, lastDay);
                return false;
            }
            return false;
        }

        private static void SelectingDates(Calendar cal, DateTime firstDate, DateTime lastDate)
        {
            if (cal.SelectedDates.Count == 0)
            {
                cal.SelectedDates.SelectRange(firstDate, lastDate);
            }
            else
            {
                for (int idx = 0; idx < (lastDate.DayOfYear - firstDate.DayOfYear + 1); idx++)
                {
                    cal.SelectedDates.Add(firstDate.AddDays(idx));
                }
            }
        }

        private void DecorateSelectedDate(DayRenderEventArgs e)
        {
            foreach (var setup in _setups)
            {
                if (setup.Start <= e.Day.Date && e.Day.Date <= setup.End)
                {
                    e.Cell.BackColor = Color.DarkGray;
                    if (setup.Arrangements.Count > 0)
                    {
                        bool isDeclined = (setup.Arrangements[0].Decline == 1); //Deltager = 0 - deltager Ikke = 1!
                        if (isDeclined)
                        {
                            e.Cell.BackColor = Color.DarkRed;
                        }
                        else if (setup.Arrangements[0].ArrivelDate <= e.Day.Date && e.Day.Date <= setup.Arrangements[0].DepartureDate)
                        {
                            e.Cell.BackColor = Color.DarkGreen;
                        }
                    }

                    var sb = new StringBuilder();
                    sb.AppendFormat(" <a");
                    sb.AppendFormat(" href='/Medlemmer/Arrangement/View.aspx?id={0}'", setup.Id);
                    sb.AppendFormat(" title='{0}'", setup.Header);
                    sb.AppendFormat(" style='color:Black'");
                    sb.AppendFormat(" >");
                    sb.AppendFormat(e.Day.Date.Day.ToString(CultureInfo.InvariantCulture));
                    sb.AppendFormat("</a>");
                    e.Cell.Text = sb.ToString();

                    break;
                }
            }
        }

        #endregion Private Methods
    }
}
