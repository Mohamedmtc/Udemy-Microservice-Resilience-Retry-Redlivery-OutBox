﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Order.Event
{
    public class IOrderStartedEvent
    {
        public Guid OrderId { get; set; }

        public string PaymentCardNumber { get; set; }

        public string ProductName { get; set; }
        public bool IsCanceled { get; set; }
    }
}
