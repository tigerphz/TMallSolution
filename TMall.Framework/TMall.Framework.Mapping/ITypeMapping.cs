using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Mapping
{
    public interface ITypeMapping
    {
        TTo MapTo<TFrom, TTo>(TFrom from);
    }
}
