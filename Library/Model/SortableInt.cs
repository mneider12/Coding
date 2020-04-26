using Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model
{
    public class SortableInt : ISortable
    {
        public int Key { get; private set; }

        public SortableInt(int key)
        {
            Key = key;
        }

        public override bool Equals(object obj)
        {
            return Key.Equals(((SortableInt)obj).Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public static implicit operator SortableInt(int key) => new SortableInt(key);
    }
}
