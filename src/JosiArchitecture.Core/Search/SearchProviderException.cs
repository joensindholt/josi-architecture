using System;

namespace JosiArchitecture.Core.Search;

public class SearchProviderException : Exception
{
    public SearchProviderException(string message) : base(message)
    {
    }
}