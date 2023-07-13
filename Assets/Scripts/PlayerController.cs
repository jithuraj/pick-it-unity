using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    private bool isRotatingClockwise = true;
    public GameObject keyHole;
    private float radius = 1.30f;
    private bool isOnTarget = false;
    private GameObject currentTarget=null;
    public Rigidbody2D rb;
    private Vector3 prevPlayerPos;

    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("target");
        SpawanTargetAtRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        prevPlayerPos = transform.position;
        transform.RotateAround(new Vector2(0, 0), Vector3.forward, isRotatingClockwise ? speed * Time.deltaTime : -speed * Time.deltaTime);

        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) ||(Input.GetKeyDown(KeyCode.Space)))
        {
            isRotatingClockwise = !isRotatingClockwise;

            // Rotate player
                GetComponent<SpriteRenderer>().flipX = !isRotatingClockwise;
            if (isOnTarget)
            {
                currentTarget.GetComponent<Rigidbody2D>().AddForce((transform.position*100-prevPlayerPos*100)*10);
                currentTarget.GetComponent<Rigidbody2D>().AddTorque(5);
                //Destroy(currentTarget);
                SpawanTargetAtRandomPos();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            isOnTarget=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            isOnTarget = false;
        }
    }

    void SpawanTargetAtRandomPos()
    {

        float angle = Random.Range(0, 360);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        currentTarget = Instantiate(keyHole, new Vector2(x, y), Quaternion.identity);
    }
}
