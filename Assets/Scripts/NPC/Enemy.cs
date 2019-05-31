using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{
    public Hitbox[] hitboxes;
    /// <summary>
    /// Linked objects from NPC hand
    /// </summary>
    public GameObject Gun, Phone, BaseballBat, Axe;
    public bool isAgressive;
	public bool hitCorrectPart;

    private Animator animator;
    public bool IsDead { private set; get;}
    private NavMeshAgent navMeshAgent;
	private int _health = 100;

	private string bellyHit = "BellyHit";
	private string LeftLeg = "Left Leg Hit";
	private string LeftShoulder = "ShoulderLeftHit";
	private string RightLeg = "Right Leg Hit";
	private string RightShoulder = "ShoulderRightHit";

	private int health {
		get { return _health; }
		set
		{
			if (value != _health)
			{
				_health = value;
				if(_health <= 0)
				{
					Die();
				}
			}
		}
	}
	/// <summary>
	/// Max number of hits in torso the enemy can withstand
	/// </summary>
	public int maxHits;

	[System.Obsolete]
	public void Start()
    {
        animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
    }

	public void OnEnable()
	{
		GetComponent<Transition>().Enable();
	}

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        HitboxType partHit = GetHitboxTypeFromHit(raycastHit);
		hitCorrectPart = false;

		//raycastHit.transform.position()
		ScenarioLogs.logs.Add(new LoggedHit(this, partHit, gun, raycastHit));

		switch (partHit) // add sth related to the part hit if we will need it
		{
			case HitboxType.HumanHead:
                if (!IsDead) Die();
            break;
            case HitboxType.HumanNeck: if (!IsDead) Die(); hitCorrectPart = false; break;

			case HitboxType.HumanPelvis: Torso(); hitCorrectPart = true;  break;
			case HitboxType.HumanSpine1: Torso(); hitCorrectPart = true; break;
            case HitboxType.HumanSpine2: Torso(); hitCorrectPart = true; break;
            case HitboxType.HumanSpine3: Torso(); hitCorrectPart = true; break;

            case HitboxType.HumanThighLeft: LeftLegHit(); hitCorrectPart = false; break;
			case HitboxType.HumanCalfLeft: LeftLegHit(); hitCorrectPart = false; break;

			case HitboxType.HumanThighRight: RightLegHit(); hitCorrectPart = false; break;
			case HitboxType.HumanCalfRight: RightLegHit(); hitCorrectPart = false; break;
				
			case HitboxType.HumanFootLeft: health -= Random.Range(1, 20); hitCorrectPart = false; break;
            case HitboxType.HumanFootRight: health -= Random.Range(1, 20); hitCorrectPart = false; break;

            case HitboxType.HumanLowerArmLeft: LeftShoulderHit(); hitCorrectPart = false; break;
			case HitboxType.HumanHandLeft: LeftShoulderHit(); hitCorrectPart = false; break;
			case HitboxType.HumanUpperArmLeft: LeftShoulderHit(); hitCorrectPart = false; break;

			case HitboxType.HumanLowerArmRight: RightShoulderHit(); hitCorrectPart = false; break;
			case HitboxType.HumanHandRight: RightShoulderHit(); hitCorrectPart = false; break;
			case HitboxType.HumanUpperArmRight: RightShoulderHit(); hitCorrectPart = false; break;

		}
		if(isAgressive == false || hitCorrectPart == false)
		{
			return HitType.UNWANTED;
		}
		else
		{
			return HitType.RIGHT;
		}
    }

	public void Torso()
	{
		animator.Play(bellyHit);
		health -= Random.Range(40, 140);
	}

	public void LeftLegHit()
	{
		animator.Play(LeftLeg);
		health -= Random.Range(20, 50);
	}

	public void LeftShoulderHit()
	{
		animator.Play(LeftShoulder);
		health -= Random.Range(20, 50);
	}

	public void RightLegHit()
	{
		animator.Play(RightLeg);
		health -= Random.Range(20, 50);
	}

	private void RightShoulderHit()
	{
		animator.Play(RightShoulder);
		health -= Random.Range(20, 50);
	}

	/// <summary>
	/// Get HitboxType which was hit with the bulletHit
	/// </summary>
	/// <param name="bulletHit"></param>
	/// <returns></returns>
	private HitboxType GetHitboxTypeFromHit(RaycastHit raycastHit)
    {
        foreach (Hitbox hitbox in hitboxes)
        {
            if (hitbox.mesh == raycastHit.collider) return hitbox.type;
        }

        return HitboxType.None;
    }

    private void Die()
    {
        IsDead = true;
        animator.enabled = false;
        if (navMeshAgent) navMeshAgent.speed = 0f;
		StartCoroutine(Dissolve(2.5f));
    }

	/// <summary>
	/// Dissolves into the void.
	/// </summary>
	/// <param name="delay"></param>
	/// <returns></returns>
	private IEnumerator Dissolve(float delay)
	{
		yield return new WaitForSeconds(delay);
		GetComponent<Transition>().Disable();
		Destroy(gameObject, 5f);
	}
}