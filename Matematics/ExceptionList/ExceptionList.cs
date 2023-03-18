using System;

namespace Matematics.Exception
{
    [Serializable]
    public class CannotMoveMonomioIsNotFoundException : System.Exception
    {
        public override string Message
        {
            get
            {
                return "Cannot move Monomio is not found";
            }
        }

        public CannotMoveMonomioIsNotFoundException() { }
        public CannotMoveMonomioIsNotFoundException(string message) : base(message) { }
        public CannotMoveMonomioIsNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected CannotMoveMonomioIsNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    public class CannotResolveException : System.Exception
    {
        public override string Message
        {
            get
            {
                return "Cannot resolve this";
            }
        }

        public CannotResolveException() { }
        public CannotResolveException(string message) : base(message) { }
        public CannotResolveException(string message, System.Exception inner) : base(message, inner) { }
        protected CannotResolveException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class CannotGetDecimalLetterArePresentException : System.Exception
    {
        public override string Message
        {
            get
            {
                return "Cannot get to a double because it contains letters";
            }
        }

        public CannotGetDecimalLetterArePresentException() { }
        public CannotGetDecimalLetterArePresentException(string message) : base(message) { }
        public CannotGetDecimalLetterArePresentException(string message, System.Exception inner) : base(message, inner) { }
        protected CannotGetDecimalLetterArePresentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class YIsNotPresentException : System.Exception
    {
        public YIsNotPresentException() { }
        public YIsNotPresentException(string message) : base(message) { }
        public YIsNotPresentException(string message, System.Exception inner) : base(message, inner) { }
        protected YIsNotPresentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NumerIsNotIntegerException : System.Exception
    {
        public NumerIsNotIntegerException() { }
        public NumerIsNotIntegerException(string message) : base(message) { }
        public NumerIsNotIntegerException(string message, System.Exception inner) : base(message, inner) { }
        protected NumerIsNotIntegerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
