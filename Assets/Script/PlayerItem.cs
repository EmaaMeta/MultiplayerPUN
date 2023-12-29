using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using JetBrains.Annotations;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    //[Header("Current Player")]
    Image backgroundImage;
    public Color highlitColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;


    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }
    public void SetPlayerInfo(Player _Player)
    {
        playerName.text = _Player.NickName;
        player = _Player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        if (backgroundImage != null)
        {
            // Forcefully set the color to highlitColor
            backgroundImage.color = highlitColor;

            leftArrowButton.SetActive(true);
            rightArrowButton.SetActive(true);
        }
        else
        {
            Debug.LogError("backgroundImage is null!");
        }

     
    }
    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length -1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else 
        {
            playerProperties["playerAvatar"] = 0;
        }
    }
}
