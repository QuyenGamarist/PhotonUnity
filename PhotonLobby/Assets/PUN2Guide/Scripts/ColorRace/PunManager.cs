using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunManager : MonoBehaviour
{
    public List<int> SetIntRandom;
    public List<int> RandomGround;
    PhotonView photonView;

    // Start is called before the first frame update
  
    void Start()
    {
        photonView = PhotonView.Get(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetrandomInt()
    {
        SetIntRandom.Clear();
        SetIntRandom.Add(0);
        SetIntRandom.Add(1);
        SetIntRandom.Add(2);
        SetIntRandom.Add(3);
        
    }

    [PunRPC]
    public List<int> SetColorGround()
    {
        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, SetIntRandom.Count);         
            RandomGround.Add(SetIntRandom[j]);
            SetIntRandom.RemoveAt(j);
        }
        return RandomGround;

    }
    public void CallRandomInt()
    {
        photonView.RPC("SetColorGround", RpcTarget.All, null);
    }
}
