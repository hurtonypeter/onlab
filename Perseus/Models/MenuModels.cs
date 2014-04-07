using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Models
{
    public class MenuViewModel
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string Classes { get; set; }
        public List<MenuViewModel> Children { get; set; }
        public MenuViewModel()
        {
            Children = new List<MenuViewModel>();
        }
    }
}