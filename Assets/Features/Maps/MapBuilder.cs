using System;
using Features.Maps;
using Object = UnityEngine.Object;

namespace Features
{
    public class MapBuilder
    {
        public event Action<Map> Created;
        
        public Map Build()
        {
            var map = Object.FindObjectOfType<Map>();
            Created?.Invoke(map);
            return map;
        }
    }
}