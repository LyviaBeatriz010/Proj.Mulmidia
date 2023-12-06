using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public int health = 20;

    public float speed;
    public float jumpForce;
    private float movement;
    public float dashForce;

    public GameObject bow;
    public Transform firePoint;

    private Rigidbody2D rig;

    public bool powerUpPuloDuplo;
    public bool powerUpDash;

    private bool isDash;
    private bool isJumping;
    private bool isDoubleJump;
    private bool isFire;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        //GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        BowFire();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement = Input.GetAxis("Horizontal");
        if (!isDash)
        {
            rig.velocity = new Vector2(movement * speed, rig.velocity.y);
        }

        if (movement > 0)
        {
            if (!isJumping && !isDash)
            {
              //  anim.SetInteger("transition", 1);
            }

            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movement < 0)
        {
            if (!isJumping && !isDash)
            {
                //anim.SetInteger("transition", 1);
            }

            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isFire && !isDash)
        {
           // anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
               // anim.SetInteger("transition", 2);
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
                isJumping = true;
            }
            else
            {
                if (!isDoubleJump)
                {
                    PuloDuplo();
                }
            }
        }
    }

    void PuloDuplo()
    {
        if (powerUpPuloDuplo)
        {
           // anim.SetInteger("transition", 2);
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            isDoubleJump = true;
        }
    }

    void BowFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFire = true;
          // anim.SetInteger("transition", 3);

            yield return new WaitForSeconds(0.4f);
           // anim.SetInteger("transition", 0);
            isFire = false;
        }
    }

    void Dash()
    {
        if (powerUpDash)
        {
            if (!isDash)
            {
                StartCoroutine(ExecucaoDash());
            }
        }
    }

    IEnumerator ExecucaoDash()
    {
        isDash = true;
        float DirecaoDoDash = rig.velocity.x + dashForce * movement;
        rig.velocity = new Vector2(DirecaoDoDash, rig.velocity.y);
       // anim.SetInteger("transition", 4);

        yield return new WaitForSeconds(0.5f);
       // anim.SetInteger("transition", 0);
        isDash = false;
    }

    public void Atirar()
    {
        GameObject Bow = Instantiate(bow, firePoint.position, firePoint.rotation);

        if (transform.rotation.y == 0)
        {
            Bow.GetComponent<Bow>().isRight = true;
        }

        if (transform.rotation.y == 180)
        {
            Bow.GetComponent<Bow>().isRight = false;
        }

    }
    
    /*
    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        sprite.color = Color.red;
        Invoke(nameof(voltarbranco), 1f);

        if (health <= 0)
        {
            anim.SetInteger("transition", 5);
            SceneManager.LoadScene(0);
        }
    }
   
    void voltarbranco()
    {
        sprite.color = Color.white;
    }
    
    */

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isJumping = false;
            isDoubleJump = false;
        }
    }
}
