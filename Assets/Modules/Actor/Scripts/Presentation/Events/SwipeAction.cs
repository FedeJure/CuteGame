namespace Modules.Actor.Scripts.Presentation.Events
{
    public class SwipeAction
    {
        private TouchDirection Direction { get; }
        public SwipeAction() { }

        private SwipeAction(TouchDirection direction)
        {
            
        }

        public SwipeAction(Vector startPosition, Vector endPosition, Vector actorPosition,
            float actorHeight, float actorWidth)
        {
            //TODO: hacer logica para q devuelva el evento correcto
        }
    }

    public enum TouchDirection
    {
        UpDown,
        DownUp,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        
        public Vector() { }
    }
}