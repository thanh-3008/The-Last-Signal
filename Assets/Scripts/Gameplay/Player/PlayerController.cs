using NUnit.Framework;
using UnityEngine;

public class PlayerController : Entity
{
    public float MoveX { get; private set; }
    public float MoveY { get; private set; }

    public PlayerStateMachine playerStateMachine;

    public Animator animator { get; private set; }

    public Rigidbody2D rb;

    [Header ("State")]
    public IdleState idleState { get; private set; }
    public RunState runState { get; private set; }

    private void Awake()
    {
        Debug.Log("Bat dau game");

        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        idleState = new IdleState(this, playerStateMachine, animator);
        runState = new RunState(this, playerStateMachine, animator);       
    }

    void Start()
    {
        playerStateMachine.Intialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
        playerStateMachine.CurrentState.LogicUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f);
        }
    }
    private void FixedUpdate()
    {
        playerStateMachine.CurrentState.PhysicsUpdate();
    }

    protected override void Die()
    {
        Debug.Log("Player die");
    }

    public void Move(Vector2 move)
    {
        rb.linearVelocity = move.normalized * moveSpeed;
    }
    
    public void Idle(Vector2 move)
    {
        rb.linearVelocity = move * moveSpeed;
        MoveX = 0;
        MoveY = 0;
    }
}
