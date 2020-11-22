using System;
using System.Collections.Generic;
using System.Text;

namespace MUSInovation.Model.TMBd.TMBdAuthor
{
    public class KnownFor
    {
        public string release_date { get; set; }
        public int id { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
    }

    public class Result
    {
        public double popularity { get; set; }
        public string known_for_department { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public List<KnownFor> known_for { get; set; }
        public string name { get; set; }
    }

    public class TMBdByAuthorModel
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }


}
