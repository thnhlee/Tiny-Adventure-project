using System.Collections;
using UnityEngine;

[AddComponentMenu("AnhDuy/EnemyAI")]
public class EnemyAI : MonoBehaviour
{

    private Transform currentTarget;
    public Transform pointAttack; 
    
    public float moveSpeed = 0.25f;
    public float waitTime = 1f; 
    public float attackRadius = 1f; 
    public float requiredDistance = 5f; 
    public float attackRate = 1f; 
    public int attackDamage = 10;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = false;
    //private Transform player;
    [SerializeField] private float roamChangeDir = 1f;
    private GameObject player;
    private enum State
    {
        TargetPlayer
    }
    private State state;
    private EnemyPathFinding enemyPathfinding;
    private bool isAttackOnCooldown = false;
    private Enemy enemy;
    private Vector2 lastPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        state = State.TargetPlayer;
        enemyPathfinding = GetComponent<EnemyPathFinding>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(TargetPlayerRoutine());
    }

    private IEnumerator TargetPlayerRoutine()
    {
        while (true)
        {
            while (state == State.TargetPlayer)
            {
                animator.SetBool("isWalking", true);
                if (player != null && !isAttackOnCooldown)
                {
                    Vector2 targetPosition = player.transform.position;
                    enemyPathfinding.MoveTo(targetPosition);
                    Flip(targetPosition);
                }

                if (player != null && Vector2.Distance(pointAttack.position, player.transform.position) <= requiredDistance)
                {
                    if (Vector2.Distance(pointAttack.position, player.transform.position) <= attackRadius && !isAttackOnCooldown)
                    {
                        StartCoroutine(PerformAttack());
                    }
                }
                else
                {
                    Debug.Log("Player is Dead!");
                    //Hien thi man hinh gameover
                    //if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
                    //{
                    //    rb.velocity = Vector2.zero;
                    //    animator.SetBool("isWalking", false);
                    //    yield return new WaitForSeconds(waitTime);

                    //}
                }

                yield return new WaitForSeconds(roamChangeDir);
            }

            yield return null;
        }
    }




    private void Flip(Vector2 targetPosition)
    {
        if ((targetPosition.x > transform.position.x && facingRight) || (targetPosition.x < transform.position.x && !facingRight))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }



    private IEnumerator PerformAttack()
    {
        isAttackOnCooldown = true;
        

        animator.SetTrigger("Attack");

    
        yield return new WaitForSeconds(0.5f); 
                
    
        if (player != null && Vector2.Distance(pointAttack.position, player.transform.position) <= attackRadius)
        {
        
            player.GetComponent<Player>().TakeDamage(gameObject, attackDamage);
        }

    
        yield return new WaitForSeconds(attackRate);
        isAttackOnCooldown = false;
    }
    public void UpdateLastPosition(Vector2 newPosition)
    {
        lastPosition = newPosition;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        player = other.transform;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        player = null;
    //    }
    //}
}
