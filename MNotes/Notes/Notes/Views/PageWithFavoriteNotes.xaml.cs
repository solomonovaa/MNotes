using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class PageWithFavoriteNotes : ContentPage
    {
        public PageWithFavoriteNotes()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();

            List<Note> favsNotesOnFavPage = new List<Note>();
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
                        favsNotesOnFavPage.Add(new Note
                        {
                            Id = xId.InnerText,
                            Title = xTitle.InnerText,
                            Text = xText.InnerText,
                            Date = DateTime.Parse(xDate.InnerText),
                            FavoriteStatus = "favoriteOrange.jpg"
                        });

                    }

                }
            }
            collectionViewOnFavPage.ItemsSource = favsNotesOnFavPage
            .OrderBy(d => d.Id)
            .ToList();
        }

        async void OnSelectionChangedFavPage(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.Id}");
            }
        }

        private void favoriteButtonOnFavPage_Clicked(object sender, EventArgs e)
        {
            //Переводим object sender в ImageButton
            ImageButton favButton = (ImageButton)sender;
            //Через BindingContext получаем информацию к какой заметке относится эта кнопка
            var context = favButton.BindingContext;
            //Так как BindingContext выдал значение типа object, то его нужно перевести в заметку
            var selectedNote = context as Note;
            //Создаем список заметок
            List<Note> favsNotes = new List<Note>();
            //Добавляем туда значения из CollectionView
            favsNotes = collectionViewOnFavPage.ItemsSource.Cast<Note>().ToList();
            //Получаем информацию о выбранной заметки
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xNode = xRoot.SelectSingleNode($"/Notes/Note[@ID={selectedNote.Id}]");
            XmlElement xNodeData = (XmlElement)xNode;
            //Создаем переменную и находим элемент в списках избранных заметок по ID,
            //где у найденной заметке должен совпадать ID с выбранной заметкой
            var itemtoremove = favsNotes.Single(d => d.Id == selectedNote.Id);
            //Удаляем найденую заметку
            favsNotes.Remove(itemtoremove);
            // Меняем статус удаленной заметки на 0
            xNodeData.Attributes.GetNamedItem("FavoriteStatus").InnerText = "0";
            //Обновляем CollectionViewFavs
            collectionViewOnFavPage.ItemsSource = favsNotes
            .OrderBy(d => d.Id)
            .ToList();
        
            xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
 
        }
    }

      
               
}