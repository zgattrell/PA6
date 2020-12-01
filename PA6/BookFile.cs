using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PA6
{
    class BookFile
    {
        public static List<Book> GetAllBooks(string cwid)
        {


            //URL
            string url = @"https://lucas-swami-api.herokuapp.com/books/" + cwid;

            //Route
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);

                List<Book> myBooks = releases.ToObject<List<Book>>();

                return myBooks;
            }

        }

        public static void SaveBook(Book myBook, string cwid, string mode)
        {
            var content = new StringContent(JsonConvert.SerializeObject(myBook).ToString(), Encoding.UTF8, "application/json");
            string url;
            if (mode == "edit")
            {
                url = @"https://lucas-swami-api.herokuapp.com/books/" + cwid + "/" + myBook._id;
            }
            else
            {
                url = @"https://lucas-swami-api.herokuapp.com/books/" + cwid;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                if (mode == "edit")
                {
                    var response = httpClient.PutAsync(new Uri(url), content).Result;
                }
                else
                {
                    var response = httpClient.PostAsync(new Uri(url), content).Result;
                }


            }
        }


        public static void DeleteBook(Book myBook, string cwid)
        {
            string url = @"http://159.89.235.64:3000/books/" + cwid + "/" + myBook._id;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.DeleteAsync(new Uri(url)).Result;

            }
        }
    }
}