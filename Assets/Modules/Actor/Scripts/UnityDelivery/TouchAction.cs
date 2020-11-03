using System.Collections.Generic;
using Modules.Actor.Scripts.Presentation.Events;

namespace Modules.Actor.Scripts.UnityDelivery
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "TouchAction", menuName = "ScriptableObjects/TouchAction", order = 1)]
    public class TouchAction : ScriptableObject
    {
        [SerializeField] public List<TouchDirection> workOnDirections = new List<TouchDirection>();
        [SerializeField] public ActorInteraction interactionResult;
    }
}