using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Results;
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
        private ChannelMessage(string text, DateTime sendingTime, int channelId)
        {
            Text = text;
            SendingTime = sendingTime;
            ChannelId = channelId;
        }
        internal static Result<ChannelMessage> Create(string text, Channel channel)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Result<ChannelMessage>.Failure("Text is null or empty!");
            if (channel is null)
                return Result<ChannelMessage>.Failure("Channel is null!");
            return Result<ChannelMessage>.Success(new ChannelMessage(text, DateTime.UtcNow, channel.Id));
        }
    }
}
