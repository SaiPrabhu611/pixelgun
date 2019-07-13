using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject FPSCamera;

    [SerializeField]
    TextMeshProUGUI playerNametext;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            transform.GetComponent<MovementController>().enabled = true;
            FPSCamera.GetComponent<Camera>().enabled = true;
        }
        else
        {
            transform.GetComponent<MovementController>().enabled = false;
            FPSCamera.GetComponent<Camera>().enabled = false; 
        }

        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if(playerNametext!=null)
        {
            playerNametext.text = photonView.Owner.NickName;

        }
    }
}
