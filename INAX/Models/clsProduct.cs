﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INAX.Models
{
    public class clsProduct
    {
        public int id
        {
            get;
            set;
        }
        public int idMenu
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Tag
        {
            get;
            set;
        }
        public int Ord
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
        public float SumPrice
        {
            get;
            set;
        }
    }
}