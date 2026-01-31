using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite[] bodyTypes;
    private BodyType _bodyType;
    private SpriteRenderer _spriteRenderer;
    private EnemyAnimator _enemyAnimator;

    public void RandomizeBodyType()
    {
        _bodyType = (BodyType)Random.Range(0, (int)BodyType.Mayhem);
        data.sprite = bodyTypes[(int)_bodyType];
    }
    
    public bool IsAlive => data.health > 0;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAnimator.InitializeEnemy(data,EnemyAnimationLibrary.Instance);
    }

    void Start()
    {
        if (EncounterManager.Instance.IsMiniBossBattle() && EncounterManager.Instance.IsMidBattle() && data.idleLines.Length > 0)
        {
            StartCoroutine(PlayIdleAudio());
        }
    }

    private IEnumerator PlayIdleAudio()
    {
        while (EncounterManager.Instance.IsMiniBossBattle() && EncounterManager.Instance.IsMidBattle())
        {
            yield return new WaitForSeconds(5f);
            int randomIndex = Random.Range(0, data.idleLines.Length);
            audioSource.PlayOneShot(data.idleLines[randomIndex]);
        }
        
    }
    
    public virtual AttackOption Attack()
    {
        var rnd = Random.Range(0, 3);
        var attack = data.attacks[rnd];
        audioSource.PlayOneShot(attack.attackVoiceLine);

        var isGrab = Convert.ToBoolean(Random.Range(0, 2));

        StartCoroutine(isGrab
            ? PlayAnimation(EnemyAnimationLibrary.Instance.GetBody(_bodyType).grabFrames, true)
            : PlayAnimation(EnemyAnimationLibrary.Instance.GetBody(_bodyType).punchFrames, false));

        return attack;
    }
    
    public MasqueType GetMasqueType() => data.type;
    public EnemyData GetData() => data;

    public virtual bool TakeDamage(float damage)
    {
        data.health -= damage;
        if (data.health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public virtual void Die()
    {
        // TODO: add death logic (probably animation)
    }
    
    private IEnumerator PlayAnimation(Sprite[] frames, bool isGrab)
    {
        var frameDelay = 0.1f; // tweak this

        for (var i = 0; i < frames.Length; i++)
        {
            if (isGrab)
                _enemyAnimator.PlayGrab(i);
            else
                _enemyAnimator.PlayPunch(i);

            yield return new WaitForSeconds(frameDelay);
        }
    }
}
