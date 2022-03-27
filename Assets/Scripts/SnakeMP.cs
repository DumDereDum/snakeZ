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
        base.Start();
        myPV = GetComponent<PhotonView>();
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
        GameObject segment = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "SnakeSegmentMP"), this.transform.position, this.transform.rotation);
        base.GrowBase(segment);
    }
}
