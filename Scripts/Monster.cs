using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    [SerializeField]
    private int _health = 1;
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private bool isMoveable = false;
    [SerializeField]
    private float speed = 3.0F;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Health = _health;
        Damage = _damage;
        Name = "Monster";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Unit unit = other.GetComponent<Unit>();

        if (unit && unit is Character) {
            unit.recieveDamage(Damage);
        }

    }
}
