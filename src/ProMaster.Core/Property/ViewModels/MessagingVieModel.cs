using System;

namespace ProMaster.Core.Property.ViewModels
{
    public class MessagingVieModel
    {
        public int MessageId { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public string SentByUserName { get; set; }
        public string SentByFullName { get; set; }
        public DateTime SentDate { get; set; }
        public int MessageTypeId { get; set; }
        public string MessageType { get; set; }
        public bool IsRead { get; set; }
        public int Propertyid { get; set; }
        public string PropertyName { get; set; }
        public string ToUserName { get; set; }
        public string ToUFullName { get; set; }
        public bool ToAllTenants { get; set; }
        public bool ToAllLandlords { get; set; }

    }
}
