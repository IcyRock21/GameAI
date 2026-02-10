using Unity.VisualScripting;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [Header("Speeds")]
    public float goSpeed;
    public float slowSpeed;
    public float accelerationSpeed;
    public float brake;

    [Header("Sensor")]
    public float frontCheckingDistance;
    public float stopDistance;
    public LayerMask carLayer;

    public TrafficLight ActiveTraficLight { get; private set; }

    public float currentSpeed { get; private set; }
    public bool CarAheadDetected { get; private set; }   
    public bool CarAheadStoppedClose { get; private set; }
    private StateMachine sm;

    public CarStopState StopState { get; private set; }
    public CarGoState GoState { get; private set; }
    public CarSlowdownState SlowDownState { get; private set;}

    private void Awake()
    {
        sm = new StateMachine();
        StopState = new CarStopState(this,sm);
        GoState = new CarGoState(this, sm);
        SlowDownState = new CarSlowdownState(this, sm);

    }
    private void Start()
    {
        sm.Change(StopState);
    }

    private void Update()
    {
        MoveForward();
        UpdateSensor();
        sm.Tick();
 
    }

    void MoveForward()
    {
        transform.position += transform.position * (Time.deltaTime * currentSpeed);
    }
    void UpdateSensor()
    {
        Debug.DrawRay(transform.position, Vector3.forward * frontCheckingDistance, Color.red);

        CarAheadDetected = false;
        CarAheadStoppedClose = false;
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, frontCheckingDistance, carLayer))
        {
            CarAheadDetected = true;

            CarAI other = hit.collider.GetComponent<CarAI>();
            
            float otherSpeed = other!= null ? other.currentSpeed : 0;

            bool otherStopped = otherSpeed <= 0.1f;

            bool veryClose = hit.distance <= stopDistance;

            CarAheadStoppedClose = otherStopped && veryClose;
        }

    }

    public void SetTargetSpeed(float target)
    {
        float rate = (target > currentSpeed) ? accelerationSpeed : brake;

        currentSpeed = Mathf .MoveTowards(currentSpeed, target, rate * Time.deltaTime);
    }

    public void SetActiveTrafficLight(TrafficLight light)
    {
        ActiveTraficLight = light;
    }

    public void ClearSetActiveTrafficLight (TrafficLight light)
    {
        if (ActiveTraficLight == light)
            ActiveTraficLight = null;
    }
}
