using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public Bullet bulletPf;
    private Rigidbody2D playerRb;
    public Camera mainCam;
    private Vector2 camPosition;
    private Vector3 camSway;
    public float camSpeed;
    private Vector3 aimVector;
    [SerializeField] private float swayStrength;
    [SerializeField] private Vector2 camMax;
    [SerializeField] private Sprite fastSprite;
    [SerializeField] private Sprite slowSprite;

    public uint health;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        camPosition = new Vector2(0, 0);
        camSway = new Vector2(0, 0);

        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //screen to world point helps convert the pixel coordinates of the mouse to world coordinates

        aimVector = (mousePos - playerRb.position).normalized;   //make player point to mouse

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0, 0, 0.5f);

        //    transform.up = (mousePos - playerRb.position).normalized;
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0, 0, -0.5f);
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        //camera movement

        //adjusts sway dynamically

        camSway = (Input.mousePosition - playerRb.transform.position - new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)) * swayStrength;
        if (camSway.x > camMax.x)
        {
            camSway.x = camMax.x;
        }
        if (camSway.y > camMax.y)
        {
            camSway.y = camMax.y;
        }
        if (camSway.x < -camMax.x)
        {
            camSway.x = -camMax.x;
        }
        if (camSway.y < -camMax.y)
        {
            camSway.y = -camMax.y;
        }

        Vector2 newPosition = playerRb.transform.position + camSway;
        camPosition = camPosition + ((newPosition - camPosition) * camSpeed * Time.deltaTime);

        mainCam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);

        //flips player sprite
        if(camPosition.x < playerRb.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //changes player sprite
        if(playerRb.velocity.magnitude > 3)
        {
            GetComponent<SpriteRenderer>().sprite = fastSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = slowSprite;
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPf, transform.position + aimVector*2, playerRb.transform.rotation);

        bullet.CreateBullet(aimVector);

        playerRb.AddForce(-aimVector, ForceMode2D.Impulse);
        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Collide with player bullets

    //    //if (collision.gameObject.tag == "enemyBullet")
    //    //{
    //    //    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
    //    //}

    //    if (collision.gameObject.tag == "enemyBullet")
    //    {
    //        health -= 10;
    //    }
    //}

}