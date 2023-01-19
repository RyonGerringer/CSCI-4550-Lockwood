using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : ScriptableObject
{

    // Constant representing the open state.
    public const bool STATE_OPEN = true;

    // Constant representing the closed state.
    public const bool STATE_CLOSED = false;

    // Current state of this object; Open or closed.
    private bool State;

    public Toggleable()
    {

        // Default the state to closed.
        this.State = Toggleable.STATE_CLOSED;

    }

    // Retrieves the current state.
    public bool GetState()
    {

        return this.State;

    }

    // Toggles the state of this toggleable instance.
    public void Toggle()
    {

        State = State == Toggleable.STATE_OPEN ? Toggleable.STATE_CLOSED : Toggleable.STATE_OPEN;

    }
}