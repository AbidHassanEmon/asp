using MidProject.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidProject.Models
{
    public class Search
    {
       
        public DateTime? Pdate { get; set; }=null;

        public DateTime? Rdate { get; set; } = null;

        public IList<Car> Car { get; set; } = new List<Car>();
    }
}