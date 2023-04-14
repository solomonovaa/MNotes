using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Notes.Models;
using Xamarin.Forms;


namespace Notes.Views
{
    public partial class NotesPage : ContentPage
    {

        public NotesPage()
        {
            InitializeComponent();
        }
        //Коллекция избранных заметок
        public List<Note> favsNotes = new List<Note>();
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            var notes = new List<Note>();

            if (File.Exists(Path.Combine(App.FolderPath, "notes.xml")))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode node in xRoot)
                {
                    XmlNode xDate = node.Attributes.GetNamedItem("Date");
                    XmlNode xTitle = node.Attributes.GetNamedItem("Title");
                    XmlNode xId = node.Attributes.GetNamedItem("ID");
                    XmlNode xFavStatus = node.Attributes.GetNamedItem("FavoriteStatus");
                    XmlNode xText = node.SelectSingleNode("Text");
                    XmlNode xChords = node.SelectSingleNode("Chords");
                    XmlNode xBoys = node.SelectSingleNode("Boys");
                    if (xFavStatus.InnerText == "1")
                    {
                        favsNotes.Add(new Note
                        {
                            Id = xId.InnerText,
                            Title = xTitle.InnerText,
                            Text = xText.InnerText,
                            Chords = xChords.InnerText,
                            Boys = xBoys.InnerText,
                            Date = DateTime.Parse(xDate.InnerText)
                        });
                    }
                    notes.Add(new Note
                    {
                        Id = xId.InnerText,
                        Title = xTitle.InnerText,
                        Text = xText.InnerText,
                        Chords = xChords.InnerText,
                        Boys = xBoys.InnerText,
                        Date = DateTime.Parse(xDate.InnerText)
                    });
                }
            }
            collectionViewFavs.ItemsSource = favsNotes
            .OrderBy(d => d.Id)
            .ToList();
            collectionView.ItemsSource = notes
            .OrderBy(d => d.FavoriteStatus)
            .ToList();
            if (favsNotes.Count > 0)
            {
                collectionViewFavs.IsVisible = true;
            }
            else
            {
                collectionViewFavs.IsVisible = false;
            }
          

        }


        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }
        private void FavoriteButton_Clicked(object sender, EventArgs e)
        {
            collectionViewFavs.IsVisible = true;

        }


        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.Id}");
            }
        }




    }
}