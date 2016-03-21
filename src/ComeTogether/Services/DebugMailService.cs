using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMessage(string from, string to, string title, string message)
        {
            Debug.WriteLine($"From: {from}; To: {to}. Title: {title}. Message info: {message}");
            return true;
        }
    }
}
