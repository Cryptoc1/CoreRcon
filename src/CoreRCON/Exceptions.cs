﻿using System.Runtime.CompilerServices;

namespace CoreRCON;

/// <summary> Represents an exception that occurs while operating on an RCON Host. </summary>
public class RCONException : Exception
{
    public RCONException()
    {
    }

    public RCONException(string message) : base(message)
    {
    }

    public RCONException(string message, Exception? inner) : base(message, inner)
    {
    }
}

/// <summary> Represents an exception that occurs while authenticating with an RCON Host. </summary>
public class RCONAuthenticationException : RCONException
{
    public RCONAuthenticationException()
    {
    }

    public RCONAuthenticationException(string message) : base(message)
    {
    }

    public RCONAuthenticationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

/// <summary> Represents an exception that occurs while sending a command against an RCON Host. </summary>
public class RCONCommandException : RCONException
{
    /// <summary> The command text that resulted in the exception to be thrown. </summary>
    public string Command { get; }

    public RCONCommandException(string message, string command) : base(message)
    {
        Command = command;
    }

    public RCONCommandException(string message, string command, Exception? inner) : base(message, inner)
    {
        Command = command;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RCONCommandException Failed(string command, Exception? inner = null) => new($"Failed to execute command '{command}'.", command, inner);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RCONCommandException Timeout(string command, TimeoutException exception) => new($"A timeout occured while attempting to execute '{command}'.", command, exception);
}
