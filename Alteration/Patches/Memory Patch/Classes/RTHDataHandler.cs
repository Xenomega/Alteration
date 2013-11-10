using System;
using HaloDevelopmentExtender;
using Alteration.Settings;

namespace Alteration.Patches.Memory_Patch.RTH_Data.Classes
{
    public abstract class RTHDataHandler
    {
        public static void PokeRTHData(RTHData RTH_Data)
        {
            //If our XDKName is set
            if (AppSettings.Settings.XDKName == "")
            {
                throw new Exception(
                    "The Xbox Development Kit could not be connected to because it's means of connection were not set.");
            }
            //Initialize our XDC360
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.XDKName);
            //Connect
            xdc.Connect();
            //Get our Xbox Memory Stream
            XboxMemoryStream xbms = xdc.ReturnXboxMemoryStream();
            //Initialize our Endian IO
            EndianIO IO = new EndianIO(xbms, EndianType.BigEndian);
            //Open our IO
            IO.Open();
            //Loop through every RTH Data Block
            for (int index = 0; index < RTH_Data.RTH_Data_Blocks.Count; index++)
            {
                //Go to that RTH Data Block's memory offset
                IO.Out.BaseStream.Position = RTH_Data.RTH_Data_Blocks[index].Memory_Offset;
                //Write our data
                IO.Out.Write(RTH_Data.RTH_Data_Blocks[index].Data_Block);
            }
            //Close our IO
            IO.Close();
            //Close our Xbox Memory Stream
            xbms.Close();
            //Disconnect from our Xbox Development Kit
            xdc.Disconnect();
        }
    }
}