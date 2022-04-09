using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : Character
{
    [SerializeField] private float movemenSpeed = 5f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelClearMenu;

    private Rigidbody2D playerRB;
    private bool isGrounded = false;
    private float speedTimer = 0f;
    private Vector3 lastPos;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        lastPos = GameData.instance.GetComponent<GameData>().lastPosition;

        if(GameData.instance.lastPosition != null){
            gameObject.transform.position = lastPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void Movement()
    {
        float finalSpeedMultiplier = 1;

        if(speedTimer > 0)
        {
            finalSpeedMultiplier = speedMultiplier;
            speedTimer -= Time.deltaTime;
        }
        float horizontalMultiplier = movemenSpeed * Input.GetAxis("Horizontal") * Time.deltaTime * finalSpeedMultiplier;
        transform.Translate(Vector2.right * horizontalMultiplier);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }   
        if(collision.gameObject.tag == "Clear")
        {
            levelClearMenu.SetActive(true);
            lastPos = Vector3.zero;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
