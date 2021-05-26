using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRGround : MonoBehaviourPun

{
    public List<int> RandomColor;
    // Start is called before the first frame update
    void Start()
    {
        CRController.Instance.PunManager.GetComponent<PunManager>().SetrandomInt();
        //CRController.Instance.PunManager.GetComponent<PunManager>().CallRandomInt();
        RandomColor = CRController.Instance.PunManager.GetComponent<PunManager>().SetColorGround();
        FillColor();
    }



    // Update is called once per frame

    private void FillColor ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			CRController.Instance.SetColor(gameObject.transform.GetChild(i).gameObject, CRController.Instance.ColorGround[RandomColor[i]]);
		}
	}

   
}
