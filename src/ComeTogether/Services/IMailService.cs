using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Services
{
    public interface IMailService
    {
        bool SendMessage(string from, string to, string title, string message);
    }
}
