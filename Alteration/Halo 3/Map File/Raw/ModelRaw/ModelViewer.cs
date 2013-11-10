using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using HaloDeveloper.Map;
using Microsoft.DirectX.Direct3D;
using System.Threading;
namespace HaloDeveloper.Raw.ModelRaw
{
    public partial class ModelViewer : UserControl
    {
        HaloMap map;
        int tagIndex;
        ModelInfo mi;

        public ModelViewer()
        {
            InitializeComponent();

            this.MouseDown += new MouseEventHandler(renderer_MouseDown);
            this.MouseUp += new MouseEventHandler(renderer_MouseUp);
            this.MouseMove += new MouseEventHandler(renderer_MouseMove);
            this.LostFocus += new EventHandler(renderer_LostFocus);
            this.GotFocus += new EventHandler(renderer_GotFocus);
        }

        public void LoadModelTag(HaloMap Map, int TagIndex)
        {
            map = Map;
            tagIndex = TagIndex;

            mi = ModelFunctions.GetModelInfo(map, tagIndex);
            mi.InitializeBuffers(RenderHelper.renderer, map);

            RenderHelper.SetupModel(mi);
            RenderHelper.messageConsole.AddMessage("Model Loaded");
        }

        void renderer_MouseUp(object sender, MouseEventArgs e)
        {
            RenderHelper.camera.mouseRightDown = (e.Button == MouseButtons.Right);
        }

        void renderer_GotFocus(object sender, EventArgs e)
        {
            RenderHelper.camera.isFocused = true;
        }

        void renderer_LostFocus(object sender, EventArgs e)
        {
            RenderHelper.camera.isFocused = false;
        }

        Point lastPos;
        void renderer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Focused)
                return;

            Point pos = this.PointToScreen(new Point(e.X, e.Y));
            RenderHelper.camera.MouseX = pos.X;
            RenderHelper.camera.MouseY = pos.Y;

            if (e.Button == MouseButtons.Left)
                RenderHelper.camera.UpdateCameraWithMouse(lastPos.X - pos.X, lastPos.Y - pos.Y);

            lastPos = new Point(pos.X, pos.Y);
        }

        void renderer_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (e.Button == MouseButtons.Right)
            {
                Point pos = this.PointToScreen(new Point(e.X, e.Y));
            }
        }

        public void Initilize(Form parentForm)
        {
            RenderHelper.SetupRenderScene(this, parentForm);
            RenderHelper.BeginThreadedRendering();
        }
    }
}