using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
public class SkillsConfig : ScriptableObject
{
    [SerializeField] private ParticleSystem _flamethrower;

    public ParticleSystem Flamethrower { get => _flamethrower; }
}
