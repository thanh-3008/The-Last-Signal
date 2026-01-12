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
        GetComponent();
        InitializeStates();
    }

    void Start()
    {
        playerStateMachine.Intialize(idleState);
    }

    void Update()
    {
        SetMove();
        LogicUpdate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }
    }
    private void FixedUpdate()
    {
        PhysicsUpdate();
    }

    protected override void Die()
    {
        GameTimeManager.Instance.SetPlayerDead(true);
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
    private void SetMove()
    {
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
    }
    private void LogicUpdate()
    {
        playerStateMachine.CurrentState.LogicUpdate();
    }
    private void PhysicsUpdate()
    {
        playerStateMachine.CurrentState.PhysicsUpdate();
    }
    private void GetComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void InitializeStates()
    {
        idleState = new IdleState(this, playerStateMachine, animator);
        runState = new RunState(this, playerStateMachine, animator);
    }
}