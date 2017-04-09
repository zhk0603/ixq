using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Extensions
{
    public static class CheckParameter
    {
        public static void CheckIsNull(object parame)
        {
            if (parame == null)
            {
                throw new ArgumentNullException(nameof(parame));
            }
        }
    }
}
