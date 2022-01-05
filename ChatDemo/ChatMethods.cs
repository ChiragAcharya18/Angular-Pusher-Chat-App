using ChatDemo.DTO;
using ChatDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo
{
    public class ChatMethods
    {
        private readonly ChatAppDemoContext context = new ChatAppDemoContext();

        public IQueryable<GetAllMessages> GetMessages()
        {
            var result = context.ChatsSp.FromSqlRaw<GetAllMessages>("EXEC GetMessages");
            //var result = context.ChatBackup;

            return result;
        }



        public string StoreMessage(MessageDTO message)
        {
            try
            {
                ChatBackup chat = new ChatBackup
                {
                    Username = message.Username,
                    Message = message.Message,
                    TextDateTime = Convert.ToDateTime(message.DateTime),
                    ChannelName = "chat",
                    EventName = "message"
                };
                context.ChatBackup.Add(chat);
                context.SaveChanges();
                return "pass";
            }
            catch (Exception)
            {
                return "fail";
            }
        }



    }
}
