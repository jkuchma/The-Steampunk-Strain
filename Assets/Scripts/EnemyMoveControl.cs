using UnityEngine;

/* EnemyMoveController :: an enemy movement controller class for a 2d side-scroller */
public class EnemyMoveControl : MonoBehaviour
{
    /************************/
    /* finite state machine */
    /************************/

    public abstract class BaseState
    {
        public float delay;
        public Rigidbody2D rb;
        public EnemyMoveControl emy;

        public BaseState(GameObject obj)
        {
            delay = 0f;
            this.rb = obj.GetComponent<Rigidbody2D>();
            this.emy = obj.GetComponent<EnemyMoveControl>();
        }
        
        public abstract void Enter();
        public abstract void Tick();
    }
    

    public class PatrolState : BaseState
    {
        public PatrolState(GameObject obj) : base(obj) {}

        public override void Enter()
        {
            Debug.Log("state: Patrol");
            delay = 0f;
        }

        public override void Tick()
        {
            delay += Time.deltaTime;
            
            if (emy.atWallOrCliff && delay > 1.5f)
            {
                delay = 0f;
                emy.atWallOrCliff = !emy.atWallOrCliff;
                emy.currentDirection = (Direction)(-1 * (int)emy.currentDirection);
            }
            
            else
            {
                var force = (20f + emy.speedModifier);
                rb.AddForce(directions[1 + (int)emy.currentDirection] * force);
            }
        }
    }


    public class PursuitState : BaseState
    {
        public PursuitState(GameObject obj) : base(obj) {}

        public override void Enter()
        {
            Debug.Log("state: Pursuit");
            delay = 0f;
        }

        public override void Tick()
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, rb.transform.position) < 3) 
                ; // TODO ATTACK HERE
            
            else if ( GameObject.FindWithTag("Player").transform.position.x < (rb.transform.position.x - 3) )
            {
                var force = (20f + emy.speedModifier);
                rb.AddForce(Vector2.right * force * Time.deltaTime);
            }
            
            else if ( GameObject.FindWithTag("Player").transform.position.x > (rb.transform.position.x + 3) )
            {
                var force = (20f + emy.speedModifier);
                rb.AddForce(Vector2.left * force * Time.deltaTime);
            }

        }
    }

    /****************************/
    /* built-in unity functions */
    /****************************/
    
    
    public static PatrolState patrol;
    public static PursuitState pursuit;

    public enum Direction { Left = -1, None, Right = 1 };
    [SerializeField] public Direction currentDirection = Direction.None;
    public BaseState currentState;
    [SerializeField] private bool atWallOrCliff = false;
    public static Vector2[] directions = {Vector2.left, Vector2.zero, Vector2.right};
    
    [Range(-1f, 1f), SerializeField] private float speedModifier = 0;

    void Awake()
    {
        patrol = new PatrolState(this.gameObject);
        pursuit = new PursuitState(this.gameObject);
    }

    void Start()
    {
        currentState = patrol;
        if ( currentDirection.Equals(Direction.None) ) currentDirection = Random.Range(0, 1) > .5 ? Direction.Left : Direction.Right;
    }

    void FixedUpdate()
    {
        currentState.Tick();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("EnterCollider");
        if (other.gameObject.tag.Equals("Player"))
        {
            pursuit.Enter();
            currentState = pursuit;
        }
        
        else atWallOrCliff = !atWallOrCliff;
    }
        
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("ExitCollider");
        if (other.gameObject.tag.Equals("Player"))
        {
            patrol.Enter();
            currentState = patrol;
        }
        
        else if (pursuit.delay > 1.5f) atWallOrCliff = !atWallOrCliff;
    }

}