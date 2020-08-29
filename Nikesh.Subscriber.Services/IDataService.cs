using Nikesh.Subscriber.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nikesh.Subscriber.Services
{
    public interface IDataService
    {
        void InsertData(InformationViewModel viewModel);
    }
}
