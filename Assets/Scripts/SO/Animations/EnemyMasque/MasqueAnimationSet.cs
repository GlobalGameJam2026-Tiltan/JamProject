using UnityEngine;

[CreateAssetMenu(fileName = "MasqueAnimationSet", menuName = "Scriptable Objects/MasqueAnimationSet")]
public class MasqueAnimationSet : ScriptableObject
{
    public MasqueType masqueType;

    public Sprite[] grabFrames;
    public Sprite[] punchFrames;
}