using System;

namespace ChatBot.Infrastructure.Core
{
    public class GenericResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
