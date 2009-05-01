using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Kona.Infrastructure {
    public class PluginSetting {

        public PluginSetting() {
            Settings = new Dictionary<string, object>();
            IsEnabled = false;
        }
        public string PluginName { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string,object> Settings { get; set; }
    }
}
