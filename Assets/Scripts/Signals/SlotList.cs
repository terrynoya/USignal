using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.terrynoya.signals
{
    public class SlotList
    {
        public ISlot Head;
        public SlotList tail;

        public bool nonEmpty = false;

        public static SlotList NIL = new SlotList(null,null);

        public SlotList(ISlot head, SlotList tail = null)
        {
            if (head == null && tail == null)
            {
                nonEmpty = false;
            }
            else if (head == null)
            {
                throw new Exception("param head cannot be null");
            }
            else
            {
                this.Head = head;
                this.tail = tail == null ? NIL : tail;
                this.nonEmpty = true;
            }
        }

        public SlotList Prepend(ISlot slot)
        {
            return new SlotList(slot,this);
        }

        public SlotList Append(ISlot slot)
        {
            if (slot == null)
            {
                return this;
            }
            if (!nonEmpty)
            {
                return new SlotList(slot);
            }
            if (this.tail == NIL)
            {
                return new SlotList(slot).Prepend(this.Head);
            }

            SlotList wholeClone = new SlotList(this.Head);
            SlotList subClone = wholeClone;
            SlotList current = this.tail;

            while (current.nonEmpty)
            {
                subClone = subClone.tail = new SlotList(current.Head);
                current = current.tail;
            }
            subClone.tail = new SlotList(slot);
            return wholeClone;
        }

        public SlotList FilterNot(Delegate listener)
        {
            if (!this.nonEmpty || listener == null)
            {
                return this;
            }
            if (listener == Head.Listener)
            {
                return this.tail;
            }
            SlotList wholeClone = new SlotList(this.Head);
            SlotList subClone = wholeClone;
            SlotList current = this.tail;

            while (current.nonEmpty)
            {
                if (current.Head.Listener == listener)
                {
                    subClone.tail = current.tail;
                    return wholeClone;
                }

                subClone = subClone.tail = new SlotList(current.Head);
                current = current.tail;
            }

            return this;
        }

        public bool Contains(Delegate listener)
        {
            if (!this.nonEmpty)
            {
                return false;
            }
            SlotList p = this;
            while (p.nonEmpty)
            {
                if(p.Head.Listener == listener)
                {
                    return true;
                }
            }
            return false;
        }

        public ISlot Find(Delegate listener)
        {
            if (!nonEmpty)
            {
                return null;
            }
            SlotList p = this;
            while (p.nonEmpty)
            {
                if (p.Head.Listener == listener)
                {
                    return p.Head;
                }
                p = p.tail;
            }
            return null;
        }

        public int Length
        {
            get
            {
                if (!this.nonEmpty)
                {
                    return 0;
                }
                if (this.tail == null)
                {
                    return 1;
                }

                int rlt = 0;
                SlotList p = this;
                while (p.nonEmpty)
                {
                    ++rlt;
                    p = p.tail;
                }
                return rlt;
            }
        }
    }
}
