using System.Collections.Generic;

namespace EteraShopInterractingLibrary.Domain
{
    public class Good
    {
        private List<string> _colors = new List<string>();

        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }

        public List<string> Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }
    }
}