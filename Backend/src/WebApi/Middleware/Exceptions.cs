﻿namespace WebApi.Middleware
{
    public class Exceptions
    {
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message) { }
        }

        public class ForbiddenException : Exception
        {
            public ForbiddenException(string message) : base(message) { }
        }

        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
        }

        public class ServiceUnavailableException : Exception
        {
            public ServiceUnavailableException(string message) : base(message) { }
        }
    }
}
