using System;

public class SkillCannotBeUsedException : Exception {
    public SkillCannotBeUsedException() { }
    public SkillCannotBeUsedException(string message) : base(message) { }
    public SkillCannotBeUsedException(string message, Exception inner) : base(message, inner) { }
}