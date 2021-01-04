using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Services : Singleton<Services>
{

    protected Services()
    {
    } // guarantee this will be always a singleton only - can't use the constructor!
    public ISettingsService SettingsService
    {
        get; private set;
    }
    
    public IGameInfoService GameInfo
    {
        get; private set;
    }
    

    
    void Awake()
    {
       
        Application.targetFrameRate = 60;
        // Your initialization code here
        Initialize();
        // PlatformService.Initialize();
        RegisterDialogs();
    


     
    }
    private void Initialize()
    {
        SettingsService = new SettingsService();
       
        GameInfo = new GameInfoService(SettingsService);
       
    
    }

   
  
   
 
    public void RunWaitWhile(Action action, Func<bool> predicate)
    {
        StartCoroutine(RunWaitWhileCor(action, predicate));
    }
    private IEnumerator RunWaitWhileCor(Action action, Func<bool> predicate)
    {
        yield return new WaitWhile(predicate);
        action();
    }

    public void RegisterDialogs()
    {/*
        ResourceLocator.Register<LoadingDialog>("Dialogs/LoadingDialog");
        //   ResourceLocator.Register<SettingsDialog>("Dialogs/SettingsDialog");
        ResourceLocator.Register<CurrencyShopDialog>("Dialogs/CurrencyShopDialog");
        ResourceLocator.Register<DecorShopDialog>("Dialogs/DecorShopDialog");
        ResourceLocator.Register<StartLevelDialog>("Dialogs/StartLevelDialog");
        ResourceLocator.Register<WinLevelDialog>("Dialogs/WinLevelDialog");
        ResourceLocator.Register<LoseLevelDialog>("Dialogs/LoseLevelDialog");
        ResourceLocator.Register<SettingsGameplayDialog>("Dialogs/SettingsGameplayDialog");
        ResourceLocator.Register<OutOfMovesDialog>("Dialogs/OutOfMovesDialog");
        ResourceLocator.Register<GoalDialog>("Dialogs/GoalDialog");
        ResourceLocator.Register<MissionsCompleteDialog>("Dialogs/MissionsCompleteDialog");
        ResourceLocator.Register<BuyBoosterDialog>("Dialogs/BuyBoosterDialog");
        ResourceLocator.Register<QuestDialog>("Dialogs/QuestDialog");
        ResourceLocator.Register<NotebookDialog>("Dialogs/Notebook/NotebookDialog");
        ResourceLocator.Register<BuildMapObjectDialog>("Dialogs/BuildDialog/BuildMapObjectDialog");
        ResourceLocator.Register<QuestVariantDialog>("Dialogs/QuestVariantDialog");
        ResourceLocator.Register<QuestNotepadDialog>("Dialogs/QuestNotepadDialog");
        ResourceLocator.Register<OutOfLivesDialog>("Dialogs/OutOfLivesDialog");
        ResourceLocator.Register<MultiQuestDialog>("Dialogs/MultiQuestDialog");
        ResourceLocator.Register<DailyBonusDialog>("Dialogs/DailyBonusDialog");
        ResourceLocator.Register<StorageConflictDialog>("Dialogs/StorageConflictDialog");
        ResourceLocator.Register<BuyGoldDialog>("Dialogs/BuyGoldDialog");
        ResourceLocator.Register<ConnectedDialog>("Dialogs/ConnectedDialog");
        ResourceLocator.Register<NoInternetDialog>("Dialogs/NoInternetDialog");
        ResourceLocator.Register<AttentionDialog>("Dialogs/AttentionDialog");
        ResourceLocator.Register<TutorialDialog>("Dialogs/TutorialDialog");
        ResourceLocator.Register<NotEnoughCoinsDialog>("Dialogs/NotEnoughCoinsDialog");
        ResourceLocator.Register<NotEnoughStarsDialog>("Dialogs/NotEnoughStarsDialog");
        ResourceLocator.Register<RetryLevelDialog>("Dialogs/RetryLevelDialog");
        ResourceLocator.Register<ChousingLevelDialog>("Dialogs/ChousingLevelDialog");
        ResourceLocator.Register<BlockDialog>("Dialogs/BlockDialog");
        ResourceLocator.Register<PremiumShopDialog>("Dialogs/PremiumShopDialog");

        */


    }

    private void OnApplicationQuit()
    {
        Debug.Log(Time.fixedTime);
    }

   
}
