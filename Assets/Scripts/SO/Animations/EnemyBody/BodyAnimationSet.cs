using UnityEngine;

[CreateAssetMenu(fileName = "BodyAnimationSet", menuName = "Scriptable Objects/BodyAnimationSet")]
public class BodyAnimationSet : ScriptableObject
{
    public BodyType bodyType;

    public Sprite[] grabFrames;
    public Sprite[] punchFrames; 
}