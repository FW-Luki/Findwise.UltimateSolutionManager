﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.Sharepoint.SolutionInstaller
{
    interface IProgressRepresentative
    {
        void SetProgress(ReportProgressEventArgs rpevent);
    }
}
