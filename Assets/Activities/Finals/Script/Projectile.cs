using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Team team;
    public int damage;
    public float speed;
    public Transform enemyTarget;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemyTarget.position, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (team == Team.Blue)
        {
            if (other.CompareTag("RedTeam"))
            {
                UnitHealth unithealth = other.GetComponent<UnitHealth>();
                unithealth.MinusHealth(damage);
                unithealth.Checkhealth();
                Destroy(gameObject);
            }
        }
        else
        {
            if(other.CompareTag("BlueTeam"))
            {
                UnitHealth unithealth = other.GetComponent<UnitHealth>();
                unithealth.MinusHealth(damage);
                unithealth.Checkhealth();
                Destroy(gameObject);
            }
        }
    }
}
