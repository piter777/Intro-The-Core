using UnityEngine;
using System.Collections;
using System;

public interface ISettingsService
{
    Action<string> OnSettingSet
    {
        get; set;
    }
    void Set<T>(string key, T value);
    T Get<T>(string key);
    T Get<T>(string key, T defaultValue);
    bool HasKey(string key);
    void Clear();
}
