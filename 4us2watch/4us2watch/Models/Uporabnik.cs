using System;
using System.Collections.Generic;
using System.Text;

namespace _4us2watch.Models
{
    public class Uporabnik
    {
        public string username;
        public string email;
        public List<string> friends = new List<string>();
        public List<string> movies = new List<string>();
    }
}
