using UnityEngine;
using DynamicFogAndMist2;
using UnityStandardAssets.Utility;

namespace DynamicFogAndMist2_Demos {

    public class FogOfWarPainter : MonoBehaviour {

        public float clearRadius = 5f;
        public float clearDuration = 0f;
        public float restoreDelay = 10f;
        public float restoreDuration = 2f;
        [Range(0,1)]
        public float borderSmoothness = 0.2f;

        DynamicFog fog;

        void OnEnable() {
            InputProxy.SetupEventSystem();
        }

        void Start() {
            fog = GetComponent<DynamicFog>();
        }

        void Update() {
            if (InputProxy.GetMouseButton(0)) {
                // Raycast to terrain and clear fog around hit position
                Vector3 mousePos = InputProxy.MousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit terrainHit;
                if (Physics.Raycast(ray, out terrainHit)) {
                    fog.SetFogOfWarAlpha(terrainHit.point, clearRadius, 0, true, clearDuration, borderSmoothness, restoreDelay, restoreDuration);
                }
            }
        }

        public void RestoreFog() {
            fog.ResetFogOfWar();
        }
    }

}