using UnityEngine;
using System.Collections;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] float mageRadius = 10f;
    [SerializeField] LayerMask enemyteamMask;
    [SerializeField] GameObject mageBullet;
    [SerializeField] GameObject enemyTarget;

    public UnitMovement unitMovement;

    private void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
        
        if (unitMovement.team == Team.Red) 
        {
            enemyteamMask = LayerMask.GetMask("BlueTeam");
        }
    }

    public void Attack()
    {
        if (unitMovement.unitType == UnitType.Mage)
        {
            MageAttack();
        }


        if (unitMovement.unitType == UnitType.Tank)
        {
            TankAttack();
        }

        if (unitMovement.unitType == UnitType.Berzerker)
        {
            BerzerkerAttack();
        }
    }


    public void BerzerkerAttack()
    {

    }

    public void TankAttack()
    {

    }

    public void MageAttack()
    {
        

        Collider[] enemy = Physics.OverlapSphere(transform.position, mageRadius, enemyteamMask);
        if(enemy.Length > 0 )
        {

            enemyTarget = enemy[0].gameObject;
            Debug.Log("enemyHit: " + enemyTarget);
            StartCoroutine(MageAttacking());

        }
    }

    IEnumerator MageAttacking()
    {
        unitMovement.animator.SetBool("IsAttacking", true);
        unitMovement.unitState = UnitState.Attack;
        unitMovement.agent.isStopped = true;
        //instantiate watermelon

        GameObject projectile = Instantiate(mageBullet, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().enemyTarget = enemyTarget.transform;
        yield return new WaitForSeconds(2f);

        unitMovement.agent.isStopped = false;
        unitMovement.unitState = UnitState.Walk; 


    }


    void OnDrawGizmos()
    {
        if(unitMovement.unitType == UnitType.Mage)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, mageRadius);
        }

        if (unitMovement.unitType == UnitType.Tank)
        {

        }

        if (unitMovement.unitType == UnitType.Berzerker)
        {

        }

    }
}
