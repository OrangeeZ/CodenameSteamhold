using Assets.Scripts.World.EntityFactory;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class TestUnitOnGui : GuiDrawer
    {
        private readonly TestUnitFactory _unitFactory;
        
        public TestUnitOnGui(TestUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public override void Draw()
        {
            GUILayout.Space(10);
            _unitFactory.IsEnemy = 
                GUILayout.Toggle(_unitFactory.IsEnemy, "Is Enemy");
            foreach (var each in EntityTypesMap.UnitTypes)
            {
                if (GUILayout.Button("Create " + each))
                {
                    _unitFactory.CreateUnit(each);
                }
            }
            GUILayout.Space(10);
            foreach (var each in EntityTypesMap.BuildingsTypes)
            {
                if (GUILayout.Button("Create " + each))
                {
                    _unitFactory.CreateBuilding(each);
                }
            }
        }


    }
}
