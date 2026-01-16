using UnityEngine;

public class PlayerController : Entity
{
    public float MoveX { get; private set; }
    public float MoveY { get; private set; }

    public FiniteStateMachine finiteStateMachine;

    public Animator animator { get; private set; }

    public Rigidbody2D rb;

    [Header ("State")]
    public IdleState idleState { get; private set; }
    public RunState runState { get; private set; }
    public DieState dieState { get; private set; }

    private void Awake()
    {
        Debug.Log("Bat dau game");
        base.Awake();
        GetComponent();
        InitializeStates();
    }

    void Start()
    {
        finiteStateMachine.Intialize(idleState);
    }

    void Update()
    {
        if (finiteStateMachine.CurrentState == dieState) return;
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
        finiteStateMachine.ChangeState(dieState);
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
        finiteStateMachine.CurrentState.LogicUpdate();
    }
    private void PhysicsUpdate()
    {
        finiteStateMachine.CurrentState.PhysicsUpdate();
    }
    private void GetComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }
    private void InitializeStates()
    {
        finiteStateMachine = new FiniteStateMachine();
        idleState = new IdleState(this, finiteStateMachine, animator);
        runState = new RunState(this, finiteStateMachine, animator);
        dieState = new DieState(this, finiteStateMachine, animator);       
    }
}