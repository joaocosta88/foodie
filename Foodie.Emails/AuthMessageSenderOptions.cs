using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodie.Emails {
    public class AuthMessageSenderOptions {
        public string SendGridKey { get; set; }
        public string Alias { get; set; }
        public string From { get; set; }
        public EmailUrlConfiguration EmailUrlConfiguration { get; set; }
    }
}
