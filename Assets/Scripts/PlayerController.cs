
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 mouseLocation;

    private Rigidbody2D playerRb;

    private Camera cam;
    
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float forceSpeed;

    [SerializeField] private GameObject ParticleSystem;
    
    [SerializeField] private float followSpeed;

    [SerializeField] private float jumpTimeCounter;

    private float counter;

    private GameManager gameManager;

    private bool isJumping;

    [SerializeField] private GameObject invisibleWall;

    [SerializeField] private float invisibleWallOffset;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        
        invisibleWall.transform.position = new Vector3(PlayerPrefs.GetFloat("X")-invisibleWallOffset, PlayerPrefs.GetFloat("Y")+invisibleWall.transform.localScale.y/2, 0);
    }

    private void Update()
    {
        if (gameManager.IsGameActive)
        {
            mouseLocation = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mouseLocation - (Vector2)playerRb.position);
            direction.Normalize();

            float rotationZ = Vector3.Cross(direction, transform.up).z;

            if (!CheckForCollidingWIthGround())
            {
                //transform.localEulerAngles+=new Vector3(0,0,rotationZ*rotationSpeed);
                playerRb.angularVelocity = -rotationZ * rotationSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AddForceToPlayer(direction);
                counter = jumpTimeCounter; 
                isJumping = true;
                Instantiate(ParticleSystem);
            }

            if (Input.GetKey(KeyCode.Mouse0) && counter > 0 && isJumping)
            {
                AddForceToPlayer(direction);
                counter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

            if (transform.position.y < -6)
            {
                gameManager.GameOver();
            }

            
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameActive)
        {
            cam.transform.position = Vector3.Slerp(cam.transform.position,new Vector3(transform.position.x, transform.position.y, -10f), followSpeed * Time.deltaTime);
        }
    }

    void AddForceToPlayer(Vector2 direction)
    {
        playerRb.AddForce(-direction*forceSpeed);
    }

    bool CheckForCollidingWIthGround()
    {
        bool check=false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position , 1.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Floor"))
            {
                check = true;
            }
        }
        if (check)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            gameManager.score++;
            gameManager.UpdateScore();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Drug"))
        {
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
        

        if (other.CompareTag("CheckPoint"))
        {
            if (gameManager.IsGameActive)
            {
                gameManager.SetPlayerPrefsData(other.transform.position.x,other.transform.position.y);
                
                PlayerPrefs.SetInt("Score",gameManager.score);
                
                Destroy(other.gameObject);

                invisibleWall.transform.position = new Vector3(PlayerPrefs.GetFloat("X")-invisibleWallOffset, PlayerPrefs.GetFloat("Y")+invisibleWall.transform.localScale.y/2-1, 0);
            }
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            gameManager.GameFinished();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("DieRoof"))
        {
            gameManager.GameOver();
        }
    }
}