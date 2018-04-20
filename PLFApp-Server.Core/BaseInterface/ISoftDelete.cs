using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public interface ISoftDelete
    {
        bool IsDelete { get; set; }
    }
}
