using System;

namespace Ixq.Extensions
{
    public static class CheckParameter
    {
        public static void CheckIsNull(object parame)
        {
            if (parame == null)
                throw new ArgumentNullException(nameof(parame));
        }
    }
}