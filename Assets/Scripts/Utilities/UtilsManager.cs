using com.spacepuppy.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructoSphere.Utilities
{
    public static class UtilsManager
    {
        [Serializable()]  public class StringGameObjectDict : SerializableDictionaryBase<string, GameObject> { }

        public static void Shuffle<T>(this IList<T> ts)
        {
            int count = ts.Count;
            int last = count - 1;
            for (int i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }


       
    }


}

