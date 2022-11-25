﻿using Microsoft.Extensions.Options;
using RabbitMq.Common.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMq.Common.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:50083") 
                
            };
            // connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
           
        }
    }
}
