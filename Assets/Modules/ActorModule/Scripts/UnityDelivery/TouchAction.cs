using System.Collections.Generic;
using Modules.ActorModule.Scripts.Presentation.Events;
using Modules.Common;
using UnityEngine;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    [CreateAssetMenu(fileName = "TouchAction", menuName = "ScriptableObjects/TouchAction", order = 1)]
    public class TouchAction : ScriptableObject
    {
        [SerializeField] public List<TouchDirection> workOnDirections = new List<TouchDirection>();
        [SerializeField] public ActorInteraction interactionResult;
    }
}