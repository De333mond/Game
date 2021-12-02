using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   private Rigidbody2D physic;
   public Transform Player;
   public float enemySpeed;
   public float dangerDist;

    void Start()
    {
        physic = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, Player.position);
        if (distToPlayer < dangerDist){
            StartEnemy();
        }
        else
        {
            StopEnemy();
        }
    }
    void StartEnemy(){
        if (Player.position.x < transform.position.x){
            physic.velocity = new Vector2(-enemySpeed,0);
        }
        else if (Player.position.x > transform.position.x){
            physic.velocity = new Vector2(enemySpeed,0);
        }
    }
    void StopEnemy(){
        physic.velocity= new Vector2(0,0);
    }
}
