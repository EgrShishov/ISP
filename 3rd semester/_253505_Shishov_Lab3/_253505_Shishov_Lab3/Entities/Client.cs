﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253505_Shishov_Lab3.Entities
{
    internal class Client
    {
        List<Order> client_orders;
        string client_name;

        public Client(string name)
        { 
            client_name = name;
            client_orders = new List<Order>();
        }

        public List<Order> ClientOrders
        {
            get { return client_orders; }
            set { client_orders = value; }
        }

        public string ClientName
        {
            get { return client_name; }
            set { client_name = value; }
        }

        public override string ToString()
        {
            return $" name - {client_name}";
        }
    }
}
