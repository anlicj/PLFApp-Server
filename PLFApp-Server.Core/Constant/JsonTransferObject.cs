using System;
using System.Collections.Generic;
using System.Text;

namespace PLFApp.Server.Core
{
    public class JsonTransferObject
    {
        public ResultType status { get; set; }
        public object content { get; set; }
        public JsonTransferObject(ResultType resultType,object data)
        {
            status = resultType;
            content = data;
        }
    }

    public enum ResultType
    {
        Success,
        Error,
        Fail,
        Logout
    }
}
