using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{

    [SerializeField]
    protected float AttackDamage, AttackSpeed, AttackRange;

    [SerializeField]
    protected Targeting targetingPriority;

    [SerializeField]
    protected GameObject LaunchObject;

    private List<GameObject> EnemiesInRange;


    private void Update() {
        
        //If something is within Attack Range
            //If target fits targeting priority 
                //If can fire again          
                    //Attack

    }


    public virtual void Attack() {

    }

    GameObject FindEnemyToAttack() {

        return null;
    }
  

    float CheckDistance() {


        return 1;
    }

  

    private void OnTriggerEnter2D(Collider2D collision) {
        //if (collision.GetComponent<>) {
        //    EnemiesInRange.Add(collision.gameObject);
        //}
    }



    private void OnTriggerExit2D(Collider2D collision) {
        //if (collision.GetComponent<>) {
        //    EnemiesInRange.Remove(collision.gameObject);
        //}
    }
}
