using Core.Common.Dtos;
using Example.Client.Models.Entities;
using System.Collections.Generic;

namespace Example.Client.Web.ViewModels
{
    public class StuffViewModel
    {
        public IEnumerable<Colour> Colours { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public PagedResults<Stuff> PagedResults { get; set; }
    }
}