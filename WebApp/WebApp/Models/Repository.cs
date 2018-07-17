using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Repository
    {
        BookContext bk = new BookContext();

        public IEnumerable<Book> GetBooks()
        {
            return bk.Books;
        }

        public IEnumerable<User> GetUsers()
        {
            return bk.Users;
        }

        public IEnumerable<Autor> GetAutors()
        {
            return bk.Autors;
        }


        /// <summary>
        /// Получаем автора по ID
        /// </summary>
        /// <param name="id">Идентификаационный номер автора</param>
        public Autor GetAutor(int id)
        {
            return bk.Autors.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Получить книгу по ID
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns></returns>
        public Book GetBook(int id)
        {
            return bk.Books.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Получаем пользователя по ID
        /// </summary>
        /// <param name="id">Идентификационный номер пользователя</param>
        public User GetUser(int id)
        {
            return bk.Users.FirstOrDefault(x => x.Id == id);
        }



        /// <summary>
        /// Привязка книги к пользователю
        /// </summary>
        /// <param name="userID">ID Пользователя</param>
        /// <param name="bookID">ID книги</param>
        public void AddUserBook(int userID, int bookID)
        {
            GetUser(userID).Books.Add(GetBook(bookID));
            GetBook(bookID).Users.Add(GetUser(userID));
            bk.SaveChanges();
        }


        /// <summary>
        /// Добавлеие пользователя
        /// </summary>
        /// <param name="u">Новый пользователь</param>
        public void AddUser(User u)
        {
            bk.Users.Add(u);
            bk.SaveChanges();
        }

        /// <summary>
        /// Добавлеие автора
        /// </summary>
        /// <param name="b">Новый автор</param>
        public void AddAuthor(Autor b)
        {
            bk.Autors.Add(b);
            bk.SaveChanges();
        }

        /// <summary>
        /// Добавление новой книги и связывание еще с автором
        /// </summary>
        /// <param name="authorName">Имя автора</param>
        /// <param name="authorSecondName">Фамилия автора</param>
        /// <param name="bookTitle">Название книги</param>
        public void AddBookByAuthor(string authorName, string authorSecondName, string bookTitle)
        {
            
            Book tmpBook = bk.Books.FirstOrDefault(n => n.Name == bookTitle);
            Autor tmpAuthor = bk.Autors.FirstOrDefault(n => n.Name == authorName && n.LastName == authorSecondName);

            // ------ Поиск автора в базе данных --------------------
            foreach (Autor b in bk.Autors)
            {
                if (b.Name == authorName && b.LastName == authorSecondName)     // Имя и фамилия совпадают
                {
                    tmpAuthor = b;                                              // получаем ссылку на запись в бд
                    break;
                }
            }
            if (tmpAuthor == null)                                              // Автор не найден, создаем нового автора
            {
                bk.Autors.Add(new Autor(authorName, authorSecondName));
                bk.SaveChanges();
            }

            int tmpID = 0;
            foreach (Autor b in bk.Autors)                                 // Находим присвоенный ID нового автора
            {
                if (b.Name == authorName && b.LastName == authorSecondName)
                {
                    tmpID = b.Id;                                               // помещаем присвоенный новый ID автора в переменную
                    break;
                }
            }

            tmpAuthor = GetAutor(tmpID);                                        // Получаем ссылку на автора по ID

            tmpBook = bk.Books.FirstOrDefault(s => s.Name == bookTitle);  // Ищем книгу по названию
            if (tmpBook == null)
            {
                bk.Books.Add(new Book(bookTitle));                         // Если книга не найдена, создаем книгу с новым названием
                bk.SaveChanges();
            }

            tmpBook = bk.Books.FirstOrDefault(t => t.Name == bookTitle);  // Получаем ссылку на только что добавленную книгу

            tmpBook.Autors.Add(tmpAuthor);                                      // Добавляем связи n-n для книги
            tmpAuthor.Books.Add(tmpBook);                                       // Добавляем связи n-n для автора

            bk.SaveChanges();                                              // Сохраняем результат
        }
        

        /// <summary>
        /// Отвязка книги от пользователя
        /// </summary>
        /// <param name="userID">ID Пользователя</param>
        /// <param name="bookID">ID книги</param>
        public void RemoveBookFromUser(int userID, int BookID)
        {
            GetUser(userID).Books.Remove(GetBook(BookID));
            GetBook(BookID).Users.Remove(GetUser(userID));
            bk.SaveChanges();
        }

       
    }
}