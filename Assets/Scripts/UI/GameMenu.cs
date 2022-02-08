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

    public void OpenGameMenu()
    {
        menuCanvas.enabled = true;
    }

    public void CloseGameMenu()
    {
        menuCanvas.enabled = false;
    }

    #region Button Events
    public void CloseMenuButton_Click()
    {
        CloseGameMenu();
    }

    public void ExitGameButton_Click()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        NetworkManager.instance.LeaveRoom();
        Application.Quit();
    }

    public void LeaveGameButton_Click()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        NetworkManager.instance.LeaveRoom();
        NetworkManager.instance.ChangeScene("Main Scene");
    }
    #endregion
}
