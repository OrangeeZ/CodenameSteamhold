﻿using UnityEngine;

namespace Assets.Scripts.World
{
    public class TestUnitOnGui : MonoBehaviour
    {
        private TestUnitFactory _testUnitFactory;
        private TestWorldData _testWorldData;

        private void Awake()
        {
            _testUnitFactory = GetComponent<TestUnitFactory>();
            _testWorldData = GetComponent<TestWorldData>();
        }

        public void DrawMenu()
        {
            GUILayout.Space(10);

            _testUnitFactory.IsEnemy = 
                GUILayout.Toggle(_testUnitFactory.IsEnemy, "Is Enemy");

            foreach (var each in _testUnitFactory.UnitInfos)
            {
                if (GUILayout.Button("Create " + each.Name))
                {
                    _testUnitFactory.CreateUnit(each);
                }
            }

            GUILayout.Space(10);

            foreach (var each in _testUnitFactory.BuildingInfos)
            {
                if (GUILayout.Button("Create " + each.Name))
                {
                    _testUnitFactory.CreateBuilding(each);
                }
            }
            DrawResource();
        }

        private void DrawResource()
        {
            //var resourceContainer = _testWorldData.GetClosestStockpile(Vector3.down);
            //var food = resourceContainer["Bread"];
            //GUILayout.Label("Bread Count :" + food);

        }

        private void OnGUI()
        {
            DrawMenu();
        }

    }
}
