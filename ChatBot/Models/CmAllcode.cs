using System;
using System.Collections.Generic;

namespace ChatBot.Models
{
    public partial class CmAllcode
    {
        public int Id { get; set; }
        public string Cdname { get; set; }
        public string Cdval { get; set; }
        public string Content { get; set; }
        public string Cdtype { get; set; }
        public int? Lstodr { get; set; }
    }
}
