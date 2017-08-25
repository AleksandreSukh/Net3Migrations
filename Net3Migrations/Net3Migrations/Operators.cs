using System;

namespace Net3Migrations
{
    public static class Operators
    {
        public static IntPtr Add(this IntPtr pointer, int offset)
        {
            return new IntPtr(pointer.ToInt64() + offset);
        }
    }
}

#if NETFX_10 || NETFX_20 || NETFX_30
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    internal class ExtensionAttribute : Attribute
    {

    }
}
#endif