using System;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using D3D = Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
namespace HaloDeveloper.Raw.ModelRaw
{
    public class Renderer
    {

        private Device device = null;
        public Device Device
        {
            get { return device; }
        }

        #region Constructor

        public Renderer(Control RenderScene)
        {

            //Presentation Parameters
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.AutoDepthStencilFormat = DepthFormat.D16;
            presentParams.EnableAutoDepthStencil = true;
            presentParams.PresentationInterval = PresentInterval.One;

            //Lets create the render device but we need to get their caps
            IEnumerator i = Manager.Adapters.GetEnumerator();
            while (i.MoveNext())
            {
                AdapterInformation ai = i.Current as AdapterInformation;
                int adapterOrdinal = ai.Adapter;
                Caps hardware = Manager.GetDeviceCaps(adapterOrdinal, DeviceType.Hardware);
                CreateFlags flags = CreateFlags.SoftwareVertexProcessing;

                if (hardware.DeviceCaps.SupportsHardwareTransformAndLight)
                {
                    flags = CreateFlags.HardwareVertexProcessing;
                }

                device = new Device(adapterOrdinal, hardware.DeviceType, RenderScene, flags, presentParams); //Create a device
                if (device != null)
                {
                    break;
                }
            }

            //For device reset we have to set some settings
            device.DeviceReset += new System.EventHandler(OnResetDevice);
            OnResetDevice(device, null);

        }

        #endregion

        #region Events

        public void OnResetDevice(object sender, EventArgs e)
        {

            Device dev = (Device)sender;

            dev.RenderState.CullMode = Cull.Clockwise;
            dev.RenderState.ZBufferEnable = true;

            if (dev.DeviceCaps.SourceBlendCaps.SupportsInverseSourceAlpha && dev.DeviceCaps.SourceBlendCaps.SupportsSourceAlpha)
            {
                dev.RenderState.SourceBlend = Blend.SourceAlpha;
                dev.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            }
            if (dev.DeviceCaps.TextureFilterCaps.SupportsMinifyLinear)
            {
                device.SamplerState[0].MinFilter = TextureFilter.Linear;
                device.SamplerState[1].MinFilter = TextureFilter.Linear;
            }
            if (dev.DeviceCaps.TextureFilterCaps.SupportsMagnifyLinear)
            {
                device.SamplerState[0].MagFilter = TextureFilter.Linear;
                device.SamplerState[1].MagFilter = TextureFilter.Linear;
            }
            if (dev.DeviceCaps.TextureFilterCaps.SupportsMipMapLinear)
            {
                device.SamplerState[0].MipFilter = TextureFilter.Linear;
                device.SamplerState[1].MipFilter = TextureFilter.Linear;
            }

            //Lighting
            dev.RenderState.Lighting = true;
            device.RenderState.Ambient = Color.FromArgb(0xFF, 0xFF, 0xFF);

        }

        #endregion

        #region Scene Functions

        public void BeginScene(Color BackBufferColor)
        {
            ClearBackbuffer(BackBufferColor);
            device.BeginScene();
        }

        public void ClearBackbuffer(Color BackBufferColor)
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, BackBufferColor, 1.0f, 0);
        }

        public void EndScene()
        {
            device.EndScene();
            device.Present();
        }

        #endregion

    }
}