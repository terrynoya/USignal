using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public interface ISlot
    {
        Delegate Listener { get; set; }
        bool Once { get; }
        bool Enabled { get; set; }
        void Excute(params object[] args);
        void Remove();
    }
}
