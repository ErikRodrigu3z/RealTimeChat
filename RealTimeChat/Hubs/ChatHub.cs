﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task MessageSender(string user, string message) 
        { 
            await Clients.All.SendAsync("MessageReceiver", user, message);
            
        }


    }
}
