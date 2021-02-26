using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 0.7f;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballShootingTime = 0.3f;

    float xMin, xMax, yMin, yMax;


    Coroutine shootCoroutine;

    bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    //setup the boundaries according to the camera
    private void SetUpMoveBoundaries()
    {
        //get the camera from Unity
        Camera gameCamera = Camera.main;

        //xMin = 0  xMax = 1
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //yMin = 0   yMax = 1
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    //moves the footballer around
    private void Move()
    {
        //deltaX is updated with the movement in the x-axis (left and right)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //newXPos = current x-pos of Player
        //+ difference in X by keyboard Input
        var newXPos = transform.position.x + deltaX;
        //clamps the newXPos between xMin and xMax
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        //the same as above but in the y-axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //update the position of the Player
        this.transform.position = new Vector2(newXPos, newYPos);

    }

     private IEnumerator FireContinuously()
    {
        while (true) //while coroutine is running
        {
            //create a ball Prefab, at the Player position
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
            //gives velocity to the ball in the y-axis
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 15f);
            //wait for x amount of seconds
            yield return new WaitForSeconds(ballShootingTime);

        }
    }

    private void Shoot()
    {
        //When the player shoots the ball
        if (Input.GetButtonDown("Fire1"))
        {
          shootCoroutine = StartCoroutine(FireContinuously());
        }

        //when button is released
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootCoroutine);
        }
    }

}
