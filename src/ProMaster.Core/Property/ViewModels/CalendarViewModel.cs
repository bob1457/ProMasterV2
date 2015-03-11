using System;

namespace ProMaster.Core.Property.ViewModels
{
    public class CalendarViewModel
    {
        public int CalendarId { get; set; }
        public string CalendarTitle { get; set; }
        public string CalendarDesc { get; set; }
        public string CalendarStart { get; set; }
        public string CalendarEnd { get; set; }
        public int PropertyManagerId { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
