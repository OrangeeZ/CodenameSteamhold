using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Info;
using UnityEngine;

namespace Assets.Scripts.World.EntityFactory
{
    public class EntityTypesMap : MonoBehaviour
    {

        #region inspector properties

        public List<UnitEnitityItem> UnitsInfo = new List<UnitEnitityItem>();
        public List<BuildingEnitityItem> BuildingsInfo = new List<BuildingEnitityItem>();

        #endregion

        #region public properties

        public Dictionary<UnitType, UnitEnitityItem> Units { get; protected set; }
        public Dictionary<BuildingType, BuildingEnitityItem> Buildings { get; protected set; }
        public Dictionary<UnitType, UnitEnitityItem> ArmyUnits { get; protected set; }

        public static List<UnitType> UnitTypes = new List<UnitType>(Enum.GetValues(typeof(UnitType)).OfType<UnitType>());

        public static List<BuildingType> BuildingsTypes = new List<BuildingType>(Enum.
            GetValues(typeof(BuildingType)).OfType<BuildingType>());

        #endregion

        #region private properties

        private void Awake()
        {
            Units = new Dictionary<UnitType, UnitEnitityItem>();
            ArmyUnits = new Dictionary<UnitType, UnitEnitityItem>();
            Buildings = new Dictionary<BuildingType, BuildingEnitityItem>();
            foreach (var unitEnitityItem in UnitsInfo)
            {
                Units[unitEnitityItem.ResourceType] = unitEnitityItem;
                if(unitEnitityItem.ResourceType!= UnitType.Peasant)
                    ArmyUnits[unitEnitityItem.ResourceType] = unitEnitityItem;
            }
            foreach (var enitityItem in BuildingsInfo)
            {
                Buildings[enitityItem.ResourceType] = enitityItem;
            }
        }

        #endregion
    }
}
