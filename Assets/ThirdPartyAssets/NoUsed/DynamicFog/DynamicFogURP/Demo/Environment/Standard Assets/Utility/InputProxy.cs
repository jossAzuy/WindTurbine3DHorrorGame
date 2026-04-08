using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;
#endif

namespace UnityStandardAssets.Utility
{
    public static class InputProxy
    {
        public static void SetupEventSystem()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            var eventSystem = EventSystem.current;
            if (eventSystem == null) return;
            var standaloneModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (standaloneModule != null)
            {
                Object.Destroy(standaloneModule);
                if (eventSystem.GetComponent<InputSystemUIInputModule>() == null)
                {
                    eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
                }
            }
#endif
        }

        public static bool GetKey(KeyCode keyCode)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKey(keyCode);
#elif ENABLE_INPUT_SYSTEM
            return GetKeyInternal(keyCode, false, false);
#else
            return false;
#endif
        }

        public static bool GetKeyDown(KeyCode keyCode)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKeyDown(keyCode);
#elif ENABLE_INPUT_SYSTEM
            return GetKeyInternal(keyCode, true, false);
#else
            return false;
#endif
        }

        public static bool GetKeyUp(KeyCode keyCode)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKeyUp(keyCode);
#elif ENABLE_INPUT_SYSTEM
            return GetKeyInternal(keyCode, false, true);
#else
            return false;
#endif
        }

        public static bool GetMouseButton(int button)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetMouseButton(button);
#elif ENABLE_INPUT_SYSTEM
            return GetMouseButtonInternal(button, false, false);
#else
            return false;
#endif
        }

        public static bool GetMouseButtonDown(int button)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetMouseButtonDown(button);
#elif ENABLE_INPUT_SYSTEM
            return GetMouseButtonInternal(button, true, false);
#else
            return false;
#endif
        }

        public static bool GetMouseButtonUp(int button)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetMouseButtonUp(button);
#elif ENABLE_INPUT_SYSTEM
            return GetMouseButtonInternal(button, false, true);
#else
            return false;
#endif
        }

        public static float GetAxis(string name)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetAxis(name);
#elif ENABLE_INPUT_SYSTEM
            return GetAxisInternal(name);
#else
            return 0f;
#endif
        }

        public static bool GetButton(string name)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetButton(name);
#elif ENABLE_INPUT_SYSTEM
            return GetButtonInternal(name, false, false);
#else
            return false;
#endif
        }

        public static bool GetButtonDown(string name)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetButtonDown(name);
#elif ENABLE_INPUT_SYSTEM
            return GetButtonInternal(name, true, false);
#else
            return false;
#endif
        }

        public static bool GetButtonUp(string name)
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetButtonUp(name);
#elif ENABLE_INPUT_SYSTEM
            return GetButtonInternal(name, false, true);
#else
            return false;
#endif
        }

        public static Vector3 MousePosition
        {
            get
            {
#if ENABLE_LEGACY_INPUT_MANAGER
                return Input.mousePosition;
#elif ENABLE_INPUT_SYSTEM
                var mouse = Mouse.current;
                if (mouse == null) return Vector3.zero;
                var pos = mouse.position.ReadValue();
                return new Vector3(pos.x, pos.y, 0f);
#else
                return Vector3.zero;
#endif
            }
        }

#if ENABLE_INPUT_SYSTEM
        static bool GetKeyInternal(KeyCode keyCode, bool down, bool up)
        {
            var keyboard = Keyboard.current;
            if (keyboard == null) return false;
            var control = GetKeyControl(keyboard, keyCode);
            if (control == null) return false;
            if (down) return control.wasPressedThisFrame;
            if (up) return control.wasReleasedThisFrame;
            return control.isPressed;
        }

        static KeyControl GetKeyControl(Keyboard keyboard, KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.A: return keyboard.aKey;
                case KeyCode.B: return keyboard.bKey;
                case KeyCode.C: return keyboard.cKey;
                case KeyCode.D: return keyboard.dKey;
                case KeyCode.E: return keyboard.eKey;
                case KeyCode.F: return keyboard.fKey;
                case KeyCode.G: return keyboard.gKey;
                case KeyCode.H: return keyboard.hKey;
                case KeyCode.I: return keyboard.iKey;
                case KeyCode.J: return keyboard.jKey;
                case KeyCode.K: return keyboard.kKey;
                case KeyCode.L: return keyboard.lKey;
                case KeyCode.M: return keyboard.mKey;
                case KeyCode.N: return keyboard.nKey;
                case KeyCode.O: return keyboard.oKey;
                case KeyCode.P: return keyboard.pKey;
                case KeyCode.Q: return keyboard.qKey;
                case KeyCode.R: return keyboard.rKey;
                case KeyCode.S: return keyboard.sKey;
                case KeyCode.T: return keyboard.tKey;
                case KeyCode.U: return keyboard.uKey;
                case KeyCode.V: return keyboard.vKey;
                case KeyCode.W: return keyboard.wKey;
                case KeyCode.X: return keyboard.xKey;
                case KeyCode.Y: return keyboard.yKey;
                case KeyCode.Z: return keyboard.zKey;
                case KeyCode.Space: return keyboard.spaceKey;
                case KeyCode.Escape: return keyboard.escapeKey;
                case KeyCode.LeftShift: return keyboard.leftShiftKey;
                case KeyCode.RightShift: return keyboard.rightShiftKey;
                case KeyCode.LeftControl: return keyboard.leftCtrlKey;
                case KeyCode.RightControl: return keyboard.rightCtrlKey;
                case KeyCode.LeftAlt: return keyboard.leftAltKey;
                case KeyCode.RightAlt: return keyboard.rightAltKey;
                case KeyCode.UpArrow: return keyboard.upArrowKey;
                case KeyCode.DownArrow: return keyboard.downArrowKey;
                case KeyCode.LeftArrow: return keyboard.leftArrowKey;
                case KeyCode.RightArrow: return keyboard.rightArrowKey;
                default: return null;
            }
        }

        static bool GetMouseButtonInternal(int button, bool down, bool up)
        {
            var mouse = Mouse.current;
            if (mouse == null) return false;
            ButtonControl btn;
            switch (button)
            {
                case 0: btn = mouse.leftButton; break;
                case 1: btn = mouse.rightButton; break;
                case 2: btn = mouse.middleButton; break;
                default: return false;
            }
            if (down) return btn.wasPressedThisFrame;
            if (up) return btn.wasReleasedThisFrame;
            return btn.isPressed;
        }

        static float GetAxisInternal(string name)
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            switch (name)
            {
                case "Horizontal":
                    if (keyboard == null) return 0f;
                    float h = 0f;
                    if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) h -= 1f;
                    if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) h += 1f;
                    return h;
                case "Vertical":
                    if (keyboard == null) return 0f;
                    float v = 0f;
                    if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) v -= 1f;
                    if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) v += 1f;
                    return v;
                case "Mouse X":
                    if (mouse == null) return 0f;
                    return mouse.delta.x.ReadValue() * 0.1f;
                case "Mouse Y":
                    if (mouse == null) return 0f;
                    return mouse.delta.y.ReadValue() * 0.1f;
                default:
                    return 0f;
            }
        }

        static bool GetButtonInternal(string name, bool down, bool up)
        {
            var mouse = Mouse.current;
            var keyboard = Keyboard.current;
            switch (name)
            {
                case "Fire1":
                    if (mouse == null) return false;
                    if (down) return mouse.leftButton.wasPressedThisFrame;
                    if (up) return mouse.leftButton.wasReleasedThisFrame;
                    return mouse.leftButton.isPressed;
                case "Fire2":
                    if (mouse == null) return false;
                    if (down) return mouse.rightButton.wasPressedThisFrame;
                    if (up) return mouse.rightButton.wasReleasedThisFrame;
                    return mouse.rightButton.isPressed;
                case "Jump":
                    if (keyboard == null) return false;
                    if (down) return keyboard.spaceKey.wasPressedThisFrame;
                    if (up) return keyboard.spaceKey.wasReleasedThisFrame;
                    return keyboard.spaceKey.isPressed;
                default:
                    return false;
            }
        }
#endif
    }
}

