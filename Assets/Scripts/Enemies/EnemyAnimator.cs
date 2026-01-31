using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer maskRenderer;

    private BodyAnimationSet _bodySet;
    private MasqueAnimationSet _masqueSet;

    public void Initialize(
        EnemyData data,
        EnemyAnimationLibrary library)
    {
        _bodySet = library.GetBody(data.bodyType);
        _masqueSet = library.GetMask(data.type);
    }

    public void PlayGrab(int frame)
    {
        bodyRenderer.sprite = _bodySet.grabFrames[frame];
        maskRenderer.sprite = _masqueSet.grabFrames[frame];
    }

    public void PlayPunch(int frame)
    {
        bodyRenderer.sprite = _bodySet.punchFrames[frame];
        maskRenderer.sprite = _masqueSet.punchFrames[frame];
    }
}