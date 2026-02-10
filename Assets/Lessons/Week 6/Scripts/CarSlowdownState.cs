using UnityEngine;

public class CarSlowdownState : ICarState
{
    private readonly CarAI car;
    private readonly StateMachine sm;

    public CarSlowdownState(CarAI car, StateMachine sm)
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
        bool red = car.ActiveTraficLight != null && car.ActiveTraficLight.IsRed;
        if (red || car.CarAheadStoppedClose)
        {
            sm.Change(car.StopState);
            return;
        }

        car.SetTargetSpeed(car.slowSpeed);

        bool green = car.ActiveTraficLight != null && car.ActiveTraficLight.IsGreen;
        if(green && !car.CarAheadDetected)
        { sm.Change(car.GoState); return; }
    }
}
