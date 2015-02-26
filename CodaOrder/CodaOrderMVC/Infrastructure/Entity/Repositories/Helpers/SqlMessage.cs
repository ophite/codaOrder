using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iOrder.Entity.Repositories
{
    public class SqlResult
    {
        public SqlMessage Message { get; set; }

        public bool IsError
        {
            get { return this.Message.IsError || !string.IsNullOrEmpty(this.Message.Message); }
        }

        private object _result = null;
        public object Result
        {
            get
            {
                if (this.IsError)
                    _result = Message.Message;

                return _result;
            }
            set { this._result = value; }
        }

        public SqlResult()
        {
            this.Message = new SqlMessage();
        }
    }

    public class SqlMessage
    {
        public string Message { get; set; }
        public string FullMessage { get; set; }
        public int RowCount { get; set; }
        public bool IsError { get; set; }
    }
}