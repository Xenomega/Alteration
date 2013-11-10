using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Alteration.Halo_3.Map_Package.Info;
using Alteration.Halo_3.Map_Package.Package_Classes;
using Alteration.Halo_3.Map_Package.Stages;
using Alteration.Halo_3.Map_Package.Package_Classes.Image_Editting;
using HaloDeveloper.Map;

namespace Alteration.Halo_3.Map_Package
{
    public partial class CreateMapPackageForm : Form
    {
        private Image _imagefile;
        private InfoFile _infofile;
        private string _maplocation;

        public CreateMapPackageForm()
        {
            InitializeComponent();
        }

        public InfoFile Info_File
        {
            get { return _infofile; }
            set { _infofile = value; }
        }

        public Image Image_File
        {
            get { return _imagefile; }
            set { _imagefile = value; }
        }

        public string MapLocation
        {
            get { return _maplocation; }
            set { _maplocation = value; }
        }

        private void CreateMapPackageForm_Load(object sender, EventArgs e)
        {
            //Initialize our welcome control
            CMP_Welcome welcomeControl = new CMP_Welcome(this);
            //Add our welcome control.
            Controls.Add(welcomeControl);
        }

        public void SaveProject(string directory, int mapID)
        {
            //Copy the .map files

            #region Map File Copy

            //Get our map short filename array
            string[] temp = MapLocation.Split('\\');
            string mapshortform = temp[temp.Length - 1];
            if (directory + mapshortform != MapLocation)
            {
                //If the file exists...
                if (File.Exists(directory + mapshortform))
                {
                    //Delete it.
                    File.Delete(directory + mapshortform);
                }
                //Copy the map to the save project directory
                File.Copy(MapLocation, directory + mapshortform);
            }

            //Open this map
            HaloMap map = new HaloMap(directory + mapshortform);

            //Create our tag indexer
            int scnr_index = -1;
            //Loop through each tag till we find our scnr
            for (int i = 0; i < map.IndexItems.Count; i++)
                if (map.IndexItems[i].Class == "scnr")
                {
                    scnr_index = i;
                    break;
                }

            //If our scnr index hasnt been set.
            if (scnr_index == -1)
                throw new Exception("Could not find [scnr] tag in the map file. Cannot continue.");

            //Open IO
            map.OpenIO();

            //Go to this tag's offset + 8
            map.IO.Out.BaseStream.Position = map.IndexItems[scnr_index].Offset + 8;

            //Write our map id
            map.IO.Out.Write(mapID);

            //Close IO
            map.CloseIO();
            #endregion

            //Save the blf's.

            #region BLF Images

            //Base bitmap
            BLFImageFile blfimage = new BLFImageFile();
            blfimage.BLFImage = Image_File;
            string blfStarterString = directory + "m_" + mapshortform.Substring(0, mapshortform.Length - 4);
            blfimage.SaveBLFImage(new FileStream(blfStarterString + ".blf", FileMode.Create),
                                  BLFImageFile.BLFImageType.BaseImage);
            blfimage.SaveBLFImage(new FileStream(blfStarterString + "_sm.blf", FileMode.Create),
                                  BLFImageFile.BLFImageType.Sm);
            BLFImageTransformer.BLF_ImageResults blfimageresults =
                BLFImageTransformer.CreateTransformedImages(Image_File);
            blfimage.BLFImage = blfimageresults.FilmBitmap;
            blfimage.SaveBLFImage(new FileStream(blfStarterString + "_film.blf", FileMode.Create),
                                  BLFImageFile.BLFImageType.Film);
            blfimage.BLFImage = blfimageresults.ClipBitmap;
            blfimage.SaveBLFImage(new FileStream(blfStarterString + "_clip.blf", FileMode.Create),
                                  BLFImageFile.BLFImageType.Clip);
            blfimage.BLFImage = blfimageresults.VariantBitmap;
            blfimage.SaveBLFImage(new FileStream(blfStarterString + "_variant.blf", FileMode.Create),
                                  BLFImageFile.BLFImageType.Variant);

            #endregion

            //Save the mapinfo

            #region Map Info

            Info_File.BLFHeader = new BLF_Header();
            Info_File.BLFHeader.Map_ID = mapID;
            Info_File.SaveInfoFile(new FileStream(directory + mapshortform + "info", FileMode.Create));

            #endregion

            //Done..
            MessageBox.Show("Done.");
        }
    }
}