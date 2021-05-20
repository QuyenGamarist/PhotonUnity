using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CRPlayerController : MonoBehaviourPun, IPunObservable
{
	Vector3 latestPos;
	Quaternion latestRot;
	public MonoBehaviour[] localScripts;
	public GameObject[] localObjects;

	void Start()
    {
		RandomPlayerColor();
		transform.SetParent(Camera.main.transform);

		if (photonView.IsMine)
		{
			
		}
		else
		{
			for (int i = 0; i < localScripts.Length; i++)
			{
				localScripts[i].enabled = false;
			}
			for (int i = 0; i < localObjects.Length; i++)
			{
				localObjects[i].SetActive(false);
			}
		}
	}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            GetComponent<Rigidbody>().velocity = new Vector2(0, -1);
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 1.5f) * 250.0f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45.0f));       
        }

		if (!photonView.IsMine)
		{
			//Update remote player (smooth this, this looks good, at the cost of some accuracy)
			transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
		}

	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//Network player, receive data
			latestPos = (Vector3)stream.ReceiveNext();
			latestRot = (Quaternion)stream.ReceiveNext();
		}
	}

	void OnTriggerEnter(Collider other)
	{

		Color color = transform.GetComponent<Renderer>().material.GetColor("_EmissionColor");
		Color color2 = other.GetComponent<Renderer>().material.GetColor("_EmissionColor");

		if (color != color2){

			transform.GetComponent<Collider>().isTrigger = false;
			CRController.Instance.GameOver();
		    
		}
		if (color == color2)
		{
			CRController.Instance.Score++;
			CRController.Instance.AddColor(CRController.Instance.ColorPlayer);
			RandomPlayerColor();
		}	
	}
    void RandomPlayerColor()
    {
		Color myColor = CRController.Instance.ColorPlayer[Random.Range(0, CRController.Instance.ColorPlayer.Count)];
		CRController.Instance.SetColor(gameObject, myColor);
	}

}
