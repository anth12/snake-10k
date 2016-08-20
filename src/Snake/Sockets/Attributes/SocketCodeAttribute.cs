using System;

namespace Snake.Sockets.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SocketCodeAttribute : Attribute
    {
        public SocketCodeAttribute(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
