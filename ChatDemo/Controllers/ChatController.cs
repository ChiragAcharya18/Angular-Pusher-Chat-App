using ChatDemo.DTO;
using ChatDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        ChatMethods chatBackup = new ChatMethods();

        [HttpGet]
        [Route("Startup")]
        public string Get()
        {
            return "API is running";
        }


        [HttpGet]
        [Route("GetMessages")]
        public JsonResult GetMessages()
        {
            
            var msgs = chatBackup.GetMessages();

            return new JsonResult(msgs); 
        }


        [HttpPost]
        [Route("Users")]
        public JsonResult Users([FromBody] UserModal userModal)
        {
            ChatAppDemoContext chat = new ChatAppDemoContext();
            var res = chat.UserDetails.FromSqlRaw<UserModal>("EXEC UserDetails {0},{1},{2} ", userModal.Action, userModal.Username, Convert.ToInt32(userModal.Id));

            return new JsonResult(res);
        }

    /*    [Route("getFileById")]
        public FileResult getFileById(int fileId)
        {
                return PhysicalFile($"D:/AngularProjects/tem1p.mp4", "application/octet-stream", enableRangeProcessing: true);
        }*/

        [HttpPost(template: "messages")]
        public async Task<IActionResult> Message(MessageDTO dto)
        {
            var options = new PusherOptions
            {
                Cluster = "ap2",
                Encrypted = true
            };
            dto.DateTime = (dto.DateTime == null || dto.DateTime == "") ? DateTime.Now.ToString("dd-MM-yyy hh:mm tt ") : dto.DateTime;

            string res = "Test Store: " + chatBackup.StoreMessage(dto);

            var pusher = new Pusher(
                "1317350",
                "86e7f851df82ccf3895c",
                "fb82ad429e7535287b30",
                options
                );

             await pusher.TriggerAsync(
                channelName: "chat",
                eventName: "message",
                data: new {
                    username = dto.Username,
                    message = dto.Message,
                    dateTime = dto.DateTime
                });

            return Ok(new string[] {
            "Message Sent!", res
            } );    
        }
    }
}
