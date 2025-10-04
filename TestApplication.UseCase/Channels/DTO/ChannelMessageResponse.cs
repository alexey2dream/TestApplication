using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.DTO
{
    public class ChannelMessageResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SendingTime { get; set; }
    }
}
