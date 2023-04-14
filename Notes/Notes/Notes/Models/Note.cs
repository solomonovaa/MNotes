using System;


namespace Notes.Models
{
    public class Note
    {
        public string Id { get; set; }
        public string FavoriteStatus { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}