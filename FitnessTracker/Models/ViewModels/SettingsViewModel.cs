using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models.ViewModels
{
    public class SettingsViewModel
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
