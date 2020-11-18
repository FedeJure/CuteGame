using System.Collections.Generic;
using UnityEngine;

namespace Modules.ActorModule.Scripts.UnityDelivery.Skin
{
    [CreateAssetMenu(fileName = "ActorSkinsConfig", menuName = "ScriptableObjects/ActorSkinsConfig", order = 3)]
    public class ActorSkinConfig : ScriptableObject
    {
        [SerializeField] public List<ActorSkinData> skins;
    }
}