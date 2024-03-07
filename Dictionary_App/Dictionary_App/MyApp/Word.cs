using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dictionary_App.MyApp
{
    public class Word
    {
        public string _name { get; set; }
        public string _definition { get; set; }
        public string _imagePath { get; set; }
        public string _category { get; set; }

        public Word() { }
        public Word(string word, string definition, string imagePath, string category)
        {
            _name = word;
            _definition = definition;
            _imagePath = imagePath;
            _category = category;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
