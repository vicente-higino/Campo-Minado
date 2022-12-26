using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class RandomSprite : MonoBehaviour {


    Sprite[] textures;
    string[] names;
    public Sprite Sprite;
    public bool isBomb = false;
    public int bombas = 0;
    private bool isOpen = false;
    private bool isMarked = false;
    // Use this for initialization
    void Start() {
        textures = Resources.LoadAll<Sprite>("Sprites");
        names = new string[textures.Length];
        for (int ii = 0; ii < names.Length; ii++)
        {
            names[ii] = textures[ii].name;

        }
        if (isBomb)
        {

        }
    }

    // Update is called once per frame
    void Update() {
       
    }

    private void OnMouseEnter()
    {
        //Debug.Log(transform.position);
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            transform.localScale = new Vector3(30, 30);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "foregrund";
        }
        
    }
    public void rightClick()
    {
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            if (GameObject.Find("GameControl").GetComponent<GameControl>().marcadores > 0 && !isMarked && !isOpen)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = textures[Array.IndexOf(names, "marcador")];
                GameObject.Find("GameControl").GetComponent<GameControl>().marcadores--;
                Debug.Log(GameObject.Find("GameControl").GetComponent<GameControl>().marcadores);

                if (isBomb)
                {
                    GameObject.Find("GameControl").GetComponent<GameControl>().bombs--;
                    Debug.Log(GameObject.Find("GameControl").GetComponent<GameControl>().bombs);


                }
                isMarked = true;
            }
            else if (isMarked)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = textures[Array.IndexOf(names, "empty")];
                GameObject.Find("GameControl").GetComponent<GameControl>().marcadores++;
                if (isBomb)
                {
                    GameObject.Find("GameControl").GetComponent<GameControl>().bombs++;
                    Debug.Log(GameObject.Find("GameControl").GetComponent<GameControl>().bombs);


                }
                isMarked = false;
                
            }
        }
       
    }
    private void OnMouseExit()
    {
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            transform.localScale = new Vector3(20, 20);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "Default";
        }
    }
    public void leftClick()
    {
        if (!GameObject.Find("GameControl").GetComponent<GameControl>().isOver)
        {
            if (!isMarked)
            {
                if (bombas == 0 && !isBomb && !isOpen)
                {
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_0")];
                    isOpen = true;
                    ShowZeros();
                }
                else if (isBomb)
                {
                    show();
                    GameObject.Find("GameControl").GetComponent<GameControl>().isOver = true;


                }
                else
                {
                    show();
                    isOpen = true;
                }
            }
            



        }

    }

    public void show()
    {
        if (isBomb)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = textures[Array.IndexOf(names, "bomb_0")];
        }
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            switch (bombas)
            {
                case 0:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_0")];
                    break;

                case 1:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_1")];
                    break;
                case 2:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_2")];
                    break;
                case 3:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_3")];
                    break;
                case 4:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_4")];
                    break;
                case 5:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_5")];
                    break;
                case 6:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_6")];
                    break;
                case 7:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_7")];
                    break;
                case 8:
                    spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_8")];
                    break;
                default:
                    break;
            }


        }
    }

    public void showSurround()
    {
        isOpen = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (bombas)
        {
            case 0:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_0")];
                break;

            case 1:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_1")];
                break;
            case 2:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_2")];
                break;
            case 3:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_3")];
                break;
            case 4:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_4")];
                break;
            case 5:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_5")];
                break;
            case 6:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_6")];
                break;
            case 7:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_7")];
                break;
            case 8:
                spriteRenderer.sprite = textures[Array.IndexOf(names, "numeros_8")];
                break;
            default:
                break;
        }
    }

    public void ShowZeros()
    {
        List<GameObject> objects = new List<GameObject>();
        List<GameObject> tentados = new List<GameObject>();
        objects.Add(gameObject);
        while (objects.Count > 0)
        {
            List<GameObject> g = new List<GameObject>();
            foreach (var item in objects)
            {
                
                
                var a = surrounds(item);
                foreach (var i in a)
                {
                    if (i.GetComponent<RandomSprite>().bombas == 0)
                    {
                        if (!tentados.Contains(i))
                        {
                            g.Add(i);
                            tentados.Add(i);
                            var c = i.GetComponent<RandomSprite>();
                            if (c.isMarked)
                            {
                                c.show();
                                GameObject.Find("GameControl").GetComponent<GameControl>().marcadores++;
                                isMarked = false;
                            }
                           
                            //Debug.Log(i);

                        }
                        

                    }
                    i.GetComponent<RandomSprite>().showSurround();

                }
                
                //objects.AddRange(a);
            }
            objects.Clear();
            
            //Debug.Log(g.Count);
            objects.AddRange(g);
           
        }

    }

    public List<GameObject> surrounds(GameObject game)
    {
        GameObject[] gameObjects = GameObject.Find("GameControl").GetComponent<GameControl>().num.ToArray();
        List<GameObject> surround = new List<GameObject>();
        var c = game.transform.position;
        foreach (var item in gameObjects)
        {
            var d = item.transform.position;
            if (c.x == d.x && (d.y == (c.y + 13) || d.y == (c.y - 13)))
            {
                surround.Add(item);
            }
            if (c.y == d.y && (d.x == (c.x + 13) || d.x == (c.x - 13)))
            {
                surround.Add(item);
            }
            if (d.x == (c.x - 13) && d.y == (c.y - 13))
            {
                surround.Add(item);
            }
            if (d.x == (c.x - 13) && d.y == (c.y + 13))
            {
                surround.Add(item);
            }
            if (d.x == (c.x + 13) && d.y == (c.y - 13))
            {
                surround.Add(item);
            }
            if (d.x == (c.x + 13) && d.y == (c.y + 13))
            {
                surround.Add(item);
            }

        }
      
        return surround;
    }

    public void SetBomb()
    {
        isBomb = true;
    }
    public bool isBomba()
    {
        return isBomb;
    }
    public void setNumberBomb(int bombs)
    {
        bombas = bombs;
    }
}
