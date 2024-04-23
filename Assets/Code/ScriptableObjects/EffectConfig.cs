using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Config", menuName = "ScriptableObject/Config/EffectConfig")]
public class EffectConfig : ScriptableObject
{
    [SerializeField] private EffectData _burningData;

    public EffectData BurningData { get => _burningData; }
}
