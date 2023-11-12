using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworking.Models
{
    public class MessageViewModel
    {
        public Guid MessageID { get; set; }

        public UserViewModel SenderUser { get; set; }

        public UserViewModel ReceiverUser { get; set; }
        public DateTime TimeSend { get; set; }
        public string Content { get; set; }


    }
}
