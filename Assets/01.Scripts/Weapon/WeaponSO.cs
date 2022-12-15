using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SwordSkillType : int
{
	None,
	//StraightSword,
	GreatSword,
	END
}
[CreateAssetMenu(menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
	public WeaponStat _weaponStat;
	public SwordSkillType swordType;
}