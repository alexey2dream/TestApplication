using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;
using TestApplication.Infrastructure.Databases.Read;
using TestApplication.UseCase.Channels.DTO;
using TestApplication.UseCase.Channels.QueryReps;
using TestApplication.UseCase.Chats.DTO;
using TestApplication.UseCase.Users.DTO;

namespace TestApplication.Infrastructure.Data.Read.QueryRepositories
{
    public class ChannelQueryRep : IChannelQueryRep
    {
        private IDbConnectionFactory connectionFactory;
        public ChannelQueryRep(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<List<ChannelResponse>> GetAllByUserId(int pageNum, int pageSize, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Channels"
                limit @Limit offset @Offset;
                """;
            var channels = await connection.QueryAsync<ChannelResponse>(sql, new
            {
                Limit = pageSize * pageNum,
                Offset = (pageNum - 1) * pageSize
            });
            return channels.ToList();
        }

        public async Task<ChannelInfoResponse> GetAllMessageByChannelId(int channelId, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Channels" c
                join "ChannelsMessages" cM on cM."ChannelId" = c."Id"
                where c."Id" = @ChannelId;
                """;
            Dictionary<int, ChannelInfoResponse> channelResponseDictionary = new Dictionary<int, ChannelInfoResponse>();
            var channels = await connection.QueryAsync<ChannelInfoResponse, ChannelMessageResponse, ChannelInfoResponse>(sql,
                (chat, message) =>
                {
                    if (channelResponseDictionary.TryGetValue(channelId, out var existingChat))
                        chat = existingChat;
                    else
                        channelResponseDictionary.Add(chat.Id, chat);
                    chat.Messages.Add(message);
                    return chat;
                },
                new { ChannelId = channelId });
            return channelResponseDictionary.Count() != 0 ? channelResponseDictionary[channelId] : null;
        }

        public async Task<int> GetTotalAmountChannels(CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select count(*) from "Channels";
                """;
            return await connection.QuerySingleAsync<int>(sql);
        }
        public async Task<int> GetTotalAmountMessagesByChannelId(int channelId, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select count(*) from "Channels" c
                join "ChannelsMessages" cM on cM."ChannelId" = c."Id"
                where c."Id" = @ChannelId;
                """;
            return await connection.QuerySingleAsync<int>(sql, new { ChannelId = channelId});
        }
    }
}
