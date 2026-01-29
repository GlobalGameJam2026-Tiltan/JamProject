using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName = "Player";
    public MasqueData[] masques = new MasqueData[3];
    public Dictionary<string, int> PlayerStats = new Dictionary<string, int>()
    {
        { "Strength", 1 },
        { "Intelligence", 1 },
        { "Charisma", 1 }
    };

    private MasqueData _activeMasque;

    public void ChangeName(string newName)
    {
        playerName = newName;
    }

    public void SetMasque(MasqueData newMasque)
    {
        if (newMasque.durability <= 0) return;
        switch (newMasque.type)
        {
            case MasqueType.Strength:
                masques[0] = newMasque;
                break;
            case MasqueType.Intelligence:
                masques[1] = newMasque;
                break;
            case MasqueType.Charisma:
                masques[2] = newMasque;
                break;
            default:
                Debug.LogError("Invalid masque type");
                break;
        }
    }

    public void SetActiveMasque(MasqueData masque)
    {
        _activeMasque = masque;
    }

    public MasqueData GetActiveMasque()
    {
        return _activeMasque;
    }
    
    public int GetPlayerDifficulty()
    {
        return PlayerStats["Strength"] + PlayerStats["Intelligence"] + PlayerStats["Charisma"];
    }
}