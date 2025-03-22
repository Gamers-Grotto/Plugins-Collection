using System;
using System.Collections.Generic;
using Unity.Netcode;

namespace GamersGrotto.Multiplayer_Sample
{
    public static class Helpers
    {
        public static List<T> AsList<T>(this NetworkList<T> networkList) where T : unmanaged, IEquatable<T>
        {
            List<T> list = new List<T>();

            foreach (var entry in networkList)
            {
                list.Add(entry);
            }

            return list;
        }
    }
}