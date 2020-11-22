using System;
using System.Collections.Generic;
using System.Text;

namespace MUSInovation.Model.TMBd.TMBdUpcoming
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for deserialized TMBd API JSON
    /// Date :          2020-11-19
    /// </summary>
    
    public class Result
    {
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string poster_path { get; set; }
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public IList<int> genre_ids { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }

    public class Dates
    {
        public string maximum { get; set; }
        public string minimum { get; set; }
    }

    public class TMBdUpcoming
    {
        public IList<Result> results { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public Dates dates { get; set; }
        public int total_pages { get; set; }
    }

}
