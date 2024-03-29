﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuItem<T>: IMenuItem where T: Attribute
    {
        new T? Attribute { get; set; }
    }
}
