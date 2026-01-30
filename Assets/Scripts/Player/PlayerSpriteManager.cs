using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    private Sprite _masqueSprite; 
    private Sprite _playerSprite;

    private void Awake()
    {
        playerData.SetActiveMasque(playerData.masques[0].type);
        _masqueSprite = playerData.GetActiveMasque().sprite;
        _playerSprite = playerData.backSprite;
    }
}