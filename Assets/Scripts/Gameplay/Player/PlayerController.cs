using UnityEngine;

public class PlayerController : Entity
{
    public float MoveX { get; private set; }
    public float MoveY { get; private set; }

    public FiniteStateMachine finiteStateMachine;

    [SerializeField]
    private PlayerData playerData;
    private PlayerData runtimeData;
    public PlayerData data => runtimeData;

    public Animator animator { get; private set; }

    public Rigidbody2D rb;

    public GameObject firePoint;

    [Header ("State")]
    public IdleState idleState { get; private set; }
    public RunState runState { get; private set; }
    public DieState dieState { get; private set; }

    private void Awake()
    {
        Debug.Log("Bat dau game");
        LoadComponents();
        InitializeStates();
        ResetData();
    }

    protected override void OnEnable()
    {
        LoadData();
        base.OnEnable();
    }

    void Start()
    {
        finiteStateMachine.Intialize(idleState);
    }

    void Update()
    {
        if (finiteStateMachine.CurrentState == dieState) { MoveX = 0;MoveY = 0; }
        SetMove();
        LogicUpdate();
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
        rb.linearVelocity = move.normalized * data.moveSpeed;
    }
    
    public void Idle(Vector2 move)
    {
        rb.linearVelocity = move * data.moveSpeed;
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
    private void LoadComponents()
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
    private void LoadData()
    {
        maxHealth = data.maxHealth;
        armor = data.armor;
    }
    private void ResetData()
    {
        runtimeData = Instantiate(playerData);
    }
}