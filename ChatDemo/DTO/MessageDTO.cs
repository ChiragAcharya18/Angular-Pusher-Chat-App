using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatDemo.DTO
{
   
    public class MessageDTO
    {
        [Key]     
        public string Username { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Message { get; set; }
        public string DateTime { get; set; }
    }
}
