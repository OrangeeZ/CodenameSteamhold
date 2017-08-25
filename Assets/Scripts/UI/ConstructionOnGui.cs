using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World.EntityFactory;
using UnityEngine;

public class ConstructionOnGui : IGuiDrawer
{
    private readonly TestUnitFactory _unitFactory;
    private readonly EntityTypesMap _entityTypesMap;
    private readonly ConstructionModule _constructionModule;

    public ConstructionOnGui(TestUnitFactory unitFactory,EntityTypesMap entityTypesMap, ConstructionModule constructionModule)
    {
        _unitFactory = unitFactory;
        _entityTypesMap = entityTypesMap;
        _constructionModule = constructionModule;
    }

    public void Draw()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height - 200, 100, 200));
        GUILayout.Label("Construction module");
        GUILayout.BeginVertical();

        if (!_constructionModule.IsPlacingBuilding)
        {
            foreach (var each in _entityTypesMap.Buildings.Values)
            {
                if (GUILayout.Button(each.ToString(), GUILayout.ExpandWidth(false)))
                {
                    _constructionModule.IsPlacingBuilding = true;
                    _constructionModule.SelectedBuilding = Object.Instantiate(each.Entity.Prefab).gameObject;
                    _constructionModule.SelectedBuildingInfo = each.Entity;
                    break;
                }
            }

            if (GUILayout.Button(_constructionModule.IsRemovingBuildings ? 
                "Stop removing buildings" :
                "Start removing buildings", GUILayout.ExpandWidth(false)))
            {
                _constructionModule.IsRemovingBuildings = !_constructionModule.IsRemovingBuildings;
            }
        }

        if (Event.current.keyCode == KeyCode.Escape)
        {
            _constructionModule.IsPlacingBuilding = false;
            _constructionModule.IsRemovingBuildings = false;
            Object.Destroy(_constructionModule.SelectedBuilding);

        }

        GUILayout.EndVertical();

        GUILayout.EndArea();
    }
}
