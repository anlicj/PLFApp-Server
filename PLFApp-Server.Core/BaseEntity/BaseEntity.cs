using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public class BaseEntity<PrimaryKeyType>
    {
        public PrimaryKeyType Id { get; set; }
    }
}
