using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using C1.CollectionView;
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
        public bool isFavorite = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                string line1 = File.ReadLines(filename).First();

                notes.Add(new Note
                {
                    Filename = filename,
                    Title = line1,
                    IsFavorite = isFavorite,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }
            collectionView.ItemsSource = notes
            .OrderBy(d => d.Date)
            .ToList();

        }


        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }


        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.Filename}");
            }
        }


        private void FavoriteButton_Clicked(object sender, EventArgs e)
        {
            isFavorite = true;

        }

    }
}

    