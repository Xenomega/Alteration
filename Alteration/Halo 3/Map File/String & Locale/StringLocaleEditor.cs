using System;
using System.Windows.Forms;
using HaloDeveloper.Locale;
using HaloDeveloper.Map;
using System.Security.Cryptography;

namespace Alteration.Halo_3.String___Locale
{
    public partial class StringLocaleEditor : UserControl
    {
        private LocaleHandler _localehandler;
        private HaloMap _map;
        public string LocaleFilter = "";
        public string StringFilter = "";

        public StringLocaleEditor(HaloMap map)
        {
            InitializeComponent();
            //Set our HaloMap class instance as the one provided.
            Map = map;
            //Initialize our Locale Handler with our HaloMap instance.
            Locale_Handler = new LocaleHandler(Map);
            //Select our first language, causing it to load.
            cmbxLanguage.SelectedIndex = 0;
            //Focus on our localeList
            localeGrid.Focus();
            //Load our Strings
            LoadStrings(StringFilter);
        }

        public HaloMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public LocaleHandler Locale_Handler
        {
            get { return _localehandler; }
            set { _localehandler = value; }
        }

        private void LoadLocales(string filter)
        {
            //Set our selected Language
            int selectedLanguage = cmbxLanguage.SelectedIndex;

            //Get our LocaleTable
            LocaleHandler.LocaleTable selectedTable = Locale_Handler.LocaleTables[selectedLanguage];

            //Set our locale count textbox's text.
            txtLocaleCount.Text = selectedTable.LocaleCount.ToString();
            //Set our locale table offset textbox's text.
            txtLocaleTableOffset.Text = selectedTable.LocaleTableOffset.ToString();
            //Set our locale table size textbox's text.
            txtLocaleTableSize.Text = selectedTable.LocaleTableSize.ToString();
            //Set our locale table indexoffset textbox's text.
            txtLocaleIndexOffset.Text = selectedTable.LocaleTableIndexOffset.ToString();

            //Clear all items in the grid.
            localeGrid.Items.Clear();

            //Loop through all locale's for this table.
            for (int i = 0; i < selectedTable.LocaleCount; i++)
            {
                //If our locale item has the text in our filter.
                if (selectedTable.LocaleStrings[i].Name.ToLower().Contains(filter.ToLower()))
                {
                    //Initialize a new ListViewItem
                    ListViewItem localeItem = new ListViewItem();
                    //Set it's index.
                    localeItem.Text = i.ToString();
                    //Set it's name
                    localeItem.SubItems.Add(selectedTable.LocaleStrings[i].Name);
                    //Set it's offset
                    localeItem.SubItems.Add(selectedTable.LocaleStrings[i].Offset.ToString());
                    //Set it's length
                    localeItem.SubItems.Add(selectedTable.LocaleStrings[i].Length.ToString());
                    //Add it to the grid
                    localeGrid.Items.Add(localeItem);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cmbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLocales(LocaleFilter);
        }

        private void LoadStrings(string filter)
        {
            //Set our stringCount
            txtStringsCount.Text = Map.StringTable.StringCount.ToString();
            //Set our string Offset Table
            txtStringTableOffset.Text = Map.MapHeader.stringTableOffset.ToString();
            //Set our string Table Size
            txtStringTableSize.Text = Map.MapHeader.stringTableSize.ToString();
            //Set our string Index Offset Table
            txtStringTableIndexOffset.Text = Map.MapHeader.stringTableIndexOffset.ToString();


            //Clear all items in our string Grid
            stringsGrid.Items.Clear();
            //Loop through each of our strings...
            for (int stringIndex = 0; stringIndex < Map.StringTable.StringCount; stringIndex++)
            {
                //If the string's name contains our filter...
                if (Map.StringTable.StringItems[stringIndex].Name.Contains(filter))
                {
                    //Initialize a new listViewItem
                    ListViewItem listStringItem = new ListViewItem();
                    //Set our list item's index
                    listStringItem.Text = stringIndex.ToString();
                    //Set our list item's name
                    listStringItem.SubItems.Add(Map.StringTable.StringItems[stringIndex].Name);
                    //Set our list item's offset
                    listStringItem.SubItems.Add(Map.StringTable.StringItems[stringIndex].Offset.ToString());
                    //Set our list item's length
                    listStringItem.SubItems.Add(Map.StringTable.StringItems[stringIndex].Length.ToString());
                    //Add it to our stringGrid
                    stringsGrid.Items.Add(listStringItem);
                }
            }
        }

        private void localeGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If we have an item selected
            if (localeGrid.SelectedItems.Count != 0)
            {
                //Set the selected locale text to it.
                txtSelectedLocale.Text = localeGrid.SelectedItems[0].SubItems[1].Text;
                //Set the maximum text length you can have in that textBox
                //txtSelectedLocale.MaxLength = int.Parse(localeGrid.SelectedItems[0].SubItems[3].Text);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //If we have an item selected
            if (localeGrid.SelectedItems.Count != 0)
            {
                bool SameLength = txtSelectedLocale.Text.Length == localeGrid.SelectedItems[0].SubItems[1].Text.Length;
                //Get out selected locale's index
                int selectedLocaleIndex = int.Parse(localeGrid.SelectedItems[0].Text);
                Locale_Handler.SaveLocale(cmbxLanguage.SelectedIndex, selectedLocaleIndex, txtSelectedLocale.Text);
                //Set our text
                localeGrid.SelectedItems[0].SubItems[1].Text = txtSelectedLocale.Text;
                
                //If the length wasn't the same.
                if (!SameLength)
                {
                    //Reload our Locale table
                    Locale_Handler = new LocaleHandler(Map);
                    //Set our table info text

                    //Set our selected Language
                    int selectedLanguage = cmbxLanguage.SelectedIndex;

                    //Get our LocaleTable
                    LocaleHandler.LocaleTable selectedTable = Locale_Handler.LocaleTables[selectedLanguage];

                    //Set our locale count textbox's text.
                    txtLocaleCount.Text = selectedTable.LocaleCount.ToString();
                    //Set our locale table offset textbox's text.
                    txtLocaleTableOffset.Text = selectedTable.LocaleTableOffset.ToString();
                    //Set our locale table size textbox's text.
                    txtLocaleTableSize.Text = selectedTable.LocaleTableSize.ToString();
                    //Set our locale table indexoffset textbox's text.
                    txtLocaleIndexOffset.Text = selectedTable.LocaleTableIndexOffset.ToString();

                    //Set our items length.
                    localeGrid.SelectedItems[0].SubItems[3].Text = selectedTable.LocaleStrings[selectedLocaleIndex].Length.ToString();

                    //Loop for every item in the list
                    for (int i = localeGrid.SelectedItems[0].Index; i < localeGrid.Items.Count; i++)
                    {
                        localeGrid.Items[i].SubItems[2].Text = selectedTable.LocaleStrings[i].Offset.ToString();
                    }
                }
                #region Old
                /*
                //Open our IO
                Map.OpenIO();
                //Get our Locale Table
                LocaleHandler.LocaleTable selectedTable = Locale_Handler.LocaleTables[cmbxLanguage.SelectedIndex];
                //Go to that locale's position
                Map.IO.Out.BaseStream.Position = selectedTable.LocaleTableOffset +
                                                 selectedTable.LocaleStrings[selectedLocaleIndex].Offset;
                //Write our new locale.
                Map.IO.Out.WriteAsciiString(txtSelectedLocale.Text, txtSelectedLocale.MaxLength);
                //Go back to the begining of the table
                Map.IO.SeekTo(selectedTable.LocaleTableOffset);
                byte[] hash = SHA1.Create().ComputeHash(Map.IO.In.ReadBytes(
                    selectedTable.LocaleTableSize));
                //Seek to where the hash is
                Map.IO.SeekTo(selectedTable.Offset + 0x24);
                //Write it
                Map.IO.Out.Write(hash);
                //Update our text
                localeGrid.SelectedItems[0].SubItems[1].Text = txtSelectedLocale.Text;
                selectedTable.LocaleStrings[selectedLocaleIndex].Name = txtSelectedLocale.Text;
                //Close our IO
                Map.CloseIO();*/
                #endregion
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            //Set our filter
            LocaleFilter = txtFilter.Text;
            //Load the locaels.
            LoadLocales(LocaleFilter);
        }

        private void btnStringApplyFilter_Click(object sender, EventArgs e)
        {
            //Set our filter
            StringFilter = txtStringFilter.Text;
            //Load the strings.
            LoadStrings(StringFilter);
        }
    }
}