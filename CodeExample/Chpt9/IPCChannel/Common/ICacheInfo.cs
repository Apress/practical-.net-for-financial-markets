using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ICacheInfo
    {
        DataSet RetrieveCache();
    }
}
