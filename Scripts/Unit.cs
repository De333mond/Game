using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int Health;
    protected int Damage;
    protected string Name;
    // protected Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        // animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void recieveDamage(int damage){
        Health -= damage;
        if (Health <= 0)
            Die();
        Debug.Log($"{Name} recieved {damage} damege. Health: {Health}");
        
    }
    public virtual void Die(){
        Destroy(gameObject);
    }

    


}
