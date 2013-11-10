using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Windows.Forms;
using Microsoft.DirectX;

namespace HaloDeveloper.Raw.ModelRaw
{
    public class Camera
    {

        private Device keyboard_device;
        public Device KeyBoard_Input_Device
        {
            get { return keyboard_device; }
        }

        private Device mouse_device;
        public Device Mouse_Input_Device
        {
            get { return mouse_device; }
        }

        public Vector3 Position
        {
            get { return cameraPositon; }
            set { cameraPositon = value; }
        }

        private Vector3 lookat = new Vector3(0f, 0f, 0f);
        public Vector3 LookAtVector
        {
            get { return lookat; }
            set { lookat = value; }
        }

        private Vector3 up = new Vector3(0f, 0f, 1f);
        public Vector3 UpVector
        {
            get { return up; }
            set { up = value; }
        }

        public Matrix Projection
        {
            get
            {
                return Matrix.PerspectiveFovRH(70.0f, 1.33f,
                    0.1f, 1000.0f);
            }
        }

        public Matrix View
        {
            get
            {
                return Matrix.LookAtRH(
                 Position,
                 LookAtVector,
                 UpVector);
            }
        }

        private bool leftMouse;
        public bool LeftMouseDown
        {
            get { return leftMouse; }
        }

        private bool rightMouse;
        public bool RightMouseDown
        {
            get { return rightMouse; }
        }

        //Render control
        private Control control;

        //Camera Speed
        private float speed = 1.0f;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        float speedConst = 0.5f;

        bool hasLetgo = true;
        public void CycleSpeed()
        {
            if (!hasLetgo)
                return;

            switch ((int)Speed)
            {
                case 1:
                    {
                        Speed = 2.0f;
                        break;
                    }
                case 2:
                    {
                        Speed = 20.0f;
                        break;
                    }
                case 20:
                    {
                        Speed = 40.0f;
                        break;
                    }
                case 40:
                    {
                        Speed = 60.0f;
                        break;
                    }
                case 60:
                    {
                        Speed = 1.0f;
                        break;
                    }
            }
            RenderHelper.messageConsole.AddMessage("Speed Changed " + speed.ToString());
        }

        //Radius info
        private float radius = 1.0f;
        private float radianh;
        private float radianv;

        //Last mouse cords
        Vector2 lastMousePos;

        //Camera position
        Vector3 cameraPositon;

        public bool mouseRightDown = false;
        public int MouseX;
        public int MouseY;

        //Camera ikj
        float i = 0f;
        float j = 0f;
        float k = 0f;

        public Camera(Control Control, Form parent)
        {

            control = Control;

            radianh = 3.14159f;
            radianv = 6.161014f;

            Initilize(parent);

            ComputePosition();

        }

        public void Initilize(Form parent)
        {

            keyboard_device = new Device(SystemGuid.Keyboard);

            keyboard_device.SetDataFormat(DeviceDataFormat.Keyboard);

            keyboard_device.SetCooperativeLevel(parent,
             CooperativeLevelFlags.NonExclusive |
              CooperativeLevelFlags.Background);

            keyboard_device.Acquire();

        }

        public bool update = false;
        public bool isFocused = true;
        public void Update()
        {

            if (!isFocused)
                return;

            //Generate the planes for Viewing Frustum Culling
            GeneratePlanes();

            #region Keyboard Input

            Key[] keys = keyboard_device.GetPressedKeys();
            List<Key> keys1 = new List<Key>();
            foreach (Key kk in keys)
            {
                keys1.Add(kk);
                switch (kk)
                {
                    case Key.W:
                        {
                            Move(MoveDirection.Forward);
                            break;
                        }
                    case Key.S:
                        {
                            Move(MoveDirection.Backward);
                            break;
                        }
                    case Key.A:
                        {
                            Move(MoveDirection.Left);
                            break;
                        }
                    case Key.D:
                        {
                            Move(MoveDirection.Right);
                            break;
                        }
                    case Key.Z:
                        {
                            Move(MoveDirection.Down);
                            break;
                        }
                    case Key.X:
                        {
                            Move(MoveDirection.Up);
                            break;
                        }
                    case Key.LeftShift:
                        {
                            CycleSpeed();
                            hasLetgo = false;
                            break;
                        }
                    case Key.P:
                        {
                            RenderHelper.CaptureScreenshot();
                            break;
                        }
                }
            }

            if (!keys1.Contains(Key.LeftShift))
                hasLetgo = true;

            if (keys.Length > 0)
            {
                update = true;
                ComputePosition();
            }

            #endregion

        }

        public enum MoveDirection
        {
            Backward = 8,
            Down = 0x20,
            Forward = 4,
            Left = 1,
            Right = 2,
            Up = 0x10
        }

        public void Move(MoveDirection Direction)
        {

            switch (Direction)
            {
                case MoveDirection.Forward:
                    {
                        cameraPositon.X += i * (speed * speedConst);
                        cameraPositon.Y += j * (speed * speedConst);
                        cameraPositon.Z += k * (speed * speedConst);
                        break;
                    }
                case MoveDirection.Backward:
                    {
                        cameraPositon.X -= i * (speed * speedConst);
                        cameraPositon.Y -= j * (speed * speedConst);
                        cameraPositon.Z -= k * (speed * speedConst);
                        break;
                    }
                case MoveDirection.Left:
                    {
                        ComputeStrafe(false);
                        break;
                    }
                case MoveDirection.Right:
                    {
                        ComputeStrafe(true);
                        break;
                    }
                case MoveDirection.Down:
                    {
                        cameraPositon.Z -= (speed * speedConst);
                        break;
                    }
                case MoveDirection.Up:
                    {
                        cameraPositon.Z += (speed * speedConst);
                        break;
                    }
            }

        }

        public void UpdateCameraWithMouse(int x, int y)
        {

            radianh += DegreesToRadian((float)x);
            radianv += DegreesToRadian((float)y);

            ComputePosition();

            update = true;

        }

        public void AimCamera(Vector3 v)
        {

            i = v.X;
            j = v.Y;
            k = v.Z;

            lookat.X = i;
            lookat.Y = j;
            lookat.Z = k;

        }

        public void ComputeStrafe(bool right)
        {

            radianh = radianh > (float)Math.PI * 2 ? radianh - (float)Math.PI * 2 : radianh;
            radianh = radianh < 0 ? radianh + (float)Math.PI * 2 : radianh;

            radianv = radianv > (float)Math.PI * 2 ? radianv - (float)Math.PI * 2 : radianv;
            radianv = radianv < 0 ? radianv + (float)Math.PI * 2 : radianv;

            float tempi = radius * (float)(Math.Cos(radianh - 1.57) * Math.Cos(radianv)) * (speed * speedConst);
            float tempj = radius * (float)(Math.Sin(radianh - 1.57) * Math.Cos(radianv)) * (speed * speedConst);

            if (right == true)
            {
                lookat.X += tempi;
                lookat.Y += tempj;

                cameraPositon.X += tempi;
                cameraPositon.Y += tempj;
            }
            else
            {
                lookat.X -= tempi;
                lookat.Y -= tempj;

                cameraPositon.X -= tempi;
                cameraPositon.Y -= tempj;
            }

        }

        public void ComputePosition()
        {

            radianh = radianh > (float)Math.PI * 2 ? radianh - (float)Math.PI * 2 : radianh;
            radianh = radianh < 0 ? radianh + (float)Math.PI * 2 : radianh;

            radianv = radianv > (float)Math.PI * 2 ? radianv - (float)Math.PI * 2 : radianv;
            radianv = radianv < 0 ? radianv + (float)Math.PI * 2 : radianv;

            i = radius * (float)(Math.Cos(radianh) * Math.Cos(radianv));
            j = radius * (float)(Math.Sin(radianh) * Math.Cos(radianv));
            k = radius * (float)Math.Sin(radianv);

            lookat.X = i + cameraPositon.X;
            lookat.Y = j + cameraPositon.Y;
            lookat.Z = k + cameraPositon.Z;

        }

        public float DegreesToRadian(float degree)
        {
            return (float)(degree * (Math.PI / 180));
        }

        public FrustumPlane[] m_frustumPlanes = new FrustumPlane[6];

        public struct FrustumPlane
        {
            public Vector3 m_normal;
            public double m_distance;
            public void Normalise()
            {
                float denom = (float)Math.Sqrt((float)((m_normal.X * m_normal.X) + (m_normal.Y * m_normal.Y) + (m_normal.Z * m_normal.Z)));
                m_normal.X /= denom;
                m_normal.Y /= denom;
                m_normal.Z /= denom;
                m_distance /= denom;
            }

            public float DistanceToPoint(Vector3 pnt)
            {
                return Vector3.Dot(m_normal, pnt) + (float)m_distance;
            }
        }

        public bool BoxInFrustum(Vector3 max, Vector3 min)
        {
            Vector3 center = Vector3.Multiply(Vector3.Add(max, min), 0.5f);
            Vector3 halfDiag = Vector3.Subtract(max, center);

            float m = 0.0f;
            float n = 0.0f;
            for (int i = 0; i < 6; i++)
            {
                m = center.X * m_frustumPlanes[i].m_normal.X + center.Y * m_frustumPlanes[i].m_normal.Y + center.Z * m_frustumPlanes[i].m_normal.Z + (float)m_frustumPlanes[i].m_distance;
                n = halfDiag.X * Math.Abs(m_frustumPlanes[i].m_normal.X) + halfDiag.Y * Math.Abs(m_frustumPlanes[i].m_normal.Y) + halfDiag.Z * Math.Abs(m_frustumPlanes[i].m_normal.Z);

                if (m + n < 0)
                    return false;
            }
            return true;
        }

        public void GeneratePlanes()
        {

            // Get combined matrix
            Matrix matComb = Matrix.Multiply(RenderHelper.renderer.Device.Transform.View, RenderHelper.renderer.Device.Transform.Projection);

            // Left clipping plane
            m_frustumPlanes[0].m_normal.X = (matComb.M14 + matComb.M11);
            m_frustumPlanes[0].m_normal.Y = (matComb.M24 + matComb.M21);
            m_frustumPlanes[0].m_normal.Z = (matComb.M34 + matComb.M31);
            m_frustumPlanes[0].m_distance = (matComb.M44 + matComb.M41);

            // Right clipping plane
            m_frustumPlanes[1].m_normal.X = (matComb.M14 - matComb.M11);
            m_frustumPlanes[1].m_normal.Y = (matComb.M24 - matComb.M21);
            m_frustumPlanes[1].m_normal.Z = (matComb.M34 - matComb.M31);
            m_frustumPlanes[1].m_distance = (matComb.M44 - matComb.M41);

            // Top clipping plane
            m_frustumPlanes[2].m_normal.X = (matComb.M14 - matComb.M12);
            m_frustumPlanes[2].m_normal.Y = (matComb.M24 - matComb.M22);
            m_frustumPlanes[2].m_normal.Z = (matComb.M34 - matComb.M32);
            m_frustumPlanes[2].m_distance = (matComb.M44 - matComb.M42);

            // Bottom clipping plane
            m_frustumPlanes[3].m_normal.X = (matComb.M14 + matComb.M12);
            m_frustumPlanes[3].m_normal.Y = (matComb.M24 + matComb.M22);
            m_frustumPlanes[3].m_normal.Z = (matComb.M34 + matComb.M32);
            m_frustumPlanes[3].m_distance = (matComb.M44 + matComb.M42);

            // Near clipping plane
            m_frustumPlanes[4].m_normal.X = (matComb.M13);
            m_frustumPlanes[4].m_normal.Y = (matComb.M23);
            m_frustumPlanes[4].m_normal.Z = (matComb.M33);
            m_frustumPlanes[4].m_distance = (matComb.M43);

            // Far clipping plane
            m_frustumPlanes[5].m_normal.X = (matComb.M14 - matComb.M13);
            m_frustumPlanes[5].m_normal.Y = (matComb.M24 - matComb.M23);
            m_frustumPlanes[5].m_normal.Z = (matComb.M34 - matComb.M33);
            m_frustumPlanes[5].m_distance = (matComb.M44 - matComb.M43);

            for (int x = 0; x < 6; x++)
                m_frustumPlanes[x].Normalise();

        }

    }
}