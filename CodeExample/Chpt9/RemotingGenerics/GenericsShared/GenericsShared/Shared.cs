using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsShared
{
    [Serializable]
    public class Order
    {
    }

    [Serializable]
    public struct LimitOrder
    {
    }

    public interface IRemoteContainer<T>
    {
        void Add(T item);
        T this[string id] { get;}
    }
}
