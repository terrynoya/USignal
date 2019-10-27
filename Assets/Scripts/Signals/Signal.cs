using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public class Signal:OnceSignal,ISignal
    {
        public ISlot Add(Delegate listener)
        {
            return this.RegisterListener(listener);
        }
    }
}
