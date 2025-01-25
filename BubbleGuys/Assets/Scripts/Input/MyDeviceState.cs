using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public struct MyDeviceState
    {
        public Vector2 MoveVector;
        public bool ButtonClicked;
    }
}