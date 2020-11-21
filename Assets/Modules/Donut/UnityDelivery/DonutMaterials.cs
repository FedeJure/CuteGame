using UnityEngine;

namespace Models.Donut
{
    [CreateAssetMenu(fileName = "DonutMaterials", menuName = "ScriptableObjects/DonutMaterials", order = 4)]
    public class DonutMaterials : ScriptableObject
    {
        public Material donutMat;
        public Material glassMat;
    }
}