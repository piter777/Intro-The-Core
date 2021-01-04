
using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingsService : ISettingsService
{
    public Action<string> OnSettingSet
    {
        get;
        set;
    }

    public SettingsService()
    {

    }

    public void Set<T>(string key, T value)
    {

        if (typeof(T) == typeof(int))
        {
            int t = Convert.ToInt32(value);
            ZPlayerPrefs.SetInt(key, t);
        }
        else if (typeof(T) == typeof(long))
        {
            string t = Convert.ToString(value);
            ZPlayerPrefs.SetString(key, t);
        }
        else if (typeof(T) == typeof(float))
        {
            float t = Convert.ToSingle(value);
            ZPlayerPrefs.SetFloat(key, t);
        }
        else if (typeof(T) == typeof(bool))
        {
            float t = Convert.ToInt32(value);
            ZPlayerPrefs.SetFloat(key, t);
        }
        else if (typeof(T) == typeof(string))
        {
            ZPlayerPrefs.SetString(key, value.ToString());
        }
        else if (typeof(T) == typeof(DateTime))
        {
            DateTime t = Convert.ToDateTime(value);
            ZPlayerPrefs.SetString(key, t.ToBinary().ToString());
        }
        else if (typeof(T).IsGenericType && (typeof(T).GetGenericTypeDefinition() == typeof(List<>)))
        {
            string t = JsonHelper.ToJson(value);
            ZPlayerPrefs.SetString(key, t);
        }
        else
        {

            string t = JsonUtility.ToJson(value);
            ZPlayerPrefs.SetString(key, t);
        }
        //  ZPlayerPrefs.Save();

        if (OnSettingSet != null)
        {
            OnSettingSet(key);
        }
    }

    public T Get<T>(string key)
    {
        //  PlayerPrefs.DeleteAll();
        object value = null;
        if (typeof(T) == typeof(int))
        {
            value = ZPlayerPrefs.GetInt(key);
        }
        else if (typeof(T) == typeof(long))
        {
            value = Convert.ToInt64(ZPlayerPrefs.GetString(key));
        }
        else if (typeof(T) == typeof(string))
        {
            value = ZPlayerPrefs.GetString(key);
        }
        else if (typeof(T) == typeof(float))
        {
            value = ZPlayerPrefs.GetFloat(key);
        }
        else if (typeof(T) == typeof(bool))
        {
            value = ZPlayerPrefs.GetFloat(key);
            value = Convert.ToBoolean(value);
        }
        else if (typeof(T) == typeof(DateTime))
        {
            var intValue = Convert.ToInt64(ZPlayerPrefs.GetString(key));
            value = DateTime.FromBinary(intValue);
        }
        else if (typeof(T).IsGenericType && (typeof(T).GetGenericTypeDefinition() == typeof(List<>)))
        {
            string t = ZPlayerPrefs.GetString(key, string.Empty);
            return JsonHelper.FromJson<T>(t);
        }
        else
        {
            string t = ZPlayerPrefs.GetString(key, string.Empty);
            return JsonUtility.FromJson<T>(t);
        }
        return (T)Convert.ChangeType(value, typeof(T));
    }

    public T Get<T>(string key, T defaultValue)
    {
        T value = HasKey(key) ? Get<T>(key) : defaultValue;
        return value != null ? value : defaultValue;
    }

    public bool HasKey(string key)
    {
        return ZPlayerPrefs.HasKey(key);
    }
    public void Clear()
    {
        ZPlayerPrefs.DeleteAll();
    }


}
