using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public class OnceSignal:IOnceSignal
    {
        protected SlotList slots = SlotList.NIL;

        public int NumListeners()
        {
            return this.slots.Length;
        }

        public ISlot AddOnce(Delegate listener)
        {
            return this.RegisterListener(listener, true);
        }

        public ISlot Remove(Delegate listener)
        {
            ISlot slot = this.slots.Find(listener);
            if (slot == null)
            {
                return null;
            }

            this.slots = this.slots.FilterNot(listener);
            return slot;
        }

        public void RemoveAll()
        {
            this.slots = SlotList.NIL;
        }

        public void Dispatch(params object[] args)
        {
            SlotList slotsToProcess = this.slots;
            if (slotsToProcess.nonEmpty)
            {
                while (slotsToProcess.nonEmpty)
                {
                    slotsToProcess.Head.Excute(args);
                    slotsToProcess = slotsToProcess.tail;
                }
            }
        }

        protected ISlot RegisterListener(Delegate listener, bool once = false)
        {
            if (this.RegisterPossible(listener, once))
            {
                ISlot newSlot = new Slot(listener,this,once);
                slots = slots.Prepend(newSlot);
                return newSlot;
            }
            return this.slots.Find(listener);
        }

        protected bool RegisterPossible(Delegate listener, bool once)
        {
            if (!slots.nonEmpty)
            {
                return true;
            }

            ISlot existSlot = this.slots.Find(listener);
            if (existSlot == null)
            {
                return true;
            }

            if (existSlot.Once != once)
            {
                throw new Exception("add once then add");
            }

            return false;
        }
    }
}
