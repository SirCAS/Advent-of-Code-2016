using System.Collections.Generic;
using System.Linq;

namespace RtgFacility
{
    public class Floor
    {
        public List<Component> Components { get; set; }
    }

    public class Component
    {
        public ComponentType Type { get; set; }
        public string Name { get; set; }
    }

    public enum ComponentType
    {
        Chip, Generator
    }
}

