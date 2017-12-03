using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models.Data
{
    class DataMenus
    {
        public List<string> MenuMain { set; get; }

        public List<string> MenuInput { set; get; }

        public List<string> MenuShow { set; get; }

        public List<string> MenuSearch { set; get; }

        public List<string> MenuReport { set; get; }

        public string folder { get; set; }

        public DataMenus()
        {
            folder = "../../Data";

            MenuMain = MenuMain.GetMenu(folder + "/Menu.txt");
            MenuInput = MenuInput.GetMenu(folder + "/MenuInput.txt");
            MenuShow = MenuShow.GetMenu(folder + "/MenuShow.txt");
            MenuSearch = MenuSearch.GetMenu(folder + "/MenuSearch.txt");
            MenuReport = MenuReport.GetMenu(folder + "/MenuReport.txt");
        }

        public DataMenus(string folder)
        {
            MenuMain = MenuMain.GetMenu(folder + "/Menu.txt");
            MenuInput = MenuInput.GetMenu(folder + "/MenuInput.txt");
            MenuShow = MenuShow.GetMenu(folder + "/MenuShow.txt");
            MenuSearch = MenuSearch.GetMenu(folder + "/MenuSearch.txt");
            MenuReport = MenuReport.GetMenu(folder + "/MenuReport.txt");
        }
    }
}
