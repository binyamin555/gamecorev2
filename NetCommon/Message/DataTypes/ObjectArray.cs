﻿using System.Collections;
using System.Collections.Generic;

namespace GameCore.NetAlpha
{
    public class ObjectArray<T> : CustomMessageHandler, IEnumerable<T> where T : CustomMessageHandler, new()
    {
        protected T[] data;

        public T[] Data
        {
            get
            {
                return data;
            }
        }

        public int Length
        {
            get
            {
                return data.Length;
            }
        }

        public ObjectArray() {
            data = new T[0];
        }

        public ObjectArray(T[] array)
        {
            data = array;
        }

        public override void InsertInto<T1>(Message<T1> message)
        {
            foreach (T v in data)
            {
                message.Insert(v);
            }
            message.Insert(Length);
        }

        public override void ExtractFrom<T1>(Message<T1> message)
        {
            int length = message.Extract<int>();
            data = new T[length];
            for (int i = length - 1; i >= 0; --i)
            {
                data[i] = message.Extract<T>();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return (T)data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}
