using UnityEngine;

public class EncounterStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        EncounterManager.Instance.FindPlayer();
        EncounterManager.Instance.StartEncounter();
    }
}
