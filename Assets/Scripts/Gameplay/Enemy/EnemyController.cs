using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private FiniteStateMachine finiteStateMachine;
    [SerializeField]
    private EnemyData enemyData;
    public EnemyData data => enemyData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public PlayerController playerController {get;private set;}
    [Header("State")]
    public EnemyAttackState enemyAttackState;
    public EnemyChaseState enemyChaseState;
    public EnemyIdleState enemyIdleState;

    private void Awake()
    {
        GetComponent();       
    }
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        playerController = playerObj.GetComponent<PlayerController>();
        IntiaInitializeStates();
        finiteStateMachine.Intialize(enemyIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        finiteStateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        finiteStateMachine.CurrentState.PhysicsUpdate();
    }
    private void GetComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();     
    }

    private void IntiaInitializeStates()
    {
        finiteStateMachine = new FiniteStateMachine();
        enemyIdleState = new EnemyIdleState(finiteStateMachine, this, animator);
        enemyAttackState = new EnemyAttackState(finiteStateMachine, this, animator);
        enemyChaseState = new EnemyChaseState(finiteStateMachine, this, animator);       
    }

    public void OnAttackHit()
    {
        if (playerController == null) return;
        float distance = Vector2.Distance(playerController.transform.position, transform.position);
        if (distance <= data.attackRange)
        {
            Debug.Log("Player an dame" + data.dameBase);
            playerController.TakeDamage(data.dameBase);
        }
        else { Debug.Log("Player đã né"); }
        ;
    }

    public void AttackFinished()
    {
        enemyAttackState.isFinishAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }
}
