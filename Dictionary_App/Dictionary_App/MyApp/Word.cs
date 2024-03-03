using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_App.MyApp
{
    internal class Word
    {
        public string _name { get; set; }
        public string _definition { get; set; }
        public string _imagePath { get; set; }

        public Word() { }
        public Word(string word, string definition, string imagePath = "\\Images\\no_image.jpg")
        {
            _name = word;
            _definition = definition;
            _imagePath = imagePath;
        }



        public override string ToString()
        {
            return _name;
        }
    }
}
