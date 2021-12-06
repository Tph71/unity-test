using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highpointFinder : MonoBehaviour
{
    private GameObject Cam;
    private GameObject blockRecycler;
    private EdgeCollider2D BRcollider;
    private GameObject blockSpawner;
    private RaycastHit2D hit;
    private RaycastHit2D noHit;
    private RaycastHit2D Spawnpoint;
    public Vector3 rayPos;
    public float startHeight;
    //public float lastHeight;
    public float spawnerHeight = 15f;
    public string detectedBlock;

    public float Height;

    void Start()
    {
        Cam = Camera.main.gameObject;
        blockRecycler = GameObject.Find("blockRecycler");
        BRcollider = blockRecycler.GetComponent<EdgeCollider2D>();
        blockSpawner = GameObject.Find("blockSpawner");
        startHeight = gameObject.transform.position.y;
        //lastHeight = startHeight;
        blockSpawner.transform.position = new Vector3(0f, startHeight + spawnerHeight, 0f);
    }
    void Update()
    {
        rayPos = gameObject.transform.position;
        hit = Physics2D.Raycast(new Vector2(rayPos.x, rayPos.y), Vector2.right, 20, LayerMask.GetMask("Blocks"));
        noHit = Physics2D.Raycast(new Vector2(rayPos.x, rayPos.y + 0.2f), Vector2.right, 20, LayerMask.GetMask("Blocks"));

        Debug.DrawRay(new Vector2(rayPos.x, rayPos.y), Vector3.right * 20, Color.green);
        if (noHit.collider)
        {
            //lastHeight = rayPos.y;
            rayPos.y += 0.1f;
            gameObject.transform.position = rayPos;
            blockSpawner.transform.position = new Vector3(0f, rayPos.y + spawnerHeight, 0f);

            /*if(hit.collider)
            {
                detectedBlock = hit.transform.name;
                Debug.Log("highpoint found " + detectedBlock);
            }*/
        }
        else if (!hit.collider && !noHit.collider)
        {
            if (rayPos.y > startHeight)
            {
                rayPos.y -= 0.1f;
                gameObject.transform.position = rayPos;
                blockSpawner.transform.position = new Vector3(0f, rayPos.y + spawnerHeight, 0f);
            }
        }

        Height = blockSpawner.transform.position.y;
        //Debug.Log(Height);
    }

    void LateUpdate()
    {
        float recycler_y = blockRecycler.transform.position.y + BRcollider.points[0].y;
        if (rayPos.y + spawnerHeight + 25 >= recycler_y)
        {
            Vector2[] newPoints = BRcollider.points;
            newPoints[0].y += 25;
            newPoints[1].y += 25;
            newPoints[4].y += 25;
            newPoints[5].y += 25;
            BRcollider.points = newPoints;
            //Debug.Log("Recycler border updated.");
        }
    }
}
