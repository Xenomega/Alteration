using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;

namespace HaloDeveloper.Raw.ModelRaw
{
    public class MessageConsole
    {

        List<ConsoleMessage> messages = new List<ConsoleMessage>();
        Font font;
        System.Drawing.Color DefaultColor = System.Drawing.Color.Gold;
        public System.Drawing.Point DrawingLocation = new System.Drawing.Point(0, 0);

        public MessageConsole(Control ctrl)
        {

            DrawingLocation = new System.Drawing.Point(10, ctrl.Height);

            System.Drawing.Font drawingFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 9f);
            font = new Font(RenderHelper.renderer.Device, drawingFont);

            AddMessage("Message System Initilized");
        }

        public void AddMessage(string Message)
        {
            messages.Add(new ConsoleMessage(Message, DefaultColor));
        }

        public void AddMessage(string Message, double DisplayTime)
        {
            messages.Add(new ConsoleMessage(Message, DisplayTime, DefaultColor));
        }

        public void AddMessage(string Message, System.Drawing.Color Color)
        {
            messages.Add(new ConsoleMessage(Message, Color));
        }

        public void Update()
        {

            List<int> removeIndexs = new List<int>();
            ConsoleMessage[] tempMessages = messages.ToArray();
            foreach (ConsoleMessage message in tempMessages)
            {
                message.Update();
                if (message.timeToRemove)
                    removeIndexs.Add(messages.IndexOf(message));
            }

            //Remove all the messages that are complete
            for (int x = removeIndexs.Count - 1; x >= 0; x--)
            {
                messages.RemoveAt(removeIndexs[x]);
            }

        }

        public void Render()
        {

            float paddingTop = 1f;
            float paddingLeft = 5f;
            float tempY = 10f;
            tempY += font.MeasureString(null, "nothing here", DrawTextFormat.None, System.Drawing.Color.Black).Height + paddingTop;
            ConsoleMessage[] tempMessages = messages.ToArray();
            foreach (ConsoleMessage message in tempMessages)
            {
                font.DrawText(null, message.message, DrawingLocation.X + (int)paddingLeft, DrawingLocation.Y - (int)tempY, message.DrawingColor);
                tempY += font.MeasureString(null, message.message, DrawTextFormat.None, message.DrawingColor).Height + paddingTop;
            }

        }

        public class ConsoleMessage
        {

            public string message = "";
            public bool timeToRemove = false;
            DateTime start;
            double totalSecondsToShow = 2.0;
            double fadeTime = 0.5;
            DateTime fadeStartTime;
            bool beginFade = false;
            public System.Drawing.Color DrawingColor = System.Drawing.Color.White;

            public ConsoleMessage(string Message)
            {
                message = Message;
                start = DateTime.Now;
            }

            public ConsoleMessage(string Message, double DisplayTime, System.Drawing.Color Color)
            {
                message = Message;
                DrawingColor = Color;
                totalSecondsToShow = DisplayTime;
                start = DateTime.Now;
            }

            public ConsoleMessage(string Message, System.Drawing.Color Color)
            {

                message = Message;
                DrawingColor = Color;
                start = DateTime.Now;

            }

            public void Update()
            {

                if (!beginFade)
                {
                    TimeSpan temp = DateTime.Now.Subtract(start);
                    if (temp.TotalSeconds >= totalSecondsToShow)
                    {
                        beginFade = true;
                        fadeStartTime = DateTime.Now;
                    }
                }
                else
                {
                    TimeSpan temp = DateTime.Now.Subtract(fadeStartTime);
                    if (temp.TotalSeconds >= fadeTime)
                    {
                        timeToRemove = true;
                    }
                    else
                    {
                        int alpha = 255 - (int)((temp.TotalSeconds / fadeTime) * 255);
                        DrawingColor = System.Drawing.Color.FromArgb(alpha, DrawingColor);
                    }
                }

            }

        }

    }
}
