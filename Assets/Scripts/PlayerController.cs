using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    private bool isRotatingClockwise = true;
    public GameObject keyHole;
    private float radius = 1.30f;
    private bool isOnTarget = false;
    private GameObject currentTarget = null;
    public Rigidbody2D rb;
    private Vector3 prevPlayerPos;
    public int torque;
    public GameObject scoreControllerObject;
    private ScoreController scoreController;
    private bool isKicked = false;
    private float r;

    public GameObject pickableHeart;
    private GameObject currentlySpawnedHeart;

    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("target");
        scoreController = scoreControllerObject.GetComponent<ScoreController>();
        SpawanTargetAtRandomPos();
        scoreController.ShowHighScore();
    }

    private void Update()
    {
        HandleInput();

    }
    void FixedUpdate()
    {
        StorePreviousPos();
        RotatePlayer();

    }

    void StorePreviousPos()
    {
        prevPlayerPos = transform.position;
    }

    void RotatePlayer()
    {
        transform.RotateAround(new Vector2(0, 0), Vector3.forward, isRotatingClockwise ? speed * Time.deltaTime : -speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(Vector3.forward, prevPlayerPos, Time.deltaTime);
    }

    void HandleInput()
    {
        if (OnTouchInput() || OnKeyboardInput())
        {
            ReversePlayerDirection();

            if (isOnTarget)
            {
                OnSuccess();
                SpawnHeartAtRandomPos();
            }
        }
    }

    void SpawnHeartAtRandomPos()
    {
        float angle = GenerateAngleWithOffset();
        float x = Mathf.Cos(360) * radius;
        float y = Mathf.Sin(360) * radius;
        currentlySpawnedHeart = Instantiate(pickableHeart, new Vector2(x, y), Quaternion.identity);
        Debug.Log(angle);
        //currentlySpawnedHeart.transform.RotateAround(new Vector2(0, 0), Vector3.up,angle);

    }

    void OnSuccess()
    {
        isKicked=true;

        // Add score
        scoreController.AddScore();

        // Add force to move the ball tangentially
        currentTarget.GetComponent<Rigidbody2D>().AddForce((transform.position * 100 - prevPlayerPos * 100) * 10);

        // Add torque to spin the ball
        currentTarget.GetComponent<Rigidbody2D>().AddTorque(torque);

        // Remove collider component of ball
        Destroy(currentTarget.GetComponent<Collider2D>());

        // Spawn new ball at random position
        SpawanTargetAtRandomPos();
    }

    void ReversePlayerDirection()
    {
        isRotatingClockwise = !isRotatingClockwise;
        GetComponent<SpriteRenderer>().flipX = !isRotatingClockwise;
    }

    bool OnTouchInput()
    {
        return (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended);
    }

    bool OnKeyboardInput()
    {
        return (Input.GetKeyDown(KeyCode.Space));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            isOnTarget = true;
            isKicked = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            isOnTarget = false;
            if(isKicked==false)
            {
                LostTarget();
            }
        }
    }

    void LostTarget()
    {
        scoreController.DecreaseLife();
    }

    void SpawanTargetAtRandomPos()
    {
        float angle = GenerateAngleWithOffset();
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        currentTarget = Instantiate(keyHole, new Vector2(x, y), Quaternion.identity);
    }

    float GenerateAngleWithOffset()
    {
        float angleOffset = 60;
        float playerRotation = transform.rotation.eulerAngles.z;
        float angle = Random.Range(0, 360);
        if(angle > playerRotation-angleOffset && angle < playerRotation + angleOffset)
        {
           angle = GenerateAngleWithOffset();
        }

        return angle;
    }
}
