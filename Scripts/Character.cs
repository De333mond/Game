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
    private int _fireballDamage = 1;
    [SerializeField]
    private Bullet projectile;
    [SerializeField]
    private float attackRange = 1.0F;
    [SerializeField]
    private float attackspeed = 0;
    private SpriteRenderer sprite;
    private Animator animator;
    public Transform attackpoint;
    private string currentAnimation;
    

    new private Rigidbody2D rigidbody;
    
    private bool isGrounded;
    private bool isAttacking;
    private bool isAttackPressed;
    
    private enum CharState {
        Idle, Run, Jump, Attack
    }

    private CharState state {
        get {return (CharState)animator.GetInteger("state");}
        set {animator.SetInteger("state", (int)value);}
    }

    const string PLAYER_IDLE = "idle";
    const string PLAYER_RUN = "run";
    const string PLAYER_ATTACK = "attack";
    const string PLAYER_JUMP = "jump";
    const string PLAYER_HURT = "hurt";
    




    void Start()
    {
        Health = _health;
        Damage = _damage;
        Name = "Character";
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {

        if(Input.GetMouseButtonDown(1) && !isAttacking){
            shoot();    
        }
        
        if(Input.GetMouseButtonDown(0) && !isAttacking){
            Attack();
            isAttackPressed = true;
        }
        
        if (Input.GetButton("Horizontal") && !(isAttacking && isGrounded)){ 
            Run();
        }
        
        if (Input.GetButtonDown("Jump") && !isAttacking) {
            Jump();
        }

        // Animations
     
        checkGround();   

        if (isGrounded && !isAttacking)
        {
            if (Input.GetButton("Horizontal"))
                playAnimation(PLAYER_RUN);
            else 
                playAnimation(PLAYER_IDLE);
        } 
        else if (!isAttacking)
            playAnimation(PLAYER_JUMP);
        

        if (isAttackPressed && !isAttacking){
            isAttackPressed = false;
            isAttacking = true;

            playAnimation(PLAYER_ATTACK);
            Invoke("AttackFinished", 0.34F / 1.5F);
        }
    }

    private void AttackFinished(){
        isAttacking = false;
    }

    private void Run() 
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
    
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);    

        sprite.flipX = direction.x < 0.0F;

        // if (isGrounded) 
        //     state = CharState.Run;
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
    private void Attack()
    {
        // state = CharState.Attack;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies) {
            Unit unit = enemy.GetComponentInParent<Unit>();
            if (unit && !(unit is Character))
            {
                unit.recieveDamage(Damage);
            }
        }
    }
    private void shoot(){
        Vector3 position = transform.position;
        position.y += 1.0F;
        position.x += 0.7F * (sprite.flipX ? -1.0F : 1.0F);

        Bullet NewProj = Instantiate(projectile, position, projectile.transform.rotation) as Bullet;

        NewProj.Direction = NewProj.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        NewProj.Parent = gameObject;
        NewProj.Damage = _fireballDamage;
    }

    public override void recieveDamage(int damage){
            Health -= damage;
            if (Health <= 0)
                Die();
            Debug.Log($"{Name} recieved {damage} damege. Health: {Health}");
            animator.SetTrigger("hurt");
            rigidbody.velocity = Vector3.zero;
            Vector2 vector = Vector2.one;
            vector.x *= 8.0F;
            vector.y *=  (sprite.flipX ? -1.0F : 1.0F ) * 16.0F;
            rigidbody.AddForce(vector, ForceMode2D.Impulse);
            

        }

    private void playAnimation(string animation)
    {
        if (animation != currentAnimation)
        {
            currentAnimation = animation;
            animator.Play(animation);
        }    
    }
    private void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;
    }

}
