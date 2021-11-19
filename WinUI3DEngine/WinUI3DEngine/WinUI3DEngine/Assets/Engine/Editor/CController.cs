using System;
using Windows.System;
using WinUI3DEngine.Assets.Engine.Components;
using WinUI3DEngine.Assets.Engine.Utilities;
using Vector3 = SharpDX.Vector3;

namespace WinUI3DEngine.Assets.Engine.Editor
{
    internal class CController
    {
        public string m_profile { get => m_camera.m_transform.ToString(); }
        CCamera m_camera;
        CInput m_input;

        public static float m_movementSpeed = 2;
        float m_rotationSpeed = 25;
        Vector3 m_direction;


        public CController(CCamera _camera)
        {
            m_camera = _camera;
            m_input = CInput.Instance;
        }

        internal void Update()
        {
            MovementSpeedCalc();

            if (m_input.GetButton(EMouseButton.IsMiddleButtonPressed))
            {
                if (m_input.SetPointerInBounds())
                {
                    TransformMovement();
                    ScreenMovement();
                    HeightTransformMovement();
                }
            }

            if (m_input.GetButton(EMouseButton.IsRightButtonPressed))
            {
                if (m_input.SetPointerInBounds())
                {
                    if (m_input.GetButton(EMouseButton.IsLeftButtonPressed))
                        ScreenMovement();
                    else
                    {
                        TransformMovement();
                        CameraMovement();
                    }
                    HeightTransformMovement();
                }
            }

            ZoomMovement();
            if (!m_input.GetKey(VirtualKey.X))
                m_camera.m_transform.m_position += m_direction * (float)CTime.m_delta;
            m_direction = new Vector3();
        }

        void MovementSpeedCalc()
        {
            if (m_input.GetButton(EMouseButton.IsLeftButtonPressed)
                || m_input.GetButton(EMouseButton.IsRightButtonPressed))
                m_movementSpeed += m_input.GetMouseWheel();

            m_movementSpeed = Math.Clamp(m_movementSpeed, 0.1f, 10);
        }

        void ZoomMovement()
        {
            if (!m_input.GetButton(EMouseButton.IsLeftButtonPressed)
                && !m_input.GetButton(EMouseButton.IsMiddleButtonPressed)
                && !m_input.GetButton(EMouseButton.IsRightButtonPressed))
                m_direction += 50 * m_camera.m_front * m_input.GetMouseWheel();
        }

        void CameraMovement(int _horizontalFactor = 1, int _verticalFactor = 1)
        {
            m_camera.m_transform.m_rotation.X += m_rotationSpeed * m_input.GetMouseAxis().X * (float)CTime.m_delta * _horizontalFactor;
            m_camera.m_transform.m_rotation.Y += m_rotationSpeed * m_input.GetMouseAxis().Y * (float)CTime.m_delta * _verticalFactor;
        }

        void TransformMovement()
        {
            m_direction = new Vector3();
            if (m_input.GetKey(VirtualKey.W)) m_direction += m_movementSpeed * m_camera.m_front;
            if (m_input.GetKey(VirtualKey.S)) m_direction -= m_movementSpeed * m_camera.m_front;
            if (m_input.GetKey(VirtualKey.A)) m_direction += m_movementSpeed * m_camera.m_right;
            if (m_input.GetKey(VirtualKey.D)) m_direction -= m_movementSpeed * m_camera.m_right;
        }
        void HeightTransformMovement()
        {
            if (m_input.GetKey(VirtualKey.LeftShift)) m_movementSpeed *= 4;
            if (m_input.GetKey(VirtualKey.LeftControl)) m_movementSpeed *= 0.25f;

            if (m_input.GetKey(VirtualKey.E)) m_direction += m_movementSpeed * m_camera.m_up;
            if (m_input.GetKey(VirtualKey.Q)) m_direction -= m_movementSpeed * m_camera.m_up;
            if (m_input.GetKey(VirtualKey.E) && m_input.GetKey(VirtualKey.W)) m_direction += m_movementSpeed * m_camera.m_localUp;
            if (m_input.GetKey(VirtualKey.Q) && m_input.GetKey(VirtualKey.W)) m_direction -= m_movementSpeed * m_camera.m_localUp;
            if (m_input.GetKey(VirtualKey.E) && m_input.GetKey(VirtualKey.S)) m_direction += m_movementSpeed * m_camera.m_localUp;
            if (m_input.GetKey(VirtualKey.Q) && m_input.GetKey(VirtualKey.S)) m_direction -= m_movementSpeed * m_camera.m_localUp;
        }
        void ScreenMovement()
        {
            m_direction -= m_camera.m_right * m_input.GetMouseAxis().X;
            m_direction -= m_camera.m_localUp * m_input.GetMouseAxis().Y;
        }
        void HorizontalScreenMovement()
        {
            m_direction += 0.1f * Vector3.Normalize(m_camera.m_front * new Vector3(1, 0, 1)) * m_input.GetMouseAxis().Y;
        }
    }
}
