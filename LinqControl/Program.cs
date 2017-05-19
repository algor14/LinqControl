using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqControl
{
    public class HighRegisterComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var higherX = int.Parse((string.Join("", x.ToString().Take(1))));
            var higherY = int.Parse((string.Join("", y.ToString().Take(1))));

            if (higherX < higherY)
                return (-1);
            else if (higherX > higherY)
                return (1);
            else
                return (0);
        }
    }
    public class LowRegisterComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var lowX = int.Parse((string.Join("", x.ToString().Skip(1))));
            var lowY = int.Parse((string.Join("", y.ToString().Skip(1))));

            if (lowX < lowY)
                return (1);
            else if (lowX > lowY)
                return (-1);
            else
                return (0);
        }
    }
    class Book
    {
        public string Name;
        public int Year;
        public int AuthorID;

        public bool Equals(Book book)
        {
            return Name == book.Name && Year == book.Year;
        }

        public override int GetHashCode()
        {
            int hCode = this.Name.Length^this.Year;
            return hCode.GetHashCode();
        }
    }
    class Author
    {
        public string Name;
        public int AuthorID;
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            books.Add(new Book() { Name = "linq", Year = 1600, AuthorID = 1 });
            books.Add(new Book() { Name = "linq", Year = 1600, AuthorID = 1 });
            books.Add(new Book() { Name = "linq", Year = 1601, AuthorID = 2 });
            books.Add(new Book() { Name = "linq2", Year = 1602, AuthorID = 1 });
            books.Add(new Book() { Name = "linq3", Year = 1603, AuthorID = 3 });
            books.Add(new Book() { Name = "linq3", Year = 1604, AuthorID = 3 });
            List<Author> authors = new List<Author>();
            authors.Add(new Author() { Name = "Author 1", AuthorID = 1 });
            authors.Add(new Author() { Name = "Author 2", AuthorID = 2 });
            authors.Add(new Author() { Name = "Author 3", AuthorID = 3 });


            //1
            Console.WriteLine(string.Join(",", books.Where(f => f.Name.Contains("linq") && (f.Year % 400 == 0 || (f.Year % 4 == 0 && f.Year % 100 != 0))).Select(f => $"{f.Name} - {f.Year}")));

            //2
            var predCollection = books.Join(authors, b => b.AuthorID, a => a.AuthorID, (b, a) => new { AuthorID = b.AuthorID, AuthorName = a.Name, BookName = b.Name, Year = b.Year });
            var collection = predCollection.GroupBy(a => a.AuthorName, (key, g) => new { AuthorName = key, Books = g.Count() });
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            //3
            int[] numbers = { 40, 44, 45, 14, 12, 23, 20, 38, 33, 32, 35 };
            Console.WriteLine(string.Join(",", numbers.OrderBy((f => f), new HighRegisterComparer()).ThenBy((f => f), new LowRegisterComparer())));

            //4
            Unique<Book>(books);

        }

      
        public static void Unique<T>(List<T> list)
        {
            //List<Person> distinctPeople = allPeople
            //var collection = list.;
            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item);
            //}
  
        }
    }
}
