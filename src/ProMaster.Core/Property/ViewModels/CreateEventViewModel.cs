using System;

namespace ProMaster.Core.Property.ViewModels
{
    public class CreateEventViewModel
    {
        public int PropertyId { get; set; }
        
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }
        public DateTime EventDate { get; set; }
        public int EventTypeId { get; set; }
        public decimal MileageIncurred { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
