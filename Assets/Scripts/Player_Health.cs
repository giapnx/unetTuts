using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Health : NetworkBehaviour {

	[SyncVar (hook = "OnHealthChanged")] private int health = 100;
	private Text healthText;

	public override void OnStartLocalPlayer ()
	{
		healthText = GameObject.Find ("HealthText").GetComponent <Text>();
		SetHealthText ();
	}

	void SetHealthText()
	{
		if(isLocalPlayer)
		{
			healthText.text = "Health: " + health.ToString ();
		}
	}

	public void DeductHealth(int dmg)
	{
		health -= dmg;
	}

	void OnHealthChanged(int hlth)
	{
		health = hlth;
		SetHealthText ();
	}
}
