using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Config", menuName = "ScriptableObject/Config/EffectConfig")]
public class EffectConfig : ScriptableObject
{
    [SerializeField] private EffectData burningData;
    [SerializeField] private EffectData soggyData;
    [SerializeField] private EffectData steamData;
    [SerializeField] private EffectData defendingData;

    public EffectData BurningData { get => burningData; }
    public EffectData SoggyData { get => soggyData; }
    public EffectData SteamData { get => steamData; }
    public EffectData DefendingData { get => defendingData; }
}
