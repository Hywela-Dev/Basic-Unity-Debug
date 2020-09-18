using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugCommandBase
{
    string _commandId;
    string _commandDescription;
    string _commandFormat;

    public string commandID { get { return _commandId; } }
    public string commandDesctiption { get { return _commandDescription; } }
    public string commandFormat {get { return _commandFormat; } }

    public DebugCommandBase(string id, string description,string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

//Command that does not take in any values
public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }

}

//Command that can take one generic paramater
public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value)
    {
        command.Invoke(value);
    }

}
