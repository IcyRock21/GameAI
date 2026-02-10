using UnityEngine;

public class CarStopState : ICarState
{
    private readonly CarAI car;
    private readonly StateMachine sm;

    public CarStopState(CarAI car, StateMachine sm)
    {
        this.car = car;
        this.sm = sm;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Tick()
    {
        car.SetTargetSpeed(0);
        bool red = car.ActiveTraficLight != null && car.ActiveTraficLight.IsRed;

        if (red || car.CarAheadStoppedClose) return;

        bool orange = car.ActiveTraficLight != null && car.ActiveTraficLight.IsOrange;

        if (orange || car.CarAheadDetected) { sm.Change(car.SlowDownState); }
        else { sm.Change(car.GoState); }
    }   
}
