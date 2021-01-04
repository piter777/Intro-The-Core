using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder  {
	// Some skript that hold data betven scenes.
	private static int weaponType,score,playerHealth;
	private static float playerDamageBuff,playerAtackSpeedBuff;
	private static GameObject player;


	public static int WeaponType{

		get{return weaponType; }
		set{weaponType = value; }

	}

	public static int Score{

		get{return score; }
		set{score = value; }

	}
	public static int PlayerHealth{

		get{return playerHealth; }
		set{playerHealth = value; }

	}


	public static float PlayerDamageBuff
	{

		get{return playerDamageBuff; }
		set{playerDamageBuff = value; }

	}

	public static float PlayerAtackSpeedBuff
	{

		get{return playerAtackSpeedBuff; }
		set{playerAtackSpeedBuff = value; }

	}
	public static GameObject Player
	{

		get{return player; }
		set{player = value; }

	}

	

}
