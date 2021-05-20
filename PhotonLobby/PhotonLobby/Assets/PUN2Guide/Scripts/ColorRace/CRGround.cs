using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRGround : MonoBehaviourPun, IPunObservable

{

    // Start is called before the first frame update
    void Start()
    {
		FillColor();
        
    }

    // Update is called once per frame

	private void FillColor ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			int j = Random.Range(0, CRController.Instance.ColorGround.Count);
			CRController.Instance.SetColor(gameObject.transform.GetChild(i).gameObject, CRController.Instance.ColorGround[j]);
			CRController.Instance.ColorGround.RemoveAt(j);
		}
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
