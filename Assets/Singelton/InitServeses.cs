using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitServeses : MonoBehaviour
{
    IGameInfoService _GameService;
    // Start is called before the first frame update
    void Start()
    {
        _GameService = Services.Instance.GameInfo;
        Services.Instance.GameInfo.Coins++;
      //  Debug.Log(Services.Instance.GameInfo.Coins);
    }

 


}
