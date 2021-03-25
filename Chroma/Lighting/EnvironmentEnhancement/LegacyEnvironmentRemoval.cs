﻿namespace Chroma
{
    using System.Collections.Generic;
    using System.Linq;
    using CustomJSONData;
    using CustomJSONData.CustomBeatmap;
    using UnityEngine;
    using static Chroma.Plugin;

    internal static class LegacyEnvironmentRemoval
    {
        internal static void Init(CustomBeatmapData customBeatmap)
        {
            dynamic dynData = customBeatmap.beatmapCustomData;
            List<object> objectsToKill = Trees.at(dynData, ENVIRONMENTREMOVAL);

            if (objectsToKill != null)
            {
                ChromaLogger.Log("Legacy Environment Removal Detected...", IPA.Logging.Logger.Level.Warning);
                ChromaLogger.Log("Please do not use Legacy Environment Removal for new maps as it is deprecated and its functionality in future versions of Chroma cannot be guaranteed", IPA.Logging.Logger.Level.Warning);

                IEnumerable<GameObject> gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (string s in objectsToKill.Cast<string>())
                {
                    if (s == "TrackLaneRing" || s == "BigTrackLaneRing")
                    {
                        foreach (GameObject n in gameObjects.Where(obj => obj.name.Contains(s)))
                        {
                            if (s == "TrackLaneRing" && n.name.Contains("Big"))
                            {
                                continue;
                            }

                            n.SetActive(false);
                        }
                    }
                    else
                    {
                        foreach (GameObject n in gameObjects
                            .Where(obj => obj.name.Contains(s) && (obj.scene.name?.Contains("Environment") ?? false) && (!obj.scene.name?.Contains("Menu") ?? false)))
                        {
                            n.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
