using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataBaseFiller
{
    class ProgressUpdate
    {
       static ViewModel _viewModel = new ViewModel();
        public static void TransferMessage(string message)
        {
            _viewModel.Update = message;
        }
    }
}
