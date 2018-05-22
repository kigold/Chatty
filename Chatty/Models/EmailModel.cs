
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Models
{
    class EmailModel
    {
        public String[] sender { get; set; }
        public String[] receiver { get; set; }
        public String subject { get; set; }
        public String body { get; set; }
        public String[] cc { get; set; }
        public String[] bcc { get; set; }
        public String attachement { get; set; }
        public String token { get; set; }

        public EmailModel(string _subject)
        {
            subject = _subject;
            token = _subject + DateTime.Now.ToString();
        }
    }
}
