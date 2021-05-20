using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRController : SingletonMono<CRController>
{
    public List<Color> ColorGround = new List<Color>();
    public List<Color> ColorPlayer = new List<Color>();
    public List<GameObject> Grounds;
    public GameObject Ground = null;
    public int Score;
    public GameObject qq;


    float lastX = 0;
    public enum GameState
    {
        Pause,
        Playing,
        GameOver,
    }
    public static GameState gamestate { get; set; } = GameState.Pause;

    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameState.Playing;

            StartSpawn();
        
        
        AddColor(ColorPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.Translate(new Vector3(1, 0, 0) * 3 * Time.deltaTime);

    }
    public void StartSpawn()
    {
        lastX = Camera.main.transform.position.x;
        InvokeRepeating("Spawn", 0.0f, 1f);
    }


    private void Spawn()
    {
        AddColor(ColorGround);

        if (gamestate == GameState.GameOver)
        {
            CancelInvoke("Spawn");
        }
        else transform.Translate(new Vector3(1, 0, 0) * 3.0f * Time.deltaTime);

        if (Ground != null)
        {
            GameObject pip = (GameObject)PhotonNetwork.Instantiate("Ground", new Vector3(lastX + 5, 0.4f, 0), Quaternion.identity);
            lastX = pip.transform.position.x;
        }
    }
    public void AddColor(List<Color> mylist)
    {
        ColorGround.Clear();
        mylist.Add(Color.red);
        mylist.Add(Color.blue);
        mylist.Add(Color.green);
        mylist.Add(Color.yellow);
    }
    public void SetColor(GameObject Pipe, Color rdlist)
    {
        Pipe.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        Pipe.GetComponent<Renderer>().material.SetColor("_EmissionColor", rdlist);
    }

    public void GameOver()
    {
        gamestate = GameState.GameOver;
        Time.timeScale = 0;
    }
}
