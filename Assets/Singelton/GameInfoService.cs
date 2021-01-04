using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInfoService : IGameInfoService
{

    ISettingsService _settingsService;
    public GameInfoService(ISettingsService settingsService)
    {
        _settingsService = settingsService;

    }

    private readonly string _currentLevelKey = "CurrentLevel";
    public int CurrentLevel
    {
        get
        {
            return _settingsService.Get<int>(_currentLevelKey, 1);
        }

        set
        {
            _settingsService.Set(_currentLevelKey, value);
            OnInfoChanged();
        }
    }
    private readonly string _livesLevelKey = "Lives";
    public int Lives
    {
        get
        {
            return _settingsService.Get<int>(_livesLevelKey, 0);
        }

        set
        {
            _settingsService.Set(_livesLevelKey, value);
            OnInfoChanged();
        }
    }
    private readonly string _coinsLevelKey = "Coins";
    public int Coins
    {
        get
        {
            return _settingsService.Get<int>(_coinsLevelKey, 0);
        }

        set
        {
            if (value >= 0)
            {
                _settingsService.Set(_coinsLevelKey, value);
                OnInfoChanged();
            }
        }
    }
    private readonly string _tokensLevelKey = "Tokens";
    public int Tokens
    {
        get
        {
            return _settingsService.Get<int>(_tokensLevelKey, 0);
        }

        set
        {
            if (value >= 0)
            {
                _settingsService.Set(_tokensLevelKey, value);
                OnInfoChanged();
            }
        }
    }
    private readonly string _diamondsLevelKey = "Diamonds";
    public int Diamonds
    {
        get
        {
            return _settingsService.Get<int>(_diamondsLevelKey, 0);
        }

        set
        {
            _settingsService.Set(_diamondsLevelKey, value);
            OnInfoChanged();
        }
    }


    
 
    private readonly string _powerUpsKey = "PowerUps";
    public int PowerUps
    {
        get
        {
            return _settingsService.Get<int>(_powerUpsKey, 0);
        }

        set
        {

            _settingsService.Set(_powerUpsKey, value);
            OnInfoChanged();

        }
    }

    private readonly string _nicknameKey = "UserNickName";
    public string UserNickName
    {
        get
        {
            return _settingsService.Get<string>(_nicknameKey, "NONAME");
        }

        set
        {
            _settingsService.Set(_nicknameKey, value);
        }
    }



    public Action OnInfoChangedAction
    {
        get; set;
    }
    public void OnInfoChanged()
    {
        if (OnInfoChangedAction != null)
        {
            OnInfoChangedAction();
        }
    }

}
