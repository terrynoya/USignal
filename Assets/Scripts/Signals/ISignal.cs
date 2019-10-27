using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public interface ISignal:IOnceSignal
    {
        ISlot Add(Delegate listener);
    }
}
