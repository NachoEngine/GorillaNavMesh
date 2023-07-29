using BepInEx;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Utilla;

namespace GorillaNavMesh
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            Debug.Log(Application.version);
        }
        GameObject GorillaPlayer;
        GameObject navmesh;
        bool flag = true;
        
        bool flag1 = true;
        NavMeshSurface navmeshsurface;
        GameObject entityagent;
        void Update()
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            if (navmesh == null)
            {
                navmesh = GameObject.Find("navmesh-platform");
                flag = true;
            }
            else if (flag)
            {
                navmeshsurface = navmesh.AddComponent<NavMeshSurface>();
                navmeshsurface.collectObjects = CollectObjects.Children;
                //navmeshsurface.overrideTileSize = true;

                navmeshsurface.BuildNavMesh();
                Debug.Log("Navmesh generated");
                flag = false;
            }

            if (entityagent == null)
            {
                entityagent = GameObject.Find("Entity");
                flag1 = true;
            }
            else
            {
                if (flag1)
                {
                    Debug.Log("Entity Agent Set");
                    flag1 = false;

                }
                entityagent.GetComponent<NavMeshAgent>().destination = GorillaLocomotion.Player.Instance.gameObject.transform.position;
                
            }
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
