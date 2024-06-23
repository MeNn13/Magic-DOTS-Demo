using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "ScriptableObject/Skill")]
    public class SkillsConfig : ScriptableObject
    {
        [SerializeField] private List<SkillData> skills;

        public List<SkillData> Skills => skills;
    }

    [Serializable]
    public class SkillData
    {
        public string name;
        public GameObject skillPrefab;
        public float duration;
    }
}
