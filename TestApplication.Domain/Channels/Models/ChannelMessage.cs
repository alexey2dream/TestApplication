using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users;

namespace TestApplication.Domain.Channels.Models
{
    public class ChannelMessage
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime SendingTime { get; private set; }
        public int ChannelId { get; private set; }
        public Channel Channel { get; private set; }
        private ChannelMessage() { }
        //private ChannelMessage()
        //{
            
        //}


    }
}
