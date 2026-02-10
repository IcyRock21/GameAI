using Unity.VisualScripting;
using UnityEngine;

public enum LightColor { Red,Orange,Green}
public class TrafficLight : MonoBehaviour
{
    public TrafficLight light;
    public LightColor LightColor = LightColor.Red;

    public bool IsRed => LightColor == LightColor.Red;

    public bool IsOrange => LightColor == LightColor.Orange;

    public bool IsGreen => LightColor == LightColor.Green;

    private void OnTriggerEnter(Collider other)
    {
        CarAI car = other.GetComponent<CarAI>();
        if(car != null)
        {
            car.SetActiveTrafficLight(light);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        CarAI car = other.GetComponent<CarAI>();
        if (car != null)
        {
            car.ClearSetActiveTrafficLight(light);
        }
    }
}
