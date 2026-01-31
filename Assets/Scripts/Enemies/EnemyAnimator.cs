using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer masqueRenderer;

    private BodyAnimationSet _bodySet;
    private MasqueAnimationSet _masqueSet;
    
    private Sprite[] _bossPunch;
    private Sprite[] _bossGrab;
    
    private bool _hasMasque;
    private bool _isBoss;

    // ---------- ENEMY ----------
    public void InitializeEnemy(
        EnemyData data,
        EnemyAnimationLibrary library)
    {
        _bodySet = library.GetBody(data.bodyType);

        _hasMasque = masqueRenderer != null;

        if (_hasMasque)
        {
            _masqueSet = library.GetMask(data.type);
        }
    }
    
    // ---------- BOSS ----------
    public void InitializeBoss(BossData data)
    {
        _isBoss = true;
        _hasMasque = false;

        _bossPunch = data.punchAnimation;
        _bossGrab = data.grabAnimation;
    }

    // ---------- PLAY ----------
    public void PlayGrab(int frame)
    {
        if (_isBoss)
        {
            bodyRenderer.sprite = _bossGrab[frame];
        }
        else
        {
            bodyRenderer.sprite = _bodySet.grabFrames[frame];

            if (_hasMasque)
                masqueRenderer.sprite = _masqueSet.grabFrames[frame];
        }
    }

    public void PlayPunch(int frame)
    {
        if (_isBoss)
        {
            bodyRenderer.sprite = _bossPunch[frame];
        }
        else
        {
            bodyRenderer.sprite = _bodySet.punchFrames[frame];

            if (_hasMasque)
                masqueRenderer.sprite = _masqueSet.punchFrames[frame];
        }
    }
}