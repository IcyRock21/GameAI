using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Team team;
    public int damage;
    public float speed;
    public Transform enemyTarget;

    private void Update()
    {
        if (enemyTarget == null)
        { 
          Destroy(gameObject);
          return;
        }
        transform.position = Vector3.MoveTowards(transform.position, enemyTarget.position, speed);
        transform.LookAt(enemyTarget.position, Vector3.up);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemyTarget != null)
        {
            if (team == Team.Blue)
            {
                if (other.CompareTag("RedTeam"))
                {
                    Debug.Log("proj hit red");
                    UnitHealth unithealth = other.GetComponent<UnitHealth>();
                    unithealth.MinusHealth(damage);
                    unithealth.Checkhealth();
                    Destroy(gameObject);

                }
            }
            else
            {
                if (other.CompareTag("BlueTeam"))
                {
                    Debug.Log("proj hit blue");
                    UnitHealth unithealth = other.GetComponent<UnitHealth>();
                    unithealth.MinusHealth(damage);
                    unithealth.Checkhealth();
                    Destroy(gameObject);
                }
            }
        }
       
    }
}
