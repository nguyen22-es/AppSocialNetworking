namespace DataAccess.Entities
{
    public class Messages
    {
        public Guid MessageID { get; set; }

        public string  SenderUserID { get; set; }

        public string ReceiverUserID { get; set;}
        public DateTime TimeSend { get; set;}
        public string Content { get; set; }
        public ManagerUser FromUser { get; set; }

        public ManagerUser ToUser { get; set; }
    }
}
