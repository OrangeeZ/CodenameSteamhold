using System.Collections.Generic;
using Assets.Scripts.Info;
using UnityEngine;
using Assets.Scripts.World.EntityFactory;
using Rotorz.ReorderableList;
using Rotorz.ReorderableList.Internal;
using UnityEditor;

[CustomEditor(typeof(EntityTypesMap),true)]
public class EntityTypesMapInspector : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
        var map = serializedObject.targetObject as EntityTypesMap;
        ReorderableListGUI.ListField(map.UnitsInfo, DrawUnit);
        GUILayout.Space(10);
        ReorderableListGUI.ListField(map.BuildingsInfo, DrawBuilding);
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private UnitEnitityItem DrawUnit(Rect rect, UnitEnitityItem item,int index)
    {
        var width = rect.width/2;
        var enumRect = new Rect(rect.position,new Vector2(width, rect.height));
        item.ResourceType = (UnitType)EditorGUI.EnumPopup(enumRect, item.ResourceType);
        var modelRect = new Rect(new Vector2(rect.x + width,rect.y), new Vector2(width, rect.height));
        item.Entity = EditorGUI.ObjectField(modelRect, item.Entity,typeof(UnitInfo),false) as UnitInfo;
        return item;
    }

    private BuildingEnitityItem DrawBuilding(Rect rect, BuildingEnitityItem item, int index)
    {
        var width = rect.width / 2;
        var enumRect = new Rect(rect.position, new Vector2(width, rect.height));
        item.ResourceType = (BuildingType)EditorGUI.EnumPopup(enumRect, item.ResourceType);
        var modelRect = new Rect(new Vector2(rect.x + width, rect.y), new Vector2(width, rect.height));
        item.Entity = EditorGUI.ObjectField(modelRect, item.Entity, typeof(BuildingInfo), false) as BuildingInfo;
        return item;
    }

}


