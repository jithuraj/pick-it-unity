using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    private bool isRotatingClockwise = false;
    public GameObject keyHole;
    public float radius = 1.35f;
    private bool isOnTarget = false;
    private GameObject currentTarget=null;
    public Rigidbody2D rb;

    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("target");
        SpawanTargetAtRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector2(0, 0), Vector3.forward, isRotatingClockwise ? speed * Time.deltaTime : -speed * Time.deltaTime);

        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) ||(Input.GetKeyDown(KeyCode.Space)))
        {
            isRotatingClockwise = !isRotatingClockwise;
            if (isOnTarget)
            {
                Debug.Log("you got it");

                currentTarget.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x,transform.position.y));
                
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
