using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SwordType : int
{
	StraightSword,
	GreatSword,
	END
}
[CreateAssetMenu(menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
	public WeaponStat _weaponStat;
	public SkillSO skillStat;
}