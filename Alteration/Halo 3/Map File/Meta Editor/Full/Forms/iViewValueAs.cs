using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HaloDeveloper.Map;
using Alteration.Halo_3.Meta_Editor.Controls;
using Alteration.Halo_3.Values;

namespace Alteration.Halo_3.Map_File.Meta_Editor.Forms
{
    public partial class iViewValueAs : Form
    {
        private int _offset;
        /// <summary>
        /// Our offset to view at.
        /// </summary>
        public int Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        private HaloMap _map;
        /// <summary>
        /// Our instance of the map class.
        /// </summary>
        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public iViewValueAs(HaloMap map, int offset)
        {
            InitializeComponent();

            //Set our instance of the offset
            Offset = offset;

            //Set our textbox offset
            txtItemOffset.Text = Offset.ToString();

            //Set our instance of the map class
            Map = map;

            RefreshValue();
        }

        private void iViewValueAs_Load(object sender, EventArgs e)
        {

        }

        private void txtItemOffset_TextChanged(object sender, EventArgs e)
        {
            //Set our offset.
            Offset = int.Parse(txtItemOffset.Text);
        }

        private void RefreshValue()
        {
            panel1.Controls.Clear();
            Map.OpenIO();
            try
            {

                mValue val = new mValue();
                val.Name = "Test";
                val.Offset = 0;
                val.Attributes = mValue.ObjectAttributes.Byte;
                iValue ival = new iValue(val);
                ival.LoadValue(Map, Offset);
                panel1.Controls.Add(ival);
                ival.Dock = DockStyle.Top;

                val.Attributes = mValue.ObjectAttributes.Int16;
                iValue ival2 = new iValue(val);
                ival2.LoadValue(Map, Offset);
                panel1.Controls.Add(ival2);
                ival2.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.UInt16;
                iValue ival3 = new iValue(val);
                ival3.LoadValue(Map, Offset);
                panel1.Controls.Add(ival3);
                ival3.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.Int32;
                iValue ival4 = new iValue(val);
                ival4.LoadValue(Map, Offset);
                panel1.Controls.Add(ival4);
                ival4.Dock = DockStyle.Top;



                val.Attributes = mValue.ObjectAttributes.UInt32;
                iValue ival5 = new iValue(val);
                ival5.LoadValue(Map, Offset);
                panel1.Controls.Add(ival5);
                ival5.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.Float;
                iValue ival6 = new iValue(val);
                ival6.LoadValue(Map, Offset);
                panel1.Controls.Add(ival6);
                ival6.Dock = DockStyle.Top;


                mBitmask8 valB = new mBitmask8();
                valB.Name = "Test";
                valB.Offset = 0;
                valB.Options = new List<mBitOption>();
                valB.Attributes = mValue.ObjectAttributes.Bitmask8;
                iBitmask ival7 = new iBitmask(valB);
                ival7.LoadValue(Map, Offset);
                panel1.Controls.Add(ival7);
                ival7.Dock = DockStyle.Top;


                mBitmask16 valB2 = new mBitmask16();
                valB2.Name = "Test";
                valB2.Offset = 0;
                valB2.Options = new List<mBitOption>();
                valB2.Attributes = mValue.ObjectAttributes.Bitmask16;
                iBitmask ival8 = new iBitmask(valB2);
                ival8.LoadValue(Map, Offset);
                panel1.Controls.Add(ival8);
                ival8.Dock = DockStyle.Top;


                mBitmask32 valB3 = new mBitmask32();
                valB3.Name = "Test";
                valB3.Offset = 0;
                valB3.Options = new List<mBitOption>();
                valB3.Attributes = mValue.ObjectAttributes.Bitmask32;
                iBitmask ival9 = new iBitmask(valB3);
                ival9.LoadValue(Map, Offset);
                panel1.Controls.Add(ival9);
                ival9.Dock = DockStyle.Top;



                val.Attributes = mValue.ObjectAttributes.String32;
                iValue ival11 = new iValue(val);
                ival11.LoadValue(Map, Offset);
                panel1.Controls.Add(ival11);
                ival11.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.String256;
                iValue ival12 = new iValue(val);
                ival12.LoadValue(Map, Offset);
                panel1.Controls.Add(ival12);
                ival12.Dock = DockStyle.Top;
    

                val.Attributes = mValue.ObjectAttributes.Unicode64;
                iValue ival13 = new iValue(val);
                ival13.LoadValue(Map, Offset);
                panel1.Controls.Add(ival13);
                ival13.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.Unicode256;
                iValue ival14 = new iValue(val);
                ival14.LoadValue(Map, Offset);
                panel1.Controls.Add(ival14);
                ival14.Dock = DockStyle.Top;


                mIdent valID = new mIdent();
                valID.Name = "Test";
                valID.Offset = 0;
                iIdent ival15 = new iIdent(valID);
                ival15.LoadValue(Map, Offset);
                panel1.Controls.Add(ival15);
                ival15.Dock = DockStyle.Top;


                val.Attributes = mValue.ObjectAttributes.StringID;
                iValue ival16 = new iValue(val);
                ival16.LoadValue(Map, Offset);
                panel1.Controls.Add(ival16);
                ival16.Dock = DockStyle.Top;

            }
            catch { }
            Map.CloseIO();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { Offset = int.Parse(txtItemOffset.Text); }
            catch { }
            RefreshValue();
        }
    }
}