
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace dpn
{
    public class PluginEventHandler
    {
        Action<int> _onEvent;
        public Action<int> onEvent
        {
            get
            {
                return _onEvent;
            }
            set
            {
                _onEvent = value;
                callback = Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public IntPtr callback
        {
            get;
            private set;
        }

        static List<PluginEventHandler> _refs = new List<PluginEventHandler>();

        int _refCount = 0;

        public void AddRef()
        {
            if(_refCount == 0)
            {
                _refs.Add(this);
            }
            _refCount++;
        }

        public void Release()
        {
            --_refCount;
           if (_refCount == 0)
           {
                _refs.Remove(this);
           }
        }

        public PluginEventHandler()
        {
        }

        PluginEventHandler(Action<int> onEvent)
        {
            this.onEvent = onEvent;
        }
    }

}

