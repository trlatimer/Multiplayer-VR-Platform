using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button CloseMenuButton;
    public Button ExitGameButton;
    public Button LeaveGameButton;

    public Canvas menuCanvas;
    public NetworkManager networkManager;

    public void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    #region Button Events
    public void CloseMenuButton_Click()
    {
        menuCanvas.enabled = false;
    }

    public void ExitGameButton_Click()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        networkManager.LeaveRoom();
        Application.Quit();
    }

    public void LeaveGameButton_Click()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        networkManager.LeaveRoom();
        networkManager.ChangeScene("Main Scene");
    }
    #endregion
}
