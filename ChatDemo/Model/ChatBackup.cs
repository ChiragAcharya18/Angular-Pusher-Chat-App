using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ChatDemo.Model
{
    public partial class ChatBackup
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime TextDateTime { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Message { get; set; }
        public string ChannelName { get; set; }
        public string EventName { get; set; }
    }
}
