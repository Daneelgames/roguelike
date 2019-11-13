using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public GameObject cameraObject;
    [HideInInspector]
    public float movementCooldown = 0;
    public float movementCooldownMax = 0.5f;
    float timeScaleSmooth = 0.3f;


    float gameSpeed = 1;

    GameManager gm;

    bool diagonalMovement = false;
    public bool playersTurn = false;

    public void Init()
    {
        gm = GameManager.instance;
    }

    public void _Update()
    {
        if (playersTurn && gm.player.health > 0)
            Movement();

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void LateUpdate()
    {
        cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, gm.player.transform.position, 0.1f);
    }

    void Movement()
    {
        if (movementCooldown > 0)
        {
            movementCooldown -= Time.deltaTime;
        }

        if (movementCooldown <= 0)
        {
            if (SimpleInput.GetAxisRaw("Vertical") > 0)
            {
                StopCoroutine(gm.movementSystem.Move(Vector3.zero, false));
                StartCoroutine(gm.movementSystem.Move(new Vector3(0, 0, 1), false));
                if (gameSpeed < 2)
                    gameSpeed += Time.deltaTime * 10;
                if (timeScaleSmooth < 0.75)
                    timeScaleSmooth += Time.deltaTime * 5;
            }
            else if (SimpleInput.GetAxisRaw("Horizontal") > 0)
            {
                StopCoroutine(gm.movementSystem.Move(Vector3.zero, false));
                StartCoroutine(gm.movementSystem.Move(new Vector3(1, 0, 0), false));
                if (gameSpeed < 2) gameSpeed += Time.deltaTime * 10;
                if (timeScaleSmooth < 0.75)
                    timeScaleSmooth += Time.deltaTime * 5;
            }
            else if (SimpleInput.GetAxisRaw("Vertical") < 0)
            {
                StopCoroutine(gm.movementSystem.Move(Vector3.zero, false));
                StartCoroutine(gm.movementSystem.Move(new Vector3(0, 0, -1), false));
                if (gameSpeed < 2) gameSpeed += Time.deltaTime * 10;
                if (timeScaleSmooth < 0.75)
                    timeScaleSmooth += Time.deltaTime * 5;
            }
            else if (SimpleInput.GetAxisRaw("Horizontal") < 0)
            {
                StopCoroutine(gm.movementSystem.Move(Vector3.zero, false));
                StartCoroutine(gm.movementSystem.Move(new Vector3(-1, 0, 0), false));
                if (gameSpeed < 2) gameSpeed += Time.deltaTime * 10;
                if (timeScaleSmooth < 0.75)
                    timeScaleSmooth += Time.deltaTime * 5;
            }

            if (SimpleInput.GetAxisRaw("Horizontal") == 0 && SimpleInput.GetAxisRaw("Vertical") == 0)
            {
                gameSpeed = 1;
                timeScaleSmooth = 0.1f;
                if (timeScaleSmooth < 0.75)
                    timeScaleSmooth += Time.deltaTime * 5;
            }
            Time.timeScale = Mathf.Lerp(Time.timeScale, gameSpeed,timeScaleSmooth);
        }
    }
}