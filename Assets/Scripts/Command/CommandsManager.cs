using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsManager : MonoBehaviour
{
    public Stack<Command> undoCommands = new Stack<Command>();
    public Stack<Command> redoCommands = new Stack<Command>();
    public int currentExcutedCommandIndex;
    public static CommandsManager Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    public void Undo()
    {
        if (undoCommands.Count <= 0)
        {
            return;
        }
        Command command = undoCommands.Pop();
        command.Undo();
        redoCommands.Push(command);
    }
    public void Redo()
    {
        if (redoCommands.Count <= 0)
        {
            return;
        }
        Command command = redoCommands.Pop();
        command.Execute();
        undoCommands.Push(command);
    }
    public void ExcuteCommand(Command commandToExcute)
    {
        undoCommands.Push(commandToExcute);
        commandToExcute.Execute();
        ClreaRedoCommands();
    }
    private void ClreaRedoCommands() 
    {
        while (redoCommands.Count > 0)
        {
            redoCommands.Pop().Delete();
        }
    }
}
