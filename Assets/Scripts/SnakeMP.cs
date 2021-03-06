using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SnakeMP : Snake
{
    PhotonView myPV;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MPGameController>();
        base.Start();
        myPV = GetComponent<PhotonView>();

        if (enemy)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine)
        {
            Control();
        }
    }

    private void FixedUpdate()
    {
        if (!myPV.IsMine)
        {
            return;
        }

        base.FixedUpdate();
    }

    protected override void Grow()
    {
        Debug.Log("Grow mp");
        GameObject segment = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "SnakeSegmentMP"), this.transform.position, this.transform.rotation);
        segment.SetActive(false);
        base.GrowBase(segment);
    }

    protected override void ResetGame()
    {
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            PhotonNetwork.Destroy(snakeSegments[i].gameObject);
        }

        base.ResetGameBase();
    }    
}
