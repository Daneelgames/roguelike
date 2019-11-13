using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameEvent {GameReady,  PlayerAct, NpcMove, NpcAct, ProjectilesMove}

    public static GameManager instance;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public MovementSystem movementSystem;
    [HideInInspector] public AttackSystem attackSystem;
    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public MusicGeneratorSystem musicGeneratorSystem;
    public HealthEntity player;

    [HideInInspector] public EntityList entityList;

    private void Awake()
    {
        instance = this;
        entityList = GetComponent<EntityList>();
        playerInput = GetComponent<PlayerInput>();
        movementSystem = GetComponent<MovementSystem>();
        attackSystem = GetComponent<AttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        musicGeneratorSystem = GetComponentInChildren<MusicGeneratorSystem>();
        Init();
    }

    void Init()
    {
        playerInput.Init();
        entityList.Init();
        movementSystem.Init();
        attackSystem.Init();
        healthSystem.Init();
        musicGeneratorSystem.Init();

        Step(GameEvent.GameReady);
    }

    private void Update()
    {
        playerInput._Update();    
    }

    public void Step(GameEvent lastEvent) 
    {
        switch(lastEvent)
        {
            case GameEvent.GameReady:
                playerInput.playersTurn = true;
                Invoke("AutoPassTurn", 1);
                break;

            case GameEvent.PlayerAct:
                CancelInvoke("AutoPassTurn");
                attackSystem.TurnOffTelegraphTurns();
                playerInput.playersTurn = false;
                // npcs that move are first
                StartCoroutine(attackSystem.MoveProjectiles());
                musicGeneratorSystem.Step();
                break;

            case GameEvent.ProjectilesMove:
                //attacking are second
                StartCoroutine(movementSystem.NpcMove());
                break;

            case GameEvent.NpcMove:
                attackSystem.NpcAttack();
                break;

            case GameEvent.NpcAct:
                playerInput.playersTurn = true;
                Invoke("AutoPassTurn", 1);
                break;
        }
    }

    void AutoPassTurn()
    {
        Step(GameEvent.PlayerAct);
    }
}