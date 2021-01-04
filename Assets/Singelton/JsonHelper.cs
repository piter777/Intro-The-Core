using System;
using UnityEngine;

public static class JsonHelper
{
    public static T FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        if (wrapper == null)
        {
            return default(T);
        }
        return wrapper.List;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T List;
    }

    public static string ToJson<T>(T list)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            List = list
        };
        return JsonUtility.ToJson(wrapper);
    }
}