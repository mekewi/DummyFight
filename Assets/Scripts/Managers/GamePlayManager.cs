using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameStateHandler
{
    public GameStates startState;
    public GameStates endState;
}
public struct DictionaryKey
 {
    public GameStates Key1;
    public GameStates Key2;
    public DictionaryKey(GameStates key1, GameStates key2) : this()
    {
        Key1 = key1;
        Key2 = key2;
    }
}
public class GamePlayManager : MonoBehaviour
{
    public DummyHandler[] playersInGame;
    public int numberOfReadyPlayers;
    public int numberOfPlayersThatFinishedStates;
    public static GamePlayManager Instance;
    bool isGameStarted;
    bool isGameFinished;

    public int Damage = 1;
    public Dictionary<DictionaryKey, int> DamageMultiplayer = new Dictionary<DictionaryKey, int>()
    {
         {new DictionaryKey(GameStates.Attack,GameStates.Idle),1},
         {new DictionaryKey(GameStates.Attack,GameStates.DodgeReturn),2},
         {new DictionaryKey(GameStates.Attack,GameStates.Attack),3}
    };
    void Start()
    {
        if (Instance == null)
        {
            HandleGameStates(playersInGame[0], playersInGame[1]);
            HandleGameStates(playersInGame[1], playersInGame[0]);
            Instance = this;
            return;
        }
        Destroy(gameObject);

    }
    public void HandleGameStates(DummyHandler player, DummyHandler oponentPlayer)
    {
        for (int i = 0; i < player.listOfLinks.Count; i++)
        {
            player.AddState(GetStateFromLink(i, ref player.listOfLinks, oponentPlayer));
        }
    }
    private GameStateHandler GetStateFromLink(int Index, ref List<LinkDetails> linkDetails, DummyHandler oponentPlayer)
    {
        switch (linkDetails[Index].linkType)
        {
            case LinkTypes.Think:
                if (Index < linkDetails.Count - 1)
                {
                    int calculatedNextIndexThink = HandleInCase(Index, linkDetails.Count, linkDetails[Index], linkDetails[Index + 1]);
                    linkDetails[Index + 1] = linkDetails[calculatedNextIndexThink];
                }
                return new GameStateHandler() { startState = GameStates.Idle, endState = GameStates.Idle };
            case LinkTypes.Watch:
                if (Index < linkDetails.Count - 1)
                {
                    int calculatedNextIndexWatch = HandleInCase(Index, linkDetails.Count, linkDetails[Index], oponentPlayer.listOfLinks[Index + 1]);
                    linkDetails[Index + 1] = linkDetails[calculatedNextIndexWatch];
                }
                return new GameStateHandler() { startState = GameStates.Idle, endState = GameStates.Idle };
            case LinkTypes.Attack:
                return new GameStateHandler() { startState = GameStates.Attack, endState = GameStates.AttackReTurn };
            case LinkTypes.Dodge:
                return new GameStateHandler() { startState = GameStates.Dodge, endState = GameStates.DodgeReturn };
            default:
                return new GameStateHandler();
        }
    }
    public int HandleInCase(int Index, int linksCount, LinkDetails currentLinkDetails, LinkDetails nextLinkDetails)
    {
        switch (nextLinkDetails.linkType)
        {
            case LinkTypes.Think:
                return CalculateNextTurn(Index, currentLinkDetails.gotoNumberOfSteps[GoToStateTypes.Idle], linksCount);
            case LinkTypes.Watch:
                return CalculateNextTurn(Index, currentLinkDetails.gotoNumberOfSteps[GoToStateTypes.Idle], linksCount);
            case LinkTypes.Attack:
                return CalculateNextTurn(Index, currentLinkDetails.gotoNumberOfSteps[GoToStateTypes.Attack], linksCount);
            case LinkTypes.Dodge:
                return CalculateNextTurn(Index, currentLinkDetails.gotoNumberOfSteps[GoToStateTypes.Dodge], linksCount);
            default:
                return Index;
        }
    }
    public int CalculateNextTurn(int currentIndex, int moveSteps, int count)
    {
        if (moveSteps == 0) return currentIndex + 1;
        var calculatedIndex = (currentIndex + moveSteps) % (count - 1);
        int startingPoint = calculatedIndex < 0 ? count : 0;
        calculatedIndex = startingPoint + calculatedIndex;
        return calculatedIndex;
    }
    public IEnumerator NextTurn()
    {
        while (!isGameFinished)
        {
            CalculateScore(playersInGame[0], playersInGame[1]);
            CalculateScore(playersInGame[1], playersInGame[0]);
            for (int i = 0; i < playersInGame.Length; i++)
            {
                playersInGame[i].PlayNextState();
            }
            yield return new WaitForSeconds(1);
        }
    }
    public int CalculateScore(DummyHandler player, DummyHandler oponent)
    {
        if (DamageMultiplayer.ContainsKey(new DictionaryKey(oponent.GetCurrentState(), player.GetCurrentState())))
        {
            int damageMultiplayer = DamageMultiplayer[new DictionaryKey(oponent.GetCurrentState(), player.GetCurrentState())] * Damage;
            player.SetDamage(damageMultiplayer);
            if (player.PlayerHealth <= 0)
            {
                GameEnded();
            }
        }
        return 0;
    }
    public void GameEnded()
    {
        playersInGame[0].SetWinLose(playersInGame[1].PlayerHealth);
        playersInGame[1].SetWinLose(playersInGame[0].PlayerHealth);
        isGameStarted = false;
        isGameFinished = true;
        StopCoroutine(NextTurn());
    }
    public void PlayerReady()
    {
        numberOfReadyPlayers++;
        if (numberOfReadyPlayers == playersInGame.Length)
        {
            isGameStarted = true;
            isGameFinished = false;
            StartCoroutine(NextTurn());
        }
    }
    public void PlayerFinishedStates()
    {
        numberOfPlayersThatFinishedStates++;
        if (numberOfReadyPlayers == playersInGame.Length)
        {
            GameEnded();
        }
    }
    public void Replay() 
    {
        SceneManager.LoadScene("Fight");
    }
    public void Close() 
    {
        SceneManager.LoadScene("Editor");
    }
}
