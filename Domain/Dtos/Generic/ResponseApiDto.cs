﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Generic
{
    public class ResponseApiDto
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public virtual object Data { get; set; }
    }
}
