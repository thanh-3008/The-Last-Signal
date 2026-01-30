using NUnit;
using System;
using UnityEngine;

public class EnemyController : Entity
{
    [SerializeField]
    private FiniteStateMachine finiteStateMachine;
    [SerializeField]
    private EnemyData enemyData;
    public EnemyData data => enemyData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public PlayerController playerController { get; private set; }

    public event Action<GameObject> OnGameObjectDeath;
    [Header("State")]
    public EnemyAttackState enemyAttackState;
    public EnemyChaseState enemyChaseState;
    public EnemyIdleState enemyIdleState;
    public EnemyDieState enemyDieState;

    private void Awake()
    {
        // 1. Load basic components
        LoadComponent();

        // 2. Load data values
        LoadData();

        // 3. Initialize state objects (FSM and states) so they exist before Start
        InitializeStates();
    }

    private void Start()
    {
        // 4. Get player reference from PlayerManager (already cached there)
        if (PlayerManager.Instance != null)
        {
            playerController = PlayerManager.Instance.Player;
        }

        // 5. Set initial state and register with manager
        if (finiteStateMachine != null && enemyIdleState != null)
        {
            finiteStateMachine.Intialize(enemyIdleState);
        }

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (finiteStateMachine != null && finiteStateMachine.CurrentState != null)
        {
            finiteStateMachine.CurrentState.LogicUpdate();
        }
    }

    protected override void OnEnable()
    {
        // ensure HP/armor set when enabled
        LoadData();
        base.OnEnable();
        if (finiteStateMachine != null && enemyIdleState != null)
        {
            finiteStateMachine.Intialize(enemyIdleState);
        }

    }

    protected void OnDisable()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }

    private void FixedUpdate()
    {
        if (finiteStateMachine != null && finiteStateMachine.CurrentState != null)
        {
            finiteStateMachine.CurrentState.PhysicsUpdate();
        }
    }

    private void LoadComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"EnemyController: Rigidbody2D missing on {gameObject.name}");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"EnemyController: Animator missing on {gameObject.name}");
        }
    }

    private void LoadData()
    {
        if (enemyData == null)
        {
            Debug.LogError($"EnemyController: enemyData not assigned on {gameObject.name}");
            return;
        }

        maxHealth = enemyData.maxHealth;
        currentHealth = maxHealth;
        armor = enemyData.armor;
    }

    private void InitializeStates()
    {
        if (animator == null)
        {
            // Can't initialize states without animator
            return;
        }

        finiteStateMachine = new FiniteStateMachine();
        enemyIdleState = new EnemyIdleState(finiteStateMachine, this, animator);
        enemyAttackState = new EnemyAttackState(finiteStateMachine, this, animator);
        enemyChaseState = new EnemyChaseState(finiteStateMachine, this, animator);
        enemyDieState = new EnemyDieState(finiteStateMachine, this, animator);
    }

    public void OnAttackHit()
    {
        if (playerController == null) return;
        if (enemyData == null) return;

        float distance = Vector2.Distance(playerController.transform.position, transform.position);
        if (distance <= enemyData.attackRange)
        {
            Debug.Log("Player an dame" + enemyData.dameBase);
            playerController.TakeDamage(enemyData.dameBase);
        }
        else
        {
            Debug.Log("Player đã né");
        }
    }

    public void AttackFinished()
    {
        if (enemyAttackState != null)
        {
            enemyAttackState.isFinishAttack = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyData == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
    }

    protected override void Die()
    {
        if (finiteStateMachine != null && enemyDieState != null)
        {
            finiteStateMachine.ChangeState(enemyDieState);
        }

        OnGameObjectDeath?.Invoke(gameObject);
    }
}
