using System;

public class NotEnoughSpException : Exception {
    public NotEnoughSpException() { }
    public NotEnoughSpException(string message) : base(message) { }
    public NotEnoughSpException(string message, Exception inner) : base(message, inner) { }
}