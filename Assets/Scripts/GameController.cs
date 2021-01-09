using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Singleton
    static GameController instance;

    public static GameController GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameController>();
            if (instance == null)
            {
                GameObject container = new GameObject("GameController");
                instance = container.AddComponent<GameController>();
            }
        }

        return instance;
    }

    [SerializeField]
    Canvas pauseScreen;

    SpawnController spawning;
    int round = 0;
    int knockouts = 0;
    int knockoutsThisRound = 0;
    int roundEnemies = 0;
    int nextRoundEnemies = 0;
    bool paused = false;

    public event EventHandler<int> RoundChanged;
    public event EventHandler<int> KnockoutsChanged;
    public event EventHandler<int> EnemiesLeftChanged;

    private void Awake()
    {
        spawning = SpawnController.GetInstance();

        EnemyHealth.OnEnemyDeath += OnEnemyDeath;
        EnemyHealth.WasHurt += EnemyHurtBeforeFirstRound;
    }

    private void OnDestroy()
    {
        EnemyHealth.OnEnemyDeath -= OnEnemyDeath;
        EnemyHealth.WasHurt -= EnemyHurtBeforeFirstRound;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextRoundEnemies = spawning.MaxEnemies;
        EnemiesLeftChanged?.Invoke(this, 0);
        KnockoutsChanged?.Invoke(this, 0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        if (pauseScreen)
            pauseScreen.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1f;
        if (pauseScreen)
            pauseScreen.gameObject.SetActive(false);
    }

    public void TogglePause()
    {
        if (paused)
            Unpause();
        else
            Pause();
    }

    public void EnemyHurtBeforeFirstRound(object sender, EventArgs e)
    {
        EnemyHealth.WasHurt -= EnemyHurtBeforeFirstRound;
        NextRound();
    }

    private void NextRound()
    {
        ++round;
        knockoutsThisRound = 0;
        spawning.MaxEnemies = nextRoundEnemies;
        roundEnemies = nextRoundEnemies;
        nextRoundEnemies = Mathf.FloorToInt(nextRoundEnemies * 1.1f);

        EnemiesLeftChanged?.Invoke(this, roundEnemies);
        RoundChanged?.Invoke(this, round);
    }

    public void OnEnemyDeath(object sender, EventArgs e)
    {
        ++knockouts;
        ++knockoutsThisRound;
        KnockoutsChanged?.Invoke(this, knockouts);
        EnemiesLeftChanged?.Invoke(this, roundEnemies - knockoutsThisRound);

        if (knockoutsThisRound >= roundEnemies)
        {
            NextRound();
        }
    }
}
