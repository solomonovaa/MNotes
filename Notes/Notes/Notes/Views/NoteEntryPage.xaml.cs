using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {

        // Перечисление объектов 
        enum typesOfObjects
        {
            guitar,
            piano,
            boy
        }
        //Структура для хранения информации об аккордах
        struct chordsStruct
        {
            public string id;
            public typesOfObjects chordsType;
            public string chordsButtonName;
        }

        //Список, который хранит аккордные кнопки
        private List<Button> chordsButtonList = new List<Button>();

        //Словарь, который хранит структуру аккордов
        private Dictionary<string, chordsStruct> chordsDictionary = new Dictionary<string, chordsStruct>
        {
            //Гитара
            {"File: a.png", new chordsStruct { id="1", chordsType = typesOfObjects.guitar, chordsButtonName = "Aa" }},
            {"File: am.png", new chordsStruct { id = "2", chordsType = typesOfObjects.guitar, chordsButtonName = "Aam" }},
            {"File: a7.png", new chordsStruct { id = "3", chordsType = typesOfObjects.guitar, chordsButtonName = "Aa7" }},
            {"File: b.png", new chordsStruct { id = "4", chordsType = typesOfObjects.guitar, chordsButtonName = "Bb" }},
            {"File: bm.png", new chordsStruct { id = "5", chordsType = typesOfObjects.guitar, chordsButtonName = "Bbm" }},
            {"File: b7.png", new chordsStruct { id = "6", chordsType = typesOfObjects.guitar, chordsButtonName = "Bb7" }},
            {"File: c.png", new chordsStruct { id = "7", chordsType = typesOfObjects.guitar, chordsButtonName = "Cc" }},
            {"File: cm.png", new chordsStruct { id = "8", chordsType = typesOfObjects.guitar, chordsButtonName = "Ccm" }},
            {"File: d.png", new chordsStruct { id = "9", chordsType = typesOfObjects.guitar, chordsButtonName = "Dd" }},
            {"File: dm.png", new chordsStruct { id = "10", chordsType = typesOfObjects.guitar, chordsButtonName = "Ddm" }},
            {"File: e.png", new chordsStruct { id = "11", chordsType = typesOfObjects.guitar, chordsButtonName = "Ee" }},
            {"File: e7.png", new chordsStruct { id = "12", chordsType = typesOfObjects.guitar, chordsButtonName = "Ee7" }},
            {"File: em.png", new chordsStruct { id = "13", chordsType = typesOfObjects.guitar, chordsButtonName = "Eem" }},
            {"File: f.png", new chordsStruct { id = "14", chordsType = typesOfObjects.guitar, chordsButtonName = "Ff" }},
            {"File: fm.png", new chordsStruct { id = "15", chordsType = typesOfObjects.guitar, chordsButtonName = "Ffm" }},
            {"File: g.png", new chordsStruct { id = "16", chordsType = typesOfObjects.guitar, chordsButtonName = "Gg" }},
            {"File: gm.png", new chordsStruct { id = "17", chordsType = typesOfObjects.guitar, chordsButtonName = "Ggm" }},

            //Пианино
            {"File: PA.png", new chordsStruct { id = "18", chordsType = typesOfObjects.piano, chordsButtonName = "Aa" }},
            {"File: PAm.png", new chordsStruct { id = "19", chordsType = typesOfObjects.piano, chordsButtonName = "Aam" }},
            {"File: PA7.png", new chordsStruct { id = "20", chordsType = typesOfObjects.piano, chordsButtonName = "Aa7" }},
            {"File: PB.png", new chordsStruct { id = "21", chordsType = typesOfObjects.piano, chordsButtonName = "Bb" }},
            {"File: PBm.png", new chordsStruct { id = "22", chordsType = typesOfObjects.piano, chordsButtonName = "Bbm" }},
            {"File: PB7.png", new chordsStruct { id = "23", chordsType = typesOfObjects.piano, chordsButtonName = "Bb7" }},
            {"File: PC.png", new chordsStruct { id = "24", chordsType = typesOfObjects.piano, chordsButtonName = "Cc" }},
            {"File: PCm.png", new chordsStruct { id = "25", chordsType = typesOfObjects.piano, chordsButtonName = "Ccm" }},
            {"File: PD.png", new chordsStruct { id = "26", chordsType = typesOfObjects.piano, chordsButtonName = "Dd" }},
            {"File: PDm.png", new chordsStruct { id = "27", chordsType = typesOfObjects.piano, chordsButtonName = "Ddm" }},
            {"File: PE.png", new chordsStruct { id = "28", chordsType = typesOfObjects.piano, chordsButtonName = "Ee" }},
            {"File: PE7.png", new chordsStruct { id = "29", chordsType = typesOfObjects.piano, chordsButtonName = "Ee7" }},
            {"File: PEm.png", new chordsStruct { id = "30", chordsType = typesOfObjects.piano, chordsButtonName = "Eem" }},
            {"File: PF.png", new chordsStruct { id = "31", chordsType = typesOfObjects.piano, chordsButtonName = "Ff" }},
            {"File: PFm.png", new chordsStruct { id = "32", chordsType = typesOfObjects.piano, chordsButtonName = "Ffm" }},
            {"File: PG.png", new chordsStruct { id = "33", chordsType = typesOfObjects.piano, chordsButtonName = "Gg" }},
            {"File: PGm.png", new chordsStruct { id = "34", chordsType = typesOfObjects.piano, chordsButtonName = "Ggm" }}

        };
        //Список, который хранит добавленные аккорды
        private List<string> addedChordsList = new List<string>();

        //Словарь, который хранит гитарные бои
        private Dictionary<string, string> boysDictionary = new Dictionary<string, string>
        {
            {"File: BDown.png", "35"},
            {"File: MDown.png", "36"},
            {"File: BUp.png", "37"},
            {"File: MUp.png", "38" },
            {"File: cross.png", "39" }
        };
        //Список, который хранит добавленые гитарные бои
        private List<string> addedBoysList = new List<string>();

        //Словарь, который хранит уникальные номера всех изображений
        private Dictionary<string, string> imagesDictionary = new Dictionary<string, string>
        {
            //Гитара
            {"1","File: a.png"},
            {"2","File: am.png"},
            {"3","File: a7.png"},
            {"4","File: b.png"},
            {"5","File: bm.png"},
            {"6","File: b7.png"},
            {"7","File: c.png"},
            {"8","File: cm.png"},
            {"9","File: d.png"},
            {"10","File: dm.png"},
            {"11","File: e.png"},
            {"12","File: e7.png"},
            {"13","File: em.png"},
            {"14","File: f.png"},
            {"15","File: fm.png"},
            {"16","File: g.png"},
            {"17","File: gm.png"},
            //Пианино
            {"18","File: PA.png"},
            {"19","File: PAm.png"},
            {"20","File: PA7.png"},
            {"21","File: PB.png"},
            {"22","File: PBm.png"},
            {"23","File: PB7.png"},
            {"24","File: PC.png"},
            {"25","File: PCm.png"},
            {"26","File: PD.png"},
            {"27","File: PDm.png"},
            {"28","File: PE.png"},
            {"29","File: PE.png"},
            {"30","File: PEm.png"},
            {"31","File: PF.png"},
            {"32","File: PFm.png"},
            {"33","File: PG.png"},
            {"34","File: PGm.png"},
            //Бой
            {"35","File: BDown.png"},
            {"36","File: MDown.png"},
            {"37","File: BUp.png"},
            {"38","File: MUp.png"},
            {"39","File: cross.png"},
        };

        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteEntryPage()
        {
            InitializeComponent();
            BindingContext = new Note();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        //Добавляет объект в стэк
        async void AddObjectToStack(typesOfObjects objectType, StackLayout stack, string buttonName, string fileSource)
        {
            Image image = new Image();
            if (buttonName != null)
            {
                //Через это Object можно найти кнопку по имени
                Object Object = new Object();
                Object = FindByName(buttonName);
                //Если наш Object нашел объект типа кнопки, то преобразовываем его в кнопку и работаем с ней
                if (Object is Button)
                {
                    Button pressedButton = (Button)Object;
                    pressedButton.BackgroundColor = Color.DarkOrange;
                    pressedButton.BorderColor = Color.Brown;
                    chordsButtonList.Add(pressedButton);
                }
            }

            //Если эта функция вызывается при загрузке заметки, то значит, что в fileSource идет значение из словаря,
            //которое не подходит для параметра Source в Image. Так как имеет приписку File:, его нужно обязательно удалить, что и делаем
            if (fileSource.StartsWith("File: "))
            {
                fileSource = fileSource.Substring(6);
            }

            image.Source = fileSource;
            //Устанавливаем размер изображений в зависимости от типа объекта
            switch (objectType)
            {
                case typesOfObjects.guitar:
                    image.WidthRequest = 80;
                    image.HeightRequest = 100;
                    break;
                case typesOfObjects.piano:
                    image.HeightRequest = 50;
                    image.WidthRequest = 100;
                    break;
                case typesOfObjects.boy:
                    image.HeightRequest = 40;
                    image.WidthRequest = 40;
                    break;
                default:
                    break;
            }
            //для анимации устанавливаем параметр прозрачности картинки 0
            image.Opacity = 0;
            stack.Children.Add(image);
            //Простая анимация появления картинки
            await image.FadeTo(1, 400, Easing.Linear);
            //Добавляем данные в словари
            if (objectType == typesOfObjects.boy)
            {
                addedBoysList.Add(boysDictionary[image.Source.ToString()]);
            }
            else
            {
                addedChordsList.Add(chordsDictionary[image.Source.ToString()].id);
            }

        }
        //Обновляет данные в Xml файле
        void UpdateNoteDataInXmlFile(XmlDocument xDoc, XmlNode xNode, string title, string text, List<string> addedChords, List<string> addedBoys, DateTime dateTime)
        {
            //Так как в функцию передается элемент типа XmlNode её нужно преобразовать в XmlElement
            //Из-за того, что с Node особо ничего нельзя сделать
            XmlElement xNodeData = (XmlElement)xNode;
            //Объединяем добавленные аккорды из списка через разделитель ";"
            string chords = string.Join(";", addedChords);
            //Объединяем добавленные гитарные бои из списка через разделитель ";"
            string boys = string.Join(";", addedBoys);
            //Обновляем аттрибут названия заметки
            xNodeData.SetAttribute("Title", title);
            //Обновляем текст в элементе для хранения текста песни
            xNodeData.SelectSingleNode("Text").InnerText = text;
            //Обновляем текст в элементе для хранения аккордов
            xNodeData.SelectSingleNode("Chords").InnerText = chords;
            //Обновляем текст в элементе для хранения гитарных боев
            xNodeData.SelectSingleNode("Boys").InnerText = boys;
            //Сохраняем заметку
            xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
        }
        //Добавляет данные в Xml файл
        void AddNoteDataToXmlFile(string title, string text, List<string> addedChords, List<string> addedBoys, DateTime dateTime)
        {
            //Создаем объект для работы с Xml документом
            XmlDocument xDoc = new XmlDocument();
            //Загружаем сам Xml документ
            xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
            //Создаем элемент для работы с корнем Xml документа
            XmlElement xRoot = xDoc.DocumentElement;
            //Создаем элемент нашей заметки
            XmlElement xNoteEl = xDoc.CreateElement("Note");
            //Создаем аттрибут, который будет хранить уникальный ID заметки
            XmlAttribute xNoteId = xDoc.CreateAttribute("ID");
            //Создаем аттрибут, который будет хранитю дату создания заметки
            XmlAttribute xNoteDate = xDoc.CreateAttribute("Date");
            //Создаем аттрибут, который будет хранить название заметки
            XmlAttribute xNoteTitle = xDoc.CreateAttribute("Title");
            //Создаем аттрибут, который будет хранить информацию о добавление заметки в избранное
            XmlAttribute xNoteFavStatus = xDoc.CreateAttribute("FavoriteStatus");
            //Получаем аттрибут хранящий кол-во созданных заметок с корневого элемента
            //На основе этого создается уникальный ID заметки
            XmlNode xRootAtrNotesCount = xRoot.Attributes.GetNamedItem("notesCreatedCount");
            int noteCount = int.Parse(xRootAtrNotesCount.InnerText);
            //Создаем элементы для хранения данных заметки
            //Текст
            XmlElement xNoteText = xDoc.CreateElement("Text");
            //Аккорды
            XmlElement xNoteChords = xDoc.CreateElement("Chords");
            //Гитарные бои
            XmlElement xNoteBoys = xDoc.CreateElement("Boys");
            //Добавляем в ранее созданый элемент текст заметки
            xNoteText.AppendChild(xDoc.CreateTextNode(text));
            //Объединяем добавленные аккорды из списка через разделитель ";"
            string chords = string.Join(";", addedChords);
            //Добавляем в ранее созданый элемент аккорды
            xNoteChords.AppendChild(xDoc.CreateTextNode(chords));
            //Объединяем добавленные гитарные бои из списка через разделитель ";"
            string boys = string.Join(";", addedBoys);
            //Добавляем в ранее созданый элемент гитарные бои
            xNoteBoys.AppendChild(xDoc.CreateTextNode(boys));
            //Добавляем уникальный ID на основе кол-во созданных заметок
            xNoteId.AppendChild(xDoc.CreateTextNode(noteCount.ToString()));
            //Добавляем дату в аттрибут
            xNoteDate.AppendChild(xDoc.CreateTextNode(dateTime.ToString()));
            //Добавляем название в аттрибут
            xNoteTitle.AppendChild(xDoc.CreateTextNode(title));
            //Добавляем в элемент статус избраности, изначально значение 0. Если заметка в избранном, то 1
            //Пока не используется
            xNoteFavStatus.AppendChild(xDoc.CreateTextNode("0"));

            //Добавляем аттрибуты заметку
            xNoteEl.Attributes.Append(xNoteId);
            xNoteEl.Attributes.Append(xNoteFavStatus);
            xNoteEl.Attributes.Append(xNoteDate);
            xNoteEl.Attributes.Append(xNoteTitle);

            //Добавляем элементы в заметку
            xNoteEl.AppendChild(xNoteText);
            xNoteEl.AppendChild(xNoteChords);
            xNoteEl.AppendChild(xNoteBoys);

            //Увеличиваем кол-во созданых заметок
            noteCount++;
            //Обновляем аттрибут с кол-вом созданых заметок
            xRoot.SetAttribute("notesCreatedCount", noteCount.ToString());
            //Добавляем в документ готовую заметку
            xRoot.AppendChild(xNoteEl);
            //Сохраняем всё в файл
            xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
        }

        // удаляет картинку аккорда из стека
        private void DeleteChord(object sender, EventArgs e)
        {
            if (stackForImages.Children.Count > 0)
            {
                DeleteChordButton.FontSize = 15;
                DeleteChordButton.Text = "Удалить аккорд";
                stackForImages.Children.Remove(stackForImages.Children.Last());

                //Записываем нашу последную кнопку в листе для проверки
                Button buttonForCheck = chordsButtonList[chordsButtonList.Count - 1];
                //Удаляем кнопку
                chordsButtonList.RemoveAt(chordsButtonList.Count - 1);
                //Если больше таких кнопок нету в листе, то возвращаем кнопке оригинальный цвет
                if (!chordsButtonList.Contains(buttonForCheck))
                {
                    buttonForCheck.BackgroundColor = Color.FromHex("eecc8d");
                    buttonForCheck.BorderColor = Color.FromHex("f08b33");
                }
                addedChordsList.RemoveAt(addedChordsList.Count - 1);

            }
            else if (stackForImages.Children.Count == 0)
            {
                DeleteChordButton.FontSize = 12;
                DeleteChordButton.Text = "Удалять нечего, дебил, зачем тыкаешь???";
            }
        }
        // Загрузка заметки, при открывании страницы
        void LoadNote(string noteID)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                XmlElement xRoot = xDoc.DocumentElement;
                //Находим нашу заметку по уникальному аттрибуту ID
                XmlNode xNote = xRoot.SelectSingleNode($"/Notes/Note[@ID={noteID}]");
                if (xNote != null)
                {
                    //Получаем аттрибуты нашей заметки
                    XmlNode xDate = xNote.Attributes.GetNamedItem("Date");
                    XmlNode xTitle = xNote.Attributes.GetNamedItem("Title");
                    //Получаем данные заметки
                    XmlNode xText = xNote.SelectSingleNode("Text");
                    XmlNode xChords = xNote.SelectSingleNode("Chords");
                    XmlNode xBoys = xNote.SelectSingleNode("Boys");
                    //Проверяем пустая ли строка с аккордами, если нет, то делим данные в строке по разделителю ";"
                    //После добавляем аккорд в стэк
                    if (!string.IsNullOrEmpty(xChords.InnerText))
                    {
                        string[] loadedChordsSplited = xChords.InnerText.Split(';');
                        foreach (string chord in loadedChordsSplited)
                        {
                            AddObjectToStack(chordsDictionary[imagesDictionary[chord]].chordsType, stackForImages, chordsDictionary[imagesDictionary[chord]].chordsButtonName, imagesDictionary[chord]);
                        }
                    }
                    //Проверяем пустая ли строка с боями, если нет, то делим данные в строке по разделителю ";"
                    //После добавляем бой в стэк
                    if (!string.IsNullOrEmpty(xBoys.InnerText))
                    {
                        string[] loadedBoysSplited = xBoys.InnerText.Split(';');
                        foreach (string boy in loadedBoysSplited)
                        {
                            AddObjectToStack(typesOfObjects.boy, stackForBoy, null, imagesDictionary[boy]);
                        }
                    }
                    //Добавляем значения
                    Note note = new Note
                    {
                        Id = noteID,
                        Title = xTitle.InnerText,
                        Text = xText.InnerText,
                        Date = DateTime.Parse(xDate.InnerText)
                    };
                    BindingContext = note;
                    //По завершению прокрутки возвращаемся в начало 
                    //   await scrollForChords.ScrollToAsync(stackForImages, ScrollToPosition.Start, false);

                }
            }
            catch (Exception)
            {
                DeleteChordButton.Text = "Удалять нечего, дебил, зачем тыкаешь?!";
            }

        }
        // Сохранить заметку
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (MainEditorForText.Text != null && MainEditorForText.Text.Length > 0)
            {
                if (File.Exists(Path.Combine(App.FolderPath, "notes.xml")))   //Проверяем создан ли Xml файл, если создан, то работаем с ним
                {
                    //Смотрим существует ли переход на новую строку
                    if (MainEditorForText.Text.Contains("\n"))
                    {
                        //Вырезаем первую строку
                        string firstLine = MainEditorForText.Text.Substring(0, MainEditorForText.Text.IndexOf('\n'));
                        note.Title = firstLine;
                    }
                    else //Если переходов на новую строку не было, то берем полностью весь текст с редактора
                    {
                        note.Title = MainEditorForText.Text;
                    }
                    XmlDocument xDoc = new XmlDocument();
                    //Загружаем документ
                    xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                    XmlElement xRoot = xDoc.DocumentElement;
                    //Проверяем создается ли новая заметка или обновляется старая
                    if (note.Id != null)    //если не NULL значит заметка старая
                    {   //Находим заметку по ID
                        XmlNode xNode = xRoot.SelectSingleNode($"/Notes/Note[@ID={note.Id}]");
                        if (xNode != null)
                        {   //Вызываем функцию для обновления данных
                            UpdateNoteDataInXmlFile(xDoc, xNode, note.Title, note.Text, addedChordsList, addedBoysList, DateTime.Now);
                        }
                    }
                    else //Заметка новая
                    {   //Добавляем заметку в Xml файл
                        AddNoteDataToXmlFile(note.Title, note.Text, addedChordsList, addedBoysList, DateTime.Now);
                    }

                }
                else
                {   //В случае отсутсвия Xml файла, то создаем файл с определенным текстом внутри
                    //Текст внутри содержит изначальный аттрибут с кол-во созданных заметок. Изначально имеет значени 1
                    File.WriteAllText(Path.Combine(App.FolderPath, "notes.xml"), "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Notes notesCreatedCount=\"1\">\r\n\r\n</Notes>");
                    if (MainEditorForText.Text.Contains("\n"))//Смотрим существует ли переход на новую строку
                    {//Вырезаем первую строку
                        string firstLine = MainEditorForText.Text.Substring(0, MainEditorForText.Text.IndexOf('\n'));
                        note.Title = firstLine;
                    }
                    else
                    {
                        note.Title = MainEditorForText.Text;
                    }
                    AddNoteDataToXmlFile(note.Title, note.Text, addedChordsList, addedBoysList, DateTime.Now);
                }
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                SaveButton.Text = "Ой дурак... нечего сохранять!!";
                
            }


        }

        // Если человек хочет выйти со страницы
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Данные со страницы не сохранены!", "Вы действительно хотите выйти со страницы?", "Да", "Нет");

                if (result)
                {
                    await Navigation.PopAsync();
                }
            });

            return true;
        }
        // Удалить заметку
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        { // Окно с предупреждением, точно ли пользователь хочет удалить заметку 
            bool answer = await DisplayAlert("Удаление заметки", "Вы уверены, что хотите удалить заметку?", "Да", "Отмена");

            if (answer)
            {
                var note = (Note)BindingContext;
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path.Combine(App.FolderPath, "notes.xml"));
                XmlElement xRoot = xDoc.DocumentElement;
                //Проверяем новая ли заметка или же загруженная
                //Если у заметки есть ID, то заметка старая. В противном случае новая
                if (note.Id != null)
                {   //Находим заметку по ID
                    XmlNode xNote = xRoot.SelectSingleNode($"/Notes/Note[@ID={note.Id}]");
                    if (xNote != null)
                    {
                        //Если заметка надена, то удаляем
                        xRoot.RemoveChild(xNote);
                        xDoc.Save(Path.Combine(App.FolderPath, "notes.xml"));
                    }
                }
                // Navigate backwards
                await Shell.Current.GoToAsync("..");
                
            }
        }
        // методы обработки нажатия на кнопку с аккордом
        private void A(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    // Получение текста из editor и разделение его на массив строк
                    string[] editorLines = MainEditorForText.Text.Split('\n');

                    // Добавление нового текста в конец первой строки
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " A ");

                    // Объединение массива строк обратно в одну строку и установка ее в editor
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " A ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Aa", "PA.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Aa", "a.png");
                }
            }
        }
        private void Am(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Am ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Am ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Aam", "PAm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Aam", "am.png");
                }
            }
        }
        private void A7(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " A7 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " A7 ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Aa7", "PA7.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Aa7", "a7.png");
                }
            }
        }
        private void B(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " B ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " B ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Bb", "PB.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Bb", "b.png");
                }
            }
        }
        private void Bm(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Bm ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Bm ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Bbm", "PBm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Bbm", "bm.png");
                }
            }
        }
        private void B7(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " B7 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " B7 ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Bb7", "PB7.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Bb7", "b7.png");
                }
            }
        }
        private void C(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " C ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " C ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Cc", "PC.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Cc", "c.png");
                }
            }
        }
        private void Cm(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Cm ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Cm ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ccm", "PCm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ccm", "cm.png");
                }
            }
        }
        private void D(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " D ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " D ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Dd", "PD.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Dd", "d.png");
                }
            }
        }
        private void Dm(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Dm ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Dm ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ddm", "PDm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ddm", "dm.png");
                }
            }
        }
        private void E(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " E ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " E ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ee", "PE.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ee", "e.png");
                }
            }
        }
        private void E7(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " E7 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " E7 ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ee7", "PE7.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ee7", "e7.png");
                }
            }
        }
        private void Em(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Em ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Em ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Eem", "PEm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Eem", "em.png");
                }
            }
        }
        private void F(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " F ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " F ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ff", "PF.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ff", "f.png");
                }
            }
        }
        private void Fm(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Fm ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Fm ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ffm", "PFm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ffm", "fm.png");
                }
            }
        }
        private void G(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " G ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " G ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Gg", "PG.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Gg", "g.png");
                }
            }
        }
        private void Gm(object sender, EventArgs e)
        {
            if (WriteDownTheChords.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " Gm ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch
                {
                    MainEditorForText.Text = " Gm ";
                }
            }
            else
            {
                if (GuitarOrPiano.IsToggled)
                {
                    AddObjectToStack(typesOfObjects.piano, stackForImages, "Ggm", "PGm.png");
                }
                else
                {
                    AddObjectToStack(typesOfObjects.guitar, stackForImages, "Ggm", "gm.png");
                }
            }
        }
        // Автопрокрутка стека с картинками аккодров до последнего добавленного элемента
        private async void scrollForChords_SizeChanged(object sender, EventArgs e)
        {
            await scrollForChords.ScrollToAsync(stackForImages, ScrollToPosition.End, true);
        }
        // Методы для обработки нажатия кнопок с боем
        private async void BDownButton(object sender, EventArgs e)
        {
            if (WriteDownTheBoy.IsToggled)
            {
                try
                {
                    // Получение текста из editor и разделение его на массив строк
                    string[] editorLines = MainEditorForText.Text.Split('\n');

                    // Добавление нового знака
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " \u25BC ");

                    // Объединение массива строк обратно в одну строку и установка ее в editor
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                // на случай, если поле для ввода изначально пустое просто записываем туда знак
                catch (Exception)
                {
                    MainEditorForText.Text = " \u25BC ";
                }
            }
            else
            {
                AddObjectToStack(typesOfObjects.boy, stackForBoy, null, "BDown.png");
                await Bdown.ScaleTo(0.8, 200, Easing.BounceIn);
                await Bdown.ScaleTo(1, 200, Easing.BounceIn);
            }
        }
        private async void MDownButton(object sender, EventArgs e)
        {
            if (WriteDownTheBoy.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " \u21D3 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch (Exception)
                {

                    MainEditorForText.Text = " \u21D3 ";
                }
            }
            else
            {
                AddObjectToStack(typesOfObjects.boy, stackForBoy, null, "MDown.png");
                await MDown.ScaleTo(0.8, 200, Easing.BounceIn);
                await MDown.ScaleTo(1, 200, Easing.BounceIn);
            }
        }
        private async void BUpButton(object sender, EventArgs e)
        {
            if (WriteDownTheBoy.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " \u25B2 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch (Exception)
                {

                    MainEditorForText.Text = " \u25B2 ";
                }
            }
            else
            {
                AddObjectToStack(typesOfObjects.boy, stackForBoy, null, "BUp.png");
                await BUp.ScaleTo(0.8, 200, Easing.BounceIn);
                await BUp.ScaleTo(1, 200, Easing.BounceIn);
            }
        }
        private async void MUpButton(object sender, EventArgs e)
        {
            if (WriteDownTheBoy.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " \u21D1 ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch (Exception)
                {

                    MainEditorForText.Text = " \u21D1 ";
                }
            }
            else
            {
                AddObjectToStack(typesOfObjects.boy, stackForBoy, null, "MUp.png");
                await MUpn.ScaleTo(0.8, 200, Easing.BounceIn);
                await MUpn.ScaleTo(1, 200, Easing.BounceIn);
            }
        }
        private async void crossButton(object sender, EventArgs e)
        {
            if (WriteDownTheBoy.IsToggled)
            {
                try
                {
                    string[] editorLines = MainEditorForText.Text.Split('\n');
                    editorLines[0] = editorLines[0].Replace(editorLines[0], editorLines[0] + " \u274C ");
                    MainEditorForText.Text = string.Join("\n", editorLines);
                }
                catch (Exception)
                {

                    MainEditorForText.Text = " \u274C ";
                }
            }
            else
            {
                AddObjectToStack(typesOfObjects.boy, stackForBoy, null, "cross.png");
                await Cross.ScaleTo(0.8, 200, Easing.BounceIn);
                await Cross.ScaleTo(1, 200, Easing.BounceIn);
            }
        }

        // Автопрокрутка стека с картинками ударов боя до последнего добавленного элемента
        private async void scrollForBoy_SizeChanged(object sender, EventArgs e)
        {
            await scrollForBoy.ScrollToAsync(stackForBoy, ScrollToPosition.End, true);
        }
        // Удаление последней добавленной картинки в стек для боя
        private void DeleteBoy(object sender, EventArgs e)
        {
            if (stackForBoy.Children.Count > 0)
            {
                DeleteBoyButton.FontSize = 15;
                DeleteBoyButton.Text = "Удалить удар";
                stackForBoy.Children.Remove(stackForBoy.Children.Last());
                addedBoysList.RemoveAt(addedBoysList.Count - 1);
            }
            else if (stackForBoy.Children.Count == 0)
            {
                DeleteBoyButton.FontSize = 12;
                DeleteBoyButton.Text = "Удалять нечего, дебил, зачем тыкаешь???";
            }
        }
        //Вызов функции скролла при нажатие кнопки
        private async void StartScroll(object sender, EventArgs e)
        {
          //Если элементов меньше 5, то не сможем осуществить прокрутку
          //Из-за этого делаем проверку на кол-во элементов
          if(addedChordsList.Count>=5) await Scroll();
        }

        async Task Scroll()
        {
            //На основе ширины всех добавленных элементов в Scroll и по кол-ву добавленных элементов выясняем размер прокрутки 
            //На самом деле формула полная туфта, тут бы в идеале еще и время прокрутки брать в учет, чтобы увеличивать или
            //уменьшать скорость прокрутки, но как прототип пойдет ¯\_(ツ)_/¯
            int scrollSize = int.Parse(scrollForChords.ContentSize.Width.ToString()) / addedChordsList.Count;
            //Двигаем список, пока кординаты прокрутки по X меньше чем 
            //ширина всех элементов минус ~5 элементов
            while (scrollForChords.ScrollX < int.Parse(scrollForChords.ContentSize.Width.ToString()) - (scrollSize * 5))
            {
                //Двигаем Scroll с текущих кординат прокрутки
                await scrollForChords.ScrollToAsync(scrollForChords.ScrollX + scrollSize, 0, true);
                //Задержка выполнения задачи, можно использовать для уменьшения/увеличения скорости прокрутки
                //   await Task.Delay(400);
            }
            //По завершению прокрутки возвращаемся в начало 
            await scrollForChords.ScrollToAsync(stackForImages, ScrollToPosition.Start, true);
        }
        //Возвращает пользователя с редактора текста обратно в добавление аккордов/гитарных боев
        //Срабатывет когда в редакторе, при открытой клавиатуре, пользователь нажимает кнопку назад
        //Есть баг, после добавление текста и нажатие кнопки назад, то возвращает наверх, но приложение тускнеет
        async void textEditingEnd(object sender, EventArgs e)
        {
            await mainScrollView.ScrollToAsync(mainScrollView, ScrollToPosition.Start, false);
        }
    }
}