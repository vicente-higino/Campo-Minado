using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameControl : MonoBehaviour
{

    public GameObject prefab;
    public GameObject camera;
    public GameObject gameOver;
    public GameObject gameWin;
    public bool firstClick = false;
    public Text bombaText;
    public Text marcadoresText;
    public int bombs;
    public int marcadores;
    public Sprite[] sprites;
    public static GameControl instance;
    public int x;
    public int y;
    public bool isOver = false;
    private float time;
    public List<GameObject> num;
    private int n = 0;
   
    Sprite[] textures;
    string[] names;
    // Use this for initialization
    void Awake()
    {
        x = Config.X;
        y = Config.Y;
        bombs = Config.Bombs;
        time = 0f;
        marcadores = bombs;
        num = new List<GameObject>();
        textures = Resources.LoadAll<Sprite>("Sprites");
        names = new string[textures.Length];
        for (int ii = 0; ii < names.Length; ii++)
        {
            names[ii] = textures[ii].name;

        }

        //var game = Instantiate(prefab, new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f)), Quaternion.identity);

        for (float i = 0; i <= 13f * x; i += 13f)
        {
            for (float e = 0; e <= 13f * y; e += 13f)
            {
                num.Add(newPoiter(i, e, string.Format("Poiter({0},{1})", i, e)));
                //Instantiate(newNumero(), new Vector3(i,e), Quaternion.identity);

            }

        }
        int bomb = 0;
        while (bomb < bombs)
        {
            var item = num[UnityEngine.Random.Range(0, num.Count)].GetComponent<RandomSprite>();
            if (!item.isBomb)
            {
                item.SetBomb();
                bomb++;
            }

        }

        foreach (var i in num)
        {
            bomb = 0;
            var c = i.transform.position;
            foreach (var b in num)
            {
                var d = b.transform.position;
                var e = b.GetComponent<RandomSprite>();
                if (c.x == d.x && (d.y == (c.y + 13) || d.y == (c.y - 13)))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }

                }
                if (c.y == d.y && (d.x == (c.x + 13) || d.x == (c.x - 13)))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }
                }
                if (d.x == (c.x - 13) && d.y == (c.y - 13))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }
                }
                if (d.x == (c.x - 13) && d.y == (c.y + 13))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }
                }
                if (d.x == (c.x + 13) && d.y == (c.y - 13))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }
                }
                if (d.x == (c.x + 13) && d.y == (c.y + 13))
                {
                    if (e.isBomb)
                    {
                        bomb++;
                    }
                }
            }
            i.GetComponent<RandomSprite>().setNumberBomb(bomb);
        }

        bombaText.text = "Bombas: " + bombs.ToString();
        marcadoresText.text = "Marcadores: " + marcadores.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if (firstClick && !isOver)
        {
            time += Time.deltaTime;
            TimeSpan t = TimeSpan.FromSeconds(time);
            bombaText.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                    t.Hours,
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds);
            marcadoresText.text = "Marcadores: " + marcadores.ToString();
        }

        if (isOver && !(bombs == 0))
        {
            gameOver.SetActive(true);
            if (Input.GetKey(KeyCode.Space) || Input.touchCount == 1 || Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
        if (bombs == 0 && !isOver)
        {
            gameWin.SetActive(true);
            isOver = true;
            GameRecord record = new GameRecord();
            DateTime dateTime = DateTime.Now;
            record.date = dateTime.ToString();
            record.bombas = Config.Bombs;
            record.x = x;
            record.y = y;
            record.secondsToWin = time;
            string infoGame = JsonUtility.ToJson(record).ToString();
            string rec = string.Format("Record_[{0}]", dateTime.ToString());
            PlayerPrefs.SetString(rec, infoGame);

            PlayerPrefs.Save();
            Debug.Log(infoGame);
            Debug.Log(rec);
            if (Input.GetKey(KeyCode.Space) || Input.touchCount == 1 || Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }




    }

    public GameObject newPoiter(float x, float y, string name)
    {
        GameObject game = new GameObject();
        game.name = name;
        game.tag = "Poiter";
        game.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = game.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = textures[Array.IndexOf(names, "empty")];
        spriteRenderer.color = new Color(255, 255, 255);
        Transform trans = game.transform;
        trans.localScale = new Vector3(20, 20);
        trans.position = new Vector3(x, y);
        game.AddComponent<RandomSprite>();
        game.AddComponent<BoxCollider2D>();
        BoxCollider2D box = game.GetComponent<BoxCollider2D>();
        box.size = new Vector2(0.64f, 0.64f);


        return game;
    }


}
public class GameRecord
{
    public string date;
    public float secondsToWin;
    public int bombas;
    public int x;
    public int y;
    public GameRecord()
    {

    }
}
