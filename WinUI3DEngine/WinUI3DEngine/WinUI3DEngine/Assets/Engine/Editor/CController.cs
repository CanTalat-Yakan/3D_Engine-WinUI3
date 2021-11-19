using System;
using Windows.System;
using WinUI3DEngine.Assets.Engine.Components;
using WinUI3DEngine.Assets.Engine.Utilities;
using Vector3 = SharpDX.Vector3;

namespace WinUI3DEngine.Assets.Engine.Editor
{
    internal class CController
    {
        internal string m_Profile { get => m_camera.m_Transform.ToString(); }

        CCamera m_camera;
        CInput m_input;

        internal static float m_MovementSpeed = 2;
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
                m_camera.m_Transform.m_Position += m_direction * (float)CTime.m_Delta;
            m_direction = new Vector3();
        }

        void MovementSpeedCalc()
        {
            if (m_input.GetButton(EMouseButton.IsLeftButtonPressed)
                || m_input.GetButton(EMouseButton.IsRightButtonPressed))
                m_MovementSpeed += m_input.GetMouseWheel();

            m_MovementSpeed = Math.Clamp(m_MovementSpeed, 0.1f, 10);
        }

        void ZoomMovement()
        {
            if (!m_input.GetButton(EMouseButton.IsLeftButtonPressed)
                && !m_input.GetButton(EMouseButton.IsMiddleButtonPressed)
                && !m_input.GetButton(EMouseButton.IsRightButtonPressed))
                m_direction += 50 * m_camera.m_Front * m_input.GetMouseWheel();
        }

        void CameraMovement(int _horizontalFactor = 1, int _verticalFactor = 1)
        {
            m_camera.m_Transform.m_Rotation.X += m_rotationSpeed * m_input.GetMouseAxis().X * (float)CTime.m_Delta * _horizontalFactor;
            m_camera.m_Transform.m_Rotation.Y += m_rotationSpeed * m_input.GetMouseAxis().Y * (float)CTime.m_Delta * _verticalFactor;
        }

        void TransformMovement()
        {
            m_direction = new Vector3();
            if (m_input.GetKey(VirtualKey.W)) m_direction += m_MovementSpeed * m_camera.m_Front;
            if (m_input.GetKey(VirtualKey.S)) m_direction -= m_MovementSpeed * m_camera.m_Front;
            if (m_input.GetKey(VirtualKey.A)) m_direction += m_MovementSpeed * m_camera.m_Right;
            if (m_input.GetKey(VirtualKey.D)) m_direction -= m_MovementSpeed * m_camera.m_Right;
        }
        void HeightTransformMovement()
        {
            if (m_input.GetKey(VirtualKey.LeftShift)) m_MovementSpeed *= 4;
            if (m_input.GetKey(VirtualKey.LeftControl)) m_MovementSpeed *= 0.25f;

            if (m_input.GetKey(VirtualKey.E)) m_direction += m_MovementSpeed * m_camera.m_Up;
            if (m_input.GetKey(VirtualKey.Q)) m_direction -= m_MovementSpeed * m_camera.m_Up;
            if (m_input.GetKey(VirtualKey.E) && m_input.GetKey(VirtualKey.W)) m_direction += m_MovementSpeed * m_camera.m_LocalUp;
            if (m_input.GetKey(VirtualKey.Q) && m_input.GetKey(VirtualKey.W)) m_direction -= m_MovementSpeed * m_camera.m_LocalUp;
            if (m_input.GetKey(VirtualKey.E) && m_input.GetKey(VirtualKey.S)) m_direction += m_MovementSpeed * m_camera.m_LocalUp;
            if (m_input.GetKey(VirtualKey.Q) && m_input.GetKey(VirtualKey.S)) m_direction -= m_MovementSpeed * m_camera.m_LocalUp;
        }
        void ScreenMovement()
        {
            m_direction -= m_camera.m_Right * m_input.GetMouseAxis().X;
            m_direction -= m_camera.m_LocalUp * m_input.GetMouseAxis().Y;
        }
        void HorizontalScreenMovement()
        {
            m_direction += 0.1f * Vector3.Normalize(m_camera.m_Front * new Vector3(1, 0, 1)) * m_input.GetMouseAxis().Y;
        }
    }
}
