using System.Collections;
using System.Collections.ObjectModel;

namespace FSTEC.Objects
{
    public class DisplayedDangers
    {
        private static ObservableCollection<DisplayedDanger> displayedDangers;
        public static ObservableCollection<DisplayedDanger> DisplayedDangersCollection
        {
            get
            {
                if (displayedDangers == null)
                    displayedDangers = DisplayDangersFromDangers(FileExcel.Dangers);
                return displayedDangers;
            }
        }
        public static ObservableCollection<DisplayedDanger> DisplayDangersFromDangers(IEnumerable dangers)
        {
            ObservableCollection<DisplayedDanger> dangerscollection = new ObservableCollection<DisplayedDanger>();
            foreach (var danger in dangers)
                dangerscollection.Add(new DisplayedDanger(danger as Danger));
            return dangerscollection;
        }
    }
}
