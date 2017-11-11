using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqBotServer
{
    class Client
    {
        public Client(int uid)
        {
            this.uid = uid;
            this.qid = -1;
        }

        public void OnMessage(String Text)
        {
            return;
        }

        #region Private
        private int uid;
        private int qid;
        #endregion
    }
}
