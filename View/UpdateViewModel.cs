using FSTEC.Objects;
using System.Collections.Generic;
using System.Linq;

namespace FSTEC.View
{
    public class UpdateViewModel: ViewModelBase 
    {
        public static List<Update> updates;
        private static int updatesCount = 0;
        public int UpdatesCount
        {
            get
            {
                return updatesCount;
            }
            set
            {
                updatesCount = value;
                OnPropertyChanged("dangersCount");
            }
        }
        public List<Update> Updates
        {
            get
            {
                return updates;
            }
            set
            {
                updates = value;
                OnPropertyChanged("dangers");
            }
        }
        public UpdateViewModel(List<Update> updates)
        {
            Updates = updates;
            UpdatesCount = updates.Select(x => x.Id).Distinct().Count();
            new UpdateFile().Show();
        }
        
        public UpdateViewModel() 
        { 
        }
    }
}
