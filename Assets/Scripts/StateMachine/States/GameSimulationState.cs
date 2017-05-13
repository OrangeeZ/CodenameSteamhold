﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.StateMachine.States
{
    public class GameSimulationState : State<GameState> {


        #region private properties

        private readonly IFactory<TestWorldData> _worldData;
        private OnGuiController _guiController;

        #endregion

        public GameSimulationState(IStateController<GameState> stateController,
            IFactory<TestWorldData> worldData) : 
            base(stateController)
        {
            _worldData = worldData;
        }

        public override IEnumerator Execute()
        {
            var world = InitializeSimulationWorld();
            
            while (true)
            {
                world.Update(Time.unscaledDeltaTime);
                yield return null;
            }
        }

        public override void Stop()
        {
            base.Stop();
            Object.DestroyImmediate(_guiController);
        }
        
        #region private methods

        private BaseWorld InitializeSimulationWorld()
        {
            //initialize world
            var testWorldData = _worldData.Create();
            var playerWorld = new PlayerWorld(testWorldData.Stockpiles,
                testWorldData.Fireplace.position);
            //init unit factory
            var unitFactory = testWorldData.GetComponent<TestUnitFactory>();
            unitFactory.SetWorld(playerWorld);
            //create player
            var player = CreatePlayer(playerWorld);
            var popularityEventsBehaviour = new PopularityEvent(playerWorld, player, unitFactory);
            playerWorld.Events.Add(popularityEventsBehaviour);
            //init parent world
            var world = new BaseWorld(new List<Stockpile>(),Vector3.zero);
            world.Children.Add(playerWorld);
            playerWorld.Parent = world;
            //initialize Temp OnGUI drawer
            _guiController = InitializeOnGuiDrawer(playerWorld,player,unitFactory);
            return world;
        }

        private Player CreatePlayer(BaseWorld world)
        {
            var playerInfo = new PlayerInfo();
            var player = new Player(playerInfo, world);
            return player;
        }

        private OnGuiController InitializeOnGuiDrawer(
            BaseWorld world,Player player, 
            TestUnitFactory unitFactory)
        {
            var guiObject = new GameObject("GuiConroller");
            var guiController = guiObject.AddComponent<OnGuiController>();
            guiController.Drawers.Add(new TestUnitOnGui(unitFactory));
            guiController.Drawers.Add(new PlayerDrawer(player));
            guiController.Drawers.Add(new ResourcesDrawer(world));
            //initialize additional events
            var selectionManager = new SelectionManager(world);
            var constructionModule = new ConstructionModule(world, unitFactory);
            world.Events.Add(constructionModule);
            var uiController = new ImUiController(world, selectionManager);
            guiController.Drawers.Add(constructionModule);
            guiController.Drawers.Add(selectionManager);
            guiController.Drawers.Add(uiController);
            return guiController;
        }

        #endregion
    }
}
