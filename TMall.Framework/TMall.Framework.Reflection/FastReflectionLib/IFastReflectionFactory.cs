using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Reflection.FastReflection
{
    public interface IFastReflectionFactory<TKey, TValue>
    {
        TValue Create(TKey key);
    }
}
