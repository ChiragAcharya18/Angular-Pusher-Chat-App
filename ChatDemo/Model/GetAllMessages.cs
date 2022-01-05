using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChatDemo.Model
{
    //[Keyless]
    public class GetAllMessages
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Message { get; set; }
        public string DateTime { get; set; }
    }
}
