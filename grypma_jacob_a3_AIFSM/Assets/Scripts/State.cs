using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();   // Called when the state is entered
    public abstract void Execute(); // Called on each update while the state is active
    public abstract void Exit();    // Called when exiting the state
}
