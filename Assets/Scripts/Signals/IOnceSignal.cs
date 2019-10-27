using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public interface IOnceSignal
    {
        int NumListeners();
        ISlot AddOnce(Delegate listener);
        ISlot Remove(Delegate listener);
        void RemoveAll();
        void Dispatch(params object[] args);
    }
}
