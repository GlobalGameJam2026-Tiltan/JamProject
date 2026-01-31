using UnityEngine;
using System.Collections.Generic;

public class EnemyAnimationLibrary : MonoBehaviour
{
    public BodyAnimationSet[] bodyAnimations;
    public MasqueAnimationSet[] maskAnimations;

    private Dictionary<BodyType, BodyAnimationSet> _bodyLookup;
    private Dictionary<MasqueType, MasqueAnimationSet> _masqueLookup;

    private void Awake()
    {
        _bodyLookup = new Dictionary<BodyType, BodyAnimationSet>();
        foreach (var set in bodyAnimations)
            _bodyLookup.Add(set.bodyType, set);

        _masqueLookup = new Dictionary<MasqueType, MasqueAnimationSet>();
        foreach (var set in maskAnimations)
            _masqueLookup.Add(set.masqueType, set);
    }

    public BodyAnimationSet GetBody(BodyType type) => _bodyLookup[type];
    public MasqueAnimationSet GetMask(MasqueType type) => _masqueLookup[type];
}