public enum LinkTypes 
{
    Think,
    Watch,
    Attack,
    Dodge
}
public enum GoToStateTypes
{
    Idle = 0,
    Attack,
    Dodge
}
public enum GameStates
{
    Idle = 0,
    Attack,
    AttackReTurn,
    Dodge,
    DodgeReturn,
    Win,
    Lose
}
public enum PlayerType 
{
    Player,
    Enemy
}