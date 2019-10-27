using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.terrynoya.signals
{
    public class Slot:ISlot
    {
        private Delegate _listener;
        protected bool _once = false;
        private int _priority = 0;
        private bool _enabled = true;
        private IOnceSignal _signal;
        private object[] _params;

        public Slot(Delegate listener, IOnceSignal signal, bool once = false, int priority = 0)
        {
            this._listener = listener;
            this._signal = signal;
            this._once = once;
            this._priority = priority;

        }

        public void Excute(params object[] args)
        {
            if (!this._enabled)
            {
                return;
            }
            if (this._once)
            {
                this.Remove();
            }
            List<object> valueObjects = new List<object>();
            valueObjects.AddRange(args);
            if (this._params != null)
            {
                valueObjects.AddRange(this._params);
            }
            this._listener.DynamicInvoke(valueObjects.ToArray());
        }

        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        public bool Once
        {
            get { return this._once; }
        }

        public int Priority
        {
            get { return this._priority; }
        }

        public Delegate Listener
        {
            get { return this._listener; }
            set
            {
                if (value == null)
                {
                    throw new Exception("listener is null!");
                }
                this.verifyListener(value);
                this._listener = value;
                
            }
        }

        protected void verifyListener(Delegate listener)
        {

        }

        public void Remove()
        {
            //Debug.Log("slot remove!!");
            this._signal.Remove(this._listener);
        }
    }
}
