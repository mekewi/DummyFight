using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHandler : MonoBehaviour
{
    public PlayerType playerType;
    public List<GameStateHandler> listOfStates = new List<GameStateHandler>();
    public List<LinkDetails> listOfLinks = new List<LinkDetails>();

    public Animator animator;
    public int currentState;
    public float PlayerHealth = 100f;
    public bool finishedMyStates;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        listOfLinks = LinksManager.Instance.GetLinksData(playerType);
    }
    public void AddState(GameStateHandler state) 
    {
        listOfStates.Add(state);
    }
    public void PlayNextState() 
    {
        animator.SetTrigger(listOfStates[currentState].startState.ToString());
        currentState++;
        if (currentState >= listOfStates.Count)
        {
            if (!finishedMyStates)
            {
                GamePlayManager.Instance.PlayerFinishedStates();
            }
            finishedMyStates = true;
            return;
        }

    }
    public void PlayerReady() 
    {
        GamePlayManager.Instance.PlayerReady();
    }
    public void SetDamage(float AmountOfDamage)
    {
        PlayerHealth -= AmountOfDamage;
        UIManager.Instance.ChangePlayerHealth(playerType, PlayerHealth);
    }

    public GameStates GetCurrentState() 
    {
        return listOfStates[currentState].startState;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetWinLose(float oponentHealth)
    {
        if (oponentHealth > PlayerHealth)
        {
            animator.SetTrigger(GameStates.Lose.ToString());
            return;
        }
        animator.SetTrigger(GameStates.Win.ToString());
    }
}
