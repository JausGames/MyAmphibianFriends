using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject canvas = null;
    [SerializeField] GameObject playAgainUI = null;
    [SerializeField] GameObject timerUI = null;
    [SerializeField] PlayAgain playAgain = null;
    [SerializeField] EnnemyController enemy = null;
    [SerializeField] int nbPlayers = 0;

    #region Singleton
    public static MatchManager instance;
    public static PlayerManager playerManager;

    private void Awake()
    {
        instance = this;
        playerManager = GetComponentInChildren<PlayerManager>();
        enemy = GetComponentInChildren<EnnemyController>();
        UI = transform.Find("UI").gameObject;
        canvas = UI.transform.Find("Canvas").gameObject;
        timerUI = canvas.transform.Find("Timer").gameObject;
        playAgainUI = canvas.transform.Find("PlayAgain").gameObject;
        playAgain = playAgainUI.GetComponent<PlayAgain>();

    }

    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        timerUI.GetComponent<Timer>().SetManager(this);
        playerManager.SetMatchUp();
        timerUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerManager.alive.Count == 0 && !playAgain.playAgain) playAgainUI.SetActive(true);
    }

    public void StartGame()
    {
        timerUI.SetActive(false);
        enemy.SetCanMove(true);
        playerManager.SetCanMove();
    }
    public void ResetGame()
    {
        Debug.Log("Touça");
        playAgain.playAgain = false;
        enemy.ResetPosition();
        enemy.SetCanMove(false);
        playerManager.SetMatchUp();
        timerUI.SetActive(true);
    }
    public void SetNbPlayers(int value)
    {
        nbPlayers = value;
        PlayerManager.instance.SpawnPlayers(nbPlayers);
    }
    public void DeletePlayers()
    {
        PlayerManager.instance.DeletePlayers();
    }


}
