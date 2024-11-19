using System.Linq;
using Model;
using UnityEditor;
using UnityEngine;

namespace HelpScripts
{
    public class PlacementDataRecorder : MonoBehaviour
    {
        [SerializeField] private PlacementData placementData;

        [ContextMenu("RecordPositions")]
        public void RecordPositions()
        {
            var positions = transform.OfType<Transform>().Select(c => c.localPosition).ToArray();
            placementData.Positions = positions;
            EditorUtility.SetDirty(placementData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}