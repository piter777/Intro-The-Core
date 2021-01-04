using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameInfoService
{
    int CurrentLevel
    {
        get; set;
    }
    int Lives
    {
        get; set;
    }
    int Coins
    {
        get; set;
    }
    int Tokens
    {
        get; set;
    }
    int Diamonds
    {
        get; set;
    }
 
    int PowerUps
    {
        get; set;
    }
    string UserNickName
    {
        get; set;
    }
    Action OnInfoChangedAction
    {
        get; set;
    }
}