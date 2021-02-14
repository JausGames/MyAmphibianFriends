using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] GameObject playerPrefab;
    public GameObject UI;
    public GameObject healtbar;
    public List<Player> players;
    public List<Player> alive;
    public Material[] materials;
    [SerializeField]  Vector3[] positions; 
    Vector3[] UIPositions = new Vector3[] 
    {   new Vector3(-14*Screen.width/30, -Screen.height/4),
        new Vector3(14*Screen.width/30 , Screen.height/4),
        new Vector3(-14*Screen.width/30, Screen.height/4),
        new Vector3(14*Screen.width/30, -Screen.height/4)
    };

    #region Singleton

    private void Awake()
    {
        instance = this;
    }

    #endregion

    


    private void Update()
    {
        foreach (Player player in players)
        {
            if (!player.GetAlive()) alive.Remove(player);
        }
    }
    public void SetMatchUp()
    {
        Debug.Log("PlayerManager, Set Match Up");
        alive.Clear();
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Set Match Up :" + players[i]);
            alive.Add(players[i]);
            players[i].SetAlive(true);
            players[i].ResetHealth();
            players[i].StopMotion();
            players[i].transform.localPosition = positions[i];
            players[i].controller.SetCanMove(false);
        }
        SetUIUp();
    }
    public void SetUIUp()
    {
        UI = transform.parent.Find("UI").gameObject;
        for (int i = 0; i < players.Count; i++)
        {
            //Create and set healthBar
            var bar = Instantiate(healtbar);
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 30, Screen.height / 3);
            bar.transform.SetParent(UI.transform.Find("Canvas"));
            bar.GetComponent<Healthometer>().setPlayer(players[i]);
            bar.GetComponent<Healthometer>().setMaxHealth(200f);
            //Set Color to player and Healthbar
            foreach(Renderer rend in players[i].renderers)
            {
                rend.GetComponent<Renderer>().material = materials[i];
            }

            var fill = bar.transform.Find("fill").gameObject;
            fill.GetComponent<Image>().color = UIColor.instance.GetColorByString(materials[i].name);
            var hearth = bar.transform.Find("hearth").gameObject;
            hearth.GetComponent<Image>().color = UIColor.instance.GetColorByString("light" + UppercaseFirst(materials[i].name));

            //Place healthbar
            var rect = bar.GetComponent<RectTransform>();
            rect.anchoredPosition = UIPositions[i];
        }
    }
    
    public void SetCanMove()
    {
        Debug.Log("PlayerManager, Set Can Move");
        foreach (Player player in players)
        {
            player.controller.SetCanMove(true);
        }

    }
    string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    public void SetSpawnPositions(Vector3[] pos)
    {
        Debug.Log("PlayerManager, SetSpawnPositions : PositionsLength = " + pos.Length );
        positions = pos;
    }
    public void SpawnPlayers(int nb)
    {
        Debug.Log("PlayerManager, SpawnPlayer : PositionsLength : " + positions.Length);
        for (int i = 0; i < nb; i++)
        {
            Debug.Log("PlayerManager, SpawnPlayer : Instantiate Player");
            var obj = Instantiate(playerPrefab);
            obj.transform.SetParent(this.transform);
            var player = obj.GetComponent<Player>();
            player.SetPlayerIndex(i);
            players.Add(player);
        }
        SetMatchUp();

    }
    public void DeletePlayers()
    {
        foreach (Player player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();
        alive.Clear();
    }

}

