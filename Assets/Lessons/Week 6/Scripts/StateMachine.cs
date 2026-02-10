using UnityEngine;

public class StateMachine
{
    public ICarState CurrentCarState { get; private set; }

    public void Change(ICarState next)
    {
        CurrentCarState?.Exit();

        CurrentCarState = next;

        CurrentCarState.Enter();
    }

    public void Tick()
    {
        CurrentCarState?.Tick();
    }
}
