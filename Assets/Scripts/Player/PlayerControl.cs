using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("AnhDuy/PlayerControl")]
public class PlayerControl : Singleton<PlayerControl>
{
    public float moveSpeed = 5f; 


    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true;
    private Animator anim;

    //DASH
    [SerializeField] private TrailRenderer myTrailRenderer;
    private float baseMoveSpeed;
    private bool isDashing = false;
    [SerializeField] private float dashSpeed = 4f;

    void Start()
    {
        baseMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        bool isWalking = movement.x != 0f || movement.y != 0f;
        anim.SetBool("isWalking", isWalking);
        if ((movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight))
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    //DASH
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDash());
        }
    }

    private IEnumerator EndDash()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = baseMoveSpeed;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
