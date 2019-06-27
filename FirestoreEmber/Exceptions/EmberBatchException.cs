using System;
using System.Collections.Generic;
using System.Text;

namespace FirestoreEmber.Exceptions
{
    public class EmberBatchException : Exception
    {
        public EmberBatchException()
        {

        }

        public EmberBatchException(string message) : base(message)
        {

        }
    }
}
