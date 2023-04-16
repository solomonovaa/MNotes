﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
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
       
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            //Коллекция заметок
            var notes = new List<Note>();
            //Коллекция избранных заметок
            List<Note> favsNotes = new List<Note>();
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
                    //Добавляем заметку в список и в независимости от того избранная заметка или нет устанавливаем в значение
                    //FavoriteStatus путь к картинке unFavoriteImage
                    notes.Add(new Note
                    {
                        Id = xId.InnerText,
                        Title = xTitle.InnerText,
                        Text = xText.InnerText,
                        Date = DateTime.Parse(xDate.InnerText),
                        FavoriteStatus = "unFavoriteImage.jpg"
                    });
                    if (xFavStatus.InnerText == "1")
                    {   //Если у заметки стоит значение избранности = 1, то дополнительно добавляем её и в список избранных заметок
                        favsNotes.Add(new Note
                        {
                            Id = xId.InnerText,
                            Title = xTitle.InnerText,
                            Text = xText.InnerText,
                            Date = DateTime.Parse(xDate.InnerText),
                            FavoriteStatus= "favoriteImage.jpg"
                        });
                        //Так же меняем у последней добавленной заметки значение FavoriteStatus на путь к картинке favoriteImage
                        notes[notes.Count - 1].FavoriteStatus = "favoriteImage.jpg";
                    }
                   
                }
            }
            collectionViewFavs.ItemsSource = favsNotes
            .OrderBy(d => d.Id)
            .ToList();
            collectionView.ItemsSource = notes
            .OrderBy(d => d.Id)
            .ToList();
            // Пока не решила, нужен ли этот стек наверху, так что пусть пока будет так
            if (favsNotes.Count > 0)
            {
                collectionViewFavs.IsVisible = false;
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
            //Переводим object sender в ImageButton
            ImageButton favButton = (ImageButton)sender;
            //Через BindingContext получаем информацию к какой заметке относится эта кнопка
            var context = favButton.BindingContext;
            //Так как BindingContext выдал значение типа object, то его нужно перевести в заметку
            var selectedNote = context as Note;
            //Создаем списки избранных и обычных заметок
            List<Note> favsNotes = new List<Note>();
            List<Note> notes = new List<Note>();
            //Добавляем туда значения из CollectionView
            notes = collectionView.ItemsSource.Cast<Note>().ToList();
            favsNotes = collectionViewFavs.ItemsSource.Cast<Note>().ToList();
            //Получаем информацию о выбранной заметки
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xNode = xRoot.SelectSingleNode($"/Notes/Note[@ID={selectedNote.Id}]");
            XmlElement xNodeData = (XmlElement)xNode;
            if (xNodeData.Attributes.GetNamedItem("FavoriteStatus").InnerText == "0")
            {//Заметка была не избранной
             //изменяем ей аттрибут
                xNodeData.SetAttribute("FavoriteStatus", "1");
                favsNotes.Add(selectedNote);
                //Меняем у экземпляра заметки значение FavoriteStatus на путь к картинке favoriteImage
                selectedNote.FavoriteStatus = "favoriteImage.jpg";
                //Меняем картинку на нажатой кнопке
                favButton.Source = "favoriteImage.jpg";
                //Обновляем CollectionViewFavs
                collectionViewFavs.ItemsSource = favsNotes
                .OrderBy(d => d.Id)
                .ToList();
            }
            else
            {   //Заметка была избранной
                //меняем аттрибут
                xNodeData.SetAttribute("FavoriteStatus", "0");
                //По какой-то причине простой favsNotes.Remove(selectedNote) не работает
                //Он просто не удаляет объект из списка ¯\_(ツ)_/¯
                //Поэтому создаем переменную и находим элемент в списках избранных заметок по ID,
                //где у найденной заметке должен совпадать ID с выбранной заметкой
                var itemtoremove = favsNotes.Single(d=>d.Id==selectedNote.Id);
                //Удаляем найденую заметку
                favsNotes.Remove(itemtoremove);
                //Меняем у экземпляра заметки значение FavoriteStatus на путь к картинке unFavoriteImage
                selectedNote.FavoriteStatus = "unFavoriteImage.jpg";
                //Меняем картинку на нажатой кнопке
                favButton.Source = "unFavoriteImage.jpg";
                //Обновляем CollectionViewFavs
                collectionViewFavs.ItemsSource = favsNotes
                .OrderBy(d => d.Id)
                .ToList();
            }
            xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
            //Обновляем основной CollectionView
            collectionView.ItemsSource = notes
            .OrderBy(d => d.Id)
            .ToList();
            // Пока не решила, нужен ли этот стек наверху, так что пусть пока будет так
            if (favsNotes.Count > 0)
            {
                collectionViewFavs.IsVisible = false;
            }
            else
            {
                collectionViewFavs.IsVisible = false;
            }

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