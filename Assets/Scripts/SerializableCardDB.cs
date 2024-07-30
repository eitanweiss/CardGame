using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SerializableCardDB
{
    string entryID;
    public List<SerializableCard> data = new List<SerializableCard>();
}