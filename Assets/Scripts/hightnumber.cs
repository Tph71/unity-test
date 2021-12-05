using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class hightnumber : MonoBehaviour
{
    private GameObject hightpoint;

    public Text hightText;

    public float hight;

    // Start is called before the first frame update
    void Start()
    {
        hightpoint = GameObject.Find("blockSpawner");
        hight = hightpoint.transform.position.y;
        hightText.text = hight.ToString("#");

    }

    // Update is called once per frame
    void Update()
    {
        hight = hightpoint.transform.position.y;
        hightText.text = hight.ToString("#");
        //Debug.Log(hight);
    }
}
