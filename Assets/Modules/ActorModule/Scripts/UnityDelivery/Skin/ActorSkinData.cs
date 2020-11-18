using UnityEngine;

namespace Modules.ActorModule.Scripts.UnityDelivery.Skin
{

    [CreateAssetMenu(fileName = "ActorSkin", menuName = "ScriptableObjects/ActorSkin", order = 2)]
    public class ActorSkinData : ScriptableObject
    {
        [SerializeField] public SkinType type;
        [SerializeField] public string key;
        [SerializeField] public Material material;
    }

    public enum SkinType
    {
        Head,
        Body
    }
    
}