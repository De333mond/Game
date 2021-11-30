using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 3.0F;
    [SerializeField]
    private int _health = 5;
    
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private Bullet projectile;
    private SpriteRenderer sprite;
    private Animator animator;

    
    

    new private Rigidbody2D rigidbody;
    private bool isGrounded;

    private enum CharState {
        Idle, Run, Jump, Attack
    }

    private CharState state {
        get {return (CharState)animator.GetInteger("state");}
        set {animator.SetInteger("state", (int)value);}
    }

    void Start()
    {
        Health = _health;
        Damage = _damage;
        Name = "Char";
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        if (isGrounded) state = CharState.Idle;

        checkGround();   

        if(Input.GetMouseButtonDown(1))
            shoot();    

        if(Input.GetMouseButtonDown(0))
            Attack();
        else if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

    }

    private void Run() 
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);    

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) 
            state = CharState.Run;
    }
    private void Jump()
    {
        if (isGrounded)
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        Debug.Log(Health);
    }

    private void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
            state = CharState.Jump;
    }

    private void Attack()
    {
        state = CharState.Attack;
    }

    private void shoot(){
        Vector3 position = transform.position;
        position.y += 1.0F;
        position.x += 0.7F * (sprite.flipX ? -1.0F : 1.0F);

        Bullet NewProj = Instantiate(projectile, position, projectile.transform.rotation) as Bullet;

        NewProj.Direction = NewProj.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        NewProj.Parent = gameObject;
    }

}
