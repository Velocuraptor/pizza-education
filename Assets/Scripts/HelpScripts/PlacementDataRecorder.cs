using System.Linq;
using Model;
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
        }
    }
}