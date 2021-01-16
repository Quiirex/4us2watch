using _4us2watch.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4us2watch.Components
{
    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Movie obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
