using UnityEngine;

namespace GGJ23.Utils
{
    public class LayerUtils
    {
        public static bool MatchesLayerMask(GameObject obj, LayerMask mask)
        {
            return (1 << obj.layer & mask.value) > 0;
        }
    }
}