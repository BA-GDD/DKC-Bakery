using System;
using System.Collections.Generic;

public class ExpansionList<T> : List<T>
{
    public event EventHandler ListChanged;

    public new void Add(T item)
    {
        base.Add(item);
        ListChanged?.Invoke(this, EventArgs.Empty);
    }
}
