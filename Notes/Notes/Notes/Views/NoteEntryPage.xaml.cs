using System;
using System.IO;
using System.Linq;
using Notes.Models;
using Xamarin.Forms;


namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
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
    
        // удаляет картинку аккорда из стека
        private void DeleteChord(object sender, EventArgs e)
        {
            
            if (stackForImages.Children.Count > 0)
            {
                DeleteChordButton.FontSize = 15;
                DeleteChordButton.Text = "Удалить аккорд";
                stackForImages.Children.Remove(stackForImages.Children.Last());
            }
            else if (stackForImages.Children.Count == 0)
            {
                DeleteChordButton.FontSize = 12;
                DeleteChordButton.Text = "Удалять нечего, дебил, зачем тыкаешь???";
            }
        }

        void LoadNote(string filename)
        {
           
            try
            {
                // Retrieve the note and set it as the BindingContext of the page.
                Note note = new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                };
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (MainEditorForText.Text != null)
            {
                if (string.IsNullOrWhiteSpace(note.Filename))
                {
                    // Сохранение файла
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                    File.WriteAllText(filename, note.Text);
                }
                else
                {
                    // Обновление файла
                    File.WriteAllText(note.Filename, note.Text);
                }
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                SaveButton.FontSize = 12;
                SaveButton.Text = "И что тут сохранять?!";
            }
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;


            // Delete the file.
            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }


            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        private void A(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Aa.BackgroundColor = Color.DarkOrange;
                Aa.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PA.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Aa.BackgroundColor = Color.DarkOrange;
                Aa.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "a.png";
                stackForImages.Children.Add(chord);
            }

        }

        private void Am(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Aam.BackgroundColor = Color.DarkOrange;
                Aam.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PAm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Aam.BackgroundColor = Color.DarkOrange;
                Aam.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "am.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void A7(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Aa7.BackgroundColor = Color.DarkOrange;
                Aa7.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PA7.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Aa7.BackgroundColor = Color.DarkOrange;
                Aa7.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "a7.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void B(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Bb.BackgroundColor = Color.DarkOrange;
                Bb.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PB.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Bb.BackgroundColor = Color.DarkOrange;
                Bb.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "b.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Bm(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Bbm.BackgroundColor = Color.DarkOrange;
                Bbm.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PBm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Bbm.BackgroundColor = Color.DarkOrange;
                Bbm.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "bm.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void B7(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Bb7.BackgroundColor = Color.DarkOrange;
                Bb7.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PB7.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Bb7.BackgroundColor = Color.DarkOrange;
                Bb7.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "b7.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void C(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Cc.BackgroundColor = Color.DarkOrange;
                Cc.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PC.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Cc.BackgroundColor = Color.DarkOrange;
                Cc.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "c.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Cm(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ccm.BackgroundColor = Color.DarkOrange;
                Ccm.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PCm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ccm.BackgroundColor = Color.DarkOrange;
                Ccm.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "cm.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void D(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Dd.BackgroundColor = Color.DarkOrange;
                Dd.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PD.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Dd.BackgroundColor = Color.DarkOrange;
                Dd.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "d.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Dm(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ddm.BackgroundColor = Color.DarkOrange;
                Ddm.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PDm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ddm.BackgroundColor = Color.DarkOrange;
                Ddm.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "dm.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void E(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ee.BackgroundColor = Color.DarkOrange;
                Ee.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PE.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ee.BackgroundColor = Color.DarkOrange;
                Ee.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "e.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void E7(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ee7.BackgroundColor = Color.DarkOrange;
                Ee7.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PE7.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ee7.BackgroundColor = Color.DarkOrange;
                Ee7.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "e7.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Em(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Eem.BackgroundColor = Color.DarkOrange;
                Eem.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PEm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Eem.BackgroundColor = Color.DarkOrange;
                Eem.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "em.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void F(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ff.BackgroundColor = Color.DarkOrange;
                Ff.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PF.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ff.BackgroundColor = Color.DarkOrange;
                Ff.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "f.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Fm(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ffm.BackgroundColor = Color.DarkOrange;
                Ffm.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PFm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ffm.BackgroundColor = Color.DarkOrange;
                Ffm.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "fm.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void G(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Gg.BackgroundColor = Color.DarkOrange;
                Gg.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PG.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Gg.BackgroundColor = Color.DarkOrange;
                Gg.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "g.png";
                stackForImages.Children.Add(chord);
            }
        }
        private void Gm(object sender, EventArgs e)
        {
            if (GuitarOrPiano.IsToggled)
            {
                Image chord = new Image();
                Ggm.BackgroundColor = Color.DarkOrange;
                Ggm.BorderColor = Color.Brown;
                chord.HeightRequest = 50;
                chord.WidthRequest = 100;
                chord.Source = "PGm.png";
                stackForImages.Children.Add(chord);
            }
            else
            {
                Image chord = new Image();
                Ggm.BackgroundColor = Color.DarkOrange;
                Ggm.BorderColor = Color.Brown;
                chord.HeightRequest = 100;
                chord.WidthRequest = 80;
                chord.Source = "gm.png";
                stackForImages.Children.Add(chord);
            }
        }

        private async void scrollForChords_SizeChanged(object sender, EventArgs e)
        {
            await scrollForChords.ScrollToAsync(stackForImages, ScrollToPosition.End, true);
        }
        private void BDownButton(object sender, EventArgs e)
        {
            Image boy = new Image();
            boy.HeightRequest = 40;
            boy.WidthRequest = 40;
            boy.Source = "BDown.png";
            stackForBoy.Children.Add(boy);
        }
        private void MDownButton(object sender, EventArgs e)
        {
            Image boy = new Image();
            boy.HeightRequest = 40;
            boy.WidthRequest = 40;
            boy.Source = "MDown.png";
            stackForBoy.Children.Add(boy);
        }
        private void BUpButton(object sender, EventArgs e)
        {
            Image boy = new Image();
            boy.HeightRequest = 40;
            boy.WidthRequest = 40;
            boy.Source = "BUp.png";
            stackForBoy.Children.Add(boy);
        }
        private void MUpButton(object sender, EventArgs e)
        {
            Image boy = new Image();
            boy.HeightRequest = 40;
            boy.WidthRequest = 40;
            boy.Source = "MUp.png";
            stackForBoy.Children.Add(boy);
        }
        private void crossButton(object sender, EventArgs e)
        {
            Image boy = new Image();
            boy.HeightRequest = 40;
            boy.WidthRequest = 40;
            boy.Source = "cross.png";
            stackForBoy.Children.Add(boy);
        }
        private async void scrollForBoy_SizeChanged(object sender, EventArgs e)
        {
            await scrollForBoy.ScrollToAsync(stackForBoy, ScrollToPosition.End, true);
        }
        private void DeleteBoy(object sender, EventArgs e)
        {

            if (stackForBoy.Children.Count > 0)
            {
                DeleteBoyButton.FontSize = 15;
                DeleteBoyButton.Text = "Удалить аккорд";
                stackForBoy.Children.Remove(stackForBoy.Children.Last());
            }
            else if (stackForBoy.Children.Count == 0)
            {
                DeleteBoyButton.FontSize = 12;
                DeleteBoyButton.Text = "Удалять нечего, дебил, зачем тыкаешь???";
            }
        }

       
    }
}