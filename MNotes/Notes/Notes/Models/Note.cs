using System;


namespace Notes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        /*
         * В заметку кладется картинка => делаем два поля:
         *  одно для картинки, другое для того, чтобы понять -
         *  избранная эта заметка или нет
         */
        public string IsFavoriteNote { get; set; }
        public string FavoriteIcon { get; set; }
        
        /*
         * Желательно ввести два поля: дату создания и дату изменения
         * public DateTime CreatedDate{ get: set; }
         * public DateTime ModifiedDate{ get: set; }
         */
    }
}