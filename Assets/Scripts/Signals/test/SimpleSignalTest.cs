using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.terrynoya.signals;
using UnityEngine;

public class SimpleSignalTest:MonoBehaviour
{
    public delegate void MyListenerDelegate(string txt);

    public void Start()
    {
        //OnEvent += this.OnMyEvent;
        MyListenerDelegate myLisPtr = new MyListenerDelegate(this.SignalListener);

        Signal signal = new Signal();
        ISlot slot = signal.Add(myLisPtr);
        slot.Enabled = false;
        signal.Dispatch("aa");
        slot.Enabled = true;
        signal.Dispatch("bb");
    }

    private void OnMyEvent()
    {
        Debug.Log("AAA");
    }

    private void SignalListener(string txt)
    {
        Debug.Log(txt);
    }
}
