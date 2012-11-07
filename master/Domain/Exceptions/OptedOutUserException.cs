﻿using System;

namespace Portfotolio.Domain.Exceptions
{
    public class OptedOutUserException : PortfotolioException
    {
        public override int HttpStatusCode { get { return 401; } }
        public override bool IsWarning { get { return true; } }

        public string UserAlias { get; private set; }

        public OptedOutUserException(string userAlias)
        {
            UserAlias = userAlias;
        }

        public OptedOutUserException(string userAlias, Exception innerException) : base(innerException)
        {
            UserAlias = userAlias;
        }

        private const string MessageFormat =
            "Nothing to see here. '{0}' do not allow their photos to be viewed with third party applications.";
        
        public override string Message
        {
            get
            {
                return string.Format(MessageFormat, UserAlias);
            }
        }
    }
}