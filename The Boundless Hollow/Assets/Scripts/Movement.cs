using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed;

    private bool moving;

    public Vector2 input;

    public LayerMask walls;

    private Player player;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        player = GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        speed = player.speed;

       

        if (!moving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(input.x < 0) { spriteRenderer.flipX = true; }
                else { spriteRenderer.flipX = false; }

                if (CollisonCheck(targetPos))
                {
                    StartCoroutine(Walk(targetPos));
                }
            }
        }
    }
    IEnumerator Walk(Vector3 targetPos)
    {
        moving = true;
        animator.SetBool("Moving", moving);
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }  
        transform.position = targetPos;
        moving = false;
        animator.SetBool("Moving", moving);
    }

    private bool CollisonCheck(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos,0.2f, walls) != null)
        {
            return false;
        }

        return true;
    }

    //All Pickups use a trigger2D collider
    //Enemys currently use a trigger2D collider
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Pickup Items and Weapons when collide with them
        if(collision.gameObject.tag == "PickUp") 
        {
            GameObject pickUp = collision.gameObject;
            
            if( pickUp.GetComponent<Pickup>() != null) 
            {
                Pickup pick = pickUp.GetComponent<Pickup>();
                if (pick.item) { player.ItemPickup(pick.itemID); }
                else { player.WeaponPickup(pick.itemID); }
                
            }
            Destroy(collision.gameObject);
        }

        //Take damage when colliding with enemy
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            player.TakeDamage(enemy.GetComponent<Enemy>().damage);
        }

        
    }
    
}

