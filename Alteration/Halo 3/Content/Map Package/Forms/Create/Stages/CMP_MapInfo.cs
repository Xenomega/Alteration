using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Alteration.Halo_3.Map_Package.Info;

namespace Alteration.Halo_3.Map_Package.Stages
{
    public partial class CMP_MapInfo : UserControl
    {
        private List<LanguageInformation> Language_Information;
        private CreateMapPackageForm _mappackageform;
        private SelectedLanguage Selected_Language;

        public CMP_MapInfo(CreateMapPackageForm mappackageform)
        {
            InitializeComponent();
            //Set our map package form
            MapPackageForm = mappackageform;
            //Set our Selected_Language
            Selected_Language = SelectedLanguage.English;
            //Initialize our language list
            Language_Information = new List<LanguageInformation>();
            //Initialize all languages.
            for (int i = 0; i < 10; i++)
            {
                Language_Information.Add(new LanguageInformation());
            }
            //Load information for this language
            LoadLanguageInformation();
        }

        public CreateMapPackageForm MapPackageForm
        {
            get { return _mappackageform; }
            set { _mappackageform = value; }
        }

        private void LoadLanguageInformation()
        {
            //Load our Map Name for this language
            txtMapName.Text = Language_Information[(int) Selected_Language].Name;
            //Load our Map Description for this language
            txtMapDescription.Text = Language_Information[(int) Selected_Language].Description;
        }

        private void txtMapName_TextChanged(object sender, EventArgs e)
        {
            //Set our map name for this language
            Language_Information[(int) Selected_Language].Name = txtMapName.Text;
        }

        private void txtMapDescription_TextChanged(object sender, EventArgs e)
        {
            //Set our map description for this language.
            Language_Information[(int) Selected_Language].Description = txtMapDescription.Text;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //Loop through all the languages
            for (int i = 0; i < Language_Information.Count; i++)
            {
                //Set the name for this language
                Language_Information[i].Name = txtMapName.Text;
                //Set the description for this language
                Language_Information[i].Description = txtMapDescription.Text;
            }
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.English;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnJapanese_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Japanese;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnGerman_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.German;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnFrench_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.French;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnSpanish_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Spanish;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnLatinSpanish_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.LatinAmericaSpanish;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnItalian_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Italian;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnKorean_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Korean;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnChinese_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Chinese;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnPortuguese_Click(object sender, EventArgs e)
        {
            //Set the selected language
            Selected_Language = SelectedLanguage.Portuguese;
            //Load language information
            LoadLanguageInformation();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Clear the controls.
            MapPackageForm.Controls.Clear();
            //Initialize a location control to locate a menu image and the map file.
            CMP_Locate cmp_locate = new CMP_Locate(MapPackageForm);
            //Add it to the map package form
            MapPackageForm.Controls.Add(cmp_locate);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            #region Set Info File

            //Set our MapPackageForm and our mapinfo information
            InfoFile infoFile = new InfoFile();
            //Set the map names.
            infoFile.EnglishName = Language_Information[(int) SelectedLanguage.English].Name;
            infoFile.JapaneseName = Language_Information[(int) SelectedLanguage.Japanese].Name;
            infoFile.GermanName = Language_Information[(int) SelectedLanguage.German].Name;
            infoFile.FrenchName = Language_Information[(int) SelectedLanguage.French].Name;
            infoFile.SpanishName = Language_Information[(int) SelectedLanguage.Spanish].Name;
            infoFile.LatinAmericaSpanishName = Language_Information[(int) SelectedLanguage.LatinAmericaSpanish].Name;
            infoFile.ItalianName = Language_Information[(int) SelectedLanguage.Italian].Name;
            infoFile.KoreanName = Language_Information[(int) SelectedLanguage.Korean].Name;
            infoFile.ChineseName = Language_Information[(int) SelectedLanguage.Chinese].Name;
            infoFile.PortugueseName = Language_Information[(int) SelectedLanguage.Portuguese].Name;
            //Set the map descriptions
            infoFile.EnglishDescription = Language_Information[(int) SelectedLanguage.English].Description;
            infoFile.JapaneseDescription = Language_Information[(int) SelectedLanguage.Japanese].Description;
            infoFile.GermanDescription = Language_Information[(int) SelectedLanguage.German].Description;
            infoFile.FrenchDescription = Language_Information[(int) SelectedLanguage.French].Description;
            infoFile.SpanishDescription = Language_Information[(int) SelectedLanguage.Spanish].Description;
            infoFile.LatinAmericanSpanishDescription =
                Language_Information[(int) SelectedLanguage.LatinAmericaSpanish].Description;
            infoFile.ItalianDescription = Language_Information[(int) SelectedLanguage.Italian].Description;
            infoFile.KoreanDescription = Language_Information[(int) SelectedLanguage.Korean].Description;
            infoFile.ChineseDescription = Language_Information[(int) SelectedLanguage.Chinese].Description;
            infoFile.PortugueseDescription = Language_Information[(int) SelectedLanguage.Portuguese].Description;
            //Set our map filename
            string mapfilenameshort = GetShortFormName(MapPackageForm.MapLocation);
            infoFile.MapFileName = mapfilenameshort;
            //Set our blf base name
            string blffilenameshort = "m_" + mapfilenameshort;
            infoFile.MapImageFileName = blffilenameshort;
            //Set the infoFile for the parentForm
            MapPackageForm.Info_File = infoFile;

            #endregion

            //Clear the controls.
            MapPackageForm.Controls.Clear();
            //Initialize a location control to locate a menu image and the map file.
            CMP_SaveProject cmp_saveproject = new CMP_SaveProject(MapPackageForm);
            //Add it to the map package form
            MapPackageForm.Controls.Add(cmp_saveproject);
        }

        private string GetShortFormName(string text)
        {
            string[] temp = text.Split('\\');
            string[] temp2 = temp[temp.Length - 1].Split('.');
            return temp[temp.Length - 1].Substring(0,
                                                   temp[temp.Length - 1].Length - (temp2[temp2.Length - 1].Length + 1));
        }

        #region Nested type: LanguageInformation

        private class LanguageInformation
        {
            private string _description;
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
        }

        #endregion

        #region Nested type: SelectedLanguage

        private enum SelectedLanguage
        {
            English = 0,
            Japanese = 1,
            German = 2,
            French = 3,
            Spanish = 4,
            LatinAmericaSpanish = 5,
            Italian = 6,
            Korean = 7,
            Chinese = 8,
            Portuguese = 9
        }

        #endregion
    }
}