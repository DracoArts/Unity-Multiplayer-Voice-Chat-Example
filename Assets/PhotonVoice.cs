using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;
public class PhotonVoice : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public Button connectButton;
    public Button voiceToggleButton;
    public Text connectionStatusText;
    public Text roomStatusText;
 
     public  Recorder voiceRecorder;
    public Speaker speaker;
    private void Awake()
    {
        // voiceRecorder = GetComponent<Recorder>();
        // speaker = GetComponent<Speaker>();

        connectButton.onClick.AddListener(ToggleConnection);
        voiceToggleButton.onClick.AddListener(ToggleVoice);
        voiceToggleButton.interactable = false;
        UpdateStatusUI();
    
      voiceRecorder.TransmitEnabled = false;
    }
    
    private void ToggleConnection()
    {
        if (PhotonNetwork.IsConnected)
        {
            Disconnect();
        }
        else
        {
            Connect();
        }
    }
    
    private void ToggleVoice()
    {
        voiceRecorder.TransmitEnabled = !voiceRecorder.TransmitEnabled;
        UpdateStatusUI();
    }
    
    private void Connect()
    {
        connectionStatusText.text = "Connecting...";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    private void Disconnect()
    {
        
        PhotonNetwork.Disconnect();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinOrCreateRoom("VoiceDemoRoom", new Photon.Realtime.RoomOptions { MaxPlayers = 4 }, null);
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
        voiceToggleButton.interactable = true;
        
        // Instantiate player character
    
      
        
        UpdateStatusUI();
    }
    
    public override void OnLeftRoom()
    {
     
        UpdateStatusUI();
    }
    
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log($"Disconnected: {cause}");
        voiceToggleButton.interactable = false;
        voiceRecorder.TransmitEnabled = false;
        UpdateStatusUI();
    }
    
    
    private void UpdateStatusUI()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectionStatusText.text = "Connected";
            connectButton.GetComponentInChildren<Text>().text = "Disconnect";
            
            if (PhotonNetwork.InRoom)
            {
                roomStatusText.text = $"In Room: {PhotonNetwork.CurrentRoom.Name}\nPlayers: {PhotonNetwork.CurrentRoom.PlayerCount}";
            }
            else
            {
                roomStatusText.text = "Not in a room";
            }
        }
        else
        {
            connectionStatusText.text = "Disconnected";
            roomStatusText.text = "";
            connectButton.GetComponentInChildren<Text>().text = "Connect";
        }
        
        voiceToggleButton.GetComponentInChildren<Text>().text = voiceRecorder.TransmitEnabled ? "Mute" : "Unmute";
    }
    
    private void OnDestroy()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }
}