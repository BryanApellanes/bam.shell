﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInputMethodArgumentProvider
    {
        object?[] GetMethodArguments(IMenuItem menuItem, IMenuInput menuInput);
    }
}
