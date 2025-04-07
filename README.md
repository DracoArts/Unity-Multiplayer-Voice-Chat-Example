
# Welcome to DracoArts

![Logo](https://dracoarts-logo.s3.eu-north-1.amazonaws.com/DracoArts.png)




#  Unity Multiplayer Voice Chat Example
 Photon Voice Chat 2 is a high-performance, real-time voice communication solution designed for multiplayer games and applications. Integrated with the Photon Engine, it enables seamless voice chat between players with low latency, high-quality audio, and efficient bandwidth usage. It supports cross-platform communication, making it ideal for multiplayer games on PC, consoles, and mobile device

 Photon Voice 2 is a real-time voice chat SDK designed for multiplayer games and applications. It enables players to communicate with low latency, high-quality audio, and cross-platform support.

 # Key Features:
## 1 Real-Time Voice Communication

- Enables instant voice chat between players in the same room or session.

- Supports both peer-to-peer and server-based voice transmission.

## 2 High-Quality Audio

- Adjustable audio codecs (Opus by default) for optimal sound quality.

- Configurable bitrate, sampling rate, and frame size for balancing quality and bandwidth.

## 3 Low Latency & Efficient Bandwidth Usage

- Optimized network protocols ensure minimal delay in voice transmission.

- Dynamic bandwidth adjustment to reduce lag and packet loss.

## 4 Cross-Platform Support

- Works across multiple platforms, including Windows, macOS, Android, iOS, and consoles.

- Compatible with Unity, Unreal Engine, and custom game engines.

## 5 Voice Activity Detection (VAD) & Noise Suppression

- Automatically detects when a player is speaking to reduce unnecessary transmissions.

- Optional noise suppression for clearer voice quality.

## 6 Echo Cancellation & Audio Effects

- Built-in echo cancellation to prevent feedback loops.

- Optional voice effects (pitch shifting, 3D spatial audio, etc.).

Secure & Encrypted Communication

Optional end-to-end encryption for secure voice chats.

## 7 Scalable & Cloud-Ready

- Works with Photon Cloud and Photon Server for large-scale multiplayer games.

- Supports load balancing and region-based matchmaking.

## Easy Integration

- Simple API for quick implementation.

- Works seamlessly with Photon Unity Networking (PUN) and Fusion.

## 10 Customizable & Extensible

- Adjust voice transmission settings per user (priority, volume, mute/unmute).

- Extend functionality with custom audio filters and processing.
# How It Works:
- Players join a Photon Room.

- Voice streams are transmitted peer-to-peer or through relays.

- Audio is encoded/decoded in real-time with Opus.

- Optional features like mute, volume control, and spatial effects can be applied.

## Use Cases:
🎮 Multiplayer Games (FPS, MMO, Battle Royale)

💬 Social Apps & Virtual Worlds (VR Chat, Metaverse)

📢 Team Coordination (Esports, Co-op Games)

## How to Set Up Photon Voice 2 (Conceptual Steps)
- Install Photon Voice 2 SDK

- Initialize Voice Connection in your game’s networking logic.

- Join a Voice Room alongside the game session.

- Configure Audio Settings (codec, bitrate, VAD).

- Handle Player Mute/Volume (optional features).

[Photon Voice 2 SDK](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518?srsltid=AfmBOooMmUUljs035iWPATeBhARgAvopIXkvq8x5WJZ4TotiTPJIqhDp) 

[Photon Account](https://dashboard.photonengine.com/)

# Installation & Setup
- Import Photon Voice 2

- Configure Photon App Settings

- Navigate to PhotonVoice → PhotonServerSettings.

- Paste your Photon App ID (from the Photon Dashboard) into the Voice App ID field.

- Add Voice to Player Prefab

- Attach the PhotonVoiceView component to your player prefab (alongside PhotonView).

- Add an AudioSource for local playback (optional for advanced setups).
# Basic Workflow
## Initialize Voice

- Call PhotonVoiceNetwork.Connect() after PUN/Fusion connects (e.g., in OnJoinedRoom).

## Enable Microphone

- The Recorder component auto-starts by default. Configure mic settings via:

- Transmit Enabled: Toggle voice activity.

- Voice Detection (VAD): Auto-mutes when silent.

- Codec Settings: Adjust bitrate/sampling (Opus recommended).

## 3D Spatial Audio (Optional)

- Enable Recorder.LocalVoice.SetSpatializerOptions() for positional voice.

- Mute/Volume Control

Use photonView.RPC to send mute commands or adjust Speaker.Volume.
## Usage/Examples

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
## Image


![](https://github.com/AzharKhemta/DemoClient/blob/main/PhotonVoice%20Chat.gif?raw=true)


## Authors

- [@MirHamzaHasan](https://github.com/MirHamzaHasan)
- [@WebSite](https://mirhamzahasan.com)


## 🔗 Links

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/company/mir-hamza-hasan/posts/?feedView=all/)
## Documentation

[Phtoton Voice 2](https://doc.photonengine.com/voice/current/getting-started/voice-for-pun)


## Tech Stack
**Client:** Unity,C#

**Plugin:** Photon Voice 2



