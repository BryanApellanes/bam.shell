﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuFooterRenderer
    {
        void RenderMenuFooter(IMenu menu, params IMenu[] otherMenus);
    }
}
