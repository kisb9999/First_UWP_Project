﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Models
{
    //This is the book object that stores every data of the books.
    public class BookObject
    {
        public string url { get; set; }
        public string name { get; set; }
        public string isbn { get; set; }
        public string[] authors { get; set; }
        public int numberOfPages { get; set; }
        public string publisher { get; set; }
        public string country { get; set; }
        public string mediaType { get; set; }
        public DateTime released { get; set; }
        public string[] characters { get; set; }
        public string[] povCharacters { get; set; }
    }
}
