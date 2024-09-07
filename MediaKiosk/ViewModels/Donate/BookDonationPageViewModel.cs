using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.ViewModels.Donate
{
    internal class BookDonationPageViewModel
    {
        public RelayCommand browseCmd => new RelayCommand(execute => BrowseForImage());

        private void BrowseForImage()
        {

        }
    }
}
