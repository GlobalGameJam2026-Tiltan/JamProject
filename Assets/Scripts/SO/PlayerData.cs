using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName = "Player";
    public MaskData[] masks = new MaskData[3];
    public Dictionary<string, int> PlayerStats = new Dictionary<string, int>()
    {
        { "Strength", 1 },
        { "Intelligence", 1 },
        { "Charisma", 1 }
    };

    private MaskData _activeMask;

    public void ChangeName(string newName)
    {
        playerName = newName;
    }

    public void SetMask(MaskData newMask)
    {
        if (newMask.durability <= 0) return;
        switch (newMask.type)
        {
            case MaskType.Strength:
                masks[0] = newMask;
                break;
            case MaskType.Intelligence:
                masks[1] = newMask;
                break;
            case MaskType.Charisma:
                masks[2] = newMask;
                break;
            default:
                Debug.LogError("Invalid mask type");
                break;
        }
    }

    public void SetActiveMask(MaskData mask)
    {
        _activeMask = mask;
    }

    public MaskData GetActiveMask()
    {
        return _activeMask;
    }
    
    public int GetPlayerDifficulty()
    {
        return PlayerStats["Strength"] + PlayerStats["Intelligence"] + PlayerStats["Charisma"];
    }
}