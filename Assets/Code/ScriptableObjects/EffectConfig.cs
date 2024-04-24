using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Config", menuName = "ScriptableObject/Config/EffectConfig")]
public class EffectConfig : ScriptableObject
{
    [SerializeField] private EffectData _burningData;
    [SerializeField] private EffectData _soggyData;
    [SerializeField] private EffectData _steamData;

    public EffectData BurningData { get => _burningData; }
    public EffectData SoggyData { get => _soggyData; }
    public EffectData SteamData { get => _steamData; }
}
