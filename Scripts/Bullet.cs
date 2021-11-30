using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private int damage = 1;

    public int Damage {set {damage = value;} get {return damage;}}

    private SpriteRenderer sprite;
    private Vector3 direction;
    public Vector3 Direction {set {direction = value;}}
    private GameObject parent;
    public GameObject Parent {set {parent = value;} get {return parent;}}


    private void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        Destroy(gameObject, 1.5F);
    }

    private void Update() {
        sprite.flipX = direction.x < 0.0F;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
       Unit unit =  other.GetComponent<Unit>();
       Debug.Log($"{unit.gameObject != parent} {unit.gameObject} {parent}");
       
       if (unit && unit.gameObject != parent) {
           Destroy(gameObject);
           unit.recieveDamage(damage);
       }
    }

}    
