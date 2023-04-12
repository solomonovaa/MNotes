﻿using System;


namespace Notes.Models
{
    public class Note
    {
        public string Filename { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime Date { get; set; }
    }
}