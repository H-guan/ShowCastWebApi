using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCastWebApi2
{
    public class Pagenation
    {
        public int PageID { get; set; }
        public List<ResponseShow> lsShow { get; set; }
    }
    public class ResponseShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Actor> Cast { get; set; }
    }
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
    }
    public class people
    {
        public int Itemid { get; set; }
        public string Jobtype { get; set; }
    }

}
