﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender.Helpers
{
    public class Connection
    {
        public ConnectionFactory Open()
        {
            return new ConnectionFactory() { HostName = "localhost" };
        }
    }
}
