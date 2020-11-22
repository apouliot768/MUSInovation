using MUSInovation.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MUSInovation.ViewModel
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for displayed a result of a movie search by title.
    /// Date :          2020-11-19
    /// </summary>
    public class OMBdByTitleViewModel
    {
        public string Title { get; set; } = "";
        public string Year { get; set; } = "";
        public string Rated { get; set; } = "";
        public string Released { get; set; } = "";
        public string Runtime { get; set; } = "";
        public string Genre { get; set; } = "";
        public string Director { get; set; } = "";
        public string Writer { get; set; } = "";
        public string Actors { get; set; } = "";
        public string Plot { get; set; } = "";
        public string Language { get; set; } = "";
        public string Country { get; set; } = "";
        public string Awards { get; set; } = "";
        public Uri Poster { get; set; }
        public IList<Rating> Ratings { get; set; }
        public string BoxOffice { get; set; } = "";
        public string Production { get; set; } = "";
        public string Website { get; set; } = "";

        // CTOR
        public OMBdByTitleViewModel()
        {

        }

        // CTOR
        public OMBdByTitleViewModel(OMBdModel model)
        {
            Title = model.Title;
            Year = model.Year;
            Rated = model.Rated;
            Released = model.Released;
            Runtime = model.Runtime;
            Genre = model.Genre;
            Director = model.Director;
            Writer = model.Writer;
            Actors = model.Actors;
            Plot = model.Plot;
            Language = model.Language;
            Country = model.Country;
            Awards = model.Awards;
            if (IsValidUri(model.Poster))
                Poster = new Uri(model.Poster);
            Ratings = model.Ratings;
            BoxOffice = model.BoxOffice;
            Production = model.Production;
            Website = model.Website;
        }

        // Valid given URI
        bool IsValidUri(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute);
        }
    }
}
