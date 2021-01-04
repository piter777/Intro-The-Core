using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClearPlayerPrefs : MonoBehaviour
{

    [MenuItem("Tools/ClearPlayerPrefs")]
    static void DeleteMyPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

}
