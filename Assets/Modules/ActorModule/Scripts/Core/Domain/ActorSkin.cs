using System;
using UnityEngine;

namespace Modules.ActorModule.Scripts.Core.Domain
{
    [Serializable]
    public class ActorSkin
    {
        public Skin bodySkin;
        public Skin headSkin;

        public ActorSkin(Skin bodySkin, Skin headSkin)
        {
            this.bodySkin = bodySkin;
            this.headSkin = headSkin;
        }
    }
    
    [Serializable]
    public class Skin
    {
        public string key;
        public float[] _color = new []{0f,0f,0f,0f};
        public bool colorOverrided = false;

        public Color color
        {
            get => new Color(_color[0],_color[1],_color[2], _color[3]);
            set => _color = new []{value.r, value.g, value.b, value.a};
        }

        public Skin(string key)
        {
            this.key = key;
        }
        
        
        public Skin(string key, Color color)
        {
            this.key = key;
            this.color = color;
            colorOverrided = true;
        }
        
    }
}