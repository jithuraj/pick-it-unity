using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float radius = 1.30f;
    public GameObject scoreControllerObject;
    private ScoreController scoreController;

   [SerializeField] private float angle;
    private bool isPlayerInside = false;

    void Start()
    {
        scoreController = scoreControllerObject.GetComponent<ScoreController>();
        scoreController.ShowHighScore();
    }

    private void Update()
    {
        HandleInput();

    }
    void FixedUpdate()
    {
        RotatePlayer();

    }

    
    void RotatePlayer()
    {

        transform.RotateAround(new Vector2(0, 0), Vector3.forward,  angle );

    }

    void HandleInput()
    {
        if (OnTouchInput() || OnKeyboardInput())
        {
            SwitchPlayerPos();
        }
    }

    void SwitchPlayerPos()
    {
        Vector3 direction = transform.position - Vector3.zero;
        if (isPlayerInside)
        {
            transform.position = direction.normalized * 1.13f;
        }
        else
        {
            transform.position = direction.normalized / 1.25f;
        }
        isPlayerInside = !isPlayerInside;
    }

    void OnSuccess()
    {

        // Add score
        scoreController.AddScore();

    }

    bool OnTouchInput()
    {
        return (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended);
    }

    bool OnKeyboardInput()
    {
        return (Input.GetKeyDown(KeyCode.Space));
    }

}
