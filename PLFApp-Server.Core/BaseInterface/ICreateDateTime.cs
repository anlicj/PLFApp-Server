using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public interface ICreateDateTime
    {
        DateTime CreateDateTime { get; set; }
        string CreateDateTimeFormat { get; }
    }
}
