using UnityEngine;

public class CarGoState : ICarState
{
    private readonly CarAI car;
    private readonly StateMachine sm;

    public CarGoState(CarAI car, StateMachine sm)
    {
        this.car = car;
        this.sm = sm;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        bool red = car.ActiveTraficLight !=null && car.ActiveTraficLight.IsRed;
        if(red || car.CarAheadStoppedClose)
        {
            sm.Change(car.StopState);
            return;
        }

        bool orange = car.ActiveTraficLight != null && car.ActiveTraficLight.IsOrange;
        if (orange || car.CarAheadDetected)
        {
            sm.Change(car.SlowDownState);
            return;
        }
        car.SetTargetSpeed(car.goSpeed);

    }

    public void Tick()
    {

    }
}
