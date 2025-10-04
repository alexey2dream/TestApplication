using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.DTO
{
    public class ChannelInfoResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatorId { get; set; }
        public List<ChannelMessageResponse> Messages { get; set; } = new List<ChannelMessageResponse>();
    }
}
