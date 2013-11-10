using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.IO;
using Microsoft.DirectX.Direct3D;

namespace HaloDeveloper.Raw.ModelRaw
{
    public sealed class RenderHelper
    {
        public static Renderer renderer;
        static RenderStatus Play = RenderStatus.Play;

        public static MessageConsole messageConsole;
        static Microsoft.DirectX.Direct3D.Font fpsFont;
        public static Control renderScene;
        public static ModelInfo mi;

        public static Camera camera;
        public static bool renderDebug = false;
        static Microsoft.DirectX.Direct3D.Font debugFont;

        public enum RenderStatus
        {
            Play,
            Pause,
            Stop
        }

        public static void SetupRenderScene(Control Control, Form Parent)
        {
            renderScene = Control;
            renderer = new Renderer(renderScene);
            System.Drawing.Font drawingFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 16f, FontStyle.Bold);
            fpsFont = new Microsoft.DirectX.Direct3D.Font(renderer.Device, drawingFont);
            drawingFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 10f, FontStyle.Regular);
            debugFont = new Microsoft.DirectX.Direct3D.Font(renderer.Device, drawingFont);
            messageConsole = new MessageConsole(Control);
            camera = new Camera(Control, Parent);
        }

        public static void SetupModel(ModelInfo Model)
        {
            mi = Model;
        }

        public static int totalFPS = 0;
        public static void BeginRender()
        {
            Render();
        }

        private static void Render()
        {
            //Setup the matricies
            renderer.Device.Transform.World = Matrix.Identity;
            renderer.Device.Transform.Projection = camera.Projection;

            //Loop and play while true
            DateTime lastFPSTime = DateTime.Now;
            int currentFPS = 0;
            while (Play != RenderStatus.Stop)
            {
                if (Play == RenderStatus.Pause)
                    continue;

                RenderFrame();

                //Calculate the fps
                TimeSpan ts = DateTime.Now.Subtract(lastFPSTime);
                currentFPS++;
                if (ts.TotalSeconds >= 1)
                {
                    lastFPSTime = DateTime.Now;
                    totalFPS = currentFPS;
                    currentFPS = 0;
                }
            }

            //This will clear the backbuffer to black
            renderer.BeginScene(Color.White);
            renderer.EndScene();
        }

        private static void RenderFrame()
        {
            //Update camera
            camera.Update();
            if (camera.update)
            {
                renderer.Device.Transform.View = camera.View;
                camera.update = false;
            }

            //Being the scene
            renderer.BeginScene(Color.CornflowerBlue);

            //Only if our bsp is set lets render it
            int objectsRendered = 0;
            if (mi != null)
            {
                renderer.Device.Transform.World = Matrix.Identity;
                mi.RenderModel();

                objectsRendered++;
            }

            //See if we sent a request to capture a screenshot
            //This helps not only that we wont take it when its still drawing(because of threading)
            //And it makes sure we dont have the fps and console messages drawn
            if (capScreenshot)
            {
                capScreenshot = false;
                DumpScreenShot();
            }

            //Update and Render the messages
            messageConsole.Update();
            messageConsole.Render();

            //Render debug info
            if (renderDebug)
            {
                string debugString = "Debug Info\n" +
                                     "----------\n" +
                                     "rendering " + objectsRendered.ToString() + " object(s)\n" +
                                     "camera pos:\n" +
                                     "x=" + camera.Position.X.ToString() + "\n" +
                                     "y=" + camera.Position.Y.ToString() + "\n" +
                                     "z=" + camera.Position.Z.ToString();
                Rectangle rec = debugFont.MeasureString(null, debugString, DrawTextFormat.None, Color.Gold);
                debugFont.DrawText(null, debugString, renderScene.Width - rec.Width - 10, 10, Color.Gold);
            }

            //Render our FPS
            fpsFont.DrawText(null, totalFPS.ToString() + " FPS", 10, 10, Color.Gold);

            //End the scene
            renderer.EndScene();
        }

        public static void StopRender()
        {
            Play = RenderStatus.Stop;
        }

        public static void Kill()
        {
            renderThread.Abort();
        }

        public static void PauseRender()
        {
            Play = RenderStatus.Pause;
        }

        public static void PlayRender()
        {
            bool needToBegin = Play == RenderStatus.Stop;
            Play = RenderStatus.Play;

            if (needToBegin)
                BeginThreadedRendering();
        }

        public static bool capScreenshot = false;
        public static void CaptureScreenshot()
        {
            capScreenshot = true;
        }

        public static void DumpScreenShot()
        {
            DirectoryInfo screenFolder = new DirectoryInfo(Application.StartupPath + "\\Screenshots\\");
            if (!screenFolder.Exists)
                screenFolder.Create();

            Surface backbuffer = renderer.Device.GetBackBuffer(0, 0, BackBufferType.Mono);
            string picPath = Application.StartupPath + "\\Screenshots\\" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".jpg";
            SurfaceLoader.Save(picPath, ImageFileFormat.Jpg, backbuffer);
            backbuffer.Dispose();

            messageConsole.AddMessage("Screenshot saved to " + picPath);
        }

        public static Thread renderThread;
        public static void BeginThreadedRendering()
        {
            renderThread = new Thread(new ThreadStart(BeginRender));
            renderThread.Priority = ThreadPriority.Normal;
            renderThread.Start();
        }

        public static void ChangeRenderPriority(ThreadPriority threadPriority)
        {
            if (renderThread != null)
                renderThread.Priority = threadPriority;
        }

        #region MeshIntersection

        public static bool MeshPick(float x, float y, Mesh mesh, Matrix mat)
        {

            Vector3 s = Vector3.Unproject(new Vector3(x, y, 0),
                renderer.Device.Viewport,
                 renderer.Device.Transform.Projection,
                 renderer.Device.Transform.View,
                mat);

            Vector3 d = Vector3.Unproject(new Vector3(x, y, 1),
                 renderer.Device.Viewport,
                 renderer.Device.Transform.Projection,
                renderer.Device.Transform.View,
                mat);

            Vector3 rPosition = s;
            Vector3 rDirection = Vector3.Normalize(d - s);

            return mesh.Intersect(rPosition, rDirection);

        }

        public static Vector3 Mark3DCursorPosition(float x, float y, Matrix m)
        {
            Vector3 tempV3 = new Vector3();
            tempV3.Project(renderer.Device.Viewport, renderer.Device.Transform.Projection, renderer.Device.Transform.View, m);

            tempV3.X = x;
            tempV3.Y = y;
            tempV3.Unproject(renderer.Device.Viewport, renderer.Device.Transform.Projection,
                renderer.Device.Transform.View, m);

            return tempV3;
        }

        #endregion

    }
}