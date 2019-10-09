using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Utils.Common
{
    public class RandomHelper
    {
        public static int Next(int minvalue,int maxvalue)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(minvalue, maxvalue);
        }
    }
}
