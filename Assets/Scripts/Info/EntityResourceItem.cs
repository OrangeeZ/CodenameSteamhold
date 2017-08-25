using System;
using Assets.Scripts.World.EntityFactory;

namespace Assets.Scripts.Info
{
    [Serializable]
    public class EntityResourceItem<TResourceType,TEntity>
    {
        public TResourceType ResourceType;
        public TEntity Entity;
    }

    [Serializable]
    public class UnitEnitityItem : EntityResourceItem<UnitType, UnitInfo> { }
    [Serializable]
    public class BuildingEnitityItem : EntityResourceItem<BuildingType, BuildingInfo> { }
}
