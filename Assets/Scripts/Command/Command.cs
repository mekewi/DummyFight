using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    //Move and maybe save command
    public abstract void Execute();
    //Undo an old command
    public virtual void Undo() { }
    public virtual void Delete() { }

}

//For keys with no binding
public class DoNothing : Command
{
    //Called when we press a key
    public override void Execute()
    {
        //Nothing will happen if we press this key
    }
}