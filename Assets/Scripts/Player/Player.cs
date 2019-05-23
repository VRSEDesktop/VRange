using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saves data relevant to the player.
/// </summary>
public class Player : MonoBehaviour, IHitable
{
    public float playerHeight = 1.8f;
    public static Player Instance { get; private set; }

    public GunController leftHand, rightHand;
    private Gun leftGun, rightGun;

    public Hitbox hitbox;

	public void Start()
	{
		leftGun = leftHand.gun;
		rightGun = rightHand.gun;
	}

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if(leftHand != null)  leftHand.GetComponent<HandController>().HandleInput();
        if(rightHand != null) rightHand.GetComponent<HandController>().HandleInput();       
    }

    public HitType OnHit(Gun gun, RaycastHit raycastHit)
    {
        Debug.Log("PLAYER WAS KILLED BY ENEMY");
        return HitType.UNWANTED;
    }

    public void OnGUI()
    {
        IList<LoggedHit> hits = ScenarioLogs.GetHits();

        for (int i = 0; i < hits.Count; i++)
        {
            GUI.Label(new Rect(Screen.width / 12, Screen.height / 24 * i, Screen.width / 4 * 2, Screen.height / 6), hits[i].part.ToString());
        }
    }
}