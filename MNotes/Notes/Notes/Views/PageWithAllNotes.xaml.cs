/*
 * 1. Изменена модель FavoriteStatus (string) => IsFavoritePage (bool)
 * 2. Изменены списки: notes => commonNotes; favsNotes => favoriteNotes
 * 3. Добавлены проверки на null в xRoot, xDoc
 * 4. Изменены названия компонентов (файлов): NotesPage => PageWithAllNotes; FavoritePage => PageWithFavoriteNotes
 * 5. Поменял модель Note.cs. Где-то нужно добавить полей, Id должен быть числом,
 *    а проверять избранная заметка или нет нужно переменной bool isFavoriteNote.
 *
 * TODO: Сделать так, чтобы все работало: пофиксить имена, которые я изменил и т.д.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class PageWithAllNotes : ContentPage
    {
        public PageWithAllNotes()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            List<Note> commonNotes = new List<Note>();
            List<Note> favoriteNotes = new List<Note>();
            
            SearchBar.Text = "";
            base.OnAppearing();

            if (File.Exists(Path.Combine(App.FolderPath, "notes.xml")))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                XmlElement xRoot = xDoc.DocumentElement;
                if (xRoot != null)
                {
                    foreach (XmlNode xNode in xRoot)
                    {
                        if (xNode.Attributes == null) 
                            continue;

                        XmlNode xDate = xNode.Attributes.GetNamedItem("Date");
                        XmlNode xTitle = xNode.Attributes.GetNamedItem("Title");
                        XmlNode xId = xNode.Attributes.GetNamedItem("Id");
                        XmlNode xIsFavoriteNote = xNode.Attributes.GetNamedItem("IsFavoriteNote");

                        XmlNode xText = xNode.SelectSingleNode("Text");
                        XmlNode xChordsPatterns = xNode.SelectSingleNode("Chords");  // TODO: CHANGE
                        XmlNode xStrummingPatterns = xNode.SelectSingleNode("Boys");  // TODO: CHANGE

                        commonNotes.Add(new Note
                        {
                            Id = Int32.Parse(xId.InnerText),
                            Title = xTitle.InnerText,
                            Text = xText.InnerText,
                            Date = DateTime.Parse(xDate.InnerText),
                            IsFavoriteNote =                                   // TODO: CHANGE
                            FavoriteIcon = "unFavoriteOrange.jpg"              // TODO: CHANGE
                        });

                        /*
                         * Лучше проверять так: если IsFavoriteNode = True, то
                         * мы реализуем какую-то логику.
                         */
                        if (xIsFavoriteNote.InnerText == "1")
                        {
                            favoriteNotes.Add(new Note
                            {
                                Id = xId.InnerText,
                                Title = xTitle.InnerText,
                                Text = xText.InnerText,
                                Date = DateTime.Parse(xDate.InnerText),
                                FavoriteStatus = "favoriteOrange.jpg"
                            });
                            //Так же меняем у последней добавленной заметки значение FavoriteStatus на путь к картинке favoriteImage
                            commonNotes[commonNotes.Count - 1].FavoriteStatus =
                                "favoriteOrange.jpg";
                        }
                    }
                }
            }
            
            CommonNotesCollection.ItemsSource = commonNotes
                .OrderBy(d => d.Id)
                .ToList();
            
            FavoriteNotesCollection.ItemsSource = favoriteNotes
                .OrderBy(d => d.Id)
                .ToList();

            FavoriteNotesCollection.IsVisible = favoriteNotes.Count > 0;       // TODO: Is it necessary?
        }
        
        async void AddNoteClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }
        
        private void ToggleFavoriteStatusButton(object sender, EventArgs e)
        {
            //Переводим object sender в ImageButton
            ImageButton favoriteButtonImage = (ImageButton) sender;
            
            //Через BindingContext получаем информацию к какой заметке относится эта кнопка
            var context = favoriteButtonImage.BindingContext;
            
            //Так как BindingContext выдал значение типа object, то его нужно перевести в заметку
            var selectedNote = context as Note;
            
            /* (!)
             * Понятно, что мы создаем новые списки. А можно ли создать
             * какой-то общий массив для всех компонентов, например,
             * массив избранных заметок и обычных заметок, к которому
             * можно будет обращаться из любого компонента.
             * Мне кажется, для этого нужно создать новую модель, может быть
             * AppModel какую-нибудь, где и будут находиться эти
             * "глобальные" модельки. И не придется создавать новые коллекции.
             */
            List<Note> favsNotes = new List<Note>();
            List<Note> notes = new List<Note>();
            
            //Добавляем туда значения из CollectionView
            notes = CommonNotesCollection.ItemsSource.Cast<Note>().ToList();
            favsNotes = FavoriteNotesCollection.ItemsSource.Cast<Note>().ToList();
            
            //Получаем информацию о выбранной заметки
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode xNode = xRoot.SelectSingleNode($"/Notes/Note[@ID={selectedNote.Id}]");
            XmlElement xNodeData = (XmlElement)xNode;
            
            if (xNodeData.Attributes.GetNamedItem("FavoriteStatus").InnerText == "0")
            {
                //Заметка была не избранной
                //изменяем ей аттрибут
                xNodeData.SetAttribute("FavoriteStatus", "1");
                favsNotes.Add(selectedNote);
                
                //Меняем у экземпляра заметки значение FavoriteStatus на путь к картинке favoriteImage
                selectedNote.FavoriteStatus = "favoriteOrange.jpg";
                
                //Меняем картинку на нажатой кнопке
                favoriteButtonImage.Source = "favoriteOrange.jpg";
                
                //Обновляем CollectionViewFavs
                FavoriteNotesCollection.ItemsSource = favsNotes
                .OrderBy(d => d.Id)
                .ToList();
            }
            else
            {   
                //Заметка была избранной
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
                selectedNote.FavoriteStatus = "unFavoriteOrange.jpg";
                
                //Меняем картинку на нажатой кнопке
                favoriteButtonImage.Source = "unFavoriteOrange.jpg";
                
                //Обновляем CollectionViewFavs
                FavoriteNotesCollection.ItemsSource = favsNotes
                .OrderBy(d => d.Id)
                .ToList();
            }
            xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
            
            //Обновляем основной CollectionView
            CommonNotesCollection.ItemsSource = notes
            .OrderBy(d => d.Id)
            .ToList();
            
            // Пока не решила, нужен ли этот стек наверху, так что пусть пока будет так
            if (favsNotes.Count > 0)
            {
                FavoriteNotesCollection.IsVisible = false;
            }
            else
            {
                FavoriteNotesCollection.IsVisible = false;
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
        private string searchQuery;
        private void SearchNotes()
        {
            // Сщздаем лист заметок
            List<Note> notes = new List<Note>();
            // Если collectionView не пустая, заполняем лист содержимым из collectionView
            if (CommonNotesCollection.ItemsSource != null)
            { 
                notes = CommonNotesCollection.ItemsSource.Cast<Note>().ToList();
            }
            if (string.IsNullOrEmpty(searchQuery))
            {
                // Если строка поиска пустая, просто отображаем полный список заметок
                CommonNotesCollection.ItemsSource = notes;
            }
            else
            {
                // Иначе выполняем поиск по строке и отображаем только совпадающие заметки
                var filteredNotes = notes.Where(n => n.Title.Contains(searchQuery) || n.Text.Contains(searchQuery)).ToList();
                CommonNotesCollection.ItemsSource = filteredNotes;
            }
        }
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            searchQuery = e.NewTextValue;
            SearchNotes();
        }

        private async void DeleteOnMainPageButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Удаление заметки", "Вы уверены, что хотите удалить заметку?", "Да", "Отмена");

            if (answer)
            {
                //Переводим object sender в ImageButton
                ImageButton favButton = (ImageButton)sender;
                //Через BindingContext получаем информацию к какой заметке относится эта кнопка
                var context = favButton.BindingContext;
                //Так как BindingContext выдал значение типа object, то его нужно перевести в заметку
                var selectedNote = context as Note;
                //Создаем список заметок
                List<Note> notes = new List<Note>();
                //Добавляем туда значения из CollectionView
                notes = CommonNotesCollection.ItemsSource.Cast<Note>().ToList();
                //Получаем информацию о выбранной заметки
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                XmlElement xRoot = xDoc.DocumentElement;
                XmlNode xNode = xRoot.SelectSingleNode($"/Notes/Note[@ID={selectedNote.Id}]");
                XmlElement xNodeData = (XmlElement)xNode;
                notes.Remove(selectedNote);
                xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
                //Обновляем основной CollectionView
                CommonNotesCollection.ItemsSource = notes
                .OrderBy(d => d.Id)
                .ToList();

                //Проверяем новая ли заметка или же загруженная
                //Если у заметки есть ID, то заметка старая. В противном случае новая
                if (selectedNote.Id != null)
                {   //Находим заметку по ID
                    XmlNode xNote = xRoot.SelectSingleNode($"/Notes/Note[@ID={selectedNote.Id}]");
                    if (xNote != null)
                    {
                        //Если заметка надена, то удаляем
                        xRoot.RemoveChild(xNote);
                        xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
                    }
                }
            }
        }
    }
}