using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour {

	public int DamageDealing(int characterHp, int minDamage, int maxDamage)
    {
        characterHp -= Random.Range(minDamage, maxDamage);
        return characterHp;
    }
}
