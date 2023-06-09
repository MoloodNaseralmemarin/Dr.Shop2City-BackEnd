﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DrShop2City.Infrastructure.DTOs.Orders
{
    public class OrderBasketDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public int Price { get; set; }

        public string ImageName { get; set; }

        public int Count { get; set; }
    }
}
